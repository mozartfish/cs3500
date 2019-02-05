using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormulaEvaluator
{
    /// <summary>
    /// A class library that evaluates mathematical expressions.
    /// Author: Pranav Rajan
    /// Date: September 7, 2018
    /// </summary>
    public static class Evaluator
    {
        /// <summary>
        /// The delegate object. The delegate evaluates the variable the user wants to use
        /// in the mathematical expression for evaluation in the Evaluate method below. If the variable does not contain a value,
        /// the delegate will throw an exception. Otherwise, the delegate returns an integer.
        ///</summary>
        /// <param name="v">The variable that the user looks up.</param>
        /// <returns>The integer </returns>
        public delegate int Lookup(String v);

        /// <summary>
        /// This method takes a mathematical expression as a string and a delegate object which looks up the
        /// value, if there is one, stored in the variable the user wants to use in the mathematical expressions.
        /// The method then evaluates the mathematical expression using the delegate object and returns an integer
        /// that represents the result of evaluating the mathematical expression that was given as input by the user.
        /// If the method is unable to evaluate the mathematical expression, it will throw an exception that details why it
        /// was unable to evaluate the expression.
        /// </summary>
        /// <param name="exp">A string that represents the mathematical expression the user wishes to evaluate.</param>
        /// <param name="variableEvaluator">A delegate object that looks up the integer located in the variable for use in the mathematical expression.</param>
        /// <returns>An integer that represents the result of evaluating the expression the user gave as input.</returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {
            Stack<int> valueStack = new Stack<int>();

            Stack<string> operatorStack = new Stack<string>();

            //create a string that represents the pattern the variable name should match
            String pattern = @"^[a-zA-Z]+[0-9]+$";

            Regex rgx = new Regex(pattern);

            string[] substrings = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            for (int i = 0; i < substrings.Length; i++)
            {
                String expToken = substrings[i].Trim();

                if (expToken.Length == 0)
                {
                    continue;
                }

                //CASE 1: expToken IS AN INTEGER
                if (Int32.TryParse(expToken, out int valueData))
                {
                    if ((operatorStack.IsOnTop("*")) || operatorStack.IsOnTop("/"))
                    {
                        if (valueStack.IsEmpty())
                        {
                            //ERROR: The value stack is empty
                            throw new ArgumentException("Incorrect expression entered");
                        }

                        int topOfValueStack = valueStack.Pop();

                        String multiplyDivide = operatorStack.Pop();

                        valueStack.Push(MathOperation(topOfValueStack, valueData, multiplyDivide));
                    }
                    else
                    {
                        valueStack.Push(valueData);
                    }
                }

                //CASE 2: expToken IS A VARIABLE
                else if (rgx.IsMatch(expToken))
                {
                    //Create a try-catch block to determine if the delegate has a value and handle any exceptions that occur
                    try
                    {
                        int variableData = variableEvaluator(expToken);

                        if ((operatorStack.IsOnTop("*")) || operatorStack.IsOnTop("/"))
                        {
                            if (valueStack.IsEmpty())
                            {
                                //ERROR: The value stack is empty
                                throw new ArgumentException("Incorrect expression entered");
                            }

                            int topOfValueStack = valueStack.Pop();

                            String multiplyDivide = operatorStack.Pop();

                            valueStack.Push(MathOperation(topOfValueStack, variableData, multiplyDivide));
                        }
                        else
                        {
                            valueStack.Push(variableData);
                        }
                    }
                    catch (ArgumentException e)
                    {
                        //ERROR:If looking up the variable reveals it has no value (the delegate throws)
                        throw new ArgumentException("There is no value associated with this variable");
                    }
                }

                //CASE 3: expToken IS A + OPERATOR OR - OPERATOR
                else if (expToken.Equals("+") || expToken.Equals("-"))
                {
                    if (operatorStack.IsOnTop("+") || operatorStack.IsOnTop("-"))
                    {
                        if (valueStack.Count < 2)
                        {
                            //ERROR: The value stack contains fewer than 2 values if trying to pop it
                            throw new ArgumentException("There are fewer than two operands");
                        }

                        int topOfValueStack = valueStack.Pop();
                        int bottomOfValueStack = valueStack.Pop();

                        String addSubtract = operatorStack.Pop();

                        valueStack.Push(MathOperation(bottomOfValueStack, topOfValueStack, addSubtract));
                    }

                    operatorStack.Push(expToken);
                }

                //CASE 4: expToken IS A * OPERATOR OR A / OPERATOR
                else if (expToken.Equals("*") || expToken.Equals("/"))
                {
                    operatorStack.Push(expToken);
                }

                //CASE 5: expToken IS A LEFT PARENTHESIS (
                else if (expToken.Equals("("))
                {
                    operatorStack.Push(expToken);
                }

                //CASE 6: expToken IS A RIGHT PARENTHESIS )
                else if (expToken.Equals(")"))
                {
                    if (operatorStack.IsOnTop("+") || operatorStack.IsOnTop("-"))
                    {
                        if (valueStack.Count < 2)
                        {
                            //ERROR: The value stack contains fewer than 2 values if trying to pop it
                            throw new ArgumentException("There are fewer than two operands");
                        }

                        int topOfValueStack = valueStack.Pop();
                        int bottomOfValueStack = valueStack.Pop();

                        String addSubtract = operatorStack.Pop();

                        valueStack.Push(MathOperation(bottomOfValueStack, topOfValueStack, addSubtract));
                    }

                    if (!operatorStack.IsOnTop("("))
                    {
                        //ERROR: A ( isn't found where expected
                        throw new ArgumentException("There are an incorrect number of parentheses");
                    }

                    operatorStack.Pop();

                    if (operatorStack.IsOnTop("*") || operatorStack.IsOnTop("/"))
                    {
                        if (valueStack.Count < 2)
                        {
                            //ERROR: The value stack contains fewer than 2 values if trying to pop it during the last step
                            throw new ArgumentException("There are fewer than two operands");
                        }

                        int topOfValueStack = valueStack.Pop();
                        int bottomOfValueStack = valueStack.Pop();

                        String multiplyDivide = operatorStack.Pop();

                        valueStack.Push(MathOperation(bottomOfValueStack, topOfValueStack, multiplyDivide));
                    }
                }
                else
                {
                    //ERROR: If no token in the substring matches then the token is invalid
                    throw new ArgumentException("Incorrect expression entered");
                }
            }

            //AT THIS POINT WE ASSUME THAT LAST TOKEN HAS BEEN PROCESSED
            if (operatorStack.IsEmpty())
            {
                if (valueStack.Count != 1)
                {
                    throw new ArgumentException("Incorrect expression entered");
                }

                return valueStack.Pop();
            }
            else
            {
                if (operatorStack.Count != 1 || valueStack.Count != 2)
                {
                    //ERROR: There isn't exactly one operator on the operator stack or exactly two numbers on the value stack
                    throw new ArgumentException("Incorrect expression entered");
                }

                if (operatorStack.IsOnTop("+") || operatorStack.IsOnTop("-"))
                {
                    int topOfValueStack = valueStack.Pop();
                    int bottomOfValueStack = valueStack.Pop();

                    String addSubtract = operatorStack.Pop();

                    return MathOperation(bottomOfValueStack, topOfValueStack, addSubtract);
                }
                else
                {
                    //ERROR: The operator on the operator stack is neither a + operator or a - operator
                    throw new ArgumentException("Incorrect expression entered");
                }
            }
        }
        /// <summary>
        /// This method is a helper method that performs different math operations based on the mathematical
        /// operators located on the operator stack.
        /// </summary>
        /// <param name="value1">An integer that represents one of the values popped off the value stack</param>
        /// <param name="value2">An integer that represents one of the values popped off the value stack</param>
        /// <param name="mathOperator">A string that represents the mathematical operator that was popped of the stack</param>
        /// <returns></returns>
        public static int MathOperation(int value1, int value2, string mathOperator)
        {
            int result = 0;

            switch (mathOperator)
            {
                case "*":
                    result = value1 * value2;
                    break;

                case "/":
                    if (value2 == 0)
                    {
                        throw new ArgumentException("Divide by zero error");
                    }
                    result = value1 / value2;
                    break;

                case "+":
                    result = value1 + value2;
                    break;

                case "-":
                    result = value1 - value2;
                    break;

                default:
                    throw new ArgumentException("Invalid operator");
            }

            return result;
        }
    }
    /// <summary>
    /// A class that uses C# extension methods allowing for additional methods 
    /// to be added to the Stack object class in C#.
    /// Authors: Pranav Rajan and Professor Daniel Kopta
    /// Date: August 30, 2018 - September 7, 2018
    /// </summary>
    static class StackExtensions
    {
        /// <summary>
        /// Method that allows a user to check the top item of a stack for the
        /// data that they are looking for. If the stack is empty, the
        /// method returns false. Otherwise, it checks to see if the 
        /// item at the top of the stack matches what the user is looking for.
        /// </summary>
        /// <typeparam name="T">Represents a generic data type T.</typeparam>
        /// <param name="s">A stack object that represents the stack the user wants to invoke the method on.</param>
        /// <param name="lookFor">The data that the user is looking for.</param>
        /// <returns></returns>
        public static bool IsOnTop<T>(this Stack<T> s, T lookFor)
        {
            if (s.Count == 0)
                return false;
            return s.Peek().Equals(lookFor);
        }
        /// <summary>
        /// Method that allows a user to check if a stack is empty.
        /// This method is based upon the Java Stack method Empty()
        /// which returns true if and only if the stack contains no items.
        /// Otherwise the method returns false.
        /// </summary>
        /// <typeparam name="T">Represents a generic type T.</typeparam>
        /// <param name="s">Represents the stack the user wants to invoke the method on.</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this Stack<T> s)
        {
            if (s.Count == 0)
                return true;
            else
                return false;
        }
    }
}
