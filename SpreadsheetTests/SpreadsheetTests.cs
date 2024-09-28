// <copyright file="SpreadsheetTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

namespace CS3500.SpreadsheetTests;

using System;
using CS3500.Spreadsheet;
using CS3500.Formula;

/// <summary>
/// Author:    Joel Rodriguez,  Profs Joe, Danny, and Jim.
/// Partner:   None
/// Date:      September 20, 2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and [Joel Rodriguez] - This work may not
///            be copied for use in Academic Coursework.
///
/// I, Joel Rodriguez, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All
/// references used in the completion of the assignments are cited
/// in my README file.
///
/// File Contents
///
///  This is a test class for the Spreadsheet class we will be testing the classes ability to find
///  circular dependencies. Find invalid names. And correctly assess which cells are direct dependents or
///  cells that need to be changed.
///
/// </summary>

/// <summary>
///  This is a test class for the Spreadsheet class we will be testing the classes ability to find
///  circular dependencies. Find invalid names. And correctly assess which cells are direct dependents or
///  cells that need to be changed.
/// </summary>
[TestClass]
public class SpreadsheetTests
{
    // --------------- Spreadsheet Constructor test ----------------------

    /// <summary>
    /// Test that ensures the spreadsheet constructor can be used and does not thrown an exception of any kind.
    /// </summary>
    [TestMethod]
    public void SpreadsheetConstructor_NewConstructor_DoesNotThrowException()
    {
        Spreadsheet sheet = new Spreadsheet();
    }

    // --------------- GetNamesOfAllNonEmptyCells tests -------------------

