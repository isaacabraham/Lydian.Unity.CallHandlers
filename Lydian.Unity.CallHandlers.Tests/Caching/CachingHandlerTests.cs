using Lydian.Unity.CallHandlers.Caching;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lydian.Unity.CallHandlers.Tests.Caching
{
	[TestClass]
	public class CachingHandlerTests
	{
		private UnityContainer container;
		private CachingHandlerTestClass sample;

		[TestInitialize]
		public void Setup()
		{
			container = new UnityContainer();
			sample = container.RegisterTypeWithCallHandler<CachingHandler, CachingHandlerTestClass>();
		}

		[TestMethod]
		public void NoArguments_MultipleCalls_CachesIt()
		{
			// Assert
			AssertIsCached(() => sample.NoArguments());
		}

		[TestMethod]
		public void NoArguments_ThrowsException_CachesIt()
		{
			var exceptionsRaised = 0;

			// Act
			try { sample.ThrowException(); }
			catch { exceptionsRaised++; }

			try { sample.ThrowException(); }
			catch { exceptionsRaised++; }

			// Assert
			Assert.AreEqual(1, sample.NumberOfCalls);
			Assert.AreEqual(2, exceptionsRaised);
		}

		[TestMethod]
		public void SimpleArgument_MultipleCallsWithSameArgument_CachesIt()
		{
			// Assert
			AssertIsCached(() => sample.SimpleArgument(5));
		}

		[TestMethod]
		public void SimpleArgument_MultipleCallsWithDifferentArgument_DoesNotCacheIt()
		{
			// Act
			sample.SimpleArgument(2);
			sample.SimpleArgument(3);

			// Assert
			Assert.AreEqual(2, sample.NumberOfCalls);
		}

		[TestMethod]
		public void SimpleArguments_MultipleCallsWithSameArgument_CachesIt()
		{
			AssertIsCached(() => sample.SimpleArguments("Test",2,3));
		}

		[TestMethod]
		public void SimpleArguments_MultipleCallsWithOneDifferentArgument_DoesNotCacheIt()
		{
			// Act
			sample.SimpleArguments("Test",2,3);
			sample.SimpleArguments("Test",2,4);

			// Assert
			Assert.AreEqual(2, sample.NumberOfCalls);
		}

		[TestMethod]
		public void SimpleArguments_MultipleCallsWithOneDifferentReferenceTypeArgument_DoesNotCacheIt()
		{
			// Act
			sample.SimpleArguments("TestTwo", 2, 3);
			sample.SimpleArguments("Test", 2, 3);

			// Assert
			Assert.AreEqual(2, sample.NumberOfCalls);
		}

		[TestMethod]
		public void SimpleArguments_MultipleCallsWithDifferentOrderedArguments_DoesNotCacheIt()
		{
			// Act
			sample.SimpleArguments("Test", 3, 2);
			sample.SimpleArguments("Test", 2, 3);

			// Assert
			Assert.AreEqual(2, sample.NumberOfCalls);
		}

		[TestMethod]
		public void SimpleArguments_MultipleCallsWithSameNullArgument_CachesIt()
		{
			// Assert
			AssertIsCached(() => sample.SimpleArguments(null, 3, 2));
		}

		[TestMethod]
		public void ComplexArgument_MultipleCallsWithSameObject_CachesIt()
		{
			var complexObject = new ComparableObject { Name = "Isaac" };

			// Assert
			AssertIsCached(() => sample.ComplexArgument(complexObject));
		}

		[TestMethod]
		public void ComplexArgument_MultipleCallsWithDifferentObject_DoesNotCacheIt()
		{
			sample.ComplexArgument(new ComparableObject { Name = "Isaac" });
			sample.ComplexArgument(new ComparableObject { Name = "Fred" });

			// Assert
			Assert.AreEqual(2, sample.NumberOfCalls);
		}

		[TestMethod]
		public void ComplexArgument_MultipleCallsWithDifferentObjectButEqual_CachesIt()
		{
			sample.ComplexArgument(new ComparableObject { Name = "Isaac" });
			sample.ComplexArgument(new ComparableObject { Name = "Isaac" });

			// Assert
			Assert.AreEqual(1, sample.NumberOfCalls);
		}

		private void AssertIsCached(Func<Int32> cachableMethod)
		{
			var first = cachableMethod();
			var second = cachableMethod();

			Assert.AreEqual(1, sample.NumberOfCalls);
			Assert.AreEqual(first, second);
		}

		public class CachingHandlerTestClass
		{
			private readonly Random generator = new Random();
			public Int32 NumberOfCalls { get; private set; }
			private void IncrementCall()
			{
				NumberOfCalls++;
			}

			public virtual Int32 NoArguments()
			{
				IncrementCall();
				return generator.Next();
			}
			public virtual Int32 SimpleArgument(Int32 value)
			{
				IncrementCall();
				return generator.Next();
			}
			public virtual Int32 SimpleArguments(String first, Int32 second, Int32 third)
			{
				IncrementCall();
				return generator.Next();
			}
			public virtual Int32 ComplexArgument(CachingHandlerTests.ComparableObject complexObject)
			{
				IncrementCall();
				return generator.Next();
			}
			public virtual void ThrowException()
			{
				IncrementCall();
				throw new NotImplementedException();
			}
		}

		public class ComparableObject : IEquatable<ComparableObject>
		{
			public String Name { get; set; }

			public bool Equals(ComparableObject other)
			{
				return this.Name.Equals(other.Name);
			}

			public override bool Equals(object obj)
			{
				var other = obj as ComparableObject;
				if (other == null)
					return false;
				return Equals(other);
			}

			public override int GetHashCode()
			{
				return Name.GetHashCode();
			}
		}
	}
}
