using PdsBusinessSystems.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdsBusinessSystems.Services
{
    public interface IGetBusinessItems
    {
        List<EventItem> GetEventForMainChamberCommons(string startDate, string endDate);
    }
}
