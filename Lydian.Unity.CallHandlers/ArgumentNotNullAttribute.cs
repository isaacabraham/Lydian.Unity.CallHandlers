using Lydian.Unity.CallHandlers.Core;
using Lydian.Unity.CallHandlers.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Lydian.Unity.CallHandlers
{
	public class ArgumentNotNullAttribute : OrderedHandlerAttribute
	{
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return CreateHandler(container, typeof(ArgumentNotNullHandler));
		}
	}
}
