using System;
using System.Reflection;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Represents the timed results of a completed method call.
	/// </summary>
	public class TimedCallEventArgs : CallSiteEventArgs
	{
		public TimeSpan CallDuration { get; private set; }

		/// <summary>
		/// Initializes a new instance of the TimedCallEventArgs class.
		/// </summary>
		internal TimedCallEventArgs(Object target, MethodBase method, TimeSpan callDuration) : base(target, method)
		{
			CallDuration = callDuration;
		}
	}
}
