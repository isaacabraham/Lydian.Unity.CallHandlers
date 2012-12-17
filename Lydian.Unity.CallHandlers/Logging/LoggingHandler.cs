using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// A call handler to log the entrance and exits of individual methods. Listeners should implement the IMethodLogListener interface and place it into Unity as a named registration.
	/// </summary>
	public class LoggingHandler : ICallHandler
	{
		private readonly CompositeLogger broadcaster;
		public Int32 Order { get; set; }

		/// <summary>
		/// Initializes a new instance of the LoggingHandler class.
		/// </summary>
		public LoggingHandler(IUnityContainer container)
		{
			broadcaster = container.Resolve<CompositeLogger>();
		}

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			var eventArgs = new CallSiteEventArgs(input.Target, input.MethodBase);

			broadcaster.BroadcastStart(eventArgs);
			var result = getNext()(input, getNext);
			broadcaster.BroadcastComplete(eventArgs);

			return result;
		}
	}
}