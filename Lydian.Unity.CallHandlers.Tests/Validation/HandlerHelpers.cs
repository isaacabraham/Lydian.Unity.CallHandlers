using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Lydian.Unity.CallHandlers.Tests.Validation
{
	public static class HandlerHelpers
	{
		public static TTestClass RegisterTypeWithCallHandler<THandler, TTestClass>(this IUnityContainer container) where THandler : ICallHandler
		{
			container.AddNewExtension<Interception>();
			container.Configure<Interception>()
					 .AddPolicy("TestPolicy")
					 .AddCallHandler<THandler>()
					 .AddMatchingRule(new MemberNameMatchingRule("*"));
			container.RegisterType<TTestClass>(new InterceptionBehavior<PolicyInjectionBehavior>(),
											   new Interceptor<VirtualMethodInterceptor>());
			return container.Resolve<TTestClass>();
		}
	}
}
