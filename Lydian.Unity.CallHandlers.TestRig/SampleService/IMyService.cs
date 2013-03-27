using Lydian.Unity.CallHandlers;
using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
	//[ArgumentNotNull] Place the attribute here in order to apply to all methods.
	public interface IMyService
	{
		Int32 NullableArgument(Nullable<Int32> nullableNumber);

		[Caching(Order = 2), Logging(Order = 3), Timing(Order = 1)]
		IEnumerable<String> GetNames();

		[Caching(Order = 2), Logging(Order = 3), Timing(Order = 1)]
		String GetDepartments(String startingLetter);

		[Caching(Order = 2), Logging(Order = 3), Timing(Order = 1)]
		Int32 GetNumber(Int32 startingNumber, Int32 adder);

		[Caching(Order = 2), Logging(Order = 3), Timing(Order = 1)]
		String ComplexTypeArgument(ComplexType type);

		void OptionalArgument(String nullableName = null);
	}
}
