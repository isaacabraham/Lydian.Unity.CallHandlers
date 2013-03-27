using Lydian.Unity.CallHandlers.Caching;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Applies the CachingHandler onto the specified method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class)]
	public class CachingAttribute : HandlerAttribute
	{
		/// <summary>
		/// Creates the handler.
		/// </summary>
		/// <param name="container">The container to use to create the handler.</param>
		/// <returns>The Caching call handler.</returns>
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return container.CreateCallHandler<CachingHandler>(Order);
		}
	}
}
