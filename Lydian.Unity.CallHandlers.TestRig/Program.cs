using Lydian.Unity.CallHandlers;
using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(String[] args)
		{
			using (var container = new UnityContainer())
			{
				container.AddNewExtension<Interception>();

				//// Illustrates how to put the ArgumentNotNullHandler on every method. You could of course change this policy or simply use the ArgumentNotNullAttribute explicitly.
				container.Configure<Interception>()
						 .AddPolicy("ArgumentNotNull")
						 .AddCallHandler<ArgumentNotNullHandler>()
						 .AddMatchingRule(new MemberNameMatchingRule("*"));

				// Required to use Unity Call Handlers.
				UnityRegistration.Register(container);

				// Sample subscribers			
				new SampleSubscriber().Subscribe(container);

				// Create our container
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
					service.GetDepartments("TEST");
					service.GetDepartments(null);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Threw an exception: {0}", ex.ToString());
				}
			}
		}

		/// <summary>
		/// Illustrates caching for parameterless methods.
		/// </summary>
		/// <param name="service"></param>
		private static void ParameterlessTest(IMyService service)
		{
			var names = service.GetNames();
			Console.WriteLine();
			names = service.GetNames();
			Console.WriteLine();
		}
		/// <summary>
		/// Illustrates caching for single-argument methods.
		/// </summary>
		/// <param name="service"></param>
		private static void SingleArgumentTest(IMyService service)
		{
			var aDepts = service.GetDepartments("a");
			Console.WriteLine();
			aDepts = service.GetDepartments("a");
			Console.WriteLine();
			var bDepts = service.GetDepartments("b");
			Console.WriteLine();
			bDepts = service.GetDepartments("b");
			Console.WriteLine();
		}
		/// <summary>
		/// Illustrates caching for methods with multiple arguments.
		/// </summary>
		/// <param name="service"></param>
		private static void MultipleArgumentTest(IMyService service)
		{
			var adderOne = service.GetNumber(1, 5);
			Console.WriteLine();
			adderOne = service.GetNumber(1, 5);
			Console.WriteLine();
			var adderTwo = service.GetNumber(1, 4);
			Console.WriteLine();
			adderTwo = service.GetNumber(1, 4);
			Console.WriteLine();
			var adderThree = service.GetNumber(2, 5);
			Console.WriteLine();
			adderThree = service.GetNumber(2, 5);
			Console.WriteLine();
		}
		/// <summary>
		/// Illustrates caching for methods with complex comparable arguments.
		/// </summary>
		/// <param name="service"></param>
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
	}
}