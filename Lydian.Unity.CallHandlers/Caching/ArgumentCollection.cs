using System;
using System.Collections.Generic;
using System.Linq;

namespace Lydian.Unity.CallHandlers.Caching
{
	/// <summary>
	/// Represents a specific set of arguments passed to a method.
	/// </summary>
	internal class ArgumentCollection : IEquatable<ArgumentCollection>, IEqualityComparer<ArgumentCollection>
	{
		/// <summary>
		/// The set of arguments on the call site.
		/// </summary>
		private Tuple<Int32, Object>[] Arguments { get; set; }

		/// <summary>
		/// Initializes a new instance of the CallSite class.
		/// </summary>
		/// <param name="arguments">The set of arguments for this call site.</param>
		public ArgumentCollection(IEnumerable<Object> arguments)
		{
			Arguments = arguments
							.Select((arg, idx) => Tuple.Create(idx, arg))
							.ToArray();
		}

		public Boolean Equals(ArgumentCollection other)
		{
			return Arguments.Count().Equals(other.Arguments.Count())
				&& Arguments.All(arg => arg.Item2.Equals(other.Arguments[arg.Item1].Item2));
		}

		public Boolean Equals(ArgumentCollection x, ArgumentCollection y)
		{
			return x.Equals(y);
		}

		public override Boolean Equals(object obj)
		{
			var other = obj as ArgumentCollection;
			if (other == null)
				return false;

			return Equals(other);
		}

		public override Int32 GetHashCode()
		{
			return GetHashCode(this);
		}

		public Int32 GetHashCode(ArgumentCollection obj)
		{
			return Arguments.Aggregate(0, (acc, arg) => acc ^ arg.Item2.GetHashCode());
		}
	}
}
