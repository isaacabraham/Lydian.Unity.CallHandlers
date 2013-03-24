using System;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Manages tracking of and publication of events to subscribers.
	/// </summary>
	internal class MethodTimePublisher : IMethodTimePublisher
	{
		public event EventHandler<TimedCallEventArgs> OnMethodCompleted;

		public void FireEvent(TimedCallEventArgs ea)
		{
			var handler = OnMethodCompleted;
			if (handler != null)
				handler(this, ea);
		}
	}
}
