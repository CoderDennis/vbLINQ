﻿#region Copyright and license information
// Copyright 2010-2011 Jon Skeet
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Edulinq.Tests
{    
    [TestFixture]
    public class MinTest
    {        
        #region Int32 tests
        [Test]
        public void NullInt32Source()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Min());
        }

        [Test]
        public void NullInt32Selector()
        {
            string[] source = { "" };
            Func<string, int> selector = null;
            Assert.Throws<ArgumentNullException>(() => source.Min(selector));
        }

        [Test]
        public void EmptySequenceInt32NoSelector()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Min());
        }

        [Test]
        public void EmptySequenceInt32WithSelector()
        {
            string[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Min(x => x.Length));
        }

        [Test]
        public void SimpleSequenceInt32NoSelector()
        {
            int[] source = { 5, 10, 6, 2, 13, 8 };
            Assert.AreEqual(2, source.Min());
        }

        [Test]
        public void SimpleSequenceInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(1, source.Min(x => x.Length));
        }

        [Test]
        public void NullNullableInt32Source()
        {
            int?[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Min());
        }

        [Test]
        public void NullNullableInt32Selector()
        {
            string[] source = { "" };
            Func<string, int?> selector = null;
            Assert.Throws<ArgumentNullException>(() => source.Min(selector));
        }

        [Test]
        public void EmptySequenceNullableInt32NoSelector()
        {
            int?[] source = { };
            Assert.IsNull(source.Min());
        }

        [Test]
        public void EmptySequenceNullableInt32WithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Min(x => (int?)x.Length));
        }

        [Test]
        public void AllNullsSequenceNullableInt32NoSelector()
        {
            int?[] source = { null, null };
            Assert.IsNull(source.Min());
        }

        [Test]
        public void AllNullsSequenceNullableInt32WithSelector()
        {
            string[] source = { "x", "y", "z" };
            Assert.IsNull(source.Min(x => (int?) null));
        }

        [Test]
        public void SimpleSequenceNullableInt32NoSelector()
        {
            int?[] source = { 5, 10, 6, 2, 13, 8 };
            Assert.AreEqual(2, source.Min());
        }

        [Test]
        public void SequenceIncludingNullsNullableInt32NoSelector()
        {
            int?[] source = { 5, null, 10, null, 6, null, 2, 13, 8 };
            Assert.AreEqual(2, source.Min());
        }

        [Test]
        public void SimpleSequenceNullableInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(1, source.Min(x => (int?)x.Length));
        }

        [Test]
        public void SequenceIncludingNullsNullableInt32WithSelector()
        {
            string[] source = { "xyz", "ab", "abcde", "0" };
            Assert.AreEqual(1, source.Min(x => x == "ab" ? null : (int?)x.Length));
        }
        #endregion

        #region Double tests
        // "Not a number" values have some interesting properties...
        [Test]
        public void SimpleSequenceDouble()
        {
            double[] source = { -2.5d, 2.5d, 0d };
            Assert.AreEqual(-2.5d, source.Min());
        }

        [Test]
        public void SequenceContainingBothInfinities()
        {
            double[] source = { 1d, double.PositiveInfinity, double.NegativeInfinity };
            Assert.IsTrue(double.IsNegativeInfinity(source.Min()));
        }

        [Test]
        public void SequenceContainingNaN()
        {
            // Comparisons with NaN are odd, basically...
            // In this case, they're consistent, but look at MaxTest.SequenceContainingNaN for oddities.
            double[] source = { 1d, double.PositiveInfinity, double.NaN, double.NegativeInfinity };
            // Enumerable.Min thinks that NaN is less than negative infinity
            Assert.IsTrue(double.IsNaN(source.Min()));
            // Math.Min thinks that NaN is less than negative infinity
            Assert.IsTrue(double.IsNaN(Math.Min(double.NegativeInfinity, double.NaN)));
        }

        #endregion

        #region Generic tests
        // String implements IEnumerable<char>, which is quite handy. For non-selector
        // methods we use a single string; for selector methods we use a string
        // and a projection to the first character

        [Test]
        public void NullGenericSource()
        {
            IEnumerable<char> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Min());
        }

        [Test]
        public void NullGenericSelector()
        {
            string[] source = { "" };
            Func<string, char> selector = null;
            Assert.Throws<ArgumentNullException>(() => source.Min(selector));
        }

        [Test]
        public void EmptyCharSequenceGenericNoSelector()
        {
            IEnumerable<char> source = "";
            Assert.Throws<InvalidOperationException>(() => source.Min());
        }

        [Test]
        public void EmptyCharSequenceGenericWithSelector()
        {
            string[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Min(x => x[0]));
        }

        [Test]
        public void EmptyStringSequenceGenericNoSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Min());
        }

        [Test]
        public void EmptyStringSequenceGenericWithSelector()
        {
            string[] source = { };
            Assert.IsNull(source.Min());
        }

        [Test]
        public void SimpleSequenceGenericNoSelector()
        {
            string source = "alphabet soup";
            Assert.AreEqual(' ', source.Min());
        }

        [Test]
        public void SimpleSequenceGenericWithSelector()
        {
            string[] source = { "zyx", "ab", "abcde", "0" };
            Assert.AreEqual('0', source.Min(x => x[0]));
        }

        [Test]
        public void SimpleNullableValueTypeSequenceNoSelector()
        {
            char?[] source = { 'z', null, 'a', null, 'a', '0' };
            Assert.AreEqual((char?) '0', source.Min());
        }

        [Test]
        public void AllNullSequenceOfStrings()
        {
            string[] source = { null, null, null };
            Assert.IsNull(source.Min());
        }
        
        [Test]
        public void SimpleSequenceOfStringsIncludingNull()
        {
            // Just for this test, we'll assume that single-letter strings can be ordered#
            // simply in the default culture...
            string[] source = { "A", "D", null, "B", "C" };
            // null values are ignored when finding the minimum
            Assert.AreEqual("A", source.Min());
        }

        [Test]
        public void AllNullSequenceOfNullableGuids()
        {
            Guid?[] source = { null, null, null };
            Assert.IsNull(source.Min());
        }

        [Test]
        public void IncomparableValues()
        {
            MinTest[] source = { new MinTest(), new MinTest() };
            Assert.Throws<ArgumentException>(() => source.Min());
        }
        #endregion
    }
}
