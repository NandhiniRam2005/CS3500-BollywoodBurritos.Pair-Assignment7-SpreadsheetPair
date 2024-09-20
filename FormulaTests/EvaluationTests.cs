// <copyright file="EvaluationTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
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
// This class tests the formula class's ability to evaluate a formula. This class does NOT check for syntax.
//
// </summary>

namespace CS3500.FormulaTests;

using System;
using CS3500.Formula;

/// <summary>
///  This is a test class for the Formula class we will be testing the classes ability to evaluate
///  formulas. We will not be testing for syntax.
/// </summary>
[TestClass]
public class EvaluationTests
{
    /// <summary>
    /// This test ensures that when evaluating a simple formula like 2 + 2 with the Evaluate method
    /// that it should return a valid answer of 4.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleEquation_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;
        Formula f = new ("2 + 2");
        Assert.AreEqual(4.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// This test ensures that evaluating a simple formula with a variable like 2 + x2 will evaluate to the
    /// correct answer (when the variable is set to be 5) so 7.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_OneVariable_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2 + x2");

        Assert.AreEqual(7.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// This test ensures that evaluating a simple formula with a variable like 2 + x2 + b3 will evaluate to the
    /// correct answer (when the variable is set to be 5) so 12.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_MultipleVariables_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2 + x2 + b3");

        Assert.AreEqual(12.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// This test ensures that evaluating when a divide by 0 happens through a number that Evaluate returns a Formula Error.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_DivideByZero_ReturnsFormulaError()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2/0");

        FormulaError actualOutput = (FormulaError)f.Evaluate(provideVariableValue);
        FormulaError expectedOutput = new FormulaError("Divide by 0 is NOT allowed!");

        Assert.AreEqual(expectedOutput.ToString(), actualOutput.ToString());
    }

    /// <summary>
    /// This test ensures that evaluating when a divide by 0 happens through a variable that Evaluate returns a Formula Error.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_DivideByZeroVariable_ReturnsFormulaError()
    {
        Formula.Lookup provideVariableValue = (name) => 0;

        Formula f = new ("2/x2");

        FormulaError actualOutput = (FormulaError)f.Evaluate(provideVariableValue);
        FormulaError expectedOutput = new FormulaError("Divide by 0 is NOT allowed!");

        Assert.AreEqual(expectedOutput.ToString(), actualOutput.ToString());
    }

    /// <summary>
    /// This test ensures that when an invalid variable (variable does not exist) that FormulaError
    /// is thrown.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_VariableDoesNotExist_ReturnsFormulaError()
    {
        double MyVariables(string name)
        {
            if (name == "A1")
            {
                return 2;
            }
            else if (name == "B1")
            {
                return 3;
            }
            else
            {
                throw new ArgumentException("I do not know that variable.");
            }
        }

        Formula f = new ("2 + a1 + b1 + b3");
        FormulaError actualOutput = (FormulaError)f.Evaluate(MyVariables);
        FormulaError expectedOutput = new FormulaError("Unknown variable: B3 please enter existing variables.");

        Assert.IsInstanceOfType(f.Evaluate(MyVariables), typeof(FormulaError));
        Assert.AreEqual(expectedOutput.ToString(), actualOutput.ToString());
    }

    /// <summary>
    /// Specifically tests that the "+" operator works with the evaluate method. For example in our case
    /// 2+2+2+2 should return 8.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleAddOperator_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2+2+2+2");

        Assert.AreEqual(8.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Specifically tests that the "*" operator works with the evaluate method. For example in our case
    /// 2*2*2*2 should return 16.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleMultiplicationOperator_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2*2*2*2");

        Assert.AreEqual(16.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Specifically tests that the "/" operator works with the evaluate method. For example in our case
    /// 1000/2/2/2 should return 125.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleDivisionOperator_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("1000/2/2/2");

        Assert.AreEqual(125.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Specifically tests that the "-" operator works with the evaluate method. For example in our case
    /// 10-2-2-2 should return 4.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleSubtractOperator_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("10-2-2-2");

        Assert.AreEqual(4.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Specifically tests that the "()" operators works with the evaluate method. For example in our case
    /// (5-2)*2 should return 6.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleParenthesesUsedInMeaningfulWay_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("(5-2)*2");

        Assert.AreEqual(6.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method using multiple operators in conjunctions with each other respects PEMDAS and
    /// returns proper result. For example in our case 2+6-3*2+6/2 should return 5.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_ComplexFormulaUsingMultipleOperators_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2+6-3*2+6/2");

        Assert.AreEqual(5.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method using multiple operators in conjunctions with each other respects PEMDAS and
    /// returns proper result. For example in our case 2+(6-3)*2+(6/2) should return 16.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_ComplexFormulaUsingParentheses_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2+(6-3)*2+(6+2)");

        Assert.AreEqual(16.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method using multiple operators in conjunctions with each other respects PEMDAS and
    /// returns proper result. For example in our case 2+(6-x3)*2+(6/v4) should return 8. When our func defines that
    /// variables equal 3.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_ComplexFormulaUsingParenthesesVariablesAndOperatorsFunc_ReturnsValid()
    {
        double MyVariables(string name)
        {
            return 3;
        }

        Formula f = new ("2+(6-x3)*2+(6/v4)");

        Assert.AreEqual(10.0, f.Evaluate(MyVariables));
    }

    /// <summary>
    /// Tests that the evaluate method using multiple operators in conjunctions with each other respects PEMDAS and
    /// returns proper result. For example in our case 2+(6-x3)*2+(6/v4) should return 8. When our Lambda defines
    /// that all variables should be 3.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_ComplexFormulaUsingLambdaForEvaluate_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 3;

        Formula f = new ("2+(6-x3)*2+(6/v4)");

        Assert.AreEqual(10.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula which is divided by a variable.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_DividingAVariable_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+2/x2");

        Assert.AreEqual(4.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a multiplication before parentheses appear.
    /// Basically checks that PEMDAS works.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_MultiplyBeforeParenthesis_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+2*(6+2)");

        Assert.AreEqual(18.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a division before parentheses appear.
    /// Basically checks that PEMDAS works.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_DivideBeforeParenthesis_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+2/(6+2)");

        Assert.AreEqual(2.25, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a division before parentheses appear.
    /// Also there is a subtract in the parenthesis Basically checks that PEMDAS works.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_DivideBeforeParenthesisAndSubtractInside_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+2/(6-2)");

        Assert.AreEqual(2.5, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a division before parentheses appear.
    /// Also there is a subtract in the parenthesis Basically checks that PEMDAS works.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_AddBeforeParenthesisAndSubtractInside_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+2-(6-2)+3");

        Assert.AreEqual(3.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a division before parentheses appear.
    /// Also there is a subtract in the parenthesis Basically checks that PEMDAS works.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_IntenseEquation_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+2-(6-2)+3-(32*4)+(20-10)-(2/2)+2-5");

        Assert.AreEqual(-119.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that when the formula has multiple things in the parenthesis that Evaluate is able to
    /// successfully solve the equation.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_MultipleThingsInParenthesis_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2*(2+2+2-2)");

        Assert.AreEqual(8.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with parenthesis and add still works as expected with the 
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleParenthesisAdd_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("(2+2)");

        Assert.AreEqual(4.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with parenthesis and subtract still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleParenthesisSub_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("(2-2)");

        Assert.AreEqual(0.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with parenthesis and divide still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleParenthesisDiv_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("(2/2)");

        Assert.AreEqual(1.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with parenthesis and multiply still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleParenthesisMultiply_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("(2*2)");

        Assert.AreEqual(4.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with parenthesis and no operator and a number still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleNumberParenthesis_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("(2)");

        Assert.AreEqual(2.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with parenthesis and no operator and a var still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleVarParenthesis_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("(x2)");

        Assert.AreEqual(1.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with no operator and a var still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleVar_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("x2");

        Assert.AreEqual(1.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests to make sure that a simple equation with just a number and no operator still works as expected with the
    /// Evaluate method.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_SimpleNumber_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2");

        Assert.AreEqual(2.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a division before parentheses appear.
    /// And on top of that makes sure it can still catch a divide by 0.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_DivideBeforeParenthesisLeadsToDivideBy0_ReturnsFormulaError()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2/(6-6)");

        FormulaError actualOutput = (FormulaError)f.Evaluate(provideVariableValue);
        FormulaError expectedOutput = new FormulaError("Divide by 0 is NOT allowed!");

        Assert.IsInstanceOfType(f.Evaluate(provideVariableValue), typeof(FormulaError));
        Assert.AreEqual(expectedOutput.ToString(), actualOutput.ToString());
    }

    /// <summary>
    /// Tests that the evaluate method can evaluate a formula has a division before parentheses appear.
    /// Checks PEMDAS.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_AddBeforeParenthesisLeadsToDivideBy0_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("2+(6-6)");

        Assert.AreEqual(2.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Checks to make sure that variables can be multiplied with each other.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_MultiplyVariablesLikeCrazy_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("x2 * v3 * b7 * v3");

        Assert.AreEqual(1.0, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Checks to make sure that variables can be multiplied with each other once.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_MultiplyVariablesOnce_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 1;

        Formula f = new ("x2 / v3");

        Assert.AreEqual(1.0, f.Evaluate(provideVariableValue));
    }


    // == Operator Tests ---------------------------------------

    /// <summary>
    /// Tests that two formulas that are complex and equal are equal according to the == operator.
    /// Therefore returning true.
    /// </summary>
    [TestMethod]
    public void FormulaEqualEqual_ComplexFormulaEqual_ReturnsTrue()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2+2*2+e3/6");
        Assert.IsTrue(complexFormulaOne == complexFormulaTwo);
    }

    /// <summary>
    ///  Tests that two formulas that are complex and not equal are  not equal according to the == operator.
    ///  Therefore returning False.
    /// </summary>
    [TestMethod]
    public void FormulaEqualEqual_ComplexFormulaNotEqual_ReturnsFalse()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2000+221231*2213+e3/6431");
        Assert.IsFalse(complexFormulaOne == complexFormulaTwo);
    }

    /// <summary>
    ///  Tests that two formulas that are simple and equal are equal according to the == operator.
    ///  Therefore returning true.
    /// </summary>
    [TestMethod]
    public void FormulaEqualEqual_SimpleFormulaEqual_ReturnsTrue()
    {
        Formula simpleFormulaOne = new ("2+20e-1");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsTrue(simpleFormulaOne == simpleFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple and not equal are  not equal according to the == operator.
    /// Therefore returning False.
    /// </summary>
    [TestMethod]
    public void FormulaEqualEqual_SimpleFormulaNotEqual_ReturnsFalse()
    {
        Formula simpleFormulaOne = new ("3+20e-1");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsFalse(simpleFormulaOne == simpleFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple, use variables, and equal are equal according to the == operator.
    /// Therefore returning true.
    /// </summary>
    [TestMethod]
    public void FormulaEqualEqual_SimpleFormulaWithVariablesEqual_ReturnsTrue()
    {
        Formula simpleFormulaOne = new ("2+20e-1+e3");
        Formula simpleFormulaTwo = new ("2+2+e3");
        Assert.IsTrue(simpleFormulaOne == simpleFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple, use variables, and not equal are  not equal according to the == operator.
    /// Therefore returning False.
    /// </summary>
    [TestMethod]
    public void FormulaEqualEqual_SimpleFormulaWithVariablesNotEqual_ReturnsFalse()
    {
        Formula simpleFormulaOne = new ("2+20e-1+e3");
        Formula simpleFormulaTwo = new ("2+2+v4");
        Assert.IsFalse(simpleFormulaOne == simpleFormulaTwo);
    }

    // != tests ---------------------------

    /// <summary>
    /// Tests that two formulas that are complex and equal are equal according to the != operator. Therefore returning False.
    /// </summary>
    [TestMethod]
    public void FormulaNotEqual_ComplexFormulaEqual_ReturnsFalse()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2+2*2+e3/6");
        Assert.IsFalse(complexFormulaOne != complexFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are complex and not equal are not equal according to the != operator.
    /// Therefore returning true.
    /// </summary>
    [TestMethod]
    public void FormulaNotEqual_ComplexFormulaNotEqual_ReturnsTrue()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2000+221231*2213+e3/6431");
        Assert.IsTrue(complexFormulaOne != complexFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple and equal are equal according to the != operator. Therefore returning False.
    /// </summary>
    [TestMethod]
    public void FormulaNotEqual_SimpleFormulaEqual_ReturnsFalse()
    {
        Formula simpleFormulaOne = new ("2+20e-1");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsFalse(simpleFormulaOne != simpleFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple and not equal are not equal according to the != operator. Therefore returning true.
    /// </summary>
    [TestMethod]
    public void FormulaNotEqual_SimpleFormulaNotEqual_ReturnsTrue()
    {
        Formula simpleFormulaOne = new ("3+20e-1");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsTrue(simpleFormulaOne != simpleFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple, use variables, and equal are equal according to the =!= operator.
    /// Therefore returning false.
    /// </summary>
    [TestMethod]
    public void FormulaNotEqual_SimpleFormulaWithVariablesEqual_ReturnsFalse()
    {
        Formula simpleFormulaOne = new ("2+20e-1+e3");
        Formula simpleFormulaTwo = new ("2+2+e3");
        Assert.IsFalse(simpleFormulaOne != simpleFormulaTwo);
    }

    /// <summary>
    /// Tests that two formulas that are simple, use variables, and not equal are  not equal according to the != operator.
    /// Therefore returning true.
    /// </summary>
    [TestMethod]
    public void FormulaNotEqual_SimpleFormulaWithVariablesNotEqual_ReturnsTrue()
    {
        Formula simpleFormulaOne = new ("2+20e-1+e3");
        Formula simpleFormulaTwo = new ("2+2+v4");
        Assert.IsTrue(simpleFormulaOne != simpleFormulaTwo);
    }

    // Equals tests -----------------

    /// <summary>
    /// Tests that the Equals method is able to evaluate two formulas as equivalent when they are equivalent.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_SimpleTwoFormulasEquivalent_True()
    {
        Formula simpleFormulaOne = new ("2+20e-1");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsTrue(simpleFormulaOne.Equals(simpleFormulaTwo));
    }

    /// <summary>
    ///  Tests that the Equals method is able to return false if a null object has been passed as the parameter.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_NullInParameter_False()
    {
        Formula simpleFormulaOne = new ("2+20e-1");
        Assert.IsFalse(simpleFormulaOne.Equals(null));
    }

    /// <summary>
    ///  Tests that the Equals method is able to return false if a non-formula object has been passed as the parameter.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_OtherObjectInParameter_False()
    {
        Formula simpleFormulaOne = new ("2+20e-1");
        Assert.IsFalse(simpleFormulaOne.Equals(new object()));
    }

    /// <summary>
    ///  Tests that the Equals method is able to evaluate two simple formulas as not equal when they are not equal.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_TwoSimpleFormulasNotEqual_False()
    {
        Formula simpleFormulaOne = new ("3+20e-1");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsFalse(simpleFormulaOne.Equals(simpleFormulaTwo));
    }

    /// <summary>
    /// Tests that the Equals method is able to evaluate two complex formulas as equal when they are equal.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_TwoComplexFormulasEquivalent_True()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2+2*2+e3/6");
        Assert.IsTrue(complexFormulaOne.Equals(complexFormulaTwo));
    }

    /// <summary>
    ///  Tests that the Equals method is able to evaluate two complex formulas as not equal when they are not equal.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_TwoComplexFormulasNotEquivalent_False()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2000+221231*2213+e3/6431");
        Assert.IsFalse(complexFormulaOne.Equals(complexFormulaTwo));
    }

    /// <summary>
    ///   Tests that the Equals method is able to evaluate two complex formulas with variables as  equal when they are equal.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_TwoFormulasWithVariablesEquivalent_True()
    {
        Formula simpleFormulaOne = new ("2+20e-1+e3");
        Formula simpleFormulaTwo = new ("2+2+e3");
        Assert.IsTrue(simpleFormulaOne.Equals(simpleFormulaTwo));
    }

    /// <summary>
    ///  Tests that the Equals method is able to evaluate two complex formulas with variables as not equal when they are not equal.
    /// </summary>
    [TestMethod]
    public void FormulaEquals_TwoFormulasWithVariablesNotEquivalent_False()
    {
        Formula simpleFormulaOne = new ("2+20e-1+e3");
        Formula simpleFormulaTwo = new ("2+2+v4");
        Assert.IsFalse(simpleFormulaOne.Equals(simpleFormulaTwo));
    }

    // GetHashCode Tests --------------------------------

    /// <summary>
    /// Test to check that the HashCode method DOES return the same HashCode for two completely equivalent formulas.
    /// </summary>
    [TestMethod]
    public void FormulaGetHashCode_TwoEquivalentFormulas_ReturnSameHashCode()
    {
        Formula complexFormulaOne = new ("2.0000+2.0*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2+2*2+e3/6");
        int c1 = complexFormulaOne.GetHashCode();
        int c2 = complexFormulaTwo.GetHashCode();

        Assert.IsTrue(c1 == c2);
    }

    /// <summary>
    /// Test to check that the HashCode method DOES NOT return the same HashCode for two completely Different formulas.
    /// </summary>
    [TestMethod]
    public void FormulaGetHashCode_TwoDifferentFormulas_DoNotReturnSameHashCode()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2000+221231*2213+e3/6431");
        int c1 = complexFormulaOne.GetHashCode();
        int c2 = complexFormulaTwo.GetHashCode();

        Assert.IsTrue(c1 != c2);
    }

    /// <summary>
    ///  Test to check that the HashCode method DOES return the same HashCode for two syntactically equivalent formulas.
    /// </summary>
    [TestMethod]
    public void FormulaGetHashCode_TwoSyntacticallyEquivalentFormulas_ReturnSameHashCode()
    {
        Formula complexFormulaOne = new ("2.0000+2.0*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2+2*2+e3/6");
        int c1 = complexFormulaOne.GetHashCode();
        int c2 = complexFormulaTwo.GetHashCode();

        Assert.IsTrue(c1 == c2);
    }

    /// <summary>
    /// Test to check that the HashCode method DOES NOT return the same HashCode for two formulas that are completely different,
    /// but share the same final answer.
    /// </summary>
    [TestMethod]
    public void FormulaGetHashCode_TwoFormulasWithSameFinalAnswerButDifferentSyntax_DoNotReturnSameHashCode()
    {
        Formula complexFormulaOne = new ("2+2*4+3-7");
        Formula complexFormulaTwo = new ("2+4");

        int c1 = complexFormulaOne.GetHashCode();
        int c2 = complexFormulaTwo.GetHashCode();

        Assert.IsTrue(c1 != c2);
    }
}