﻿////////////////////////////////////////////////////////////////////////////
//
// Copyright 2016 Realm Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
////////////////////////////////////////////////////////////////////////////

using NUnit.Framework;
using System.Linq;
using Realms;

namespace IntegrationTests
{
    [TestFixture, Preserve(AllMembers = true)]
    public class LINQvariableTests : PeopleTestsBase
    {
        // see comment on base method why this isn't decorated with [SetUp]
        public override void Setup ()
        {
            base.Setup ();
            MakeThreePeople ();
        }

        [TestCase("Peter", 1)]
        [TestCase("Zach", 0)]
        [TestCase("John", 2)]
        public void FirstNamesEqual(string matchName, int expectFound)
        {
            var c0 = _realm.All<Person>().Count(p => p.FirstName == matchName);
            Assert.That(c0, Is.EqualTo(expectFound));

            var c1 = _realm.All<Person>().Count(p => p.FirstName.StartsWith(matchName));
            Assert.That(c1, Is.EqualTo(expectFound));

            var c2 = _realm.All<Person>().Count(p => p.FirstName.Contains(matchName));
            Assert.That(c2, Is.EqualTo(expectFound));

            var c3 = _realm.All<Person>().Count(p => p.FirstName.EndsWith(matchName));
            Assert.That(c3, Is.EqualTo(expectFound));
        }

        [TestCase("P", 1)]
        [TestCase("Z", 0)]
        [TestCase("J", 2)]
        public void SingleLetterStartSearch(string matchName, int expectFound)
        {
            var c1 = _realm.All<Person>().Count(p => p.FirstName.StartsWith(matchName));
            Assert.That(c1, Is.EqualTo(expectFound));

            var c2 = _realm.All<Person>().Count(p => p.FirstName.Contains(matchName));
            Assert.That(c2, Is.EqualTo(expectFound));
        }

        [TestCase("r", 1)]
        [TestCase("z", 0)]
        [TestCase("n", 2)]
        public void SingleLetterEndSearch(string matchName, int expectFound)
        {
            var c2 = _realm.All<Person>().Count(p => p.FirstName.Contains(matchName));
            Assert.That(c2, Is.EqualTo(expectFound));

            var c3 = _realm.All<Person>().Count(p => p.FirstName.EndsWith(matchName));
            Assert.That(c3, Is.EqualTo(expectFound));
        }

        [TestCase(0.0f, 200.0f, 2)]
        [TestCase(0.0f, 90.0f, 1)]
        [TestCase(0.0f, 30.0f, 0)]
        [TestCase(-1.0f, 0.0f, 1)]
        public void ScoreWithinRange(float minScore, float maxScore, int expectFound)
        {
            var c0 = _realm.All<Person>().Count(p => p.Score > minScore && p.Score <= maxScore);
            Assert.That(c0, Is.EqualTo(expectFound));
        }
    }
}

