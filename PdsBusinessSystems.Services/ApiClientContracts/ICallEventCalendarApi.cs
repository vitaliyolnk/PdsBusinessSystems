using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PdsBusinessSystems.Services.Contracts
{
   public interface ICallEventCalendarApi
    {
        Task<string> GetEventsForDates(string fromDate, string toDate);
    }
}
