using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdsBusinessSystems.Services.Impl;
using PdsBusinessSystems.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Autofac;

namespace PdsBusinessSystems.Services.Tests
{
    [TestClass]
    public class EventCalendarTests : TestStart
    {
        [TestInitialize]
        public void Startup()
        {
            Configuration();
        }

        [TestMethod]
        public void CallEvenCalendarApiSuccess()
        {
            string startDate = "2015-11-12";
            string endDate = "2015-11-20";
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallEventCalendarApi>();
                var result = app.GetEventsForDates(startDate, endDate);

                Assert.IsTrue(result.Result.Contains("</Event>"));
            }
        }

        [TestMethod]
        public void CallEvenCalendarApiEndBeforeStartFailed()
        {
            string endDate = "2015-11-12";
            string startDate = "2015-11-20";
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallEventCalendarApi>();
                var result = app.GetEventsForDates(startDate, endDate);
                Assert.IsFalse(result.Result.Contains("</Event>"));
            }
        }
    }
}
