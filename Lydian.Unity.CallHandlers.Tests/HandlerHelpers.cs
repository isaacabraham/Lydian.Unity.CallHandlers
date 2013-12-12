using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Tests
{
    public static class HandlerHelpers
    {
        public static void RegisterTypeWithCallHandler<THandler, TTestClass>(this IUnityContainer container, bool withPolicy = true) where THandler : ICallHandler
        {
            if (withPolicy)
            {
                container.Configure<Interception>()
                         .AddPolicy(String.Format("TestPolicyFor{0}{1}", typeof(THandler).Name, typeof(TTestClass).Name))
                         .AddMatchingRule(new TypeMatchingRule(typeof(TTestClass)))
                         .AddCallHandler<THandler>()
                         .AddMatchingRule(new MemberNameMatchingRule("*"));
            }
            container.RegisterType<TTestClass>(new InterceptionBehavior<PolicyInjectionBehavior>(),
                                               new Interceptor<VirtualMethodInterceptor>());
        }
    }
}
