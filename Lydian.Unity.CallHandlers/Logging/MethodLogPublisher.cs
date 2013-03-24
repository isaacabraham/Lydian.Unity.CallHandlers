using System;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Manages tracking of and publication of log events to listeners.
	/// </summary>
	internal class MethodLogPublisher : IMethodLogPublisher
	{
		public event EventHandler<CallSiteEventArgs> OnMethodStarted;
		public event EventHandler<CallSiteEventArgs> OnMethodCompleted;

		internal void FireStarted(CallSiteEventArgs args) { FireEvent(args, OnMethodStarted); }
		internal void FireCompleted(CallSiteEventArgs args) { FireEvent(args, OnMethodCompleted); }

		private void FireEvent(CallSiteEventArgs args, EventHandler<CallSiteEventArgs> handler)
		{
			if (handler != null)
				handler(this, args);
		}
	}
}
