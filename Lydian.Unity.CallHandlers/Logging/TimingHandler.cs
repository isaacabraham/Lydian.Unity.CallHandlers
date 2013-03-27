
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Diagnostics;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// A call handler to time how long individual methods take. Listeners should subscribe to events published by the IMethodTimePublisher.
	/// </summary>
	public class TimingHandler : ICallHandler
	{
		/// <summary>
		/// Order in which the handler will be executed.
		/// </summary>
		public Int32 Order { get; set; }
		private readonly MethodTimePublisher publisher;

		/// <summary>
		/// Creates a new instance of the Timing Handler.
		/// </summary>
		/// <param name="publisher">The publisher used to broadcast timing events.</param>
		public TimingHandler(IMethodTimePublisher publisher)
		{
			this.publisher = (MethodTimePublisher)publisher;
		}


		/// <summary>
		/// Invokes the Timing Handler
		/// </summary>
		/// <param name="input">Inputs to the current call to the target.</param>
		/// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
		/// <returns>Return value from the target.</returns>
		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			var stopwatch = Stopwatch.StartNew();
			var result = getNext()(input, getNext);
			stopwatch.Stop();
			publisher.FireEvent(new TimedCallEventArgs(input.Target, input.MethodBase, stopwatch.Elapsed));
			return result;
		}
	}
}