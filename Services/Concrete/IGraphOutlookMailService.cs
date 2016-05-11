using System.Collections.Generic;
using System.Threading.Tasks;
using Pelism.MSGraph.Models;

namespace Pelism.MSGraph.Services.Concrete {
    public interface IGraphOutlookMailService {
        Task<IEnumerable<MailItem>> ListMessages();

        Task<IEnumerable<MailItem>> ListMessages(GraphQuery graphQuery);

        Task<IEnumerable<MailAttachment>> GetMessageAttachments(string messageId);
    }
}