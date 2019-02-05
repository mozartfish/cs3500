using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FormulaEvaluator
{
    [TestClass]
    public class FormulaEvaluatorTests
    {
        [TestMethod()]
        public void TestingIntegerTest1()
        {
            Assert.AreEqual(27, Evaluator.Evaluate("(2 + 3) * 5 + 2", s => 0));
        }

        [TestMethod()]
        public void TestingIntegerTest2()
        {
            Assert.AreEqual(30, Evaluator.Evaluate("5 * 3 * 2", s => 0));
        }

        [TestMethod()]
        public void TestingIntegerTest3()
        {
            Assert.AreEqual(15, Evaluator.Evaluate(" 72 / 9 + 7", s => 0));
        }

        [TestMethod()]
        public void TestingIntegerTest4()
        {
            Assert.AreEqual(37, Evaluator.Evaluate("2 + 7 * 5", s => 0));
        }

        [TestMethod()]
        public void TestingIntegerTest5()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("9 + 8 - 7", s => 0));
        }

        [TestMethod()]
        public void TestingIntegerTest6()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("20 / (4 - (10 - 8))", s => 0));
        }

        [TestMethod()]
        public void TestingIntegerTest7()
        {
            Assert.AreEqual(1, Evaluator.Evaluate("(5 + 16) / 7 - 2", s => 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingEmptyString()
        {
            Evaluator.Evaluate("", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingWhiteSpace()
        {
            Evaluator.Evaluate("                                           ", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void IntegerDivisionByZero()
        {
            Evaluator.Evaluate("3 / 0", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void VariableDivisionByZero()
        {
            Evaluator.Evaluate("A3 / 0", s => 2);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AdditionWithMoreThanOneAdditionOperator()
        {
            Evaluator.Evaluate("5 + 2 +", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void SubtractionWithMoreThanOneSubtractionOperator()
        {
            Evaluator.Evaluate("5 - 42 -", s => 0);
        }

        [TestMethod()]
        public void TestingExpressionWithVariables()
        {
            Assert.AreEqual(47, Evaluator.Evaluate("(2 + A6) * 5 + 2", s => 7));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void VariableDivideByZero()
        {
            Evaluator.Evaluate("3 / A1", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingIncorrectVariableNameTest1()
        {
            Evaluator.Evaluate("(2 + 3) + A            15", s => 35);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingIncorrectVariableNameTest2()
        {
            Evaluator.Evaluate("(2 + 3) + &", s => 42);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingIncorrectVariableNameTest3()
        {
            Evaluator.Evaluate("(2 + 3) + A_320", s => 21);
        }

        [TestMethod()]
        
        public void TestingIncorrectVariableNameTest4()
        {
            
            Assert.AreEqual(12345683, Evaluator.Evaluate("(2 + 3) + 12345678", s => 45));
        }

        [TestMethod()]
        public void TestingCorrectVariablesTest1()
        {
            Assert.AreEqual(26, Evaluator.Evaluate("(2 + 3) + HELLO32", s => 21));
        }

        [TestMethod()]
        public void TestingCorrectVariablesTest2()
        {
            Assert.AreEqual(24, Evaluator.Evaluate("(2 + 3) + PRANAV320", PRANAV320 => 19));
        }

        [TestMethod()]
        public void TestingCorrectVariablesTest3()
        {
            Assert.AreEqual(24, Evaluator.Evaluate("(2 + 3) + PRANAV320", s => 19));
        }

        [TestMethod()]
        public void TestingCorrectVariablesTest4()
        {
            Assert.AreEqual(22, Evaluator.Evaluate("(2 + 3) +  ISHAAN32", s => 17));
        }

        [TestMethod()]
        public void TestingCorrectVariablesTest5()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("(2 + 3) + Supercalifragilisticexpialidocious42", s => 3));
        }

        [TestMethod()]
        public void TestingCorrectVariablesCamelCase()
        {
            Assert.AreEqual(30, Evaluator.Evaluate("(2 + 3) + hElLo32", s => 25));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMathematicalOperatorPowerTest()
        {
            Evaluator.Evaluate("(2 + 3)^7", s => 21);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMathematicalOperatorModulo()
        {
           Evaluator.Evaluate("(2 + 3) % 5", s => 19);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidMathematicalOperatorBangOperator()
        {
           Evaluator.Evaluate("(2 + 3) ! hello32", s => 19);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingDelegateThatHasNoValueAssociatedWithIt()
        {
           Evaluator.Evaluate("(2 + 3) * cs1410", s => throw new ArgumentException());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingExtraClosingParenthese()
        {
            Evaluator.Evaluate("(2 + 3) + 3 (", s => 1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingExtraOpeningParenthese()
        {
            Evaluator.Evaluate("(2 + 3) + 5 (", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingOnlyParentheses()
        {
            Evaluator.Evaluate("()", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingTwoOperators()
        {
            Evaluator.Evaluate("++", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingIncorrectNumberOfParentheses()
        {
            Evaluator.Evaluate("(2 + 3) + 5 (", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestingValuesOfTypeDouble()
        {
            Evaluator.Evaluate("2.0 + 5.0", s => 10);
        }

        [TestMethod()]
        public void TestingMultipleParentheses()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("(((2 + 3) + 5))", s => 10));
        }

        [TestMethod()]
        public void TestingNegativeOutput()
        {
            Assert.AreEqual(-1, Evaluator.Evaluate("2 - 3", s => 10));
        }

        [TestMethod()]
        public void TestTheDivision()
        {
            Assert.AreEqual(2, Evaluator.Evaluate("36 / 9 / 2 / 1", s => 10));
        }

        [TestMethod()]
        public void testMultipleOperatorsThatAreWithinParentheses()
        {
            Assert.AreEqual(42, Evaluator.Evaluate("3 + (2 * 2 * 2 * 2) + 4 + (10 + 9)", s => 10));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CrazyNumberOfParentheses()
        {
            Evaluator.Evaluate("(((((((((((((((((())))))))))))))))))", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidOperator()
        {
            Evaluator.Evaluate("7 % 2", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongFinalOperatorOnTheOperatorStack()
        {
            Evaluator.Evaluate("3 * 2 * 4 *", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void FewerThanTwoOperands()
        {
            Evaluator.Evaluate("3 +", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TheValueStackIsEmpty()
        {
            Evaluator.Evaluate("* 2", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TheValueStackIsEmptyVariableCase()
        {
            Evaluator.Evaluate("3 * s *", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void LessThanTwoOperandsOnTheValueStack()
        {
            Evaluator.Evaluate("((3 + 4) +)", s => 10);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidMathOperator()
        {
            Evaluator.MathOperation(3, 2, "%");
        }




        /// <summary>
        /// PS1 Staff Tests
        /// </summary>
        [TestMethod()]
        public void TestSingleNumber()
        {
            Assert.AreEqual(5, Evaluator.Evaluate("5", s => 0));
        }

        [TestMethod()]
        public void TestSingleVariable()
        {
            Assert.AreEqual(13, Evaluator.Evaluate("X5", s => 13));
        }

        [TestMethod()]
        public void TestAddition()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("5+3", s => 0));
        }

        [TestMethod()]
        public void TestSubtraction()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("18-10", s => 0));
        }

        [TestMethod()]
        public void TestMultiplication()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("2*4", s => 0));
        }

        [TestMethod()]
        public void TestDivision()
        {
            Assert.AreEqual(8, Evaluator.Evaluate("16/2", s => 0));
        }

        [TestMethod()]
        public void TestArithmeticWithVariable()
        {
            Assert.AreEqual(6, Evaluator.Evaluate("2+X1", s => 4));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUnknownVariable()
        {
            Evaluator.Evaluate("2+X1", s => { throw new ArgumentException("Unknown variable"); });
        }

        [TestMethod()]
        public void TestLeftToRight()
        {
            Assert.AreEqual(15, Evaluator.Evaluate("2*6+3", s => 0));
        }

        [TestMethod()]
        public void TestOrderOperations()
        {
            Assert.AreEqual(20, Evaluator.Evaluate("2+6*3", s => 0));
        }

        [TestMethod()]
        public void TestParenthesesTimes()
        {
            Assert.AreEqual(24, Evaluator.Evaluate("(2+6)*3", s => 0));
        }

        [TestMethod()]
        public void TestTimesParentheses()
        {
            Assert.AreEqual(16, Evaluator.Evaluate("2*(3+5)", s => 0));
        }

        [TestMethod()]
        public void TestPlusParentheses()
        {
            Assert.AreEqual(10, Evaluator.Evaluate("2+(3+5)", s => 0));
        }

        [TestMethod()]
        public void TestPlusComplex()
        {
            Assert.AreEqual(50, Evaluator.Evaluate("2+(3+5*9)", s => 0));
        }

        [TestMethod()]
        public void TestComplexTimesParentheses()
        {
            Assert.AreEqual(26, Evaluator.Evaluate("2+3*(3+5)", s => 0));
        }

        [TestMethod()]
        public void TestComplexAndParentheses()
        {
            Assert.AreEqual(194, Evaluator.Evaluate("2+3*5+(3+4*8)*5+2", s => 0));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDivideByZero()
        {
            Evaluator.Evaluate("5/0", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSingleOperator()
        {
            Evaluator.Evaluate("+", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExtraOperator()
        {
            Evaluator.Evaluate("2+5+", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExtraParentheses()
        {
            Evaluator.Evaluate("2+5*7)", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidVariable()
        {
            Evaluator.Evaluate("xx", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPlusInvalidVariable()
        {
            Evaluator.Evaluate("5+xx", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParensNoOperator()
        {
            Evaluator.Evaluate("5+7+(5)8", s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmpty()
        {
            Evaluator.Evaluate("", s => 0);
        }

        [TestMethod()]
        public void TestComplexMultiVar()
        {
            Assert.AreEqual(6, Evaluator.Evaluate("y1*3-8/2+4*(8-9*2)/14*x7", s => (s == "x7") ? 1 : 4));
        }

        [TestMethod()]
        public void TestComplexNestedParensRight()
        {
            Assert.AreEqual(6, Evaluator.Evaluate("x1+(x2+(x3+(x4+(x5+x6))))", s => 1));
        }

        [TestMethod()]
        public void TestComplexNestedParensLeft()
        {
            Assert.AreEqual(12, Evaluator.Evaluate("((((x1+x2)+x3)+x4)+x5)+x6", s => 2));
        }

        [TestMethod()]
        public void TestRepeatedVar()
        {
            Assert.AreEqual(0, Evaluator.Evaluate("a4-a4*a4/a4", s => 3));
        }
    }
}
