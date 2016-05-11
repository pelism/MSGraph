using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Pelism.MSGraph.Models;

namespace Pelism.MSGraph.Drivers {
    public class AzureSettingsPartDriver : ContentPartDriver<MSGraphSettingsPart> {
        protected override string Prefix {
            get { return "MSGraphSettings"; }
        }

        protected override DriverResult Editor(MSGraphSettingsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_MSGraphSettings_Edit", () =>
               shapeHelper.EditorTemplate(TemplateName: "Parts/MSGraphSettings", Model: part, Prefix: Prefix)).OnGroup("Azure");
        }

        protected override DriverResult Editor(MSGraphSettingsPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}