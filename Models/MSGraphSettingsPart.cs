using System.ComponentModel;
using Orchard.ContentManagement;

namespace Pelism.MSGraph.Models {
    public class MSGraphSettingsPart : ContentPart{
        [DisplayName("Client Id")]
        public string ClientId {
            get { return this.Retrieve(x => x.ClientId); }
            set { this.Store(x => x.ClientId, value); }
        }

        [DisplayName("Client Secret")]
        public string ClientSecret {
            get { return this.Retrieve(x => x.ClientSecret); }
            set { this.Store(x => x.ClientSecret, value); }
        }
    }
}