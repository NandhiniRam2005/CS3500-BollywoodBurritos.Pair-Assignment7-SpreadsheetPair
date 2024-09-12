// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Joel Rodriguez,  Profs Joe, Danny, and Jim.
// Partner:   None
// Date:      September 6, 2024
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
// This class tests the formula class for consistency and checks the constructor, ToString, and GetVariables.
// </summary>

namespace CS3500.FormulaTests;

using CS3500.Formula;

/// <summary>
///   <para>
///     The following class shows the basics of how to use the MSTest framework,
///     including:
///   </para>
///   <list type="number">
///     <item> How to catch exceptions. </item>
///     <item> How a test of valid code should look. </item>
///   </list>
/// </summary>
[TestClass]
public class FormulaSyntaxTests
{
    // --- Tests for One Token Rule ---

    /// <summary>
    ///   <para>
    ///     This test makes sure the right kind of exception is thrown
    ///     when trying to create a formula with no tokens.
    ///   </para>
    ///   <remarks>
    ///     <list type="bullet">
    ///       <item>
    ///         We use the _ (discard) notation because the formula object
    ///         is not used after that point in the method.  Note: you can also
    ///         use _ when a method must match an interface but does not use
    ///         some of the required arguments to that method.
    ///       </item>
    ///       <item>
    ///         string.Empty is often considered best practice (rather than using "") because it
    ///         is explicit in intent (e.g., perhaps the coder forgot to but something in "").
    ///       </item>
    ///       <item>
    ///         The name of a test method should follow the MS standard:
    ///         https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    ///       </item>
    ///       <item>
    ///         All methods should be documented, but perhaps not to the same extent
    ///         as this one.  The remarks here are for your educational
    ///         purposes (i.e., a developer would assume another developer would know these
    ///         items) and would be superfluous in your code.
    ///       </item>
    ///       <item>
    ///         Notice the use of the attribute tag [ExpectedException] which tells the test
    ///         that the code should throw an exception, and if it doesn't an error has occurred;
    ///         i.e., the correct implementation of the constructor should result
    ///         in this exception being thrown based on the given poorly formed formula.
    ///       </item>
    ///     </list>
    ///   </remarks>
    ///   <example>
    ///     <code>
    ///        // here is how we call the formula constructor with a string representing the formula
    ///        _ = new Formula( "5+5" );
    ///     </code>
    ///   </example>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestNoTokens_Invalid()
    {
        _ = new Formula(string.Empty);  // note: it is arguable that you should replace "" with string.Empty for readability and clarity of intent (e.g., not a cut and paste error or a "I forgot to put something there" error)
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple number token is recognized.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "77" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOneNumberToken_Valid()
    {
        _ = new Formula("77");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple variable token is recognized.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "a7" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOneVariableToken_Valid()
    {
        _ = new Formula("a7");
    }

    /// <summary>
    ///   <para>
    ///     Make sure only a space token is not a valid token on its own. It should throw a FormulaFormatException.
    ///     This also tests the first token rule and the last token rule.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula " " is an invalid formula which should cause an errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSpaceTokenOnly_Invalid()
    {
        _ = new Formula(" ");
    }

    // --- Tests for Valid Token Rule ---

    /// <summary>
    ///   <para>
    ///     Make sure an unrecognized token "#" is recognized as invalid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "#" is not a valid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestOneToken_Invalid()
    {
        _ = new Formula("#");
    }

    /// <summary>
    ///   <para>
    ///     Make sure an unrecognized tokens are recognized as invalid.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestMultipleInvalidToken_Invalid()
    {
        _ = new Formula("1 + udiahfkj + 12");
    }

    /// <summary>
    ///   <para>
    ///     Make sure an invalid variable token "aaa" is recognized as invalid when mixed with other valid tokens.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "3+2+aaa*7" is not a valid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestMixedBadVariableToken_Invalid()
    {
        _ = new Formula("3+2+aaa*7");
    }

    /// <summary>
    /// Tests if a bad variable token such as "x1x" throws an exception.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestOneAndAHalfVariables_Invalid()
    {
        _ = new Formula("x1x");
    }

    /// <summary>
    ///   <para>
    ///     Make sure an invalid token "&" is recognized as invalid when mixed with other valid tokens.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "62+11+x2+7&-12+&" is not a valid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestBadTokenMixed_Invalid()
    {
        _ = new Formula("62+11+x2+7&-12+&");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a longer variable such as
    ///     "ebsja232", is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + ebsja232" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLongerVariable_Valid()
    {
        _ = new Formula("72 + ebsja23");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a variable using
    ///     a lowercase e, is recognized as valid. This test doubles and tests the extra following rule.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + e232" is not a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestVariableWithE_Valid()
    {
        _ = new Formula("72 + e232");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a variable using
    ///     a capital E, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + E232" is not a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestVariableWithCapitalE_Valid()
    {
        _ = new Formula("72 + E232");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces, is recognized as valid.
    ///     This test doubles and tests the extra following rule.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 5" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSpaceToken_Valid()
    {
        _ = new Formula("72 + 5");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and scientific notation using
    ///     a lowercase e and decimals, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 3.5e3" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestDecimalScientificToken_Valid()
    {
        _ = new Formula("72 + 3.5e3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and scientific notation using
    ///     a whole number and lowercase e, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 3.5e3" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestWholeScientificToken_Valid()
    {
        _ = new Formula("72 + 3e3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and scientific notation using
    ///     a decimal, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 3.5E3" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestDecimalCapitalScientificToken_Valid()
    {
        _ = new Formula("72 + 3.5E3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and scientific notation using
    ///     a negative number, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 3.5E-3" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNegativeScientificToken_Valid()
    {
        _ = new Formula("72 + 3.5E-3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula with valid tokens, using scientific notation with no number after e, is recognized
    ///     as invalid and throws a FormulaFormatException as Expected.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "72 + 3.5e" is a invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestScientificTokenWithoutNumberAfterE_Valid()
    {
        _ = new Formula("72 + 3.5e");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a decimal
    ///     number, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 3.245" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestRegularDecimal_Valid()
    {
        _ = new Formula("72 + 3.245");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a decimal which
    ///     begins with 0, is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 0.245" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestZeroDecimal_Valid()
    {
        _ = new Formula("72 + 0.245");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a decimal without a front
    ///     number (.245), is recognized as valid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + .245" is  a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOnlyDotDecimal_Valid()
    {
        _ = new Formula("72 + .245");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple formula with valid tokens, including spaces and a decimal without a decimal
    ///     number (2.), is recognized as valid and does not throw a FormulaFormatException.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "72 + 2." is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestDotWithNoNumber_Valid()
    {
        _ = new Formula("72 + 2.");
    }

    // --- Tests for Closing Parenthesis Rule

    /// <summary>
    ///   <para>
    ///     Make sure a simple valid formula with valid tokens and with closing parenthesis never
    ///     exceeding that of regular parenthesis is recognized as valid. This test doubles and tests
    ///     the extra following rule.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to not throw an exception, i.e., it succeeds.
    ///     In other words, the formula "(7+32)" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestClosingParenthesisSimple_Valid()
    {
        _ = new Formula("(7+32)");
    }

    /// <summary>
    ///   <para>
    ///     Make sure an invalid formula where the number of closing parenthesis exceeds the
    ///     number of opening parenthesis is recognized as invalid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula ")(6+7" is not a valid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestTooManyClosingParenthesis_Invalid()
    {
        _ = new Formula("5 + 6)(6+7");
    }

    // --- Tests for Balanced Parentheses Rule

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula where the number of parenthesis is balanced
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "((7+32))" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestBalancedParenthesis_Valid()
    {
        _ = new Formula("((7+32))");
    }

    /// <summary>
    ///   <para>
    ///     Make sure an invalid formula where the number of closing parenthesis exceeds the
    ///     number of opening parenthesis, which in turn is an equation where the number of
    ///     parenthesis is not balanced is recognized as invalid.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "((7+32)" is not a valid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestUnbalancedParenthesis_Invalid()
    {
        _ = new Formula("((7+32)");
    }

    // --- Tests for First Token Rule

    /// <summary>
    ///   <para>
    ///     Make sure a formula in which there is a space as the first token is accepted by the constructor
    ///     and throws no exceptions.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula " 1+1" is a valid formula which should not cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSpacesFollowingOpeningParenthesis_Valid()
    {
        _ = new Formula(" 1+1");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula is accepted by the constructor (the constructor
    ///     should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "1211+1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenNumber_Valid()
    {
        _ = new Formula("1211+1");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula which starts with a variable
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "x7+32" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenVariable_Valid()
    {
        _ = new Formula("x7+32");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula which starts with an opening parenthesis
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "(x7)+32" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenOpeningParenthesis_Valid()
    {
        _ = new Formula("(x7)+32");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula where the first token is an operator is not seen as
    ///     valid and throws the correct exception.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "+32-16" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestOperatorStart_Invalid()
    {
        _ = new Formula("+32-16");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula where the first token is an invalid variable is not seen as
    ///     valid and throws the correct exception.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "$3" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestFirstNonVariable_Invalid()
    {
        _ = new Formula("$3");
    }

    // --- Tests for  Last Token Rule ---

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula where the last token is a number
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "71+1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenIsNumber_Valid()
    {
        _ = new Formula("71+1");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula where the last token is a closing parenthesis
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "71+(1)" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenIsClosingParenthesis_Valid()
    {
        _ = new Formula("71+(1)");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula where a variable is the last token
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "71+x1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenIsVariable_Valid()
    {
        _ = new Formula("71+x1");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a invalid formula where the last token is an opening parenthesis is not
    ///     accepted by the constructor (the constructor should throw a FileFormatException).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "(71+x1)(" is an invalid formula which should cause an errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestLastTokenIsOpeningParenthesis_Invalid()
    {
        _ = new Formula("(71+x1)(");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a invalid formula where the last token is an operator is not
    ///     accepted by the constructor (the constructor should throw a FileFormatException).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "71+x1+" is an invalid formula which should cause an errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestLastTokenIsOperator_Invalid()
    {
        _ = new Formula("71+x1+");
    }

    // --- Tests for Parentheses/Operator Following Rule ---

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formula where a number follows an opening parenthesis is accepted by
    ///     the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "7+(1)+3" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNumberFollowsOpeningParenthesis_Valid()
    {
        _ = new Formula("7+(1)+3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formula where a variable follows an opening parenthesis is accepted by
    ///     the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "7+(x1)+3" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestVariableFollowsOpeningParenthesis_Valid()
    {
        _ = new Formula("7+(x1)+3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formula where a opening parenthesis follows an opening parenthesis
    ///     is accepted by the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "7+((1))+3" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestOpeningParenthesisFollowsOpeningParenthesis_Valid()
    {
        _ = new Formula("7+((1))+3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formula where a number follows an operator is accepted by
    ///     the constructor (the constructor should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "1+1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestNumberFollowsOperator_Valid()
    {
        _ = new Formula("1+1");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple invalid formula where an operator follows an operator is not accepted by
    ///     the constructor (the constructor should throw a FormulaFormatException).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "1++1" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestOperatorFollowsOperator_Invalid()
    {
        _ = new Formula("1++1");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a simple invalid formula where a closing parenthesis follows an opening parenthesis is
    ///     not accepted by the constructor (the constructor should throw a FormulaFormatException).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "()" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestClosingParenthesisFollowsOpeningParenthesis_Invalid()
    {
        _ = new Formula("()");
    }

    // --- Tests for Extra Following Rule ---

    /// <summary>
    ///   <para>
    ///     Make sure a formula in which there is a space between two numbers and no operator that the constructor throws
    ///     a FormulaFormatException as expected.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "1  1" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSpacesFollowingNumber_Invalid()
    {
        _ = new Formula("(1 1)");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula in which there is a space between a number and a variable and no
    ///     operator that the constructor throws a FormulaFormatException as expected.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "x1  1" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSpacesFollowingVariable_Invalid()
    {
        _ = new Formula("(x1 1)");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula in which there is a space (no valid token) between an opening
    ///     parenthesis and another valid token and no operator throws a FormulaFormatException as expected.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "(1+1) 2+3" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestNoValidTokenFollowingClosingParenthesis_Invalid()
    {
        _ = new Formula("(1+1) 2+3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula in which there is multiple spaces between two valid tokens and no
    ///     operator in between that the constructor throws a FormulaFormatException as expected.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is expected to throw an exception, i.e., it fails.
    ///     In other words, the formula "(1+1)        2+3" is an invalid formula which should cause an error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestMultipleSpacesFollowing_Invalid()
    {
        _ = new Formula("(1+1)       2+3");
    }

    /// <summary>
    ///   <para>
    ///     Make sure a formula in which there is multiple spaces between two valid tokens and there is an
    ///     operator in between that the constructor accepts it is a formula.
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is  not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "(1+1)    +    2+3" is a valid formula which should cause no error.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestMultipleSpacesFollowing_Valid()
    {
        _ = new Formula("1+1   +    2+3");
    }

    // --- Tests for ToString ---

    /// <summary>
    ///  This test check to see if a basic formula can be successfully translated using the ToString method.
    /// </summary>
    [TestMethod]
    public void ToString_TestBasicFormula_Valid()
    {
        Formula testFormula = new Formula("1.0000 + x7 + 36");
        string expectedString = "1+X7+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    ///  This test ensures that the ToString method works with scientific notation.
    /// </summary>
    [TestMethod]
    public void ToString_TestFormulaWithScientificNotation_Valid()
    {
        Formula testFormula = new Formula("10e-1 + x7 + 36E-2");
        string expectedString = "1+X7+0.36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// This test ensures that the ToString method is able to translate variables that use both capital and lower case.
    /// </summary>
    [TestMethod]
    public void ToString_TestFormulaWithLowerAndUpperCaseVariables_Valid()
    {
        Formula testFormula = new Formula("1.0000 + x7 + 36 + Y7");
        string expectedString = "1+X7+36+Y7";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// This test ensures that decimals can be properly translated using the ToString method.
    /// </summary>
    [TestMethod]
    public void ToString_TestDecimalFormula_Valid()
    {
        Formula testFormula = new Formula("1.234 + x7 + 36");
        string expectedString = "1.234+X7+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// This test ensures that scientific notation numbers that use positive numbers to make the number bigger
    /// can be translated.
    /// </summary>
    [TestMethod]
    public void ToString_TestPositiveScientificNotation_Valid()
    {
        Formula testFormula = new Formula("1.2e1 + x7 + 3.6e2");
        string expectedString = "12+X7+360";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method will not return invalid results if handed
    /// formulas with odd spacing.
    /// </summary>
    [TestMethod]
    public void ToString_TestOddSpacing_Valid()
    {
        Formula testFormula = new Formula("1.0000               + x7 +       36");
        string expectedString = "1+X7+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method is able to handle all operators.
    /// </summary>
    [TestMethod]
    public void ToString_TestOperators_Valid()
    {
        Formula testFormula = new Formula("1.0000 + x7 * 36 / 8 - 9");
        string expectedString = "1+X7*36/8-9";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method can properly translate parentheses.
    /// </summary>
    [TestMethod]
    public void ToString_TestParenthesis_Valid()
    {
        Formula testFormula = new Formula("(1.0000 + x7 ) + 36");
        string expectedString = "(1+X7)+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method can properly translate a small scientific number.
    /// </summary>
    [TestMethod]
    public void ToString_TestSmallScientific_Valid()
    {
        Formula testFormula = new Formula("(1e-10 + x7 ) + 36");
        string expectedString = "(1E-10+X7)+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method can properly translate a huge scientific notation number.
    /// </summary>
    [TestMethod]
    public void ToString_TestBigScientific_Valid()
    {
        Formula testFormula = new Formula("(1e20 + x7 ) + 36");
        string expectedString = "(1E+20+X7)+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method can properly translate a small decimal.
    /// </summary>
    [TestMethod]
    public void ToString_TestSmallDecimal_Valid()
    {
        Formula testFormula = new Formula("(0.000005 + x7 ) + 36");
        string expectedString = "(5E-06+X7)+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method can properly translate a huge decimal.
    /// </summary>
    [TestMethod]
    public void ToString_TestBigDecimal_Valid()
    {
        Formula testFormula = new Formula("(1000000.723 + x7 ) + 36");
        string expectedString = "(1000000.723+X7)+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    /// <summary>
    /// Test to ensure that the ToString method can properly translate a huge integer.
    /// </summary>
    [TestMethod]
    public void ToString_TestBigInt_Valid()
    {
        Formula testFormula = new Formula("(100000000000000000000 + x7 ) + 36");
        string expectedString = "(1E+20+X7)+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }

    // --- Tests for GetVariables ---

    /// <summary>
    /// This test ensures that the get variables method returns the proper set even when a formula contains
    /// multiple of the same variable.
    /// </summary>
    [TestMethod]
    public void GetVariables_TestMultipleIdenticalVariables_Valid()
    {
        Formula testFormula = new Formula("x7 + y3 + x7 + z4+ y3 + h8");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        expectedVariables.Add("X7");
        expectedVariables.Add("Y3");
        expectedVariables.Add("Z4");
        expectedVariables.Add("H8");
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Test to ensure the GetVariables method is able to return a single variable.
    /// </summary>
    [TestMethod]
    public void GetVariables_TestOneVariable_Valid()
    {
        Formula testFormula = new Formula("x7");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        expectedVariables.Add("X7");
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Test to ensure the GetVariables returns the proper set of variables even when there is a
    /// long formula with the same duplicate variable.
    /// </summary>
    [TestMethod]
    public void GetVariables_TestAllTheSameVariable_Valid()
    {
        Formula testFormula = new Formula("x7 + x7 + x7+ x7");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        expectedVariables.Add("X7");
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Test to ensure the GetVariables method returns the proper set of Variables even when there are the same
    /// variables with different cases in the formula.
    /// </summary>
    [TestMethod]
    public void GetVariables_SameVariableDifferentCase_Valid()
    {
        Formula testFormula = new Formula("x7 + y3 + X7 + Y3");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        expectedVariables.Add("X7");
        expectedVariables.Add("Y3");
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// This test ensures that formula with no variables returns an empty set when calling GetVariables.
    /// </summary>
    [TestMethod]
    public void GetVariables_NoVariablesReturnsEmptySet_Valid()
    {
        Formula testFormula = new Formula("2+2+7/2");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// This test ensures that formula with a variable only at the end returns the proper variables when calling
    /// GetVariables.
    /// </summary>
    [TestMethod]
    public void GetVariables_VariableOnlyAtEnd_Valid()
    {
        Formula testFormula = new Formula("2+2+7/x2");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        expectedVariables.Add("X2");
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// This test ensures that formula with a variable only at the start returns the proper variables when calling
    /// GetVariables.
    /// </summary>
    [TestMethod]
    public void GetVariables_VariableOnlyAtStart_Valid()
    {
        Formula testFormula = new Formula("x2+2+7/2");
        HashSet<string> variables = (HashSet<string>)testFormula.GetVariables();
        HashSet<string> expectedVariables = new HashSet<string>();
        expectedVariables.Add("X2");
        int expectedSize = expectedVariables.Count;
        int actualSize = variables.Count;

        bool sameContents = variables.SetEquals(expectedVariables);
        Assert.AreEqual(expectedSize, actualSize);
        Assert.IsTrue(sameContents);
    }
}