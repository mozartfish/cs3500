using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace FormulaUnitTest
{
    /// <summary>
    /// A set of unit tests that test the function of the formula object class
    /// </summary>
    [TestClass]
    public class FormulaUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOneTokenRule()
        {
            Formula f1 = new Formula("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidStartingTokenRule()
        {
            Formula f1 = new Formula("+ 3 + 4");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidEndingTokenRule()
        {
            Formula f1 = new Formula("+ 3 + 4 *");
        }


        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestKoptaConstructorTest1()
        {
            Regex match = new Regex("[a-zA-Z][0-9]");
            Formula f1 = new Formula("2x+y3,", N => N.ToUpper(), V => match.IsMatch(V));
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestKoptaConstructorTest2()
        {
            Regex match = new Regex("[a-zA-Z][0-9]");
            Formula f1 = new Formula("2x+y3", N => N.ToUpper(), V => match.IsMatch(V));
        }

        [TestMethod]
        public void TestKoptaConstructorTest3()
        {
            Regex match = new Regex("[a-zA-Z][0-9]");
            Formula f1 = new Formula("x2+y3", N => N.ToUpper(), V => match.IsMatch(V));
        }

        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        [TestMethod]
        public void TestKoptaGetVariablesTest1()
        {
            Formula f1 = new Formula("x+y+z", N => N.ToUpper(), p => true);
            HashSet<String> hello = (HashSet<string>)f1.GetVariables();
            Assert.AreEqual(3, hello.Count);
        }

        [TestMethod]
        public void TestKoptaGetVariablesTest2()
        {
            Formula f1 = new Formula("x+X+z", N => N.ToUpper(), p => true);
            HashSet<String> hello = (HashSet<string>)f1.GetVariables();
            Assert.AreEqual(2, hello.Count);
            //Assert.AreEqual(2.0, f1.Evaluate(x => 0));
        }

        [TestMethod]
        public void TestKoptaGetVariablesTest3()
        {
            Formula f1 = new Formula("x+X+z");
            HashSet<String> hello = (HashSet<string>)f1.GetVariables();
            Assert.AreEqual(3, hello.Count);
        }

        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        [TestMethod]
        public void TestKoptaToStringTest1()
        {
            Formula f1 = new Formula("x+y", N => N.ToUpper(), s => true);
            String hello = f1.ToString();
            Assert.AreEqual("X+Y", hello);
        }

        [TestMethod]
        public void TestKoptaToStringTest2()
        {
            Formula f1 = new Formula("x+Y");
            String hello = f1.ToString();
            Assert.AreEqual("x+Y", hello);
        }

        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        [TestMethod]
        public void TestKoptaEqualsMethodTest1()
        {
            Assert.IsTrue(new Formula("x1+y2", N => N.ToUpper(), s => true).Equals(new Formula("X1  +  Y2")));
        }

        [TestMethod]
        public void TestKoptaEqualsMethodTest2()
        {
            Assert.IsFalse(new Formula("x1+y2").Equals(new Formula("X1+Y2")));
        }

        [TestMethod]
        public void TestKoptaEqualsMethodTest3()
        {
            Assert.IsFalse(new Formula("x1+y2").Equals(new Formula("y2+x1")));
        }

        [TestMethod]
        public void TestKoptaEqualsMethodTest4()
        {
            Assert.IsTrue(new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")));
        }

        /// <summary>
        /// PS1 STAFF TESTS
        /// </summary>
        [TestMethod()]
        public void TestSingleNumber()
        {
            Formula f1 = new Formula("5");
            Assert.AreEqual(5.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestSingleVariable()
        {
            Formula f1 = new Formula("X5");
            Assert.AreEqual(13.0, f1.Evaluate(s => 13.0));
        }

        [TestMethod()]
        public void TestAddition()
        {
            Formula f1 = new Formula("5 + 3");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestSubtraction()
        {
            Formula f1 = new Formula("18-10");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestMultiplication()
        {
            Formula f1 = new Formula("2 * 4");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestDivision()
        {
            Formula f1 = new Formula("16 / 2");
            Assert.AreEqual(8.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestArithmeticWithVariable()
        {
            Formula f1 = new Formula("2 + X1");
            Assert.AreEqual(6.0, f1.Evaluate(s => 4));
        }

        [TestMethod()]
        public void TestUnknownVariable()
        {
            Formula f1 = new Formula("2 + X1");
            Assert.IsTrue(f1.Evaluate(x => throw new ArgumentException()) is FormulaError);
        }

        [TestMethod()]
        public void TestLeftToRight()
        {
            Formula f1 = new Formula("2*6+3");
            Assert.AreEqual(15.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestOrderOperations()
        {
            Formula f1 = new Formula("2 + 6 * 3");
            Assert.AreEqual(20.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestParenthesesTimes()
        {
            Formula f1 = new Formula("2 + 6 * 3");
            Assert.AreEqual(20.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestTimesParentheses()
        {
            Formula f1 = new Formula("2 * (3 + 5)");
            Assert.AreEqual(16.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestPlusParentheses()
        {
            Formula f1 = new Formula("2+(3+5)");
            Assert.AreEqual(10.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestPlusComplex()
        {
            Formula f1 = new Formula("2+(3+5*9)");
            Assert.AreEqual(50.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestComplexTimesParentheses()
        {
            Formula f1 = new Formula("2+3*(3+5)");
            Assert.AreEqual(26.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        public void TestComplexAndParentheses()
        {
            Formula f1 = new Formula("2+3*5+(3+4*8)*5+2");
            Assert.AreEqual(194.0, f1.Evaluate(s => 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSingleOperator()
        {
            Formula f1 = new Formula("+");
            f1.Evaluate(s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraOperator()
        {
            Formula f1 = new Formula("2+5+");
            f1.Evaluate(s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraParentheses()
        {
            Formula f1 = new Formula("2+5*7)");
            f1.Evaluate(s => 0);
        }

        [TestMethod()]
        public void TestInvalidVariable()
        {
            Formula f1 = new Formula("xx");
            Assert.AreEqual(0.0, f1.Evaluate(x => 0));
        }

        [TestMethod()]
        public void TestPlusInvalidVariable()
        {
            Formula f1 = new Formula("5 + xx");

            Assert.AreEqual(5.0, f1.Evaluate(x => 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParensNoOperator()
        {
            Formula f1 = new Formula("5+7+(5)8");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmpty()
        {
            Formula f1 = new Formula("");
            f1.Evaluate(s => 0);
        }

        [TestMethod()]
        public void TestComplexMultiVar()
        {
            Formula f1 = new Formula("y1*3-8/2+4*(8-9*2)/14*x7");
            Assert.AreEqual(5.14285714285714, (double)f1.Evaluate(s => (s == "x7") ? 1 : 4), 1e-9);
        }

        [TestMethod()]
        public void TestComplexNestedParensRight()
        {
            Formula f1 = new Formula("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6.0, f1.Evaluate(s => 1));
        }

        [TestMethod()]
        public void TestComplexNestedParensLeft()
        {
            Formula f1 = new Formula("((((x1+x2)+x3)+x4)+x5)+x6");
            Assert.AreEqual(12.0, f1.Evaluate(s => 2));
        }

        [TestMethod()]
        public void TestRepeatedVar()
        {
            Formula f1 = new Formula("a4-a4*a4/a4");
            Assert.AreEqual(0.0, f1.Evaluate(s => 3));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestCheckValidIncorrectStartingToken()
        {
            Formula f1 = new Formula("+ 4 + 3");
            Assert.AreEqual(7.0, f1.Evaluate(x => 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestCheckValidIncorrectEndingToken()
        {
            Formula f1 = new Formula("(15 * 3) +");
            Assert.AreEqual(45.0, f1.Evaluate(x => 0));
        }

        [TestMethod()]
        public void TestGetReason()
        {
            FormulaError f1 = new FormulaError("This is an error");
            Assert.AreEqual("This is an error", f1.Reason);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestWrongStartingTokenParentheses()
        {
            Formula f1 = new Formula("(#");
            Assert.AreEqual(0.0, f1.Evaluate(s => 3));
        }

        [TestMethod()]
        public void BreakTheEqualsMethod()
        {
            Assert.IsFalse(new Formula("2.0 * 3.0").Equals(new Formula("2.000 + 3.00")));
        }

        [TestMethod()]
        public void TestTheEqualsEqualsMethod()
        {
            Assert.IsFalse(new Formula("2.0 * 3.0") == (new Formula("2.000 + 3.00")));
        }

        [TestMethod()]
        public void TestTheEqualsEqualsThatWorksMethod()
        {
            Assert.IsTrue(new Formula("2.0 * 3.0") == (new Formula("2.000 * 3.00")));
        }

        [TestMethod()]
        public void NotEqualsMethod()
        {
            Assert.IsTrue(new Formula("2.0 * 3.0") != (new Formula("2.000 + 3.00")));
        }

        [TestMethod()]
        public void NotEqualsThatDoesNotMethod()
        {
            Assert.IsFalse(new Formula("2.0 * 3.0") != (new Formula("2.000 * 3.00")));
        }

        [TestMethod()]
        public void TestTheGetHashCode()
        {
            Formula f1 = new Formula("1 + 2 * 3");
            Formula f2 = new Formula("1 + 2 * 3");
            Assert.IsTrue(f1.GetHashCode() == f2.GetHashCode());
        }

        [TestMethod()]
        public void TestTheEqualsMethod()
        {
            Formula f1 = null;
            Formula f2 = null;
            Assert.IsTrue(f1 == f2);
        }

        [TestMethod()]
        public void TestTheEqualsNullMethod()
        {
            Formula f1 = new Formula("3 * 4 + 1");
            Formula f2 = null;
            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod()]
        public void TestThEqualsMethodDifferntTypeMethod()
        {
            Formula f1 = new Formula("3 * 4 + 1");
            String f2 = "bonsoir";
            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod()]
        public void TestThEqualsMethodDifferentLengthFormulasMethod()
        {
            Formula f1 = new Formula("3*4+1");
            Formula f2 = new Formula("4*8+9-10+3*32");
            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod()]
        public void TestTheEqualsMethodSameSizeFormulasDifferentNumbersMethod()
        {
            Formula f1 = new Formula("3*4+1");
            Formula f2 = new Formula("4*3+2");
            Assert.IsFalse(f1.Equals(f2));
        }


        [TestMethod()]
        public void TestTheEqualsEqualsMethodWhereOneFormulaIsNullMethod()
        {
            Formula f1 = new Formula("3*4+1");
            Formula f2 = null;
            Assert.IsFalse(f1 == f2);
        }


        [TestMethod()]
        public void TestTheEqualsEqualsMethodWhereOneFormulaIsNullMethodTest2()
        {
            Formula f1 = null;
            Formula f2 = new Formula(" 4 + 3 * 7 - 1 + 19 -1");
            Assert.IsFalse(f1 == f2);
        }

        [TestMethod()]
        public void TestTheNotEqualsMethodWhereOneFormulaIsNull()
        {
            Formula f1 = null;
            Formula f2 = new Formula(" 4 + 3 * 7 - 1 + 19 -1");
            Assert.IsTrue(f1 != f2);
        }

        [TestMethod()]
        public void TestTheNotEqualsMethodWhereOneFormulaIsNullTest2()
        {
            Formula f1 = new Formula(" 4 + 3 * 7 - 1 + 19 -1");
            Formula f2 = null;
            Assert.IsTrue(f1 != f2);
        }

        [TestMethod()]
        public void TestTheNotEqualsMethodWhereBothFormulasAreNull()
        {
            Formula f1 = null;
            Formula f2 = null;
            Assert.IsFalse(f1 != f2);
        }

        [TestMethod()]
        public void TestDivisonByZeroTest1()
        {
            Formula f1 = new Formula("3 / 0");
            Assert.IsTrue(f1.Evaluate(x => 0) is FormulaError);
        }

        [TestMethod()]
        public void TestDivisonByZeroTest2()
        {
            Formula f1 = new Formula(" 4 + 16 * 2 / (3 - 3)");
            Assert.IsTrue(f1.Evaluate(x => 0) is FormulaError);
        }

        [TestMethod()]
        public void TestDivisonByZeroTest3()
        {
            Formula f1 = new Formula("4 * 3/ a1");
            Assert.IsTrue(f1.Evaluate(x => 0) is FormulaError);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRule()
        {
            Formula f1 = new Formula("( * 4 + 8 - 15)");
            Assert.IsTrue(f1.Evaluate(x => 0) is FormulaError);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRuleTest2()
        {
            Formula f1 = new Formula("(4 + * 8 - 15)");
            Assert.IsTrue(f1.Evaluate(x => 0) is FormulaError);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleTest()
        {
            Formula f1 = new Formula("(4 + 8)4");
            Assert.IsTrue(f1.Evaluate(x => 0) is FormulaError);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleTest2()
        {
            Formula f1 = new Formula("(4 + a1 8)");
            Assert.IsTrue(f1.Evaluate(x => 10) is FormulaError);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleTest3()
        {
            Formula f1 = new Formula("(4 + 8 a1)");
            Assert.IsTrue(f1.Evaluate(x => 10) is FormulaError);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestRightParenthesesRule()
        {
            Formula f1 = new Formula("(4) + 3))");
            Assert.IsTrue(f1.Evaluate(x => 10) is FormulaError);
        }

        [TestMethod()]
        public void TestTheThing()
        {
            Formula f1 = new Formula("2 + p1");
            Assert.AreEqual(12.0, (f1.Evaluate(x => 10)));
        }
    }

}