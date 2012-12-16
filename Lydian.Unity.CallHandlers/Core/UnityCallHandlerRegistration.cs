using Lydian.Unity.CallHandlers.Caching;
using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;

namespace Lydian.Unity.CallHandlers.Core
{
	/// <summary>
	/// Contains key registrations for the Unity Call Handlers.
	/// </summary>
	public static class UnityCallHandlerRegistration
	{
		public static void Register(IUnityContainer container)
		{
			container.RegisterType<MethodCache>(new ContainerControlledLifetimeManager());
			container.RegisterType<CompositeLogger>(new ContainerControlledLifetimeManager());
			container.RegisterType<CompositeTimer>(new ContainerControlledLifetimeManager());
		}
	}
}
