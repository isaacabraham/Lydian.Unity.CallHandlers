using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Lydian.Unity.CallHandlers.Tests.Logging
{
	[TestClass]
	public class LoggingHandlerTests
	{
		private UnityContainer container;
		
		[TestInitialize]
		public void Setup()
		{
			container = new UnityContainer();
			HandlerHelpers.RegisterTypeWithCallHandler<LoggingHandler, SampleLoggingClass>(container);
		}

		[TestMethod]
		public void MethodCalled_OneListener_BroadcastsOnStart()
		{
			var listener = CreateListener("TEST");
			var sample = container.Resolve<SampleLoggingClass>();

			// Act
			sample.Foo();
			
			// Assert
			listener.Verify(l => l.OnMethodStarted(It.Is<CallSiteEventArgs>(e => e.Target.Equals(sample) && e.Method.Name.Equals("Foo"))));
		}

		[TestMethod]
		public void MethodCalled_OneListener_BroadcastsOnComplete()
		{
			var listener = CreateListener("TEST");
			var sample = container.Resolve<SampleLoggingClass>();

			// Act
			sample.Foo();

			// Assert
			listener.Verify(l => l.OnMethodCompleted(It.Is<CallSiteEventArgs>(e => e.Target.Equals(sample) && e.Method.Name.Equals("Foo"))));
		}

		[TestMethod]
		public void MethodCalled_ManyListeners_BroadcastsOnStart()
		{
			var first = CreateListener("TEST");
			var second = CreateListener("TEST2");
			var sample = container.Resolve<SampleLoggingClass>();

			// Act
			sample.Foo();

			// Assert
			first.Verify(l => l.OnMethodCompleted(It.IsAny<CallSiteEventArgs>()));
			second.Verify(l => l.OnMethodCompleted(It.IsAny<CallSiteEventArgs>()));
		}

		private Mock<IMethodLogListener> CreateListener(String registrationName)
		{
			var listener = new Mock<IMethodLogListener>();
			container.RegisterInstance(registrationName, listener.Object);
			return listener;
		}

		public class SampleLoggingClass
		{
			public virtual void Foo() { }
		}
	}
}
