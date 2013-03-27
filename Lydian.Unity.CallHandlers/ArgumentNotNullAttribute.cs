using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Applies the ArgumentNotNullHandler onto the specified method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class)]
	public class ArgumentNotNullAttribute : HandlerAttribute
	{
		/// <summary>
		/// Creates the handler.
		/// </summary>
		/// <param name="container">The container to use to create the handler.</param>
		/// <returns>The Argument Not Null call handler.</returns>
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return container.CreateCallHandler<ArgumentNotNullHandler>(Order);
		}
	}
}
