using Lydian.Unity.CallHandlers.Logging;
using System;

namespace ConsoleApplication1
{
	/// <summary>
	/// A sample console logger for timing publications.
	/// </summary>
	public class ConsoleTimer : IMethodTimeListener
	{
		public void OnMethodCompleted(TimedCallEventArgs eventArgs)
		{
			Console.WriteLine("Method {0} took {1}ms.", eventArgs.Method.Name, eventArgs.CallDuration.TotalMilliseconds);
		}
	}
}
