using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
	internal class MyService : IMyService
	{
		public IEnumerable<String> GetNames()
		{
			return new[] { "Isaac", "Mike", "Mark", "Dan", "Richard", "Alex" };
		}

		public String GetDepartments(String startingLetter)
		{
			return startingLetter + " department!";
		}

		public Int32 GetNumber(Int32 startingNumber, Int32 adder)
		{
			return startingNumber + adder;
		}

		public String ComplexTypeArgument(ComplexType type)
		{
			return String.Format("{0} - {1}", type.Name, type.Age);
		}

		public void OptionalArgument(String name = null)
		{
			if (name != null)
				Console.WriteLine(name);
		}

		public Int32 NullableArgument(Nullable<Int32> theNumber)
		{
			if (theNumber.HasValue)
				return theNumber.Value;
			return default(Int32);
		}
	}
}
