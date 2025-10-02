using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryState
{
    public class States
    {
        readonly int length;
        readonly State[] states;
        public States(int numberOfStates) 
        {
            if (numberOfStates <= 0)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numberOfStates, nameof(numberOfStates));
            }

            length = numberOfStates;
            states = new State[(numberOfStates/State.MaxLength)+(numberOfStates % State.MaxLength > 0 ? 1 : 0)];
            int remaining = numberOfStates;
            int counter = 0;
            while (remaining > State.MaxLength)
            {
                states[counter++] = new State();
                remaining -= State.MaxLength;
            }

            if (remaining > 0)
            {
                states[counter]=  new State(remaining);
            }
        }

        public int Count { get => this.length; }

        public bool GetState(int index)
        {
            ValidateIndex(index);

            return states[index / State.MaxLength].GetState(index % State.MaxLength);
        }

        public void SetState(int index, bool value)
        {
            ValidateIndex(index);

            states[index / State.MaxLength].SetState(index % State.MaxLength, value);
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
            var sb = new StringBuilder();
            foreach (var state in this.states)
            {
                sb.Append(state.ToString());
            }

            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            if(obj is States s)
            {
                if (this.length != s.length) 
                    return false;

                for(int i = 0; i<this.states.Length; i++)
                {
                    if (!this.states[i].Equals(s.states[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(states.GetHashCode(), this.length);
        }
    }
}
