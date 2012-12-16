using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lydian.Unity.CallHandlers.Tests.AttributeTests
{
	[TestClass]
	public class LoggingAttributeTests
	{
		private LoggingAttribute loggingAttribute;
		private UnityContainer unityContainer;
		
		[TestInitialize]
		public void Setup()
		{
			loggingAttribute = new LoggingAttribute();
			unityContainer = new UnityContainer();
		}

		[TestMethod]
		public void CreateHandler_ReturnsTheHandler()
		{
			// Act
			var handler = loggingAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.IsInstanceOfType(handler, typeof(LoggingHandler));
		}

		[TestMethod]
		public void CreateHandler_HandlerCreated_SetsTheOrder()
		{
			loggingAttribute.Order = 24;

			// Act
			var handler = loggingAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.AreEqual(24, handler.Order);
		}
	}
}
