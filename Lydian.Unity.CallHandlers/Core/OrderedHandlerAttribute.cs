using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Core
{
	/// <summary>
	/// A CallHandler attribute that can be ordered.
	/// </summary>
	public abstract class OrderedHandlerAttribute : HandlerAttribute
	{
		/// <summary>
		/// Creates the handler with the order specified on this attribute.
		/// </summary>
		/// <param name="container">The container to use to create the handler.</param>
		/// <param name="callHandler">The call handler to create.</param>
		/// <returns>The Ordered Handler call handler.</returns>
		protected ICallHandler CreateHandler(IUnityContainer container, Type callHandler)
		{
			var handler = (ICallHandler)container.Resolve(callHandler);
			handler.Order = Order;
			return handler;
		}
	}
}
