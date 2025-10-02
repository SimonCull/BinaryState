using BenchmarkDotNet.Attributes;
using BinaryState;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearch.Benchmark
{
    public class StatesBenchmarkTestData
    {
        private readonly bool[] testData;
        public readonly List<bool> list;
        public readonly Dictionary<int, bool> dictionary;
        public readonly StringBuilder stringBuilder;
        public readonly bool[] array;
        public readonly States states;

        const int numberOfTestDataItems = 1000000;
        public readonly int indexToFind;

        public StatesBenchmarkTestData()
        {
            var rng = new Random();
            this.testData = new bool[numberOfTestDataItems];
            for (var i = 0; i < numberOfTestDataItems; i++)
            {
                this.testData[i] = rng.Next(0,2) == 1;
            }

            indexToFind = rng.Next(0, numberOfTestDataItems);

            list = [.. testData];
            array = [.. testData];
            states = new States(numberOfTestDataItems);
            dictionary = new Dictionary<int, bool>();
            stringBuilder = new StringBuilder();

            for (var i = 0; i < testData.Length; i++)
            {
                states.SetState(i, testData[i]);
                dictionary.Add(i, testData[i]);
                stringBuilder.Append(testData[i]);
            }
        }

        public StatesBenchmarkTestData(bool[] testData)
        {
            this.testData = testData;
            list = [.. testData];
            array = [.. testData];
            states = new States(numberOfTestDataItems);
            dictionary = new Dictionary<int, bool>();
            stringBuilder = new StringBuilder();

            for (var i = 0; i < testData.Length; i++)
            {
                states.SetState(i, testData[i]);
                dictionary.Add(i, testData[i]);
                stringBuilder.Append(testData[i]);
            }
        }
    }

    [MemoryDiagnoser]
    public class StatesBenchmarks_GetItem
    {
        private readonly StatesBenchmarkTestData testData = new StatesBenchmarkTestData();

        [Benchmark]
        public bool List_GetItem()
        {
            return testData.list[testData.indexToFind];
        }

        [Benchmark]
        public bool Dictionary_GetItem()
        {
            return testData.dictionary[testData.indexToFind];
        }

        [Benchmark]
        public bool StringBuilder_GetItem()
        {
            return testData.stringBuilder[testData.indexToFind] == '1';
        }

        [Benchmark]
        public bool Array_GetItem()
        {
            return testData.array[testData.indexToFind];
        }

        [Benchmark]
        public bool States_GetItem()
        {
            return testData.states.GetState(testData.indexToFind);
        }
    }

    [MemoryDiagnoser]
    public class StatesBenchmarks_SetItem
    {
        private readonly StatesBenchmarkTestData testData = new StatesBenchmarkTestData();

        [Benchmark]
        public void List_SetItem()
        {
            testData.list[testData.indexToFind] = true;
        }

        [Benchmark]
        public void Dictionary_SetItem()
        {
            testData.dictionary[testData.indexToFind] = true;
        }

        [Benchmark]
        public void StringBuilder_SetItem()
        {
            testData.stringBuilder[testData.indexToFind] = '1';
        }

        [Benchmark]
        public void Array_SetItem()
        {
            testData.array[testData.indexToFind] = true;
        }

        [Benchmark]
        public void States_SetItem()
        {
            testData.states.SetState(testData.indexToFind, true);
        }
    }

    [MemoryDiagnoser]
    public class StatesBenchmarks_Compare
    {
        private readonly StatesBenchmarkTestData testData1 = new StatesBenchmarkTestData();
        private readonly StatesBenchmarkTestData testData2;

        public StatesBenchmarks_Compare()
        {
            testData2 = new StatesBenchmarkTestData(testData1.array);
        }

        [Benchmark]
        public bool List_Compare()
        {
            if (testData1.list.Count != testData2.list.Count)
            {
                throw new Exception(nameof(List_Compare));
            }

            for (var i = 0; i < testData1.list.Count; i++)
            {
                if (!testData1.list[i].Equals(testData2.list[i]))
                {
                    throw new Exception(nameof(List_Compare));
                }
            }

            return true;
        }

        [Benchmark]
        public bool Dictionary_Compare()
        {
            if(testData1.dictionary.Count != testData2.dictionary.Count)
            {
                throw new Exception(nameof(Dictionary_Compare));
            }

            for (var i = 0; i < testData1.dictionary.Count; i++)
            {
                if (!testData1.dictionary[i].Equals(testData2.dictionary[i]))
                {
                    throw new Exception(nameof(Dictionary_Compare));
                }
            }

            return true;
        }

        [Benchmark]
        public bool StringBuilder_Compare()
        {
            return testData1.stringBuilder.Equals(testData2.stringBuilder);
        }

        [Benchmark]
        public bool Array_Compare()
        {
            if (testData1.array.Length != testData2.array.Length)
            {
                throw new Exception(nameof(Array_Compare));
            }

            for (var i = 0; i < testData1.array.Length; i++)
            {
                if (!testData1.array[i].Equals(testData2.array[i]))
                {
                    throw new Exception(nameof(Array_Compare));
                }
            }

            return true;
        }

        [Benchmark]
        public bool States_Compare()
        {
            return testData1.states.Equals(testData2.states);
        }
    }
}
