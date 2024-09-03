// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors> Joel Rodriguez </authors>
// <date> August 29, 2024 </date>

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
        _ = new Formula("");  // note: it is arguable that you should replace "" with string.Empty for readability and clarity of intent (e.g., not a cut and paste error or a "I forgot to put something there" error)
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
        _ = new Formula("72 + ebsja232");
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
    public void FormulaConstructor_TestClosingParenthesissSimple_Valid()
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
        _ = new Formula(")(6+7");
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
    ///     and throws no exceptions
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
    ///     not accepted bythe constructor (the constructor should throw a FormulaFormatException).
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

    // TO STRING TESTS

    [TestMethod]
    public void ToString_TestBasicFormula_Valid()
    {
        Formula testFormula = new Formula("1.0000 + x7 + 36");
        string expectedString = "1+X7+36";
        Assert.AreEqual(expectedString, testFormula.ToString());
    }
}