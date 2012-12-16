
namespace Lydian.Unity.CallHandlers.Caching
{
	/// <summary>
	/// The results of an attempt to retrieve something from the method cache.
	/// </summary>
	internal enum CacheHitResult
	{
		/// <summary>
		/// The call was located.
		/// </summary>
		Success,
		/// <summary>
		/// The call was not located.
		/// </summary>
		Failure
	}
}
