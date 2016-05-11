using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using Orchard.Themes;
using Pelism.MSGraph.Models;
using Pelism.MSGraph.Services;
using Pelism.MSGraph.Services.Concrete;

namespace Pelism.MSGraph.Controllers {
    [Themed]
    public class OutlookMailController : Controller {
        private readonly IGraphOutlookMailService _outlookMailService;

        public OutlookMailController(IGraphOutlookMailService outlookMailService) {
            _outlookMailService = outlookMailService;
        }

        public async Task<ActionResult> Index() {
            var items = await _outlookMailService.ListMessages();
            return View(items);
        }

        public async Task<ActionResult> MessagesWithAttachments() {
            GraphQueryParam[] graphQueryParam = new GraphQueryParam[1] {
                new GraphQueryParam(Services.GraphQueryParam.QueryType.Filter, "hasAttachments eq true")
            };

            var items = await _outlookMailService.ListMessages(new GraphQuery(graphQueryParam));
            return View(items);
        }

        [HttpGet]
        public async Task<ActionResult> GetMessageAttachments(string messageId) {
            IEnumerable<MailAttachment> items = null;

            if (!string.IsNullOrEmpty(messageId)) {
                items = await _outlookMailService.GetMessageAttachments(messageId);
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}