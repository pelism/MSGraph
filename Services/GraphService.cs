using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using Orchard.Mvc;
using Pelism.MSGraph.Helpers;

namespace Pelism.MSGraph.Services
{
    public abstract class GraphService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpSessionStateBase _session;

        protected GraphService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.Current().Session;
        }

        protected bool HasToken() {
            if (_session[SessionKeys.Login.AccessToken] == null) {
                return false;
            }

            return true;
        }

        protected async Task<JObject> GetResults(string url)
        {
            if (HasToken()) {
                var token = _session[SessionKeys.Login.AccessToken].ToString();
                using (var client = new HttpClient()) {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, url)) {
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        using (var response = await client.SendAsync(request)) {
                            if (response.StatusCode == HttpStatusCode.OK) {
                                var result = await response.Content.ReadAsStringAsync();
                                var json = JObject.Parse(result);
                                return json;
                            }
                        }
                    }
                }
            }

            return new JObject();
        }

        protected string GetUrl(string url, params object[] args) {
            return string.Format(url, args);
        }

        protected string GetUrl(string url, GraphQuery graphQuery) {
            if (graphQuery == null) {
                return url;
            }

            StringBuilder query = new StringBuilder();
            bool start = false;
            foreach (var q in graphQuery) {
                if (!start) {
                    start = true;
                }
                else {
                    query.Append("&");
                }

                query.Append(q.Query());
            }

            return string.Format("{0}?{1}", url, query.ToString());
        }
        
    }
}