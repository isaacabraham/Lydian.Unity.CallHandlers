
using System;
namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Exposes method timing events.
	/// </summary>
	public interface IMethodTimePublisher
	{
		/// <summary>
		/// Fired whenever a method completes.
		/// </summary>
		event EventHandler<TimedCallEventArgs> OnMethodCompleted;
	}
}
