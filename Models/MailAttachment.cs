using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Pelism.MSGraph.Models
{
    public class MailAttachment
    {
        public byte[] ContentBytes { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public long Size { get; set; }
    }
}