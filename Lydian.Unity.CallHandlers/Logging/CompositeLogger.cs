using Lydian.Unity.CallHandlers.Core;
namespace Lydian.Unity.CallHandlers.Logging
{
	internal class CompositeLogger : Composite<IMethodLogListener>
	{
		public void BroadcastStart(CallSiteEventArgs e)
		{
			Broadcast(p => p.OnMethodStarted(e));
		}

		public void BroadcastComplete(CallSiteEventArgs e)
		{
			Broadcast(p => p.OnMethodCompleted(e));
		}
	}
}
