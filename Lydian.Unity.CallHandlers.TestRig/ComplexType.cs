using System;

namespace ConsoleApplication1
{
	public class ComplexType : IEquatable<ComplexType>
	{
		public String Name { get; set; }
		public Int32 Age { get; set; }

		public Boolean Equals(ComplexType other)
		{
			return Name.Equals(other.Name) && Age.Equals(other.Age);
		}

		public override Boolean Equals(object obj)
		{
			if (obj is ComplexType)
				return Equals((ComplexType)obj);

			return false;
		}

		public override Int32 GetHashCode()
		{
			return Name.GetHashCode() ^ Age.GetHashCode();
		}
	}
}
