using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Core
{
	public abstract class OrderedHandlerAttribute : HandlerAttribute
	{
		public Int32 Order { get; set; }

		protected ICallHandler CreateHandler(IUnityContainer container, Type callHandler)
		{
			var handler = (ICallHandler)container.Resolve(callHandler);
			handler.Order = Order;
			return handler;
		}
	}
}
