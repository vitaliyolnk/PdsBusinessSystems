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
    public class MembersTest : TestStart
    {

        [TestInitialize]
        public void Startup()
        {
            Configuration();
        }

        [TestMethod]
        public void CallMemebersApiSuccess()
        {
            int memeberId = 759;

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallMemberApi>();
                var result = app.GetMemeberDetails(memeberId);

                Assert.IsTrue(result.Result.Contains("</Member>"));
            }
        }

        [TestMethod]
        public void CallMemebersApiInvalidId()
        {
            int memeberId = 0;

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<ICallMemberApi>();
                var result = app.GetMemeberDetails(memeberId);

                Assert.IsFalse(result.Result.Contains("</Member>"));
            }
        }

    }
}
