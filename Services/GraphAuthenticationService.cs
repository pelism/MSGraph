using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Orchard.Mvc;
using Orchard.Security;
using Pelism.MSGraph.Helpers;
using Pelism.MSGraph.Models;

namespace Pelism.MSGraph.Services {
    public class GraphAuthenticationService : GraphService, IAuthenticationService {
        private readonly IMembershipService _membershipService;

        public GraphAuthenticationService(IHttpContextAccessor httpContextAccessor, IMembershipService membershipService) : base(httpContextAccessor){
            _membershipService = membershipService;
        }

        public void SignIn(IUser user, bool createPersistentCookie) {}

        public void SignOut() {}

        public void SetAuthenticatedUserForRequest(IUser user) {}

        public IUser GetAuthenticatedUser() {
            if (HasToken()) {
                var userPrincipleName = Task.Run(() => GetUserPrincipleName()).Result;
                return _membershipService.GetUser(userPrincipleName);
            }

            return null;
        }

        private async Task<string> GetUserPrincipleName() {
            var result = await GetResults(GraphSettings.GetMeUrl);
            var userName = result != null ? result["userPrincipalName"].ToString() : string.Empty;

            return userName;
        }
    }
}