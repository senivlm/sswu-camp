using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Pair<key, value>
    {
        public Pair(key Key, value Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public value Value { get; set; }
        public key Key { get; set; }

        public override string ToString() => $"{Value} - {Key}";

        public static bool operator ==(Pair<key, value> lhs, Pair<key, value> rhs)
        {
            return EqualityComparer<key>.Default.Equals(lhs.Key, rhs.Key) &&
                EqualityComparer<value>.Default.Equals(lhs.Value, rhs.Value);
        }

        public static bool operator!= (Pair<key, value> lhs, Pair<key, value> rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (!(obj is Pair<key, value>))
            {
                throw new Exception("Error: " + nameof(obj) + "must have the same type of keys and values");
            }

            return this == obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}
