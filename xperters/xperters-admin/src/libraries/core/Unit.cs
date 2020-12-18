using System;

namespace Xperters.Core
{
    public sealed class Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {
        public static readonly Unit Value = new Unit();

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object other)
        {
            return other == null || other is Unit;
        }

        public static bool operator ==(Unit obj1, Unit obj2)
        {
            return true;
        }

        public static bool operator !=(Unit obj1, Unit obj2)
        {
            return false;
        }

        public bool Equals(Unit other)
        {
            return true;
        }

        int IComparable<Unit>.CompareTo(Unit other)
        {
            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            if (Equals(obj))
            {
                return 0;
            }

            return 1;
        }
    }
}
