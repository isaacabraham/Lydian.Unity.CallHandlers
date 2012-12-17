namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Represents a listener to LoggingHandler messages.
	/// </summary>
	public interface IMethodLogListener
	{
		/// <summary>
		/// Called whenever a method call begins.
		/// </summary>
		/// <param name="eventArgs">Details of the method call.</param>
		void OnMethodStarted(CallSiteEventArgs eventArgs);
		/// <summary>
		/// Called whenever a method call ends.
		/// </summary>
		/// <param name="eventArgs">Details of the method call.</param>
		void OnMethodCompleted(CallSiteEventArgs eventArgs);
	}
}
