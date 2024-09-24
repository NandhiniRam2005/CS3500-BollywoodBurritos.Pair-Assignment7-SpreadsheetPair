// <copyright file="Formula.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Joel Rodriguez,  Profs Joe, Danny, and Jim.
// Partner:   None
// Date:      September 20, 2024
// Course:    CS 3500, University of Utah, School of Computing
// Copyright: CS 3500 and [Joel Rodriguez] - This work may not
//            be copied for use in Academic Coursework.
//
// I, Joel Rodriguez, certify that I wrote this code from scratch and
// did not copy it in part or whole from another source.  All
// references used in the completion of the assignments are cited
// in my README file.
//
// File Contents
//
//    This file contains the structure for a what a formula should be within the context of a spreadsheet.
//    Withing this file we have code  that ensures that formulas are in valid syntax and have 2 other methods
//    which can be used to turn a formula to a string and get all the variables in a string.
//
// </summary>

namespace CS3500.Formula;

using System.Text;
using System.Text.RegularExpressions;

/// <summary>
///   <para>
///     This class represents formulas written in standard infix notation using standard precedence
///     rules.  The allowed symbols are non-negative numbers written using double-precision
///     floating-point syntax; variables that consist of one ore more letters followed by
///     one or more numbers; parentheses; and the four operator symbols +, -, *, and /.
///   </para>
///   <para>
///     Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
///     a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable;
///     and "x 23" consists of a variable "x" and a number "23".  Otherwise, spaces are to be removed.
///   </para>
///   <para>
///     For Assignment Two, you are to implement the following functionality:
///   </para>
///   <list type="bullet">
///     <item>
///        Formula Constructor which checks the syntax of a formula.
///     </item>
///     <item>
///        Get Variables
///     </item>
///     <item>
///        ToString
///     </item>
///   </list>
/// </summary>
public class Formula
{
    /// <summary>
    ///   All variables are letters followed by numbers.  This pattern
    ///   represents valid variable name strings.
    /// </summary>
    private const string VariableRegExPattern = @"[a-zA-Z]+\d+";

    // The ordered formula as a list.
    private readonly List<string> orderedFormula = new ();

    // The formula as a string this will be canonical for any equal string.
    private readonly string formulaString = string.Empty;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Formula"/> class.
    ///   <para>
    ///     Creates a Formula from a string that consists of an infix expression written as
    ///     described in the class comment.  If the expression is syntactically incorrect,
    ///     throws a FormulaFormatException with an explanatory Message.  See the assignment
    ///     specifications for the syntax rules you are to implement.
    ///   </para>
    ///   <para>
    ///     Non Exhaustive Example Errors:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>
    ///        Invalid variable name, e.g., x, x1x  (Note: x1 is valid, but would be normalized to X1)
    ///     </item>
    ///     <item>
    ///        Empty formula, e.g., string.Empty
    ///     </item>
    ///     <item>
    ///        Mismatched Parentheses, e.g., "(("
    ///     </item>
    ///     <item>
    ///        Invalid Following Rule, e.g., "2x+5"
    ///     </item>
    ///   </list>
    /// </summary>
    /// <param name="formula"> The string representation of the formula to be created.</param>
    public Formula(string formula)
    {
        this.orderedFormula = new ();

        int closingParenthesis = 0;
        int openingParenthesis = 0;
        int numberOfTokens = 0;
        string previousToken = string.Empty;

        StringBuilder formulaStringBuilder = new StringBuilder();

        List<string> tokens = GetTokens(formula);

        // One Token Rule
        if (tokens.Count == 0)
        {
            throw new FormulaFormatException("Formula's must have at least one token!");
        }

        foreach (string token in tokens)
        {
            numberOfTokens++;

            // Valid Token Rule
            IsValidToken(token);

            // Following rules
            IsPrevValid(token, previousToken);

            // First token rule
            if (numberOfTokens == 1)
            {
                if (!token.Equals("(") && !IsNum(token) && !IsVar(token))
                {
                    throw new FormulaFormatException("Invalid starting token! Formulas must begin with an opening parenthesis, number, or variable ");
                }
            }

            // Closing parenthesis rule
            if (token == ")")
            {
                closingParenthesis++;
                if (closingParenthesis > openingParenthesis)
                {
                    throw new FormulaFormatException("Number of closing parenthesis exceeded number of opening parenthesis");
                }
            }

            // Balance Parentheses and Closing Parentheses rules
            else if (token == "(")
            {
                openingParenthesis++;
            }

            // Last Token rule
            else if (numberOfTokens == tokens.Count())
            {
                if (!token.Equals(")") && !IsVar(token) && !IsNum(token))
                {
                    throw new FormulaFormatException("Last token must be a \")\" number or variable!");
                }
            }

            previousToken = token;
            string normalizedToken = NormalizeToken(token);
            formulaStringBuilder.Append(normalizedToken);
            this.orderedFormula.Add(normalizedToken);
        }

        // Balanced Parenthesis rule
        if (openingParenthesis != closingParenthesis)
        {
            throw new FormulaFormatException("Number of closing and opening parenthesis not equal!");
        }

        this.formulaString = formulaStringBuilder.ToString();
    }