    /// <summary>
    /// This test ensures that the GetAllNoneEmptyCells method is able to return the correct set of one
    /// cells when the spreadsheet has one nonempty cell.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonEmptyCells_OneNonEmpty_ReturnsOneName()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", 2.0);
        HashSet<string> actualNames = sheet.GetNamesOfAllNonemptyCells().ToHashSet();
        HashSet<string> expectedNames = new HashSet<string>();
        expectedNames.Add("X2");
        Assert.IsTrue(actualNames.SetEquals(expectedNames));
    }

    /// <summary>
    /// This test ensures that the GetAllNoneEmptyCells method is able to return the correct set of zero
    /// cells when the spreadsheet has zero nonempty cell.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonEmptyCells_ZeroNonEmpty_ReturnsNoName()
    {
        Spreadsheet sheet = new Spreadsheet();
        HashSet<string> actualNames = sheet.GetNamesOfAllNonemptyCells().ToHashSet();
        HashSet<string> expectedNames = new HashSet<string>();
        Assert.IsTrue(actualNames.SetEquals(expectedNames));
    }

    /// <summary>
    /// This test ensures that the GetAllNoneEmptyCells method is able to return the correct set of multiple
    /// cells when the spreadsheet has multiple nonempty cells.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonEmptyCells_MultipleNonEmpty_ReturnsMultipleName()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", 2.0);
        sheet.SetCellContents("b2", new Formula("2+2"));
        sheet.SetCellContents("a2", 5.0);
        sheet.SetCellContents("s2", "aq2");
        sheet.SetCellContents("f2", 9.0);
        HashSet<string> actualNames = sheet.GetNamesOfAllNonemptyCells().ToHashSet();
        HashSet<string> expectedNames = new HashSet<string>();
        expectedNames.Add("X2");
        expectedNames.Add("B2");
        expectedNames.Add("A2");
        expectedNames.Add("S2");
        expectedNames.Add("F2");
        Assert.IsTrue(actualNames.SetEquals(expectedNames));
    }

    /// <summary>
    /// This test ensures that the GetAllNoneEmptyCells method is able to return the correct set of multiple
    /// cells when the spreadsheet has multiple nonempty cells. Even after one was added and then removed.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonEmptyCells_MultipleAddedThenOneRemovedViaSettingToEmpty_ReturnsMultipleName()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", 2.0);
        sheet.SetCellContents("b2", new Formula("2+2"));
        sheet.SetCellContents("a2", 5.0);
        sheet.SetCellContents("s2", "aq2");
        sheet.SetCellContents("f2", 9.0);
        sheet.SetCellContents("f2", string.Empty);
        HashSet<string> actualNames = sheet.GetNamesOfAllNonemptyCells().ToHashSet();
        HashSet<string> expectedNames = new HashSet<string>();
        expectedNames.Add("X2");
        expectedNames.Add("B2");
        expectedNames.Add("A2");
        expectedNames.Add("S2");
        Assert.IsTrue(actualNames.SetEquals(expectedNames));
    }

    /// <summary>
    /// This test ensures that the GetAllNoneEmptyCells method is able to return the correct set of zero
    /// cells when the spreadsheet has zero nonempty cells. Even when one was added and then removed.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonEmptyCells_OneAddedThenOneRemovedViaSettingToEmpty_ReturnsZeroName()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("f2", 9.0);
        sheet.SetCellContents("f2", string.Empty);
        HashSet<string> actualNames = sheet.GetNamesOfAllNonemptyCells().ToHashSet();
        HashSet<string> expectedNames = new HashSet<string>();
        Assert.IsTrue(actualNames.SetEquals(expectedNames));
    }

    /// <summary>
    /// This test ensures that the GetAllNoneEmptyCells method is able to return the correct set of multiple
    /// cells when the spreadsheet has multiple nonempty cells. Even after multiple have been removed via setting
    /// to empty cells.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonEmptyCells_MultipleAddedThenMultipleRemovedViaSettingToEmpty_ReturnsMultipleName()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", 2.0);
        sheet.SetCellContents("b2", new Formula("2+2"));
        sheet.SetCellContents("a2", 5.0);
        sheet.SetCellContents("s2", "aq2");
        sheet.SetCellContents("f2", 9.0);
        sheet.SetCellContents("f2", string.Empty);
        sheet.SetCellContents("b2", string.Empty);
        sheet.SetCellContents("a2", string.Empty);
        HashSet<string> actualNames = sheet.GetNamesOfAllNonemptyCells().ToHashSet();
        HashSet<string> expectedNames = new HashSet<string>();
        expectedNames.Add("X2");
        expectedNames.Add("S2");
        Assert.IsTrue(actualNames.SetEquals(expectedNames));
    }

    // --------------- GetCellContents tests -------------------

    /// <summary>
    /// Test that ensures the GetCellContents can correctly get the contents of a cell which has text.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetCellContents_CellWithText_ReturnsString()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", "Hello");
        string actualCellContents = (string)sheet.GetCellContents("x2");
        string expectedCellContents = "Hello";
        Assert.AreEqual(expectedCellContents, actualCellContents);
    }

    /// <summary>
    /// Test that ensures the GetCellContents can correctly get the contents, which is nothing, if nothing has been added to
    /// the spreadsheet.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetCellContents_SheetThatHasNothing_ReturnsStringEmpty()
    {
        Spreadsheet sheet = new Spreadsheet();
        Assert.AreEqual(sheet.GetCellContents("A1"), string.Empty);
    }

    /// <summary>
    /// Test that ensures the GetCellContents can correctly get the contents of a cell which has a double.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetCellContents_CellWithDouble_ReturnsDouble()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", 5.0);
        double actualCellContents = (double)sheet.GetCellContents("x2");
        double expectedCellContents = 5.0;
        Assert.AreEqual(expectedCellContents, actualCellContents);
    }

    /// <summary>
    /// Test that ensures the GetCellContents can correctly get the contents of a cell which has a Formula.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetCellContents_CellWithFormula_ReturnsFormula()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", new Formula("2+23"));
        Formula actualCellContents = (Formula)sheet.GetCellContents("x2");
        Formula expectedCellContents = new Formula("2+23");
        Assert.AreEqual(expectedCellContents, actualCellContents);
    }

    /// <summary>
    /// Test that ensures the GetCellContents can correctly throw an InvalidNameException when the name is invalid.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadsheetGetCellContents_InvalidName_ThrowsException()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("x2", new Formula("2+23"));
        object actualCellContents = sheet.GetCellContents("2321fewf32");
    }

    /// <summary>
    /// Test that ensures the GetCellContents can return an empty string when the given cell name is empty.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetCellContents_ValidNameThatIsEmpty_ReturnsEmptyString()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetCellContents("ff4", "22123");
        sheet.SetCellContents("zf4", 0.2);
        sheet.SetCellContents("rf4", "223");

        string actualCellContents = (string)sheet.GetCellContents("x2");
        string expectedCellContents = string.Empty;
        Assert.AreEqual(expectedCellContents, actualCellContents);
    }

    // --------------- SetCellContents double tests -------------------

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for doubles returns the proper
    /// list of cells affected.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsDouble_AffectsNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", 2.2);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for doubles returns the proper
    /// list of cells affected. Even when a cell has been replaced with other contents.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsDouble_OverwritingCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("x2", 2.2);
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", 5.0);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for doubles returns the proper
    /// list of cells affected only directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsDouble_AffectsOneThingDirectly_ReturnsListOfTwoElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", 2.2);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for doubles returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsDouble_AffectsThingsDirectlyAndIndirectly_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", 2.2);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for doubles returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsDouble_AffectsThingsDirectlyOrderDoesNotMatter_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("x2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", 2.2);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for doubles is able to
    /// throw the correct InvalidNameException when the name is not a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetSetCellContentsDouble_InvalidName_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("2fdasd", 2.2);
    }

    // --------------- SetCellContents string tests -------------------

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for strings returns the proper
    /// list of cells affected.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsString_AffectsNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", "Hi");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for strings returns the proper
    /// list of cells affected. Even after a cell has been overwritten with nothing therefore drastically affecting
    /// the whole spreadsheet.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsString_OverwriteWithNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("X2", new Formula("b3 + g6"));
        spreadsheet.SetCellContents("b3", new Formula("c5 + 2"));
        spreadsheet.SetCellContents("g6", new Formula("c8 + bp0"));
        spreadsheet.SetCellContents("v4", new Formula("x2 + 3"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", string.Empty);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("V4");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for strings returns the proper
    /// list of cells affected. Even when a cell's contents have been replaced.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsString_OverwritingCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("x2", "Goober");
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", "Hi");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for strings returns the proper
    /// list of cells affected only directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsString_AffectsOneThingDirectly_ReturnsListOfTwoElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", "Lol");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for strings returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsString_AffectsThingsDirectlyAndIndirectly_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", "Hello");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for strings returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsString_AffectsThingsDirectlyOrderDoesNotMatter_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("x2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", "Totally Tubular");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A2");

        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for strings is able to
    /// throw the correct InvalidNameException when the name is not a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetSetCellContentsString_InvalidName_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("2fdasd", "Bro, this is invalid Hawk");
    }

    // --------------- SetCellContents Formula tests -------------------

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AffectsNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected. When something has changed which results to indirect having to change to.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_ChangingReturnsProperListNestedStuff_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("b2", new Formula("x2+3"));
        spreadsheet.SetCellContents("a3", new Formula("b2+x2"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A3");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetCellContents.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_OverwritingCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("X2", new Formula("2004"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas and overwriting a valid cell with a
    /// cell that causes a Circular exception leads to Circular Exception.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetCellContentsFormula_OverwritingCellWithVariablesThatDoCircular_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("X2", new Formula("2004"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("X2+2"));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetCellContents.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_OverwritingCellWithValidVariables_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("X2", new Formula("2004"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("y2+z2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetCellContents.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_OverwritingCellWithValidVariablesWithDifferentVariables_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("X2", new Formula("b3 + g6"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("y2+z2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetCellContents in a very meaningful way which
    /// affects cells directly and indirectly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_ExtremelyMeaningfulCascadingOverwritingOfCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("X2", new Formula("b3 + g6"));
        spreadsheet.SetCellContents("b3", new Formula("c5 + 2"));
        spreadsheet.SetCellContents("g6", new Formula("c8 + bp0"));
        spreadsheet.SetCellContents("v4", new Formula("x2 + 3"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+z2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("V4");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected only directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AffectsOneThingDirectly_ReturnsListOfTwoElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AffectsThingsDirectlyAndIndirectly_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AffectsThingsDirectlyOrderDoesNotMatter_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("x2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AddingOfDirectCircularExpressionDoesNotChangeSpreadsheet_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("x2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        try
        {
            spreadsheet.SetCellContents("a2", new Formula("a2+2"));
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly. And does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AddingOfIndirectCircularExpression_DoesNotChangeSpreadsheet()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        try
        {
            spreadsheet.SetCellContents("a2", new Formula("b2+2"));
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AddingOfIndirectCircularExpressionInNewCell_DoesNotChangeSpreadsheet()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        try
        {
            spreadsheet.SetCellContents("x2", new Formula("b2+a2"));
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.GetCellContents("b2"), new Formula("a2 + 5"));
        Assert.AreEqual(spreadsheet.GetCellContents("x2"), string.Empty);
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly specifically when a CircularExceptionOccurred and we are overwriting
    /// via our SetCellContents.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AddingOverWriteAfterCircularExceptionWorks_DoesNotThrowException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        try
        {
            spreadsheet.SetCellContents("x2", new Formula("b2+a2"));
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.GetCellContents("b2"), new Formula("a2 + 5"));
        Assert.AreEqual(spreadsheet.GetCellContents("x2"), string.Empty);

        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetCellContents method for formulas returns the proper
    /// list of cells affected both indirectly and directly specifically when a CircularExceptionOccurred the add does not
    /// overwrite.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetCellContentsFormula_AddingAfterCircularExceptionWorks_DoesNotThrowException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 + 1"));
        spreadsheet.SetCellContents("b2", new Formula("a2 + 5"));
        try
        {
            spreadsheet.SetCellContents("x2", new Formula("b2+a2"));
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.GetCellContents("b2"), new Formula("a2 + 5"));
        Assert.AreEqual(spreadsheet.GetCellContents("x2"), string.Empty);

        List<string> actualList = (List<string>)spreadsheet.SetCellContents("f2", new Formula("2+2"));
        List<string> expectedList = new List<string>();
        expectedList.Add("F2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for formulas is able to
    /// throw the correct InvalidNameException when the name is not a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetSetCellContentsFormula_InvalidName_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("2fdasd", new Formula("2+2"));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for formulas is able to
    /// throw the correct CircularException when the setting of cell contents results in a cycle between itself and itself.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetCellContentsFormula_CircularRightAway_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("x2+2"));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for formulas is able to
    /// throw the correct CircularException when the setting of cell contents results in a cycle between itself
    /// and a direct cell.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetCellContentsFormula_CircularDirect_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("a2+2"));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetCellContents method for formulas is able to
    /// throw the correct CircularException when the setting of cell contents results in a cycle between itself
    /// and an indirect cell.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetCellContentsFormula_CircularIndirect_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetCellContents("b2", new Formula("a2 +1"));
        spreadsheet.SetCellContents("a2", new Formula("x2 +1"));
        List<string> actualList = (List<string>)spreadsheet.SetCellContents("x2", new Formula("b2+2"));
    }
}