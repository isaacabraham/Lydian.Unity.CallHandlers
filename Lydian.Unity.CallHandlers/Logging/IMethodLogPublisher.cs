using System;

namespace Lydian.Unity.CallHandlers.Logging
{	
	public interface IMethodLogPublisher
	{
		void OnMethodStarted(CallSiteEventArgs e);
		void OnMethodCompleted(CallSiteEventArgs e);
	}
}
