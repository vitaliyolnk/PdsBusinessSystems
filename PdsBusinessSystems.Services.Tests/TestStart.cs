using Autofac;
using PdsBusinessSystems.Services.Contracts;
using PdsBusinessSystems.Services.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdsBusinessSystems.Services.Tests
{
    public class TestStart
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get { return _container; }
        }

        public static void Configuration()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => { return new MemberApiClient(); }).As<ICallMemberApi>()   
                .InstancePerLifetimeScope();
            builder.Register(c => { return new EventCalendarApiClient(); }).As<ICallEventCalendarApi>()
             .InstancePerLifetimeScope();
            builder.RegisterType<BusinesItemService>().UsingConstructor(typeof(ICallEventCalendarApi), typeof(ICallMemberApi))
                .As<IGetBusinessItems>()
             .InstancePerLifetimeScope(); ;
          
            _container = builder.Build();
        }
    }
}
