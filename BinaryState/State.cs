using System;
using System.Reflection;

namespace BinaryState
{
    internal class State
    {
        readonly System.Threading.Lock _stateLock = new();

        ulong data;
        internal const int MaxLength = 63;
        readonly int length;
        static readonly ulong unit = 1ul;

        internal State()
        {
            this.length = MaxLength;
            lock (this._stateLock)
            {
                this.data = 0b10000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000;
            }
        }

        internal State(int length)
        {
            if (length <= 0)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length, nameof(length));
            }

            if (length > MaxLength)
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(length, State.MaxLength, nameof(length));
            }

            this.length = length;
            lock (this._stateLock)
            {
                data = unit << length + 1;
            }
        }

        internal void SetState(int index, bool bitValue)
        {
            ValidateIndex(index);

            lock (this._stateLock)
            {
                if (bitValue)
                {
                    data |= (unit << length - index);
                }
                else
                {
                    data = ~(~data | (unit << length - index));
                }
            }
        }

        internal bool GetState(int index)
        {
            ValidateIndex(index);

            lock (this._stateLock)
            {
                return (((data << index) >>> length) & unit) == 1;
            }
        }

        void ValidateIndex(int index)
        {
            if (index < 0)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            }

            if (index > length)
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(index, length, nameof(index));
            }
        }

        public override string ToString()
        {
            return Convert.ToString((long)data, 2);
        }

        public override bool Equals(object? obj)
        {
            if (obj is State s)
            {
                return this.data == s.data;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return ((this.data << 1) >>> 1).GetHashCode();
        }

        public bool[] ToArray()
        {
            var array = new bool[length];

            for(var i  = 0; i < length; i++)
            {
                array[i] = this.GetState(i);
            }
            return array;
        }
    }
}