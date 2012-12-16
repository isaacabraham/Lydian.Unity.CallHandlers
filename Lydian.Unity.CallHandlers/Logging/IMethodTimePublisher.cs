
namespace Lydian.Unity.CallHandlers.Logging
{
	public interface IMethodTimePublisher
	{
		void OnMethodCompleted(TimedCallEventArgs eventArgs);
	}
}
