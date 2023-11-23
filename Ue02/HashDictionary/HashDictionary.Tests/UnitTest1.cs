using HashDictionary.Impl;
using System;
using System.Collections.Generic;
using Xunit;

namespace HashDictionary.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CountTest()
        {
            var dict = new HashDictionary<int, int>();
            Assert.Empty(dict);
            dict.Add(1, 2);
            dict.Add(2, 3);
            Assert.Equal(2, dict.Count);

            dict[2] = 4;
            Assert.Equal(2, dict.Count);
        }

        [Fact]
        public void SimpleAddTest()
        {
            var dict = new HashDictionary<int, int>();
            dict.Add(1, 10);
            Assert.Equal(10, dict[1]);

            dict.Add(2, 20);
            Assert.Equal(10, dict[1]);
            Assert.Equal(20, dict[2]);
        }

        [Fact]
        public void AddExceptionTest()
        {
            var dict = new HashDictionary<int, int>();
            dict.Add(1, 10);
            Assert.Equal(10, dict[1]);
            Assert.Throws<ArgumentException>(() => dict.Add(1, 20));
        }

        [Theory]
        [InlineData(new []{ 1, 2, 3, 4, 5 }, 5)]
        [InlineData(new []{ 1, 2 }, 2)]
        [InlineData(new []{ 1 }, 1)]
        public void CountTherory(IEnumerable<int> list, int expected)
        {
            var dict = new HashDictionary<int, int>();
            var i = 0;
            foreach (var item in list)
            {
                dict.Add(i++, item);
            }
            Assert.Equal(dict.Count, expected);
        }
    }
}
