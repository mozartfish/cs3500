// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)
// Version 1.3 - Pranav Rajan
//              Implemented the required methods for a dependency graph as specified by Professor Joe Zachary and Professor Daniel Kopta
// Version 1.4 - Pranav Rajan
//              Staff tests written by the School of Computing revealed that Version 1.3 I submitted on September 14, failed 11 tests out of 46.
//              The tests Version 1.3 failed were due to the implementation of the HasDependents, HasDependees, and RemoveDependency methods
//              Version 1.4 fixes the bugs in Version 1.3 and now passes all of the Staff tests written by the School of Computing
//
// Version 1.5 - Pranav Rajan (10/12/18)
//               Changed some of the variable names to make code more readable. Made some minor changes to white space.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///    dependents("b") = {"d"}
    ///    dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// Authors: Professor Joe Zachary, Professor Daniel Kopta, Pranav Rajan
    /// Date: September 22, 2018
    /// </summary>
    public class DependencyGraph
    {

        /// <summary>
        /// An integer used to keep track of the number of ordered pairs in the DependencyGraph
        /// </summary>
        private int numPairs;

        /// <summary>
        /// A dictionary object with the following specifications:
        /// Key: string that the value set depends on
        /// Value: A set of strings that depend on the key
        /// </summary>
        private Dictionary<String, HashSet<String>> dependentsMap;

        /// <summary>
        /// A dictionary object with the following specifications:
        /// Key: The string that depends on the value set
        /// Value: A set of strings that the key depends on
        /// </summary>
        private Dictionary<String, HashSet<String>> dependeesMap;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            this.numPairs = 0;
            this.dependentsMap = new Dictionary<string, HashSet<string>>();
            this.dependeesMap = new Dictionary<string, HashSet<string>>();
        }

        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return this.numPairs; }
        }

        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get
            {
                if (dependeesMap.ContainsKey(s))
                {
                    return dependeesMap[s].Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (dependentsMap.ContainsKey(s))
            {
                if (dependentsMap[s].Count > 0)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (dependeesMap.ContainsKey(s))
            {
                if (dependeesMap[s].Count > 0)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependentsMap.ContainsKey(s))
            {
                HashSet<String> getDependents = new HashSet<string>(dependentsMap[s]);

                return getDependents;
            }
            else
            {
                return new HashSet<String>();
            }
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (dependeesMap.ContainsKey(s))
            {
                HashSet<String> getDependees = new HashSet<string>(dependeesMap[s]);

                return getDependees;
            }
            else
            {
                return new HashSet<String>();
            }
        }

        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>
        public void AddDependency(string s, string t)
        {
            //CASE 1: the dependency graph does not contain s or t
            if (!(dependeesMap.ContainsKey(t)) && !(dependentsMap.ContainsKey(s)))
            {
                HashSet<String> dependeesSet = new HashSet<string>();

                HashSet<String> dependentsSet = new HashSet<string>();

                dependeesSet.Add(s);
                dependentsSet.Add(t);

                dependentsMap.Add(s, dependentsSet);
                dependeesMap.Add(t, dependeesSet);

                this.numPairs++;

                return;
            }

            //CASE 2: s already exists in the dependentsMap but t is not in the set mapped to s
            else if (!(dependeesMap.ContainsKey(t)) && dependentsMap.ContainsKey(s))
            {
                dependentsMap[s].Add(t);

                HashSet<String> newDependeesSet = new HashSet<String>();

                newDependeesSet.Add(s);

                dependeesMap.Add(t, newDependeesSet);

                dependentsMap[s].Add(t);

                this.numPairs++;

                return;
            }

            //CASE 3: t already exists in the dependeesMap but s is not in the set mapped to t
            else if (!(dependentsMap.ContainsKey(s) && dependeesMap.ContainsKey(t)))
            {
                dependeesMap[t].Add(s);

                HashSet<String> newDependentsSet = new HashSet<String>();

                newDependentsSet.Add(t);

                dependentsMap.Add(s, newDependentsSet);

                dependeesMap[t].Add(s);

                this.numPairs++;

                return;
            }

            //CASE 4: s and t already exist in the dependeesMap and dependentsMap
            else
            {
                if ((dependentsMap.ContainsKey(s) && dependeesMap.ContainsKey(t)))
                {
                    if (dependentsMap[s].Contains(t) && dependeesMap[t].Contains(s))
                    {
                        return;
                    }
                    else
                    {
                        dependentsMap[s].Add(t);
                        dependeesMap[t].Add(s);

                        this.numPairs++;

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s">For the dependentsMap, s represents the string that elements in the set mapped to s depend on
        /// For the dependeesMap, s represents the string that depends on the elements in the set mapped to s
        /// </param>
        /// <param name="t">For the dependentsMap, t represents the string in the set mapped to s that depend on s
        /// For the dependeesMap, t represents the string in the set mapped to s that s depends on
        /// </param>
        public void RemoveDependency(string s, string t)
        {
            if ((dependentsMap.ContainsKey(s) && dependentsMap[s].Contains(t)) && (dependeesMap.ContainsKey(t) && dependeesMap[t].Contains(s)))
            {
                dependentsMap[s].Remove(t);
                dependeesMap[t].Remove(s);

                this.numPairs--;

                return;
            }

            else
            {
                return;
            }
        }

        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            //CASE 1: check if s is not a key in the dependentsMap
            if (!dependentsMap.ContainsKey(s))
            {
                IEnumerable<String> addNewDependentsSet = newDependents;

                foreach (String t in addNewDependentsSet)
                {
                    this.AddDependency(s, t);
                }

                return;
            }

            //CASE 2: s is a key in the dependentsMap
            else
            {
                IEnumerable<String> removeOldDependentsSet = this.GetDependents(s);

                foreach (String r in removeOldDependentsSet)
                {
                    this.RemoveDependency(s, r);
                }

                IEnumerable<String> addNewDependentsSet = newDependents;

                foreach (String t in addNewDependentsSet)
                {
                    this.AddDependency(s, t);
                }

                return;
            }
        }

        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>     
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            //CASE 1: check if s is not a key in the dependeesMap
            if (!dependeesMap.ContainsKey(s))
            {
                IEnumerable<String> addNewDependeesSet = newDependees;

                foreach (String t in addNewDependeesSet)
                {
                    this.AddDependency(t, s);
                }

                return;
            }

            //CASE 2: s is a key in the dependeesMap
            else
            {
                IEnumerable<String> removeOldDependeesSet = this.GetDependees(s);

                foreach (String r in removeOldDependeesSet)
                {
                    this.RemoveDependency(r, s);
                }

                IEnumerable<String> addNewDependeesSet = newDependees;

                foreach (String t in addNewDependeesSet)
                {
                    this.AddDependency(t, s);
                }

                return;
            }
        }
    }
}

