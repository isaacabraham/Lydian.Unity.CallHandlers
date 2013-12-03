using System;
namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Exposes method log events.
	/// </summary>
	public interface IMethodLogPublisher
	{
		/// <summary>
		/// Fired whenever a method call begins or ends.
		/// </summary>
		event EventHandler<CallSiteEventArgs> OnLogMessage;
	}
}
