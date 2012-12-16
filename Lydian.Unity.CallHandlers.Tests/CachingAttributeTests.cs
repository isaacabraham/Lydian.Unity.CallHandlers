using Lydian.Unity.CallHandlers.Caching;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lydian.Unity.CallHandlers.Tests
{
	[TestClass]
	public class CachingAttributeTests
	{
		private CachingAttribute cachingAttribute;
		private UnityContainer unityContainer;

		[TestInitialize]
		public void Setup()
		{
			cachingAttribute = new CachingAttribute();
			unityContainer = new UnityContainer();
		}

		[TestMethod]
		public void CreateHandler_ReturnsTheHandler()
		{
			// Act
			var handler = cachingAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.IsInstanceOfType(handler, typeof(CachingHandler));
		}

		[TestMethod]
		public void CreateHandler_HandlerCreated_SetsTheOrder()
		{
			cachingAttribute.Order = 24;

			// Act
			var handler = cachingAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.AreEqual(24, handler.Order);
		}
	}
}