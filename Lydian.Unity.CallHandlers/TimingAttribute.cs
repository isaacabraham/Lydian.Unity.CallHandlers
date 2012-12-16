using Lydian.Unity.CallHandlers.Core;
using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Lydian.Unity.CallHandlers
{
	public class TimingAttribute : OrderedHandlerAttribute
	{
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return base.CreateHandler(container, typeof(TimingHandler));
		}
	}
}
