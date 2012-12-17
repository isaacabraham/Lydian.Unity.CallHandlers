using Lydian.Unity.CallHandlers.Logging;
using System;

namespace ConsoleApplication1
{
	/// <summary>
	/// A console logger for method start / exit.
	/// </summary>
	public class ConsoleLogger : IMethodLogListener
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
