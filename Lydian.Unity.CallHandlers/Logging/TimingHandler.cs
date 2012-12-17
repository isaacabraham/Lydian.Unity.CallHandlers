
using Microsoft.Practices.Unity;
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
		private readonly CompositeTimer broadcaster;

		/// <summary>
		/// Initializes a new instance of the TimingHandler class.
		/// </summary>
		/// <param name="publisher">The publisher.</param>
		public TimingHandler(IUnityContainer container)
		{
			broadcaster = container.Resolve<CompositeTimer>();
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