using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.Localization;
using Pelism.MSGraph.Models;

namespace Pelism.MSGraph.Handlers {
    public class MSGraphSettingsPartHandler : ContentHandler {
        public Localizer T { get; set; }
		
        public MSGraphSettingsPartHandler() {
			T= NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<MSGraphSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<MSGraphSettingsPart>("MSGraphSettings", "Parts/MSGraphSettings", "MS Graph"));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site") {
                return;
            }

            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("MS Graph")));
        }
    }
}