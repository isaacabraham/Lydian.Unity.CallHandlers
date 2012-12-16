using Lydian.Unity.CallHandlers.Logging;
using System;

namespace ConsoleApplication1
{
	public class ConsoleLogger : IMethodLogPublisher
	{
		public void OnMethodStarted(CallSiteEventArgs e)
		{
			Console.WriteLine("Entered method {0}.", e.Method.Name);
		}

		public void OnMethodCompleted(CallSiteEventArgs e)
		{
			Console.WriteLine("Exited method {0}.", e.Method.Name);
		}
	}
}
