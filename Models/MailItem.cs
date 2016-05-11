using System;

namespace Pelism.MSGraph.Models {
    public class MailItem {
        public Uri WebLink { get; set; }

        public string From { get; set; }

        public string Sender { get; set; }

        public DateTime ReceivedDateTime { get; set; }

        public string Subject { get; set; }

        public string Preview { get; set; }

        public string Body { get; set; }

        public string MessageId { get; set; }

        public Guid Id { get; set; }
    }
}