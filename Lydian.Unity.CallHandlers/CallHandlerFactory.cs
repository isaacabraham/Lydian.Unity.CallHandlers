using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Helper methods to quickly and easier construct call handlers.
	/// </summary>
	internal static class CallHandlerFactory
	{
		/// <summary>
		/// Creates a Call Handler.
		/// </summary>
		/// <param name="container">The container to create the handler.</param>
		/// <param name="order">The order of the handler.</param>
		/// <typeparam name="TCallHandler">The type of call handler to construct.</typeparam>
		/// <returns>The created call handler.</returns>
		public static TCallHandler CreateCallHandler<TCallHandler>(this IUnityContainer container, Int32 order) where TCallHandler : ICallHandler
		{
			var handler = container.Resolve<TCallHandler>();
			handler.Order = order;
			return handler;
		}
	}
}
