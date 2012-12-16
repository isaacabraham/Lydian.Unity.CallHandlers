using System;
using System.Collections.Generic;

namespace Lydian.Unity.CallHandlers.Caching
{
	/// <summary>
	/// Stores calls to methods, keyed on method and then argument list.
	/// </summary>
	internal class MethodCache
	{
		private readonly IDictionary<String, ArgumentCollectionCache> callSiteCache = new Dictionary<String, ArgumentCollectionCache>();

		/// <summary>
		/// Attempts to retrieve a previous cached call.
		/// </summary>
		/// <param name="methodName">The method call.</param>
		/// <param name="arguments">The set of arguments.</param>
		/// <returns>A tuple containing a Boolean on whether the cache was hit, and the cached result.</returns>
		internal Tuple<CacheHitResult, Object> TryGetResult(String methodName, ArgumentCollection arguments)
		{
			AddArgumentCacheIfRequired(methodName);

			var argumentCache = callSiteCache[methodName];
			var callSiteHasBeenCalledBefore = argumentCache.ContainsKey(arguments);
			if (callSiteHasBeenCalledBefore)
				return Tuple.Create(CacheHitResult.Success, argumentCache[arguments]);

			return Tuple.Create(CacheHitResult.Failure, (Object)null);
		}

		/// <summary>
		/// Adds a result for a method with the supplied set of arguments.
		/// </summary>
		/// <param name="methodName">The method name.</param>
		/// <param name="arguments">The set of arguments.</param>
		/// <param name="result">The result of the call to cache.</param>
		internal void AddToCache(String methodName, ArgumentCollection arguments, Object result)
		{
			callSiteCache[methodName][arguments] = result;
		}

		private void AddArgumentCacheIfRequired(String methodName)
		{
			if (!callSiteCache.ContainsKey(methodName))
				callSiteCache[methodName] = new ArgumentCollectionCache();
		}

		/// <summary>
		/// A cache of all calls made to a specific method.
		/// </summary>
		class ArgumentCollectionCache : Dictionary<ArgumentCollection, Object> { }
	}
}
