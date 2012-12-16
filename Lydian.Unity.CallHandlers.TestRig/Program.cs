using Lydian.Unity.CallHandlers;
using Lydian.Unity.CallHandlers.Core;
using Lydian.Unity.CallHandlers.Logging;
using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
	class Program
	{
		private static void ParameterlessTest(IMyService service)
		{
			var names = service.GetNames();
			Console.WriteLine();
			names = service.GetNames();
			Console.WriteLine();
		}
		private static void SingleArgumentTest(IMyService service)
		{
			var aDepts = service.GetDepartments("a");
			Console.WriteLine();
			var bDepts = service.GetDepartments("b");
			Console.WriteLine();
			aDepts = service.GetDepartments("a");
			Console.WriteLine();
			bDepts = service.GetDepartments("b");
			Console.WriteLine();
		}
		private static void MultipleArgumentTest(IMyService service)
		{
			var adderOne = service.GetNumber(1, 5);
			Console.WriteLine();
			var adderTwo = service.GetNumber(1, 4);
			Console.WriteLine();
			var adderThree = service.GetNumber(2, 5);
			Console.WriteLine();
			adderOne = service.GetNumber(1, 5);
			Console.WriteLine();
			adderTwo = service.GetNumber(1, 4);
			Console.WriteLine();
			adderThree = service.GetNumber(2, 5);
			Console.WriteLine();
		}
		private static void ComplexTypeTest(IMyService service)
		{
			var sharedObject = new ComplexType { Name = "Isaac", Age = 32 };
			var referenceType = service.ComplexTypeArgument(sharedObject);
			Console.WriteLine();
			referenceType = service.ComplexTypeArgument(sharedObject);
			Console.WriteLine();

			var differentObject = service.ComplexTypeArgument(new ComplexType { Name = "Isaac", Age = 32 });
			Console.WriteLine();
			var differentValues = service.ComplexTypeArgument(new ComplexType { Name = "Isaaced", Age = 35 });
			Console.WriteLine();
		}

		private static void RegisterSampleLoggers(UnityContainer container)
		{
			container.RegisterType<IMethodLogPublisher, ConsoleLogger>("Console");
			container.RegisterType<IMethodTimePublisher, ConsoleTimer>("Console");
		}

		static void Main(String[] args)
		{
			using (var container = new UnityContainer())
			{
				container.AddNewExtension<Interception>();
				container.Configure<Interception>()
						 .AddPolicy("ArgumentNotNull")
						 .AddCallHandler<ArgumentNotNullHandler>()
						 .AddMatchingRule(new MemberNameMatchingRule("*"));
				UnityCallHandlerRegistration.Register(container);

				RegisterSampleLoggers(container);
				container.RegisterType<IMyService, MyService>(new InterceptionBehavior<PolicyInjectionBehavior>(), new Interceptor<InterfaceInterceptor>());
				var service = container.Resolve<IMyService>();

				ParameterlessTest(service);
				SingleArgumentTest(service);
				MultipleArgumentTest(service);
				ComplexTypeTest(service);

				try
				{
					service.NullableArgument(null);
					service.OptionalArgument();
					service.GetDepartments(null);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Threw an exception: {0}", ex.ToString());
				}
			}
		}
	}
}