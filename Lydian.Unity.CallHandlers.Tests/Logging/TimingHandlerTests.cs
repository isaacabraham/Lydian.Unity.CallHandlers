using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Lydian.Unity.CallHandlers.Tests.Logging
{
	[TestClass]
	public class TimingHandlerTests
	{
		private UnityContainer container;

		[TestInitialize]
		public void Setup()
		{
			container = new UnityContainer();
			HandlerHelpers.RegisterTypeWithCallHandler<TimingHandler, SampleTimingClass>(container);
		}

		[TestMethod]
		public void MethodCalled_OneListener_BroadcastsOnComplete()
		{
			var listener = CreateListener("TEST");
			var sample = container.Resolve<SampleTimingClass>();

			// Act
			sample.Foo();

			// Assert
			listener.Verify(l => l.OnMethodCompleted(It.Is<TimedCallEventArgs>(e => e.Target.Equals(sample)
																				 && e.Method.Name.Equals("Foo")
																				 && e.CallDuration.TotalMilliseconds > 0)));
		}

		[TestMethod]
		public void MethodCalled_ManyListeners_BroadcastsOnComplete()
		{
			var first = CreateListener("TEST");
			var second = CreateListener("TEST2");
			var sample = container.Resolve<SampleTimingClass>();

			// Act
			sample.Foo();

			// Assert
			first.Verify(l => l.OnMethodCompleted(It.IsAny<TimedCallEventArgs>()));
			second.Verify(l => l.OnMethodCompleted(It.IsAny<TimedCallEventArgs>()));
		}

		private Mock<IMethodTimeListener> CreateListener(String registrationName)
		{
			var listener = new Mock<IMethodTimeListener>();
			container.RegisterInstance(registrationName, listener.Object);
			return listener;
		}

		public class SampleTimingClass
		{
			public virtual void Foo() { }
		}
	}
}
