using Autofac;
using Orchard.Mvc;
using Pelism.MSGraph.Services;
using Pelism.MSGraph.Services.Concrete;

namespace Pelism.MSGraph {
    public class ModuleLoader : Module {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ModuleLoader(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<GraphOutlookMailService>().As<IGraphOutlookMailService>().WithParameter("httpContextAccessor", _httpContextAccessor);
        }
    }
}