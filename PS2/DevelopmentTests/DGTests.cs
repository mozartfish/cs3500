using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;


namespace DevelopmentTests
{
    /// <summary>
    ///This is a test class for DependencyGraphTest and is intended
    ///to contain all DependencyGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DependencyGraphTest
    {
        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest2()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }

        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void SimpleReplaceTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }

        ///<summary>
        ///It should be possibe to have more than one DG at a time.
        ///</summary>
        [TestMethod()]
        public void StaticTest()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest2()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest3()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.AddDependency("c", "b");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest4()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("c", "b");
            t.RemoveDependency("a", "d");
            t.AddDependency("e", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("e", "b");
            t.RemoveDependency("x", "y");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest5()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Using lots of data
        ///</summary>
        [TestMethod()]
        public void StressTest()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();

            // A bunch of strings to use
            const int SIZE = 200;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 4; j < SIZE; j += 4)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Add some back
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j += 2)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove some more
            for (int i = 0; i < SIZE; i += 2)
            {
                for (int j = i + 3; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }
        }

        /// <summary>
        ///Test that tests the size of dependees
        ///</summary>
        [TestMethod()]
        public void TestSizeOfDependees()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            t.AddDependency("b", "x");
            t.AddDependency("c", "x");
            t.AddDependency("d", "x");
            t.AddDependency("e", "x");
            t.AddDependency("f", "x");
            t.AddDependency("g", "x");
            t.AddDependency("h", "x");
            t.AddDependency("i", "x");
            t.AddDependency("j", "x");
            t.AddDependency("k", "x");
            t.AddDependency("l", "x");
            t.AddDependency("m", "x");
            t.AddDependency("n", "x");
            t.AddDependency("o", "x");
            t.AddDependency("p", "x");
            t.AddDependency("q", "x");
            t.AddDependency("r", "x");
            t.AddDependency("s", "x");
            t.AddDependency("t", "x");
            t.AddDependency("u", "x");
            t.AddDependency("v", "x");
            t.AddDependency("w", "x");
            t.AddDependency("x", "x");
            t.AddDependency("y", "x");
            t.AddDependency("z", "x");
            Assert.AreEqual(26, t["x"]);
        }

        /// <summary>
        ///Test that tests the size of dependees for an empty string
        ///</summary>
        [TestMethod()]
        public void TestStringThatDoesNotHaveAnyDependees()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            t.AddDependency("b", "x");

            Assert.AreEqual(0, t["k"]);
        }

        /// <summary>
        ///Test that tests GetDependents for null
        ///</summary>
        [TestMethod()]
        public void TestNullStringGetDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            Assert.AreEqual(false, t.HasDependees("s"));
        }

        /// <summary>
        ///Test that tests GetDependents for empty string
        ///</summary>
        [TestMethod()]
        public void TestEmptyStringGetDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            Assert.AreEqual(false, t.HasDependees(""));
        }

        /// <summary>
        ///Test that tests HasDependents with a valid key
        ///</summary>
        [TestMethod()]
        public void TestEmptyStringHasDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            Assert.AreEqual(true, t.HasDependents("a"));
        }

        /// <summary>
        ///Test that tests HasDependents with a key not in the set
        ///</summary>
        [TestMethod()]
        public void TestEmptyStringHasNoDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            Assert.AreEqual(false, t.HasDependents("b"));
        }

        /// <summary>
        ///Test that tests HasDependees with a key in the set
        ///</summary>
        [TestMethod()]
        public void TestHasDependees()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            Assert.AreEqual(true, t.HasDependees("x"));
        }


        /// <summary>
        ///Test that tests replaceDependees
        ///</summary>
        [TestMethod()]
        public void ReplaceDependeesNoElements()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            HashSet<String> addDependees = new HashSet<string>();
            addDependees.Add("A");
            addDependees.Add("B");
            addDependees.Add("C");
            addDependees.Add("D");
            addDependees.Add("E");
            addDependees.Add("F");

            t.ReplaceDependees("c", addDependees);
            Assert.AreEqual(6, t["c"]);

            Assert.AreEqual(true, t.HasDependees("x"));
        }

        /// <summary>
        ///Test that tests replaceDependents
        ///</summary>
        [TestMethod()]
        public void ReplaceDependentsNoElements()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            HashSet<String> addDependents = new HashSet<string>();
            addDependents.Add("a");
            addDependents.Add("b");
            addDependents.Add("c");
            addDependents.Add("d");
            addDependents.Add("e");
            addDependents.Add("f");
            t.ReplaceDependents("c", addDependents);
            Assert.AreEqual(7, t.Size);

            Assert.AreEqual(true, t.HasDependees("x"));
        }

        /// <summary>
        ///Test adding empty string
        ///</summary>
        [TestMethod()]
        public void AddingEmptyStrings()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            t.AddDependency("", "g");
            t.AddDependency("", "h");
            t.AddDependency("", "hello");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Test that tests adding null strings
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullElements()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency(null, "x");
            t.AddDependency("p", null);
        }

        /// <summary>
        ///Test that tests stuff 
        ///</summary>
        [TestMethod()]
        public void AddingStuff()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("x", "z");
            t.AddDependency("x", "m");
            t.AddDependency("p", "y");
        }
    }
}
