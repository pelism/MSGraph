using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Mvc;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Themes;
using Pelism.MSGraph.Helpers;
using Pelism.MSGraph.Models;

namespace Pelism.MSGraph.Controllers {
    [Themed]
    [OrchardSuppressDependency("Orchard.Users.Controllers.AccountController")]
    public class AccountController : Controller {
        private string clientId;
        private string clientSecret;
        private readonly Uri loginRedirectUri;

        public AccountController(IHttpContextAccessor httpContextAccessor, ISiteService siteService) {
            var site = siteService.GetSiteSettings();
            var msGraph = site.As<MSGraphSettingsPart>();
            clientId = msGraph.ClientId;
            clientSecret = msGraph.ClientSecret;
            var url = new UrlHelper(httpContextAccessor.Current().Request.RequestContext);
            var urlAction = url.Action("Authorize", "Account", new {area = "Pelism.MSGraph"}, url.RequestContext.HttpContext.Request.Url.Scheme);
            loginRedirectUri = new Uri(urlAction);
        }

        public ActionResult LogOn(string returnUrl) {
            if (Request.IsAuthenticated) {
                return Redirect(returnUrl);
            }

            var authContext = new AuthenticationContext(GraphSettings.AzureADAuthority);

            // Generate the parameterized URL for Azure login.
            var authUri = authContext.GetAuthorizationRequestURL(
                GraphSettings.O365UnifiedAPIResource,
                clientId,
                loginRedirectUri,
                UserIdentifier.AnyUser,
                null);

            return Redirect(authUri.ToString());
        }

        public async Task<ActionResult> Authorize() {
            var authContext = new AuthenticationContext(GraphSettings.AzureADAuthority);

            // Get the token.
            var authResult = await authContext.AcquireTokenByAuthorizationCodeAsync(
                Request.Params["code"],
                loginRedirectUri,
                new ClientCredential(clientId, clientSecret),
                GraphSettings.O365UnifiedAPIResource);

            // Save the token in the session.
            Session[SessionKeys.Login.AccessToken] = authResult.AccessToken;

            return Redirect(Url.Content("~/"));
        }

        public ActionResult AccessDenied() {
            return View();
        }
    }
}