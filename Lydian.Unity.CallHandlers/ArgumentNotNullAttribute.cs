using Lydian.Unity.CallHandlers.Core;
using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Applies the ArgumentNotNullHandler onto the specified method.
	/// </summary>
	public class ArgumentNotNullAttribute : OrderedHandlerAttribute
	{
		/// <summary>
		/// Creates the handler.
		/// </summary>
		/// <param name="container">The container to use to create the handler.</param>
		/// <returns>The Argument Not Null call handler.</returns>
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return CreateHandler(container, typeof(ArgumentNotNullHandler));
		}
	}
}
