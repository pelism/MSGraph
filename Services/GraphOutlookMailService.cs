using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Orchard.Mvc;
using Orchard.Mvc.ViewEngines;
using Pelism.MSGraph.Helpers;
using Pelism.MSGraph.Models;
using Pelism.MSGraph.Services.Concrete;

namespace Pelism.MSGraph.Services {
    public class GraphOutlookMailService : GraphService, IGraphOutlookMailService {
        public GraphOutlookMailService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) {}

        public async Task<IEnumerable<MailItem>> ListMessages() {
            return await ListMessages(null);
        }

        public async Task<IEnumerable<MailItem>> ListMessages(GraphQuery graphQuery) {
            var mailItems = new List<MailItem>();
            var result = await GetResults(GetUrl(GraphSettings.MessageUrl, graphQuery));
            foreach (var mail in result["value"]){
                mailItems.Add(new MailItem{
                    Preview = mail["bodyPreview"].ToString(),
                    WebLink = new Uri(mail["webLink"].ToString()),
                    Subject = mail["subject"].ToString(),
                    From = mail["from"]["emailAddress"]["name"].ToString(),
                    ReceivedDateTime = DateTime.Parse(mail["receivedDateTime"].ToString()),
                    MessageId = mail["id"].ToString(),
                    Id = Guid.NewGuid()
                });
            }

            return mailItems;
        }

        public async Task<IEnumerable<MailAttachment>> GetMessageAttachments(string messageId) {
            var attachments = new List<MailAttachment>();
            string url = GetUrl(GraphSettings.MessageAttachments, new object[] { messageId });
            var result = await GetResults(url);
            foreach (var attach in result["value"]) {
                attachments.Add(new MailAttachment {
                    Name = attach["name"].ToString(),
                    ContentType = attach["contentType"].ToString(),
                    Size = Convert.ToInt64(attach["size"])//,
                    //ContentBytes = Utility.ObjectToByteArray(attach["contentBytes"])
                });
            }

            return attachments;
        }
    }
}