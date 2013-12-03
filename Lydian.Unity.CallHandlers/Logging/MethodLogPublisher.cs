using System;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Manages tracking of and publication of log events to listeners.
	/// </summary>
	internal class MethodLogPublisher : IMethodLogPublisher
	{
		public event EventHandler<CallSiteEventArgs> OnLogMessage;

		internal void Trigger(CallSiteEventArgs args) { Trigger(args, OnLogMessage); }

		private void Trigger(CallSiteEventArgs args, EventHandler<CallSiteEventArgs> handler)
		{
			if (handler != null)
				handler(this, args);
		}
	}
}
