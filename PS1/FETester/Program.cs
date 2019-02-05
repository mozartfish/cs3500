using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaEvaluator; // You need to add a reference to your FormulaEvaluator.

namespace FETester
{

    /// <summary>
    /// A program that tests the function of the FormulaEvaluator class library
    /// Some of the tests are written by Professor Daniel Kopta
    /// Authors: Pranav Rajan and Professor Daniel Kopta
    ///// Date: August 30, 2018 - September 7, 2018
    /// </summary>
    class FormulaEvaluatorTester
    {

        public static int NullLookup(string s)
        {
            throw new Exception();
        }

        public static int SimpleLookup(string s)
        {
            if (s == "A4")
                return 7;
            else
                return 0;
        }


        static void Main(string[] args)
        {

            FakeSpreadsheet sheet = new FakeSpreadsheet();

            //Create some variables that satisfy the naming convention
            sheet.AddVariable("A1", 0);
            sheet.AddVariable("C5", 7);
            sheet.AddVariable("A4", 2);
            sheet.AddVariable("A6", 7);
            sheet.AddVariable("A14", 28);

            //Create some variables that do not satisfy the naming convention
            sheet.AddVariable("A_320", 32);
            sheet.AddVariable("A            15", 10);
            sheet.AddVariable("12345678", 17);
            sheet.AddVariable("&*&*", 22);


            //Create some variables that test for capitalization
            sheet.AddVariable("HELLO32", 21);
            sheet.AddVariable("PRANAV320", 19);
            sheet.AddVariable("ISHAAN32", 17);
            sheet.AddVariable("hElLo32", 25);
            sheet.AddVariable("mozartfish320", 10);

            //Create some variables with very long strings
            sheet.AddVariable("Supercalifragilisticexpialidocious42", 3);

















































            //Various Edge Cases that Professor Kopta and CS 3500 Proposed on September 4, 2018






            //Test 35-Test expression that works followed by one that doesn't work
            //Expected Output: 10, There are two few values on the value stack
            //try
            //{
            //    Console.WriteLine(Evaluator.Evaluate("(2 + 3) + 5", sheet.LookupVariable));
            //    Console.WriteLine(Evaluator.Evaluate("1 + 2 + 3 + )", sheet.LookupVariable));

            //}

            //catch (ArgumentException e)
            //{
            //    Console.WriteLine("exception caught: " + e);
            //}





            //Test 36 - Testing Multiple Parentheses
            //Expected Output: 10
            try
            {
                Console.WriteLine(Evaluator.Evaluate("(((2 + 3) + 5))", sheet.LookupVariable));
                

            }

            catch (ArgumentException e)
            {
                Console.WriteLine("exception caught: " + e);
            }

            // Keep the console window open.
            Console.Read();
        }
    }

    /// <summary>
    /// A class that creates a fake spreadsheet to test the functionality of the FormulaEvaluator when 
    /// evaluating expressions that contain variables.
    /// Author: Professor Daniel Kopta
    /// Date: August 30, 2018
    /// </summary>
    public class FakeSpreadsheet
    {

        // For variables, we want mapping between string -> int
        // e.g.
        //  "a1"   -> 50
        //  "ZZ14" -> 9
        // A Dictionary provides such a mapping.
        private Dictionary<String, int> vars;


        /// <summary>
        /// Default constructor.
        /// </summary>
        public FakeSpreadsheet()
        {
            vars = new Dictionary<string, int>();
        }


        /// <summary>
        /// Add a variable to the tracker.
        /// </summary>
        /// <param name="var">The name of the variable</param>
        /// <param name="value">The value of the variable</param>
        public void AddVariable(string var, int value)
        {
            vars.Add(var, value);
        }


        /// <summary>
        /// Lookup a variable in the tracker. Throw ArgumentException if the variable doesn't exist.
        /// </summary>
        /// <param name="v">The variable to lookup.</param>
        /// <returns>Returns the value of the variable.</returns>
        public int LookupVariable(String v)
        {
            if (vars.ContainsKey(v))
                return vars[v];

            throw new ArgumentException();
        }
    }
}
