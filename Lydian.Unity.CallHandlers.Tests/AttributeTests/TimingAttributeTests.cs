using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lydian.Unity.CallHandlers.Tests.AttributeTests
{
	[TestClass]
	public class TimingAttributeTests
	{
		private TimingAttribute timingAttribute;
		private UnityContainer unityContainer;

		[TestInitialize]
		public void Setup()
		{
			timingAttribute = new TimingAttribute();
			unityContainer = new UnityContainer();
		}

		[TestMethod]
		public void CreateHandler_ReturnsTheHandler()
		{
			// Act
			var handler = timingAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.IsInstanceOfType(handler, typeof(TimingHandler));
		}

		[TestMethod]
		public void CreateHandler_HandlerCreated_SetsTheOrder()
		{
			timingAttribute.Order = 24;

			// Act
			var handler = timingAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.AreEqual(24, handler.Order);
		}
	}
}
