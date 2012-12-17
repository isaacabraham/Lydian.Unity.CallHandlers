using System;
using System.Reflection;

namespace Lydian.Unity.CallHandlers.Logging
{
	/// <summary>
	/// Contains details of a method call.
	/// </summary>
	public class CallSiteEventArgs : EventArgs
	{
		/// <summary>
		/// The target of the method call.
		/// </summary>
		public Object Target { get; private set; }
		/// <summary>
		/// The method that is called.
		/// </summary>
		public MethodBase Method { get; private set; }

		internal CallSiteEventArgs(Object target, MethodBase method)
		{
			Target = target;
			Method = method;
		}
	}
}
