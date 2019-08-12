using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace sidecar_dependent_app.Services
{
    public class AppAutherizationService : IAppAutherizationService
    {
        private HttpClient _authzApiClinet;
        private Func<long, long, String> _authzAppUserScopesPath = (appid, userid) => $"/api/auth/{appid}/users/{userid}";

        public AppAutherizationService(HttpClient authzApiClinet)
        {
            _authzApiClinet = authzApiClinet;
        }


        public async Task<IEnumerable<string>> GetAthenticatedUserScopesAsync(long appId, long userId)
        {
            var response = await _authzApiClinet.GetAsync(_authzAppUserScopesPath(appId, userId));

            if (response.StatusCode == HttpStatusCode.InternalServerError) throw new HttpRequestException("auth service is down");
            else if (response.StatusCode == HttpStatusCode.NotFound) return Enumerable.Empty<string>();

            var scopes = JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync());
            return scopes;
        }
    }
}