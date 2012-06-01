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
using System.Linq;
using NUnit.Framework;

namespace Edulinq.Tests
{
    [TestFixture]
    public class SingleTest
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Single());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Assert.Throws<ArgumentNullException>(() => source.Single(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Single());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.Single());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Assert.Throws<InvalidOperationException>(() => source.Single());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.Single(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void EarlyOutWithoutPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Assert.Throws<InvalidOperationException>(() => query.Single());
        }

        [Test]
        [Ignore("LINQ to Objects doesn't perform the early out here. https://connect.microsoft.com/VisualStudio/feedback/details/639955")]
        public void EarlyOutWithPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Assert.Throws<InvalidOperationException>(() => query.Single(x => true));
        }
    }
}
