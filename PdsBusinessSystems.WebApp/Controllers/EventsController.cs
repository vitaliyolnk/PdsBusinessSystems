using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PdsBusinessSystems.Domain.Events;
using PdsBusinessSystems.Services;

namespace PdsBusinessSystems.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        IGetBusinessItems _eventsService;

        public EventsController(IGetBusinessItems eventsService)
        {
            _eventsService = eventsService;
        }
           // GET: api/Events
        [HttpGet("[action]")]
        public IEnumerable<EventItem> GetCurrentEvents()
        {
            var currentDate = DateTime.Today;

            var thisWeekStart = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);

            var result = _eventsService.GetEventForMainChamberCommons(thisWeekStart.ToString("yyyy-MM-dd"),
                thisWeekEnd.ToString("yyyy-MM-dd"));

            return result;
        }

        [Route("{startDate}/{endDate}")]
        public IEnumerable<EventItem> GetEvents(string startDate, string endDate)
        {
            var result = _eventsService.GetEventForMainChamberCommons(startDate, endDate);

            return result;
        }
    }
}
