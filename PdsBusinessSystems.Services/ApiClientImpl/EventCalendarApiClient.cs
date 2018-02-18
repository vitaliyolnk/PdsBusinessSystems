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
    public class EventCalendarApiClient : ICallEventCalendarApi
    {
        private static HttpClient eventCalendarClient = new HttpClient();

        static  EventCalendarApiClient()
        {
            Uri _baseUri = new Uri("http://service.calendar.parliament.uk/calendar/events/list.xml");

            eventCalendarClient.BaseAddress = _baseUri;
            eventCalendarClient.DefaultRequestHeaders.Clear();
            eventCalendarClient.DefaultRequestHeaders.ConnectionClose = false;

            ServicePointManager.FindServicePoint(_baseUri).ConnectionLeaseTimeout = 60 * 1000;
        }

        public async Task<string> GetEventsForDates(string startDate, string endDate)
        {
            var response = await eventCalendarClient
                .GetAsync($"?startdate={startDate}&enddate={endDate}");

            if (!response.IsSuccessStatusCode)
            {
                    throw new HttpRequestException();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}