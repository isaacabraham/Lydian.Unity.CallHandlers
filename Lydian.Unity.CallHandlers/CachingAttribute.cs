using Lydian.Unity.CallHandlers.Caching;
using Lydian.Unity.CallHandlers.Core;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// An attribute for the CachingHandler call handler.
	/// </summary>
	public class CachingAttribute : OrderedHandlerAttribute
	{
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return base.CreateHandler(container, typeof(CachingHandler));
		}
	}
}
