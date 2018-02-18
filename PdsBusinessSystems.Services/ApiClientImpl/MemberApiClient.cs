using PdsBusinessSystems.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PdsBusinessSystems.Services.Impl
{
    public class MemberApiClient : ICallMemberApi
    {
        private static HttpClient memberApiClient = new HttpClient();

        static MemberApiClient()
        {
            Uri _baseUri = new Uri("http://data.parliament.uk/membersdataplatform/services/mnis/members/query/");

            memberApiClient.BaseAddress = _baseUri;
            memberApiClient.DefaultRequestHeaders.Clear();
            memberApiClient.DefaultRequestHeaders.ConnectionClose = false;

            ServicePointManager.FindServicePoint(_baseUri).ConnectionLeaseTimeout = 60 * 1000;
        }

        public async Task<string> GetMemeberDetails(int memebrId)
        {
            var response = await memberApiClient.GetAsync($"id={memebrId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
