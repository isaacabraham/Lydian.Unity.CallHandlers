using Lydian.Unity.CallHandlers.Logging;
using System;

namespace ConsoleApplication1
{
	public class ConsoleTimer : IMethodTimePublisher
	{
		public void OnMethodCompleted(TimedCallEventArgs eventArgs)
		{
			Console.WriteLine("Method {0} took {1}ms.", eventArgs.Method.Name, eventArgs.CallDuration.TotalMilliseconds);
		}
	}
}
