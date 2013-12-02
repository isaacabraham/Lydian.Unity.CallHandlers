using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lydian.Unity.CallHandlers.Tests.Logging
{
	[TestClass]
	public class LoggingHandlerTests
	{
		private UnityContainer container;
		private IMethodLogPublisher publisher;
		
		[TestInitialize]
		public void Setup()
		{
			container = new UnityContainer();
			CallHandlerInitialiser.RegisterCallHandlerDependencies(container);
			HandlerHelpers.RegisterTypeWithCallHandler<LoggingHandler, SampleLoggingClass>(container);
			publisher = container.Resolve<IMethodLogPublisher>();
		}

		[TestMethod]
		public void MethodCalled_Always_BroadcastsOnStart()
		{
			var sample = container.Resolve<SampleLoggingClass>();
			CallSiteEventArgs args = null;
			publisher.OnMethodStarted += (o, e) => args = e;

			// Act
			sample.Foo();
			
			// Assert
			Assert.AreEqual("Foo", args.Method.Name);
			Assert.AreSame(sample, args.Target);
		}

		[TestMethod]
		public void MethodCalled_Always_BroadcastsOnComplete()
		{
			var sample = container.Resolve<SampleLoggingClass>();
			CallSiteEventArgs args = null;
			publisher.OnMethodCompleted += (o, e) => args = e;

			// Act
			sample.Foo();

			// Assert
			Assert.AreEqual("Foo", args.Method.Name);
			Assert.AreSame(sample, args.Target);
		}

		public class SampleLoggingClass
		{
			public virtual void Foo() { }
		}
	}
}
