
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Diagnostics;

namespace Lydian.Unity.CallHandlers.Logging
{
	public class TimingHandler : ICallHandler
	{
		public Int32 Order { get; set; }
		private readonly Lydian.Unity.CallHandlers.Logging.CompositeTimer broadcaster;

		/// <summary>
		/// Initializes a new instance of the TimingHandler class.
		/// </summary>
		/// <param name="publisher">The publisher.</param>
		public TimingHandler(IUnityContainer container)
		{
			broadcaster = container.Resolve<Lydian.Unity.CallHandlers.Logging.CompositeTimer>();
		}

		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			var stopwatch = Stopwatch.StartNew();
			var result = getNext()(input, getNext);
			stopwatch.Stop();
			broadcaster.BroadcastComplete(new TimedCallEventArgs(input.Target, input.MethodBase, stopwatch.Elapsed));
			return result;
		}
	}
}