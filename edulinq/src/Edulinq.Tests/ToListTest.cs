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
using Edulinq.TestSupport;
using NUnit.Framework;

namespace Edulinq.Tests
{
    [TestFixture]
    public class ToListTest
    {
        [Test]
        public void ResultIsIndependentOfSource()
        {
            List<string> source = new List<string> { "xyz", "abc" };
            List<string> result = source.ToList();
            result.AssertSequenceEqual("xyz", "abc");
            Assert.AreNotSame(source, result);

            source.Add("extra element");
            // The extra element hasn't been added to the result
            Assert.AreNotEqual(source.Count, result.Count);
        }

        [Test]
        public void SequenceIsEvaluatedEagerly()
        {
            int[] numbers = { 5, 3, 0 };
            var query = numbers.Select(x => 10 / x);
            Assert.Throws<DivideByZeroException>(() => query.ToList());
        }
        
        [Test]
        public void NullSource()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.ToList());
        }

        [Test]
        public void ConversionOfLazilyEvaluatedSequence()
        {
            var range = Enumerable.Range(3, 3);
            var query = range.Select(x => x * 2);
            var list = query.ToList();
            list.AssertSequenceEqual(6, 8, 10);
        }

        [Test]
        public void ICollectionOptimization()
        {
            var source = new NonEnumerableCollection<string> { "hello", "there" };
            // If ToList just iterated over the list, this would throw
            var list = source.ToList();
            list.AssertSequenceEqual("hello", "there");
        }
    }
}
