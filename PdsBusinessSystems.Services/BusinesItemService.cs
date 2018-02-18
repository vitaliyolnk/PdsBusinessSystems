using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using PdsBusinessSystems.Domain;
using PdsBusinessSystems.Domain.Events;
using PdsBusinessSystems.Services.Contracts;

namespace PdsBusinessSystems.Services
{
    public class BusinesItemService : IGetBusinessItems
    {
        
        ICallEventCalendarApi _eventCalendarClient;
        ICallMemberApi _memebrApiClient;
        public BusinesItemService(ICallEventCalendarApi eventCalendarClient, ICallMemberApi memberApiClient)
        {
            _eventCalendarClient = eventCalendarClient;
            _memebrApiClient = memberApiClient;
        }

        public List<EventItem> GetEventForMainChamberCommons(string startDate, string endDate)
        {
            var dateRangeEventsApiResults = _eventCalendarClient.GetEventsForDates(startDate, endDate);

            XmlDocument doc = CreateXml(dateRangeEventsApiResults.Result);

            List<EventItem> events = null;

            if (!string.IsNullOrWhiteSpace(doc.InnerXml))
            {
                events = ParseEventXml(doc);
            }

            return events;
        }

        private List<EventItem> ParseEventXml(XmlDocument doc)
        {
            var allEvents = doc.GetElementsByTagName("Event");

            List<EventItem> eventList = new List<EventItem>();

            var mainChamberCommons = allEvents.Cast<XmlNode>()
                .Where(n => string.Equals(n["Type"].InnerText, "Main Chamber") 
                && string.Equals(n["House"].InnerText, "Commons"));

            foreach (XmlNode item in mainChamberCommons)
            {
                EventItem eventItem = new EventItem
                {
                    Description = item.SelectSingleNode("./Description")?.InnerText,
                    StartDate = FormatDate(item.SelectSingleNode("./StartDate")?.InnerText),
                    StartTime = item.SelectSingleNode("./StartTime")?.InnerText,
                    EndDate = FormatDate(item.SelectSingleNode("./EndDate")?.InnerText),
                    EndTime = item.SelectSingleNode("./EndTime")?.InnerText,
                    Category = item.SelectSingleNode("./Category")?.InnerText,
                    Members = GetMemberInfo(item.SelectNodes("./Members/Member"))
                };

                eventList.Add(eventItem);
            }

            return eventList;
        }

        private string FormatDate(string inputDate)
        {
            if(string.IsNullOrWhiteSpace(inputDate))
            {
                return string.Empty;
            }

            DateTime.TryParse(inputDate, out DateTime date);

            return date.ToString("dd/MM/yyyy");
        }

        private List<Member> GetMemberInfo(XmlNodeList membersNodes)
        {
            if (membersNodes == null || membersNodes?.Count == 0) return null;

            List<Member> members = new List<Member>();

            foreach (XmlNode item in membersNodes)
            {
                var memberId = item.Attributes["Id"];
                var memberApiResult = _memebrApiClient.GetMemeberDetails(int.Parse(memberId.InnerText));

                XmlDocument membersDoc = CreateXml(memberApiResult.Result);

                if (!string.IsNullOrWhiteSpace(membersDoc.InnerXml))
                {
                    members.Add(ParseMemberXml(membersDoc));
                }
            }
           
            return members;
        }

        private Member ParseMemberXml(XmlDocument membersDoc)
        {
            var memberInfo = membersDoc.SelectSingleNode("Members/Member");

            return new Member
            {
                FullTitle = memberInfo.SelectSingleNode("./FullTitle")?.InnerText,
                MemberFrom = memberInfo.SelectSingleNode("./MemberFrom")?.InnerText,
                Party = memberInfo.SelectSingleNode("./Party")?.InnerText,
            };
        }

        private XmlDocument CreateXml(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);

            return xmlDoc;
        }
    }
}
