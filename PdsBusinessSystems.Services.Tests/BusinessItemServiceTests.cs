using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdsBusinessSystems.Services.Contracts;
using PdsBusinessSystems.Services.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdsBusinessSystems.Services.Tests
{
    [TestClass]
    public class BusinessItemServiceTests : TestStart
    {
        [TestInitialize]
        public void Startup()
        {
            Configuration();
        }

        [TestMethod]
        public void CallEventsServiceSuccess()
        {
            string startDate ="2015-11-14";
            string endDate = "2015-11-20";

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IGetBusinessItems>();
                var events = app.GetEventForMainChamberCommons(startDate, endDate);

                Assert.IsNotNull(events);
            }
        }

        [TestMethod]
        public void CallEventsEndBeforeStartDateServiceFailed()
        {
            string endDate = "2015-11-14";
            string startDate = "2015-11-20";

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IGetBusinessItems>();
                var events = app.GetEventForMainChamberCommons(startDate, endDate);

                Assert.IsTrue(events.Count == 0);
            }
        }
    }
}
