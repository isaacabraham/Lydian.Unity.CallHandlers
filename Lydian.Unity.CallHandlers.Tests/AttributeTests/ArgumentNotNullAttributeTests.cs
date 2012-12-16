using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lydian.Unity.CallHandlers.Tests.AttributeTests
{
	[TestClass]
	public class ArgumentNotNullAttributeTests
	{
		private ArgumentNotNullAttribute argumentNotNullAttribute;
		private UnityContainer unityContainer;

		[TestInitialize]
		public void Setup()
		{
			argumentNotNullAttribute = new ArgumentNotNullAttribute();
			unityContainer = new UnityContainer();
		}

		[TestMethod]
		public void CreateHandler_ReturnsTheHandler()
		{
			// Act
			var handler = argumentNotNullAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.IsInstanceOfType(handler, typeof(ArgumentNotNullHandler));
		}

		[TestMethod]
		public void CreateHandler_HandlerCreated_SetsTheOrder()
		{
			argumentNotNullAttribute.Order = 24;

			// Act
			var handler = argumentNotNullAttribute.CreateHandler(unityContainer);

			// Assert
			Assert.AreEqual(24, handler.Order);
		}
	}
}
