using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lydian.Unity.CallHandlers.Tests.Validation
{

	[TestClass]
	public class ArgumentNotNullHandlerTests
	{
		private UnityContainer container;
		private ArgumentNotNullTest sample;
		
		[TestInitialize]
		public void Setup()
		{
			container = new UnityContainer();
			sample = container.RegisterTypeWithCallHandler<ArgumentNotNullHandler, ArgumentNotNullTest>();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ReferenceType_ArgumentIsNull_ThrowsException()
		{
			// Act
			sample.ReferenceType(null);
		}

		[TestMethod]
		public void ReferenceType_ArgumentIsNotNull_DoesNotThrowException()
		{
			// Act
			sample.ReferenceType(String.Empty);
		}

		[TestMethod]
		public void ReferenceType_ArgumentIsBoxedValueType_DoesNotThrowException()
		{
			// Act
			sample.ReferenceType(123);
		}

		[TestMethod]
		public void ValueType_ArgumentIsNotNull_DoesNotThrowException()
		{
			sample.ValueType(123);
		}

		[TestMethod]
		public void NullableType_ArgumentDoesNotHaveAValue_DoesNotThrowException()
		{
			sample.NullableType(new Nullable<Int32>());
		}

		[TestMethod]
		public void NullableType_ArgumentHasAValue_DoesNotThrowException()
		{
			sample.NullableType(new Nullable<Int32>(123));
		}

		[TestMethod]
		public void NullableType_ArgumentIsLifted_DoesNotThrowException()
		{
			sample.NullableType(123);
		}

		[TestMethod]
		public void OptionalReference_ArgumentIsNull_DoesNotThrowException()
		{
			sample.OptionalReference(null);
		}

		[TestMethod]
		public void OptionalReference_ArgumentIsNotNull_DoesNotThrowException()
		{
			sample.OptionalReference(String.Empty);
		}

		[TestMethod]
		public void Mixture_ReferenceArgumentNotNull_DoesNotThrowException()
		{
			sample.Mixture(String.Empty, 123);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Mixture_ReferenceArgumentNull_ThrowsException()
		{
			sample.Mixture(null, 123);
		}

		public class ArgumentNotNullTest
		{
			public virtual void ReferenceType(Object item) { }
			public virtual void ValueType(Int32 item) { }
			public virtual void NullableType(Nullable<Int32> item) { }
			public virtual void OptionalReference(Object item = null) { }
			public virtual void Mixture(Object item, ValueType other) { }
		}
	}	
}
