using System;
using System.Reflection;

namespace Lydian.Unity.CallHandlers.Logging
{
	public class TimedCallEventArgs : CallSiteEventArgs
	{
		public TimeSpan CallDuration { get; private set; }

		/// <summary>
		/// Initializes a new instance of the TimedCallEventArgs class.
		/// </summary>
		public TimedCallEventArgs(Object target, MethodBase method, TimeSpan callDuration)
			: base(target, method)
		{
			CallDuration = callDuration;
		}
	}
}
