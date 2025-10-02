using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryState
{
    public static class StatesExtensions
    {
        public static States ToStatesCollection(this ICollection<bool> collection)
        {
            var states = new States(collection.Count);
            var index = 0;
            foreach (var state in collection) 
            {
                states.SetState(index++, state);
            }
            return states;
        }

        public static States ToStatesCollection<T>(this ICollection<T> collection, Func<T, bool> func)
        {
            var states = new States(collection.Count);
            var index = 0;
            foreach (var state in collection)
            {
                states.SetState(index++, func(state));
            }
            return states;
        }

    }
}
