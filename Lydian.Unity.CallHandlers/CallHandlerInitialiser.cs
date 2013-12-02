using Lydian.Unity.CallHandlers.Caching;
using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;

namespace Lydian.Unity.CallHandlers
{
	/// <summary>
	/// Contains key registrations for the Unity Call Handlers.
	/// </summary>
	public static class CallHandlerInitialiser
	{
		/// <summary>
		/// Registers key singletons into the Unity Container required by the various Unity Call Handlers.
		/// </summary>
		/// <param name="container"></param>
		public static void RegisterCallHandlerDependencies(IUnityContainer container)
		{
			container.RegisterType<MethodCache>(new ContainerControlledLifetimeManager());
			container.RegisterType<IMethodLogPublisher, MethodLogPublisher>(new ContainerControlledLifetimeManager());
			container.RegisterType<IMethodTimePublisher, MethodTimePublisher>(new ContainerControlledLifetimeManager());
		}
	}
}
