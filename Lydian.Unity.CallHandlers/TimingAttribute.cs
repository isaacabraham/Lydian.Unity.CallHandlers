using Lydian.Unity.CallHandlers.Core;
using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Applies the TimingHandler onto the specified method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class TimingAttribute : OrderedHandlerAttribute
	{
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return base.CreateHandler(container, typeof(TimingHandler));
		}
	}
}
