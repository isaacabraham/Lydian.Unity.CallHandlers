
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Diagnostics;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// A call handler to time how long individual methods take. Listeners should implement the IMethodTimeListener interface and place it into Unity as a named registration.
	/// </summary>
	public class TimingHandler : ICallHandler
	{
		public Int32 Order { get; set; }
		private readonly MethodTimePublisher publisher;

		public TimingHandler(IMethodTimePublisher publisher)
		{
			this.publisher = (MethodTimePublisher)publisher;
		}

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