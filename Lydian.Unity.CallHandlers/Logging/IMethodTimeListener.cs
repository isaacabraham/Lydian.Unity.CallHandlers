
namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Represents a listener to TimingHandler messages.
	/// </summary>
	public interface IMethodTimeListener
	{
		/// <summary>
		/// Called whenever a method has completed.
		/// </summary>
		/// <param name="eventArgs">Timing details of the method call.</param>
		void OnMethodCompleted(TimedCallEventArgs eventArgs);
	}
}
