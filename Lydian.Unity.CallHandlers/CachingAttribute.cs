using Lydian.Unity.CallHandlers.Caching;
using Lydian.Unity.CallHandlers.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Applies the CachingHandler onto the specified method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class CachingAttribute : OrderedHandlerAttribute
	{
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return base.CreateHandler(container, typeof(CachingHandler));
		}
	}
}
