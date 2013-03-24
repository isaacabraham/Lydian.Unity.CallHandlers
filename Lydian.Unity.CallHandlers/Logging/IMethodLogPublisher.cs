using System;
namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Exposes method log events.
	/// </summary>
	public interface IMethodLogPublisher
	{
		/// <summary>
		/// Fired whenever a method call begins.
		/// </summary>
		event EventHandler<CallSiteEventArgs> OnMethodStarted;
		/// <summary>
		/// Fired whenever a method call completes.
		/// </summary>
		event EventHandler<CallSiteEventArgs> OnMethodCompleted;
	}
}
