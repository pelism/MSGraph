using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Pelism.MSGraph {
    public class Routes : IRouteProvider {
        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                new RouteDescriptor {
                    Route = new Route(
                        "Users/Account/{action}",
                        new RouteValueDictionary {
                            {"area", "Pelism.MSGraph"},
                            {"controller", "Account"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Pelism.MSGraph"}
                        },
                        new MvcRouteHandler())
                },
                new RouteDescriptor {
                    Route = new Route(
                        "OutlookMail/{action}",
                        new RouteValueDictionary {
                            {"area", "Pelism.MSGraph"},
                            {"action", "Index"},
                            {"controller", "OutlookMail"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "Pelism.MSGraph"}
                        },
                        new MvcRouteHandler())
                }
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var route in GetRoutes()) {
                routes.Add(route);
            }
        }
    }
}