using System;
using System.Reflection;

namespace Lydian.Unity.CallHandlers.Logging
{
	public class CallSiteEventArgs : EventArgs
	{
		public Object Target { get; private set; }
		public MethodBase Method { get; private set; }

		/// <summary>
		/// Initializes a new instance of the CallSiteEventArgs class.
		/// </summary>
		internal CallSiteEventArgs(Object target, MethodBase method)
		{
			Target = target;
			Method = method;
		}
	}
}
