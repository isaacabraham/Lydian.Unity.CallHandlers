using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lydian.Unity.CallHandlers.Tests.Logging
{
	[TestClass]
	public class TimingHandlerTests
	{
		private UnityContainer container;
		private IMethodTimePublisher publisher;

		[TestInitialize]
		public void Setup()
		{
			container = new UnityContainer();
			CallHandlerInitialiser.RegisterCallHandlerDependencies(container);
			HandlerHelpers.RegisterTypeWithCallHandler<TimingHandler, SampleTimingClass>(container);
			publisher = container.Resolve<IMethodTimePublisher>();
		}

		[TestMethod]
		public void MethodCalled_Always_BroadcastsOnComplete()
		{
			var sample = container.Resolve<SampleTimingClass>();
			TimedCallEventArgs args = null;
			publisher.OnMethodCompleted += (o, e) => args = e;

			// Act
			sample.Foo();

			// Assert
			Assert.AreEqual("Foo", args.Method.Name);
			Assert.AreSame(sample, args.Target);
			Assert.AreNotEqual(0, args.CallDuration.TotalMilliseconds);
		}

		public class SampleTimingClass
		{
			public virtual void Foo() { }
		}
	}
}
