using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Linq;

namespace Lydian.Unity.CallHandlers.Caching
{
	/// <summary>
	/// A call handler which caches calls whose arguments are IEquitable.
	/// </summary>
    public class CachingHandler : ICallHandler
    {
		private readonly MethodCache cache;
		/// <summary>
		/// Order in which the handler will be executed.
		/// </summary>
		public Int32 Order { get; set; }

		/// <summary>
		/// Initializes a new instance of the CachingHandler class.
		/// </summary>
		/// <param name="container">The Unity Container.</param>
		public CachingHandler(IUnityContainer container)
		{
			this.cache = container.Resolve<MethodCache>();
		}


		/// <summary>
		/// Invokes the Caching Handler.
		/// </summary>
		/// <param name="input">Inputs to the current call to the target.</param>
		/// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
		/// <returns>Return value from the target.</returns>
		public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
		{
			var methodName = String.Format("{0}.{1}", input.Target.GetType().FullName, input.MethodBase.Name);
			var callSiteDetails = new ArgumentCollection(input.Arguments.OfType<Object>());

			var cachedCall = cache.TryGetResult(methodName, callSiteDetails);
			switch (cachedCall.Item1)
			{
				case CacheHitResult.Success:
					return cachedCall.Item2;
				case CacheHitResult.Failure:
					var result = getNext()(input, getNext);
					cache.AddToCache(methodName, callSiteDetails, result);
					return result;
				default:
					throw new Exception("Unknown cache status.");
			}
		}
	}
}