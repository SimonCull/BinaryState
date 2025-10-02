using BinaryState;
using System;
using System.Collections.Generic;
using Xunit;

namespace BinaryState.Test
{
    public class States_Tests_Positive
    {
        [Theory]
        [InlineData(1)]
        [InlineData(63)]
        [InlineData(100)]
        [InlineData(1000)]
        public void CountIsCorrectAfterConstructor(int expected)
        {
            var states = new States(expected);

            Assert.Equal(expected, states.Count);
        }

        [Theory]
        [InlineData(63)]
        [InlineData(100)]
        [InlineData(1000)]
        public void StatesSetCorrectlyGetReturnsBool(int length)
        {
            var states = new States(length);
            states.SetState(length - 5, true);

            var value = states.GetState(length - 5);

            Assert.True(value);
        }

        [Fact]
        public void SetStatesCorrectlySetsMultipleStates()
        {
            var states = new States(100);
            states.SetState(1, true);
            states.SetState(10, true);
            states.SetState(50, true);
            states.SetState(99, true);
            states.SetState(1, false);

            Assert.False(states.GetState(1));
            Assert.True(states.GetState(10));
            Assert.True(states.GetState(50));
            Assert.True(states.GetState(99));
        }

        [Fact]
        public void DuplicateSetStatesTruePrevervesValue()
        {
            var states = new States(3);
            states.SetState(1, true);
            states.SetState(1, true);

            Assert.False(states.GetState(0));
            Assert.True(states.GetState(1));
            Assert.False(states.GetState(2));
        }

        [Fact]
        public void DuplicateSetStatesFalsePrevervesValue()
        {
            var states = new States(3);
            states.SetState(1, false);
            states.SetState(1, false);

            Assert.False(states.GetState(0));
            Assert.False(states.GetState(1)); 
            Assert.False(states.GetState(2));
        }

        [Theory]
        [InlineData(63)]
        [InlineData(100)]
        [InlineData(1000)]
        public void ComparisonIsCorrect(int length)
        {
            var states1 = new States(length);
            var states2 = new States(length);

            var result = states1.Equals(states2);

            Assert.True(result);
        }
    }

    public class States_Tests_Negative
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CountThrowsIfInvalid(int expected)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new States(expected));           
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-1)]
        public void GetStateThrowsIfIndexOutOfRange(int index)
        {
            var states = new States(10);
            Assert.Throws<ArgumentOutOfRangeException>(() => states.GetState(index));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(-1)]
        public void SetStateThrowsIfIndexOutOfRange(int index)
        {
            var states = new States(10);
            Assert.Throws<ArgumentOutOfRangeException>(() => states.SetState(index, true));
        }
    }
}
