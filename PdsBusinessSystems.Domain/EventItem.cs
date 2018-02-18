using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PdsBusinessSystems.Domain.Events
{
    public class EventItem
    {
        public string StartDate
        {
            get;set;
        }
        public string EndDate
        {
            get; set;
        }
        public string StartTime
        {
            get;set;
        }

        public string EndTime
        {
            get;set;
        }

        public string Description
        {
            get;set;
        }
        public string Category { get; set; }

        public List<Member> Members { get; set; }
    }
}
