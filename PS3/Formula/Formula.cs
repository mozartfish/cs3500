// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens

// (Pranav Rajan)
// Version 1.3 (9/22/18)
// Version 1.4 (9/24/18)
// Version 1.5 (9/26/18)
// Version 1.6 (10/12/18)

//Change Log:
// (Version 1.3) Completed all the method stubs in the Formula Class
// wrote a testing suite that tests whether the methods work according to School of Computing Specs
// (Version 1.4) Removed all unnecessary comments that were in Version 1.3
// (Version 1.5) Corrected my code to pass School of Computing Staff Tests and cleaned up the logic from versions 1.3 and 1.4
// (Version 1.6) Cleaned up the code from version 1.5. Changes included renaming some variables, cleaning up comments, and making code
// easier to read


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision 
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator 
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable; 
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.  The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement 
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// 
    /// Authors: Professor Joe Zachary, Professor Daniel Kopta, Pranav Rajan
    /// Date: September 21, 2018
    /// </summary>
    public class Formula
    {
        /// <summary>
        /// A list object that stores the tokens of the expression entered by the user.
        /// </summary>
        private List<String> expressionTokens;

        /// <summary>
        /// A set object that stores a list of the variables that have been normalized
        /// </summary>
        private HashSet<String> normalizedVariables;

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) :
            this(formula, s => s, s => true)
        {
        }

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalize, Func<string, bool> isValid)
        {
            //create a set of static variables to keep track of the number of parentheses in a formula
            int numOpeningParen = 0;
            int numClosingParen = 0;

            this.normalizedVariables = new HashSet<string>();

            this.expressionTokens = new List<string>(GetTokens(formula));

            //check to make sure that there is at least one token in the expressionTokens list
            if (this.expressionTokens.Count < 1)
            {
                throw new FormulaFormatException("There is less than one token in the expression!" +
                    "Make sure your expression contains one or more operands and operators.");
            }

            //convert the list to an array so that we can iterate through the list using indexes
            //to check if a token is valid according to a set of rules that are defined for formula objects
            String[] tokenCheck = expressionTokens.ToArray();

            //Clear all the elements in expressionToken list so we can add each of the tokens that have been
            //verified back to the list for use in evaluation
            this.expressionTokens.Clear();

            for (int i = 0; i < tokenCheck.Length; i++)
            {
                //RIGHT PARENTHESES RULE
                //When reading tokens from left to right, at no point should the number of closing parentheses seen so far
                //be greater than the number of opening parentheses seen so far
                if (numClosingParen > numOpeningParen)
                {
                    throw new FormulaFormatException("There are more closing parentheses seen so far than number of opening parentheses seen so far!" +
                        "Make sure you are aware of your opening and closing parentheses when you enter an expression.");
                }

                //STARTING TOKEN RULE
                //The first token of an expression must be a number a variable or an opening parenthesis
                if (i == 0)
                {
                    if (Double.TryParse(tokenCheck[i], out double firstToken))
                    {
                        this.expressionTokens.Add(tokenCheck[i]);
                    }

                    //check if the first token is an opening parenthesis
                    else if (tokenCheck[i].Equals("("))
                    {
                        //if the token is an opening parenthesis, add it to the expressionToken list
                        //increment the number of opening parentheses
                        this.expressionTokens.Add(tokenCheck[i]);
                        numOpeningParen++;
                    }

                    //check if the first token is a valid variable name as defined by the constructor
                    else if (CheckIfValidVariable(tokenCheck[i], normalize, isValid))
                    {
                        //if the token is a valid variable, normalize it and then add it to the expressionTokens list
                        //add the normalized variable to the normalizedVariable list
                        this.expressionTokens.Add(normalize(tokenCheck[i]));
                        this.normalizedVariables.Add(normalize(tokenCheck[i]));
                    }

                    else
                    {
                        throw new FormulaFormatException("The first token of an expression must be a number, a legal variable name, " +
                            "or an opening parenthesis! Make sure that your expression begins with a number, a valid variable " +
                            "or an opening parenthesis.");
                    }

                    //if we have checked the first token then move on to the next token
                    continue;
                }

                //ENDING TOKEN RULE
                //The last token of an expression must be a number, a variable or a closing parenthesis
                else if (i == (tokenCheck.Length - 1))
                {
                    //check if the token is a double
                    if (!(Double.TryParse(tokenCheck[i], out double lastToken)) &&
                        !(tokenCheck[i].Equals(")")) &&
                        !(CheckIfValidVariable(tokenCheck[i], normalize, isValid)))
                    {
                        if (!(tokenCheck[i - 1].Equals(")")))
                        {
                            throw new FormulaFormatException("The previous token was a parenthesis and violated the rule that " +
                                "any token that immediately follows a number, " +
                                "a variable, or a closing parenthesis must be either an operator or a closing parenthesis!" +
                                "Make sure that you enter an operator or a closing parenthesis in your expression after a number, " +
                                "operator or closing parenthesis.");
                        }

                        throw new FormulaFormatException("The last token of an expression must be a number, a LEGAL variable name, " +
                            "or a closing parenthesis!" +
                            "Make sure you end your mathematical expression with a number, a legal variable name or a closing parenthesis.");
                    }
                }

                //PARENTHESES FOLLOWING RULE
                //Any token that immediately follows an opening parenthesis or an operator must be either a number,
                //a variable, or an opening parenthesis
                if ((tokenCheck[i - 1].Equals("(") || CheckOperator(tokenCheck[i - 1])))
                {
                    //check if the token is a number
                    if (!(Double.TryParse(tokenCheck[i], out double followingValue)) &&
                        !(tokenCheck[i].Equals("(")) &&
                        !((CheckIfValidVariable(tokenCheck[i], normalize, isValid))))
                    {
                        throw new FormulaFormatException("Any token that immediately follow an opening parenthesis or " +
                            "an operator must be either a number, a legal variable, " +
                            "or an opening parenthesis! Make sure that the token following an opening parenthesis is a number, " +
                            "a legal variable or an opening parenthesis.");
                    }
                }

                //EXTRA FOLLOWING RULE
                //Any token that immediately follows a number, a variable or a closing parenthesis must be either an 
                //operator or a closing parenthesis
                else if ((Double.TryParse(tokenCheck[i - 1], out double extraValue) ||
                    CheckIfValidVariable(tokenCheck[i - 1], normalize, isValid) ||
                    tokenCheck[i - 1].Equals(")")))
                {
                    if (!(CheckOperator(tokenCheck[i]) || tokenCheck[i].Equals(")")))
                    {
                        throw new FormulaFormatException("Any token that immediately follows a number, a variable, " +
                            "or a closing parenthesis must be either an operator or a closing parenthesis!" +
                            "Make sure that the token that follows a number, a variable " +
                            "or a closing parenthesis is an operator or a closing parenthesis.");
                    }
                }

                //add the token to the expressionList
                if (!CheckIfValidVariable(tokenCheck[i], normalize, isValid))
                {
                    if (tokenCheck[i].Equals("("))
                    {
                        numOpeningParen++;
                    }

                    if (tokenCheck[i].Equals(")"))
                    {
                        numClosingParen++;
                    }
                    this.expressionTokens.Add(tokenCheck[i]);
                }
                else
                {
                    //if the token is a valid variable,normalize the variable and add it to the expressionTokens list
                    //add the normalized variable to the normalizedVariable list
                    this.expressionTokens.Add(normalize(tokenCheck[i]));
                    this.normalizedVariables.Add(normalize(tokenCheck[i]));
                }
            }

            //AT THIS POINT WE HAVE EXITED THE FOR LOOP LOOKING AT EACH ELEMENT

            //BALANCED PARENTHESES RULE
            //The total number of opening parentheses must equal the total number of closing parentheses
            if (numOpeningParen != numClosingParen)
            {
                throw new FormulaFormatException("The total number of opening parentheses does not equal the total number of closing parentheses! " +
                    "Make sure that you have the correct number of opening and closing parentheses.");
            }
        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to 
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters 
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value 
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.  
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            Stack<Double> valueStack = new Stack<double>();

            Stack<String> operatorStack = new Stack<String>();

            string[] substrings = this.expressionTokens.ToArray();

            for (int i = 0; i < substrings.Length; i++)
            {
                //CASE 1: CHECK IF THE TOKEN IS A NUMBER AND CAN PARSE TO TYPE DOUBLE
                if (Double.TryParse(substrings[i], out double valueData))
                {
                    if ((operatorStack.IsOnTop("*")) || operatorStack.IsOnTop("/"))
                    {
                        double topOfValueStack = valueStack.Pop();

                        String multiplyDivide = operatorStack.Pop();

                        //Check for division by zero
                        if (multiplyDivide.Equals("/") && valueData == 0.0)
                        {
                            return new FormulaError("Division by zero error.");
                        }

                        valueStack.Push(MathOperation(topOfValueStack, valueData, multiplyDivide));
                    }
                    else
                    {
                        valueStack.Push(valueData);
                    }
                }

                // CASE 2: THE TOKEN IS + OR - OPERATOR
                else if (substrings[i].Equals("+") || substrings[i].Equals("-"))
                {
                    if (operatorStack.IsOnTop("+") || operatorStack.IsOnTop("-"))
                    {
                        double topOfValueStack = valueStack.Pop();
                        double bottomOfValueStack = valueStack.Pop();

                        String addSubtract = operatorStack.Pop();

                        valueStack.Push(MathOperation(bottomOfValueStack, topOfValueStack, addSubtract));
                    }

                    operatorStack.Push(substrings[i]);
                }

                //CASE 3: THE TOKEN IS A * OR / OPERATOR
                else if (substrings[i].Equals("*") || substrings[i].Equals("/"))
                {
                    operatorStack.Push(substrings[i]);
                }

                //CASE 4: THE TOKEN IS A LEFT PARENTHESIS (
                else if (substrings[i].Equals("("))
                {
                    operatorStack.Push(substrings[i]);
                }

                //CASE 5: THE TOKEN IS A RIGHT PARENTHESIS )
                else if (substrings[i].Equals(")"))
                {

                    if (operatorStack.IsOnTop("+") || operatorStack.IsOnTop("-"))
                    {
                        double topOfValueStack = valueStack.Pop();
                        double bottomOfValueStack = valueStack.Pop();

                        String addSubtract = operatorStack.Pop();

                        valueStack.Push(MathOperation(bottomOfValueStack, topOfValueStack, addSubtract));
                    }

                    operatorStack.Pop();

                    if (operatorStack.IsOnTop("*") || operatorStack.IsOnTop("/"))
                    {
                        double topOfValueStack = valueStack.Pop();
                        double bottomOfValueStack = valueStack.Pop();

                        String multiplyDivide = operatorStack.Pop();

                        //Check for divide by zero
                        if (multiplyDivide.Equals("/") && topOfValueStack == 0.0)
                        {
                            return new FormulaError("Division by zero error.");
                        }

                        valueStack.Push(MathOperation(bottomOfValueStack, topOfValueStack, multiplyDivide));
                    }
                }

                //CASE 6: THE TOKEN IS A VARIABLE
                else
                {
                    try
                    {
                        double variableValue = lookup(substrings[i]);

                        if ((operatorStack.IsOnTop("*")) || operatorStack.IsOnTop("/"))
                        {
                            double topOfValueStack = valueStack.Pop();

                            String multiplyDivide = operatorStack.Pop();

                            //check for division by zero
                            if (multiplyDivide.Equals("/") && variableValue == 0.0)
                            {
                                return new FormulaError("Division by zero error.");
                            }

                            valueStack.Push(MathOperation(topOfValueStack, variableValue, multiplyDivide));
                        }
                        else
                        {
                            valueStack.Push(variableValue);
                        }
                    }
                    catch (ArgumentException e)
                    {
                        //ERROR:If looking up the variable reveals it has no value (the delegate throws)
                        return new FormulaError("There is no value associated with this variable");
                    }
                }
            }

            //AT THIS POINT WE ASSUME THAT LAST TOKEN HAS BEEN PROCESSED

            //check if the operator stack is empty
            if (operatorStack.IsEmpty())
            {
                return valueStack.Pop();
            }
            else
            {
                //create a static variable to store the result if there is a + OR - operator still on the operator stack
                double result = 0.0;

                //check if + operator or - operator are on top of the operator stack
                if (operatorStack.IsOnTop("+") || operatorStack.IsOnTop("-"))
                {
                    double topOfValueStack = valueStack.Pop();
                    double bottomOfValueStack = valueStack.Pop();

                    String addSubtract = operatorStack.Pop();

                    result = MathOperation(bottomOfValueStack, topOfValueStack, addSubtract);
                }

                return result;
            }
        }



        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even 
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            //create a shallow copy of normalizedVariables to prevent the user / developer from doing stuff to the internals
            return new HashSet<string>(this.normalizedVariables);
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            String result = "";

            String[] expressionFormula = this.expressionTokens.ToArray();

            for (int i = 0; i < expressionFormula.Length; i++)
            {
                if ((Double.TryParse(expressionFormula[i], out double standardDouble)))
                {

                    result += standardDouble;

                    //if we verify that the numbers are the same continue on to the next token
                    continue;
                }
                else
                {
                    result += expressionFormula[i];
                }
            }

            return result;
        }

        /// <summary>
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Formula))
            {
                return false;
            }

            Formula compareFormula = (Formula)obj;

            return this.ToString().Equals(compareFormula.ToString());
        }

        /// <summary>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return true.  If one is
        /// null and one is not, this method should return false.
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            //check if both formulas are null
            if (Object.ReferenceEquals(f1, null) && Object.ReferenceEquals(f2, null))
            {
                return true;
            }

            //check if one of the formulas is null
            if (Object.ReferenceEquals(f1, null) || Object.ReferenceEquals(f2, null))
            {
                return false;
            }

            return f1.Equals(f2);
        }

        /// <summary>
        /// Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// Note that if both f1 and f2 are null, this method should return false.  If one is
        /// null and one is not, this method should return true.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            //check if both both formulas are null
            if (Object.ReferenceEquals(f1, null) && Object.ReferenceEquals(f2, null))
            {
                return false;
            }

            //check if one of the formulas is null
            if (Object.ReferenceEquals(f1, null) || Object.ReferenceEquals(f2, null))
            {
                return true;
            }

            return !(f1.Equals(f2));
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two 
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// A helper method that checks whether a token is a valid operator as defined by the definition of a formula object
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool CheckOperator(String s)
        {
            switch (s)
            {
                case "*":
                    return true;

                case "/":
                    return true;

                case "+":
                    return true;

                case "-":
                    return true;

                default:
                    return false;
            }
        }
        /// <summary>
        /// A helper method that checks whether a variable name is valid according to the rules for a formula variable name
        /// </summary>
        /// <param name="variableName">The variable name</param>
        /// <param name="normalize">a function that normalizes a variable name according to what the user defines for a standard variable name</param>
        /// <param name="userValidator">a function that the user defines for what is a legal variable name</param>
        /// <returns></returns>
        private static bool CheckIfValidVariable(String variableName, Func<string, string> normalize, Func<string, bool> userValidator)
        {
            //pattern definition for a legal variable name
            String pattern = @"^[_a-zA-Z][_0-9a-zA-Z]*$";

            //the regex object allows for checking whether the input variable name matches
            //the requirement of a valid formula variable name
            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(variableName) && userValidator(normalize(variableName)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method is a helper method that performs different math operations based on the mathematical
        /// operators located on the operator stack.
        /// </summary>
        /// <param name="value1">A double that represents one of the values popped off the value stack</param>
        /// <param name="value2">A double that represents one of the values popped off the value stack</param>
        /// <param name="mathOperator">A string that represents the mathematical operator that was popped of the operator stack</param>
        /// <returns></returns>
        private static double MathOperation(double value1, double value2, string mathOperator)
        {
            //initialize a variable to keep track of the result
            double result = 0.0;

            switch (mathOperator)
            {
                case "*":
                    result = value1 * value2;
                    break;

                case "/":
                    result = value1 / value2;
                    break;

                case "+":
                    result = value1 + value2;
                    break;

                case "-":
                    result = value1 - value2;
                    break;
            }

            return result;
        }



        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal; and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }

        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason)
            : this()
        {
            Reason = reason;
        }

        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
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
