using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Validation
{
	/// <summary>
	/// A handler to ensure that all reference type arguments are non-null. If any arguments are null, an ArgumentNullException is raised.
	/// </summary>
	public class ArgumentNotNullHandler : ICallHandler
	{
		public Int32 Order { get; set; }

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			for (int i = 0; i < input.Inputs.Count; i++)
			{
				if (input.Inputs[i] != null)
					continue;

				var parameterInfo = input.Inputs.GetParameterInfo(i);

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