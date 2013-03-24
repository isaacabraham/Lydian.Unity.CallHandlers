using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// A call handler to log the entrance and exits of individual methods. Listeners should implement the IMethodLogListener interface and place it into Unity as a named registration.
	/// </summary>
	public sealed class LoggingHandler : ICallHandler
	{
		public Int32 Order { get; set; }
		private readonly MethodLogPublisher publisher;

		public LoggingHandler(IMethodLogPublisher publisher)
		{
			this.publisher = (MethodLogPublisher)publisher;
		}

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			var eventArgs = new CallSiteEventArgs(input.Target, input.MethodBase);

			publisher.FireStarted(eventArgs);
			var result = getNext()(input, getNext);
			publisher.FireCompleted(eventArgs);

			return result;
		}

	}
}