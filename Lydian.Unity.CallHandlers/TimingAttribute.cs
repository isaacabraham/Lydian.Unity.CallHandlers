using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Applies the TimingHandler onto the specified method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class)]
	public class TimingAttribute : HandlerAttribute
	{
		/// <summary>
		/// Creates the handler.
		/// </summary>
		/// <param name="container">The container to use to create the handler.</param>
		/// <returns>The Timing call handler.</returns>
		public override ICallHandler CreateHandler(IUnityContainer container)
		{
			return container.CreateCallHandler<TimingHandler>(Order);
		}
	}
}
