using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Validation
{
	/// <summary>
	/// A handler to ensure that all reference type arguments are non-null. If any arguments are null, an ArgumentNullException is raised.
	/// </summary>
	public class ArgumentNotNullHandler : ICallHandler
	{
		/// <summary>
		/// Order in which the handler will be executed.
		/// </summary>
		public Int32 Order { get; set; }

		/// <summary>
		/// Invokes the Argument Not Null Handler.
		/// </summary>
		/// <param name="input">Inputs to the current call to the target.</param>
		/// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
		/// <returns>Return value from the target.</returns>
		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			for (int argument = 0; argument < input.Inputs.Count; argument++)
			{
				if (input.Inputs[argument] != null)
					continue;

				var parameterInfo = input.Inputs.GetParameterInfo(argument);

				if (parameterInfo.ParameterType.IsValueType ||
					parameterInfo.IsOptional)
				{
					continue;
				}

				return input.CreateExceptionMethodReturn(new ArgumentNullException(parameterInfo.Name));
			}

			return getNext()(input, getNext);
		}
	}
}