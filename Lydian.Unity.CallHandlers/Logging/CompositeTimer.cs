using Lydian.Unity.CallHandlers.Core;

namespace Lydian.Unity.CallHandlers.Logging
{
	internal class CompositeTimer : Composite<IMethodTimeListener>
	{
		public void BroadcastComplete(TimedCallEventArgs e)
		{
			Broadcast(p => p.OnMethodCompleted(e));
		}
	}
}