    /// <summary>
    ///   Any method meeting this type signature can be used for
    ///   looking up the value of a variable.  In general the expected behavior is that
    ///   the Lookup method will "know" about all variables in a formula
    ///   and return their appropriate value.
    /// </summary>
    /// <exception cref="ArgumentException">
    ///   If a variable name is provided that is not recognized by the implementing method,
    ///   then the method should throw an ArgumentException.
    /// </exception>
    /// <param name="variableName">
    ///   The name of the variable (e.g., "A1") to lookup.
    /// </param>
    /// <returns> The value of the given variable (if one exists). </returns>
    public delegate double Lookup(string variableName);

    /// <summary>
    ///   <para>
    ///     Reports whether f1 == f2, using the notion of equality from the <see cref="Equals"/> method.
    ///   </para>
    /// </summary>
    /// <param name="f1"> The first of two formula objects. </param>
    /// <param name="f2"> The second of two formula objects. </param>
    /// <returns> true if the two formulas are the same.</returns>
    public static bool operator ==(Formula f1, Formula f2)
    {
        if (f1.Equals(f2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///   <para>
    ///     Reports whether f1 != f2, using the notion of equality from the <see cref="Equals"/> method.
    ///   </para>
    /// </summary>
    /// <param name="f1"> The first of two formula objects. </param>
    /// <param name="f2"> The second of two formula objects. </param>
    /// <returns> true if the two formulas are not equal to each other.</returns>
    public static bool operator !=(Formula f1, Formula f2)
    {
        if (f1.Equals(f2))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    ///   <para>
    ///     Returns a set of all the variables in the formula.
    ///   </para>
    ///   <remarks>
    ///     Important: no variable may appear more than once in the returned set, even
    ///     if it is used more than once in the Formula.
    ///   </remarks>
    ///   <para>
    ///     For example, if N is a method that converts all the letters in a string to upper case:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>new("x1+y1*z1").GetVariables() should enumerate "X1", "Y1", and "Z1".</item>
    ///     <item>new("x1+X1"   ).GetVariables() should enumerate "X1".</item>
    ///   </list>
    /// </summary>
    /// <returns> the set of variables (string names) representing the variables referenced by the formula. </returns>
    public ISet<string> GetVariables()
    {
        HashSet<string> variables = new HashSet<string>();
        foreach (string token in this.orderedFormula)
        {
            if (IsVar(token) && !variables.Contains(token))
            {
                variables.Add(token);
            }
        }

        return variables;
    }

    /// <summary>
    ///   <para>
    ///     Returns a string representation of a canonical form of the formula.
    ///   </para>
    ///   <para>
    ///     The string will contain no spaces.
    ///   </para>
    ///   <para>
    ///     If the string is passed to the Formula constructor, the new Formula f
    ///     will be such that this.ToString() == f.ToString().
    ///   </para>
    ///   <para>
    ///     All of the variables in the string will be normalized.  This
    ///     means capital letters.
    ///   </para>
    ///   <para>
    ///       For example:
    ///   </para>
    ///   <code>
    ///       new("x1 + y1").ToString() should return "X1+Y1"
    ///       new("X1 + 5.0000").ToString() should return "X1+5".
    ///   </code>
    ///   <para>
    ///     This code should execute in O(1) time.
    ///   </para>
    /// </summary>
    /// <returns> A canonical version (string) of the formula. All "equal" formulas
    ///   should have the same value here.</returns>
    public override string ToString()
    {
        return this.formulaString;
    }

    /// <summary>
    ///   <para>
    ///     Determines if two formula objects represent the same formula.
    ///   </para>
    ///   <para>
    ///     By definition, if the parameter is null or does not reference
    ///     a Formula Object then return false.
    ///   </para>
    ///   <para>
    ///     Two Formulas are considered equal if their canonical string representations
    ///     (as defined by ToString) are equal.
    ///   </para>
    /// </summary>
    /// <param name="obj"> The other object.</param>
    /// <returns>
    ///   True if the two objects represent the same formula.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (!(obj is Formula))
        {
            return false;
        }

        Formula givenFormula = (Formula)obj;

        return this.ToString().Equals(givenFormula.ToString());
    }

    /// <summary>
    ///   <para>
    ///     Evaluates this Formula, using the lookup delegate to determine the values of
    ///     variables.
    ///   </para>
    ///   <remarks>
    ///     When the lookup method is called, it will always be passed a Normalized (capitalized)
    ///     variable name.  The lookup method will throw an ArgumentException if there is
    ///     not a definition for that variable token.
    ///   </remarks>
    ///   <para>
    ///     If no undefined variables or divisions by zero are encountered when evaluating
    ///     this Formula, the numeric value of the formula is returned.  Otherwise, a
    ///     FormulaError is returned (with a meaningful explanation as the Reason property).
    ///   </para>
    ///   <para>
    ///     This method should never throw an exception.
    ///   </para>
    /// </summary>
    /// <param name="lookup">
    ///   <para>
    ///     Given a variable symbol as its parameter, lookup returns the variable's (double) value
    ///     (if it has one) or throws an ArgumentException (otherwise).  This method should expect
    ///     variable names to be capitalized.
    ///   </para>
    /// </param>
    /// <returns> Either a double or a formula error, based on evaluating the formula.</returns>
    public object Evaluate(Lookup lookup)
    {
        Stack<string> valueStack = new Stack<string>();
        Stack<string> operatorStack = new Stack<string>();

        foreach (string token in this.orderedFormula)
        {
            if (IsNum(token))
            {
                // This method does the multiples and or divide and will also tell us if we need to return a FormulaError
                if (MultiplyOrDivideUnderNumber(valueStack, operatorStack, token))
                {
                    return new FormulaError("Divide by 0 is NOT allowed!");
                }
            }

            if (IsVar(token))
            {
                try
                {
                    lookup(token);
                }
                catch (ArgumentException)
                {
                    return new FormulaError("Unknown variable: " + token + " please enter existing variables.");
                }

                // This method does the multiples and or divide and will also tell us if we need to return a FormulaError
                // However this method does it with the conditions of the variable.
                if (MultiplyOrDivideClosingUnderVariables(valueStack, operatorStack, token, lookup))
                {
                    return new FormulaError("Divide by 0 is NOT allowed!");
                }
            }

            if (IsPlus(token) || IsMinus(token))
            {
                // Completes add or subtract as specified look to method header for more information.
                AddOrSubtract(valueStack, operatorStack, token);
            }

            if (IsDivide(token) || IsMultiply(token))
            {
                operatorStack.Push(token);
            }

            if (IsOpeningParenthesis(token))
            {
                operatorStack.Push(token);
            }

            if (IsClosingParenthesis(token))
            {
                AddOrSubtractUnderParentheses(valueStack, operatorStack);

                operatorStack.Pop(); // Pops "(" since parentheses should be removed together.

                // Completes steps for dividing and multiplying under closed parentheses and checks for division by 0.
                if (MultiplyOrDivideClosingUnderParenthese(valueStack, operatorStack))
                {
                    return new FormulaError("Divide by 0 is NOT allowed!");
                }
            }
        }

        return GetFinalValue(valueStack, operatorStack);
    }

    /// <summary>
    ///   <para>
    ///     Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
    ///     case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two
    ///     randomly-generated unequal Formulas have the same hash code should be extremely small.
    ///   </para>
    /// </summary>
    /// <returns> The hashcode for the object. </returns>
    public override int GetHashCode()
    {
        string currentForumulasString = this.ToString();

        return currentForumulasString.GetHashCode();
    }

    /// <summary>
    /// Since a reference to the stacks is passed into this method we can directly modify it using this method.
    /// This method gets the final value of the Evaluate method as described in Assignment 4 instructions.
    /// </summary>
    /// <param name="valueStack"> The stack which holds the values (doubles). </param>
    /// <param name="operatorStack"> The stack which holds the operators (*-+...)</param>
    /// <returns>The final value for the Evaluate method.</returns>
    private static double GetFinalValue(Stack<string> valueStack, Stack<string> operatorStack)
    {
        // If stuff remains in the stack keep do this special implementation which involves add/subtract
        if (operatorStack.Count != 0)
        {
            string valueToPush = string.Empty;
            double value = 0;

            if (IsPlus(operatorStack.Peek()))
            {
                value = Convert.ToDouble(valueStack.Pop()) + Convert.ToDouble(valueStack.Pop());
                operatorStack.Pop();
            }
            else if (IsMinus(operatorStack.Peek()))
            {
                double firstPopped = Convert.ToDouble(valueStack.Pop());
                double secondPopped = Convert.ToDouble(valueStack.Pop());
                value = secondPopped - firstPopped;
                operatorStack.Pop();
            }

            return value;
        }
        else
        {
            return Convert.ToDouble(valueStack.Pop());
        }
    }

    /// <summary>
    /// Since a reference to the stacks is passed into this method we can directly modify it using this method.
    /// This does multiplication or division under closed parentheses. It will also return a boolean which represents
    /// if a divide by 0 occurred.
    /// </summary>
    /// <param name="valueStack"> The stack which holds the values (doubles). </param>
    /// <param name="operatorStack"> The stack which holds the operators (*-+...)</param>
    /// <returns>A bool determining whether a Formula Error should be thrown or not due to divide by 0.</returns>
    private static bool MultiplyOrDivideClosingUnderParenthese(Stack<string> valueStack, Stack<string> operatorStack)
    {
        // If the operator stack is not empty and there is a divide or multiplication operator token at the top of the operator stack
        // we will pop an operator from the operatorStack and two values from the value stack and apply that operator to the
        // the two popped values.
        if (operatorStack.Count != 0 && (IsDivide(operatorStack.Peek()) || IsMultiply(operatorStack.Peek())))
        {
            string valueToPush = string.Empty;
            if (IsMultiply(operatorStack.Peek()))
            {
                double value = Convert.ToDouble(valueStack.Pop()) * Convert.ToDouble(valueStack.Pop());  // May need to switch order
                valueToPush = value.ToString();
            }
            else if (IsDivide(operatorStack.Peek()))
            {
                // For division the order of the popped variables matters so we need to clarify which is first and which is second.
                double firstPopped = Convert.ToDouble(valueStack.Pop());
                double secondPopped = Convert.ToDouble(valueStack.Pop());
                if (firstPopped == 0)
                {
                    return true;
                }
                else
                {
                    double value = secondPopped / firstPopped;
                    valueToPush = value.ToString();
                }
            }

            // We do not want to push an empty string to our value stack or pop an operator off if it was never used. So
            // this if statement ensures we do not.
            if (!valueToPush.Equals(string.Empty))
            {
                operatorStack.Pop();
                valueStack.Push(valueToPush);
            }
        }

        return false;
    }

    /// <summary>
    /// Since a reference to the stacks is passed into this method we can directly modify it using this method.
    /// This does multiplication or division under a number. It will also return a boolean which represents
    /// if a divide by 0 occurred.
    /// </summary>
    /// <param name="valueStack"> The stack which holds the values (doubles). </param>
    /// <param name="operatorStack"> The stack which holds the operators (*-+...)</param>
    /// <param name="token"> The token which will be added to the operator stack.</param>
    /// <returns>A bool determining whether a Formula Error should be thrown or not due to divide by 0.</returns>
    private static bool MultiplyOrDivideUnderNumber(Stack<string> valueStack, Stack<string> operatorStack, string token)
    {
        // If the operator stack is not empty and there is a divide or operator token at the top of the operator stack
        // we will pop an operator from the operatorStack and a value from the value stack and apply that operator to the
        // the popped value and the current token.
        if (operatorStack.Count != 0 && (IsDivide(operatorStack.Peek()) || IsMultiply(operatorStack.Peek())))
        {
            string valueToPush = string.Empty;
            if (IsMultiply(operatorStack.Peek()))
            {
                double value = Convert.ToDouble(valueStack.Pop()) * Convert.ToDouble(token);
                valueToPush = value.ToString();
            }
            else if (IsDivide(operatorStack.Peek()))
            {
                // Prevents division of zero
                if (Convert.ToDouble(token) == 0)
                {
                    return true;
                }
                else
                {
                    double value = Convert.ToDouble(valueStack.Pop()) / Convert.ToDouble(token);
                    valueToPush = value.ToString();
                }
            }

            // If a value is going to be pushed we also pop the operator that was used.
            if (!valueToPush.Equals(string.Empty))
            {
                operatorStack.Pop();
            }

            valueStack.Push(valueToPush);
        }
        else
        {
            valueStack.Push(token);
        }

        return false;
    }

    /// <summary>
    /// Since a reference to the stacks is passed into this method we can directly modify it using this method.
    /// This does multiplication or division under variable conditions. It will also return a boolean which represents
    /// if a divide by 0 occurred.
    /// </summary>
    /// <param name="valueStack"> The stack which holds the values (doubles). </param>
    /// <param name="operatorStack"> The stack which holds the operators (*-+...)</param>
    /// <param name="token"> The token which will be added to the operator stack.</param>
    /// <param name="lookup"> Given a variable symbol as its parameter, lookup returns the variable's (double) value
    ///     (if it has one) or throws an ArgumentException (otherwise).  This method should expect
    ///     variable names to be capitalized. </param>
    /// <returns>A bool determining whether a Formula Error should be thrown or not due to divide by 0.</returns>
    private static bool MultiplyOrDivideClosingUnderVariables(Stack<string> valueStack, Stack<string> operatorStack, string token, Lookup lookup)
    {
        // If the operator stack is not empty and there is a divide or multiplication operator token at the top of the operator stack
        // we will pop an operator from the operatorStack and a value from the value stack and apply that operator to the
        // the popped value and the current token.
        if (operatorStack.Count != 0 && (IsDivide(operatorStack.Peek()) || IsMultiply(operatorStack.Peek())))
        {
            string valueToPush = string.Empty;
            if (IsMultiply(operatorStack.Peek()))
            {
                double value = Convert.ToDouble(valueStack.Pop()) * lookup(token);
                valueToPush = value.ToString();
            }
            else if (IsDivide(operatorStack.Peek()))
            {
                if (Convert.ToDouble(lookup(token)) == 0)
                {
                    return true;
                }
                else
                {
                    double value = Convert.ToDouble(valueStack.Pop()) / lookup(token);
                    valueToPush = value.ToString();
                }
            }

            // If a value is going to be pushed we also pop the operator that was used.
            if (!valueToPush.Equals(string.Empty))
            {
                operatorStack.Pop();
            }

            valueStack.Push(valueToPush);
        }
        else
        {
            valueStack.Push(lookup(token).ToString());
        }

        return false;
    }

    /// <summary>
    /// Since a reference to the stacks is passed into this method we can directly modify it using this method.
    /// This method does add or subtract under closed parenthesis.
    /// </summary>
    /// <param name="valueStack"> The stack which holds the values (doubles). </param>
    /// <param name="operatorStack"> The stack which holds the operators (*-+...)</param>
    private static void AddOrSubtractUnderParentheses(Stack<string> valueStack, Stack<string> operatorStack)
    {
        // If there is an add or subtract operator token at the top of the operator stack
        // we will pop an operator from the operatorStack and two values from the value stack and apply that operator to the
        // the two popped values. The operator stack CANNOT be empty at this point as there must be a "(" in there.
        if (IsPlus(operatorStack.Peek()) || IsMinus(operatorStack.Peek()))
        {
            string valueToPush = string.Empty;
            if (IsPlus(operatorStack.Peek()))
            {
                double value = Convert.ToDouble(valueStack.Pop()) + Convert.ToDouble(valueStack.Pop());
                valueToPush = value.ToString();
            }
            else if (IsMinus(operatorStack.Peek()))
            {
                double firstPopped = Convert.ToDouble(valueStack.Pop());
                double secondPopped = Convert.ToDouble(valueStack.Pop());
                double value = secondPopped - firstPopped;
                valueToPush = value.ToString();
            }

            // We do not want to push an empty string to our value stack or pop an operator off if it was never used. So
            // this if statement ensures we do not.
            if (!valueToPush.Equals(string.Empty))
            {
                operatorStack.Pop();
                valueStack.Push(valueToPush);
            }
        }
    }

    /// <summary>
    /// Since a reference to the stacks is passed into this method we can directly modify it using this method.
    /// This method does add or subtract the regular way without considering parentheses.
    /// </summary>
    /// <param name="valueStack"> The stack which holds the values (doubles). </param>
    /// <param name="operatorStack"> The stack which holds the operators (*-+...)</param>
    /// <param name="token"> The token which will be added to the operator stack.</param>
    private static void AddOrSubtract(Stack<string> valueStack, Stack<string> operatorStack, string token)
    {
        // If the operator stack is not empty and there is an add or subtract operator token at the top of the operator stack
        // we will pop an operator from the operatorStack and two values from the value stack and apply that operator to the
        // the two popped values.
        string valueToPush = string.Empty;
        if (operatorStack.Count != 0 && IsPlus(operatorStack.Peek()))
        {
            double value = Convert.ToDouble(valueStack.Pop()) + Convert.ToDouble(valueStack.Pop());
            valueToPush = value.ToString();
        }
        else if (operatorStack.Count != 0 && IsMinus(operatorStack.Peek()))
        {
            // For subtraction the order of the popped variables matters so we need to clarify which is first and which is second.
            double firstPopped = Convert.ToDouble(valueStack.Pop());
            double secondPopped = Convert.ToDouble(valueStack.Pop());
            double value = secondPopped - firstPopped;
            valueToPush = value.ToString();
        }

        // We do not want to push an empty string to our value stack or pop an operator off if it was never used. So
        // this if statement ensures we do not.
        if (!valueToPush.Equals(string.Empty))
        {
            operatorStack.Pop();
            valueStack.Push(valueToPush);
        }

        operatorStack.Push(token);
    }

    /// <summary>
    /// This method checks if a currentToken's previous token is valid and follows all following rules for a formula.
    /// </summary>
    /// <param name="currToken"> This is the current token that is being checked in our formula.</param>
    /// <param name="prevToken"> The token that comes prior to the current token. </param >
    ///
    private static void IsPrevValid(string currToken, string prevToken)
    {
        if (IsOperator(prevToken) || prevToken.Equals("("))
        {
            if (!currToken.Equals("(") && !IsNum(currToken) && !IsVar(currToken))
            {
                throw new FormulaFormatException("Tokens following an operator or \"(\" must be a number, variable or \"(\"!");
            }
        }
        else if (IsNum(prevToken) || IsVar(prevToken) || prevToken.Equals(")"))
        {
            if (!currToken.Equals(")") && !IsOperator(currToken))
            {
                throw new FormulaFormatException("Tokens following a number, \")\", or variable must be an operator or \")\"!");
            }
        }
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a number.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a number.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a number.</returns>
    private static bool IsNum(string token)
    {
        return double.TryParse(token, out double _);
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as an operator.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a operator.</returns>
    private static bool IsOperator(string token)
    {
        if (token.Equals("+") || token.Equals("*") || token.Equals("-") || token.Equals("/"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a +.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a +.</returns>
    private static bool IsPlus(string token)
    {
        if (token.Equals("+"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a -.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a -.</returns>
    private static bool IsMinus(string token)
    {
        if (token.Equals("-"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a *.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a *.</returns>
    private static bool IsMultiply(string token)
    {
        if (token.Equals("*"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a /.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a /.</returns>
    private static bool IsDivide(string token)
    {
        if (token.Equals("/"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// A private helper method that "normalizes" tokens. For example 5.00000 and 50e-1 turns into 5. Variables
    /// such as x1 turn into X1.
    /// </summary>
    /// <param name="token"> The token to be normalized.</param>
    /// <returns>A normalized token. Refer to method summary on what normalizing is.</returns>
    private static string NormalizeToken(string token)
    {
        string normalizedToken = string.Empty;
        if (IsNum(token))
        {
            double numberAsDouble = Convert.ToDouble(token);
            normalizedToken = numberAsDouble.ToString();
            return normalizedToken;
        }
        else if (IsVar(token))
        {
            normalizedToken = token.ToUpper();
            return normalizedToken;
        }
        else
        {
            return token;
        }
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a (.
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a (.</returns>
    private static bool IsOpeningParenthesis(string token)
    {
        if (token.Equals("("))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method processes a token to see if its contents can be processed as a ).
    /// </summary>
    /// <param name="token"> The token that is being checked for being a operator.</param>
    /// <returns> A bool that represents whether or not the token can be represented as a ).</returns>
    private static bool IsClosingParenthesis(string token)
    {
        if (token.Equals(")"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method checks to see if a token is valid as described in assignment instructions.
    /// </summary>
    /// <param name="token"> The token which is being checked for validity. </param>
    /// <exception cref="FormulaFormatException"> The exception to be thrown if a token is invalid.</exception>
    private static void IsValidToken(string token)
    {
        if (!IsVar(token) && !IsOperator(token) && !IsNum(token) && !token.Equals("(") && !token.Equals(")"))
        {
            throw new FormulaFormatException(token + " is not a valid token!");
        }
    }

    /// <summary>
    ///   Reports whether "token" is a variable.  It must be one or more letters
    ///   followed by one or more numbers.
    /// </summary>
    /// <param name="token"> A token that may be a variable. </param>
    /// <returns> true if the string matches the requirements, e.g., A1 or a1. </returns>
    private static bool IsVar(string token)
    {
        // notice the use of ^ and $ to denote that the entire string being matched is just the variable
        string standaloneVarPattern = $"^{VariableRegExPattern}$";
        return Regex.IsMatch(token, standaloneVarPattern);
    }

    /// <summary>
    ///   <para>
    ///     Given an expression, enumerates the tokens that compose it.
    ///   </para>
    ///   <para>
    ///     Tokens returned are:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>left paren</item>
    ///     <item>right paren</item>
    ///     <item>one of the four operator symbols</item>
    ///     <item>a string consisting of one or more letters followed by one or more numbers</item>
    ///     <item>a double literal</item>
    ///     <item>and anything that doesn't match one of the above patterns</item>
    ///   </list>
    ///   <para>
    ///     There are no empty tokens; white space is ignored (except to separate other tokens).
    ///   </para>
    /// </summary>
    /// <param name="formula"> A string representing an infix formula such as 1*B1/3.0. </param>
    /// <returns> The ordered list of tokens in the formula. </returns>
    private static List<string> GetTokens(string formula)
    {
        List<string> results = new ();

        string lpPattern = @"\(";
        string rpPattern = @"\)";
        string opPattern = @"[\+\-*/]";
        string doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
        string spacePattern = @"\s+";

        // Overall pattern
        string pattern = string.Format(
                                        "({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                        lpPattern,
                                        rpPattern,
                                        opPattern,
                                        VariableRegExPattern,
                                        doublePattern,
                                        spacePattern);

        // Enumerate matching tokens that don't consist solely of white space.
        foreach (string s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
        {
            if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
            {
                results.Add(s);
            }
        }

        return results;
    }
}

/// <summary>
///   Used to report syntax errors in the argument to the Formula constructor.
/// </summary>
public class FormulaFormatException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="FormulaFormatException"/> class.
    ///   <para>
    ///      Constructs a FormulaFormatException containing the explanatory message.
    ///   </para>
    /// </summary>
    /// <param name="message"> A developer defined message describing why the exception occured.</param>
    public FormulaFormatException(string message)
        : base(message)
    {
        // All this does is call the base constructor. No extra code needed.
    }
}

/// <summary>
/// Used as a possible return value of the Formula.Evaluate method.
/// </summary>
public class FormulaError
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="FormulaError"/> class.
    ///   <para>
    ///     Constructs a FormulaError containing the explanatory reason.
    ///   </para>
    /// </summary>
    /// <param name="message"> Contains a message for why the error occurred.</param>
    public FormulaError(string message)
    {
        this.Reason = message;
    }

    /// <summary>
    ///  Gets the reason why this FormulaError was created.
    /// </summary>
    public string Reason { get; private set; }
}
