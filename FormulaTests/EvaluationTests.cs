// <copyright file="EvaluationTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Joel Rodriguez,  Profs Joe, Danny, and Jim.
// Partner:   None
// Date:      September 12, 2024
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
        Assert.AreEqual(4, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(7, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(12, f.Evaluate(provideVariableValue));
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

        Formula f = new ("2 + x2 + b3");

        Assert.ThrowsException<ArgumentException>(() => f.Evaluate(MyVariables));

        // or?
        Assert.AreEqual(new FormulaError("I do not know that variable."), f.Evaluate(MyVariables));
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

        Assert.AreEqual(8, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(16, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(125, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(4, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(6, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(5, f.Evaluate(provideVariableValue));
    }

    /// <summary>
    /// Tests that the evaluate method using multiple operators in conjunctions with each other respects PEMDAS and
    /// returns proper result. For example in our case 2+(6-3)*2+(6/2) should return 11.
    /// </summary>
    [TestMethod]
    public void FormulaEvaluate_ComplexFormulaUsingParentheses_ReturnsValid()
    {
        Formula.Lookup provideVariableValue = (name) => 5;

        Formula f = new ("2+(6-3)*2+(6+2)");

        Assert.AreEqual(11, f.Evaluate(provideVariableValue));
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

        Assert.AreEqual(8, f.Evaluate(MyVariables));
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

        Assert.AreEqual(8, f.Evaluate(provideVariableValue));
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
        Formula simpleFormulaOne = new ("2+2");
        Formula simpleFormulaTwo = new ("2+2");
        Assert.IsTrue(simpleFormulaOne.GetHashCode == simpleFormulaTwo.GetHashCode);
    }

    /// <summary>
    /// Test to check that the HashCode method DOES NOT return the same HashCode for two completely Different formulas.
    /// </summary>
    [TestMethod]
    public void FormulaGetHashCode_TwoDifferentFormulas_DoNotReturnSameHashCode()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2000+221231*2213+e3/6431");
        Assert.IsTrue(complexFormulaOne.GetHashCode != complexFormulaTwo.GetHashCode);
    }

    /// <summary>
    ///  Test to check that the HashCode method DOES return the same HashCode for two syntactically equivalent formulas.
    /// </summary>
    [TestMethod]
    public void FormulaGetHashCode_TwoSyntacticallyEquivalentFormulas_ReturnSameHashCode()
    {
        Formula complexFormulaOne = new ("2.0000+20e-1*2.000+e3/6.00000");
        Formula complexFormulaTwo = new ("2+2*2+e3/6");
        Assert.IsTrue(complexFormulaOne.GetHashCode == complexFormulaTwo.GetHashCode);
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
        Assert.IsTrue(complexFormulaOne.GetHashCode != complexFormulaTwo.GetHashCode);
    }
}