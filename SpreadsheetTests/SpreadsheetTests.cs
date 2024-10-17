// <copyright file="SpreadsheetTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

namespace CS3500.SpreadsheetTests;

using System;
using CS3500.Spreadsheet;
using CS3500.Formula;
using System.Diagnostics;
using System.Text;

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
        sheet.SetContentsOfCell("x2", "2.0");
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
        sheet.SetContentsOfCell("x2", "2.0");
        sheet.SetContentsOfCell("b2", "=2+2");
        sheet.SetContentsOfCell("a2", "5.0");
        sheet.SetContentsOfCell("s2", "aq2");
        sheet.SetContentsOfCell("f2", "9.0");
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
        sheet.SetContentsOfCell("x2", "2.0");
        sheet.SetContentsOfCell("b2", "=2+2");
        sheet.SetContentsOfCell("a2", "5.0");
        sheet.SetContentsOfCell("s2", "aq2");
        sheet.SetContentsOfCell("f2", "9.0");
        sheet.SetContentsOfCell("f2", string.Empty);
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
        sheet.SetContentsOfCell("f2", "=x5+7");
        sheet.SetContentsOfCell("f2", string.Empty);
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
        sheet.SetContentsOfCell("x2", "2.0");
        sheet.SetContentsOfCell("b2", "=2+2");
        sheet.SetContentsOfCell("a2", "5.0");
        sheet.SetContentsOfCell("s2", "aq2");
        sheet.SetContentsOfCell("f2", "9.0");
        sheet.SetContentsOfCell("f2", string.Empty);
        sheet.SetContentsOfCell("b2", string.Empty);
        sheet.SetContentsOfCell("a2", string.Empty);
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
        sheet.SetContentsOfCell("x2", "Hello");
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
        sheet.SetContentsOfCell("x2", "5.0");
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
        sheet.SetContentsOfCell("x2", "=2+23");
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
        sheet.SetContentsOfCell("x2", "=2+23");
        object actualCellContents = sheet.GetCellContents("2321fewf32");
    }

    /// <summary>
    /// Test that ensures the GetCellContents can return an empty string when the given cell name is empty.
    /// </summary>
    [TestMethod]
    public void SpreadsheetGetCellContents_ValidNameThatIsEmpty_ReturnsEmptyString()
    {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("ff4", "22123");
        sheet.SetContentsOfCell("zf4", "0.2");
        sheet.SetContentsOfCell("rf4", "223");

        string actualCellContents = (string)sheet.GetCellContents("x2");
        string expectedCellContents = string.Empty;
        Assert.AreEqual(expectedCellContents, actualCellContents);
    }

    // --------------- SetContentsOfCell double tests -------------------

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for doubles returns the proper
    /// list of cells affected.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellDouble_AffectsNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "2.2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for doubles returns the proper
    /// list of cells affected. Even when a cell has been replaced with other contents.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellDouble_OverwritingCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("x2", "2.2");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "5.0");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for doubles returns the proper
    /// list of cells affected only directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellDouble_AffectsOneThingDirectly_ReturnsListOfTwoElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "2.2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for doubles returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellDouble_AffectsThingsDirectlyAndIndirectly_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "2.2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for doubles returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellDouble_AffectsThingsDirectlyOrderDoesNotMatter_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=x2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "2.2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for doubles is able to
    /// throw the correct InvalidNameException when the name is not a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetSetContentsOfCellDouble_InvalidName_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("2fdasd", "2.2");
    }

    // --------------- SetContentsOfCell string tests -------------------

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for strings returns the proper
    /// list of cells affected.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellString_AffectsNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "Hi");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for strings returns the proper
    /// list of cells affected. Even after a cell has been overwritten with nothing therefore drastically affecting
    /// the whole spreadsheet.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellString_OverwriteWithNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("X2", "=b3 + g6");
        spreadsheet.SetContentsOfCell("b3", "=c5 + 2");
        spreadsheet.SetContentsOfCell("g6", "=c8 + bp0");
        spreadsheet.SetContentsOfCell("v4", "=x2 + 3");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", string.Empty);
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("V4");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for strings returns the proper
    /// list of cells affected. Even when a cell's contents have been replaced.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellString_OverwritingCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("x2", "Goober");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "Hi");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for strings returns the proper
    /// list of cells affected only directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellString_AffectsOneThingDirectly_ReturnsListOfTwoElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "Lol");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for strings returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellString_AffectsThingsDirectlyAndIndirectly_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "Hello");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for strings returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellString_AffectsThingsDirectlyOrderDoesNotMatter_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=x2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "Totally Tubular");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A2");

        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for strings is able to
    /// throw the correct InvalidNameException when the name is not a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetSetContentsOfCellString_InvalidName_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("2fdasd", "Bro, this is invalid Hawk");
    }

    // --------------- SetContentsOfCell Formula tests -------------------

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AffectsNothing_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected. When something has changed which results to indirect having to change to.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_ChangingReturnsProperListNestedStuff_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("b2", "=x2+3");
        spreadsheet.SetContentsOfCell("a3", "=b2+x2");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A3");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetContentsOfCell.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_OverwritingCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("X2", "=2004");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas and overwriting a valid cell with a
    /// cell that causes a Circular exception leads to Circular Exception.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetContentsOfCellFormula_OverwritingCellWithVariablesThatDoCircular_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("X2", "=2004");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=X2+2");
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetContentsOfCell.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_OverwritingCellWithValidVariables_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("X2", "=2004");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=y2+z2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetContentsOfCell.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_OverwritingCellWithValidVariablesWithDifferentVariables_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("X2", "=b3 + g6");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=y2+z2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected. Even when a cell has been replaced with SetContentsOfCell in a very meaningful way which
    /// affects cells directly and indirectly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_ExtremelyMeaningfulCascadingOverwritingOfCell_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("X2", "=b3 + g6");
        spreadsheet.SetContentsOfCell("b3", "=c5 + 2");
        spreadsheet.SetContentsOfCell("g6", "=c8 + bp0");
        spreadsheet.SetContentsOfCell("v4", "=x2 + 3");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+z2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("V4");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected only directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AffectsOneThingDirectly_ReturnsListOfTwoElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AffectsThingsDirectlyAndIndirectly_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AffectsThingsDirectlyOrderDoesNotMatter_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=x2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("B2");
        expectedList.Add("A2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AddingOfDirectCircularExpressionDoesNotChangeSpreadsheet_ReturnsListOfElements()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=x2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        try
        {
            spreadsheet.SetContentsOfCell("a2", "=a2+2");
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly. And does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AddingOfIndirectCircularExpression_DoesNotChangeSpreadsheet()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        try
        {
            spreadsheet.SetContentsOfCell("a2", "=b2+2");
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly. And does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AddingOfIndirectCircularExpression_DoesNotChangeSpreadsheetOrChanged()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        try
        {
            spreadsheet.SetContentsOfCell("a2", "=b2+2");
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.Changed, true);
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AddingOfIndirectCircularExpressionInNewCell_DoesNotChangeSpreadsheet()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        try
        {
            spreadsheet.SetContentsOfCell("x2", "=b2+a2");
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.GetCellContents("b2"), new Formula("a2 + 5"));
        Assert.AreEqual(spreadsheet.GetCellContents("x2"), string.Empty);
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly specifically when a CircularExceptionOccurred and we are overwriting
    /// via our SetContentsOfCell.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AddingOverWriteAfterCircularExceptionWorks_DoesNotThrowException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        try
        {
            spreadsheet.SetContentsOfCell("x2", "=b2+a2");
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.GetCellContents("b2"), new Formula("a2 + 5"));
        Assert.AreEqual(spreadsheet.GetCellContents("x2"), string.Empty);

        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("X2");
        expectedList.Add("A2");
        expectedList.Add("B2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas returns the proper
    /// list of cells affected both indirectly and directly specifically when a CircularExceptionOccurred the add does not
    /// overwrite.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_AddingAfterCircularExceptionWorks_DoesNotThrowException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 + 1");
        spreadsheet.SetContentsOfCell("b2", "=a2 + 5");
        try
        {
            spreadsheet.SetContentsOfCell("x2", "=b2+a2");
        }
        catch (CircularException)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("a2"), new Formula("x2 + 1"));
        Assert.AreEqual(spreadsheet.GetCellContents("b2"), new Formula("a2 + 5"));
        Assert.AreEqual(spreadsheet.GetCellContents("x2"), string.Empty);

        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("f2", "=2+2");
        List<string> expectedList = new List<string>();
        expectedList.Add("F2");
        Assert.IsTrue(actualList.SequenceEqual(expectedList));
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for formulas is able to
    /// throw the correct InvalidNameException when the name is not a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetSetContentsOfCellFormula_InvalidName_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("2fdasd", "=2+2");
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for formulas is able to
    /// throw the correct CircularException when the setting of cell contents results in a cycle between itself and itself.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetContentsOfCellFormula_CircularRightAway_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=x2+2");
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for formulas is able to
    /// throw the correct CircularException when the setting of cell contents results in a cycle between itself
    /// and a direct cell.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetContentsOfCellFormula_CircularDirect_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=a2+2");
    }

    /// <summary>
    /// Test that ensures that when adding to a sheet the SetContentsOfCell method for formulas is able to
    /// throw the correct CircularException when the setting of cell contents results in a cycle between itself
    /// and an indirect cell.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void SpreadSheetSetContentsOfCellFormula_CircularIndirect_ThrowsException()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("b2", "=a2 +1");
        spreadsheet.SetContentsOfCell("a2", "=x2 +1");
        List<string> actualList = (List<string>)spreadsheet.SetContentsOfCell("x2", "=b2+2");
    }

    // SET CONTENTS OF CELL GENERAL TESTS ----------------------------

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a double turning into a string.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromDoubleToString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        s.SetContentsOfCell("A1", "newValue");
        object newValue = s.GetCellValue("B1");
        Assert.IsTrue(newValue is FormulaError);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a double turning into a formula.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromDoubleToFormula_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        s.SetContentsOfCell("A1", "=2+4");
        object newValue = s.GetCellValue("B1");
        Assert.AreEqual(newValue, 9.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a double turning into an empty string.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromDoubleToEmptyString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        s.SetContentsOfCell("A1", string.Empty);
        object newValue = s.GetCellValue("B1");
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a double turning into a different double.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromDoubleToDifferentDouble_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        s.SetContentsOfCell("A1", "10.0");
        object newValue = s.GetCellValue("B1");
        Assert.AreEqual(newValue, 13.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a string turning into a double.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromStringToDouble_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "newValue");
        s.SetContentsOfCell("B1", "=A1+3");
        object newValue = s.GetCellValue("B1");
        Assert.IsTrue(newValue is FormulaError);
        s.SetContentsOfCell("A1", "5");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a string turning into a formula.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromStringToFormula_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "newValue");
        s.SetContentsOfCell("B1", "=A1+3");
        object newValue = s.GetCellValue("B1");
        Assert.IsTrue(newValue is FormulaError);
        s.SetContentsOfCell("A1", "=2+3");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a string turning into an empty cell.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromStringToAnEmptyString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "newValue");
        s.SetContentsOfCell("B1", "=A1+3");
        object newValue = s.GetCellValue("B1");
        Assert.IsTrue(newValue is FormulaError);
        s.SetContentsOfCell("A1", string.Empty);
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a formula turning into a double.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromFormulaToADouble_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 10.0);
        s.SetContentsOfCell("A1", "12");
        object newValue = s.GetCellValue("B1");
        Assert.AreEqual(newValue, 15.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a formula turning into another formula.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromFormulaToAnotherFormula_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 10.0);
        s.SetContentsOfCell("A1", "=12-10");
        object newValue = s.GetCellValue("B1");
        Assert.AreEqual(newValue, 5.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a formula turning into a string.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromFormulaToAString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+2");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 7.0);
        s.SetContentsOfCell("A1", "newValue");
        object newValue = s.GetCellValue("B1");
        Assert.IsTrue(newValue is FormulaError);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// a formula turning into another formula.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromFormulaToAnEmptyString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+5");
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.AreEqual(s.GetCellValue("B1"), 10.0);
        s.SetContentsOfCell("A1", string.Empty);
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// an EmptyString into a double.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromAnEmptyStringToADouble_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
        s.SetContentsOfCell("A1", "2");
        Assert.AreEqual(s.GetCellValue("B1"), 5.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// an EmptyString into a formula.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromAnEmptyStringToFormula_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
        s.SetContentsOfCell("A1", "=2+8");
        Assert.AreEqual(s.GetCellValue("B1"), 13.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents when it has been updated. Specifically for the case of
    /// an EmptyString into a string.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ChangingFromAnEmptyStringToAString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("B1", "=A1+3");
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
        s.SetContentsOfCell("A1", "Hello");
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
    }

    // TESTS FOR SAVING (save function) -----------------------------

    /// <summary>
    /// Test to ensure that the save method is able to save a spreadsheet with only one cell filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetSave_BasicOneObjectJSON_CreatesExpectedObject()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "3.0");

        File.WriteAllText("values.txt", string.Empty);

        s.Save("values.txt");

        Spreadsheet sheetFromSaved = new Spreadsheet();

        sheetFromSaved.Load("values.txt");

        string expectedContents = "3";

        string? actualContents = sheetFromSaved.GetCellContents("A1").ToString();

        Assert.AreEqual(expectedContents, actualContents);

        Assert.IsTrue(sheetFromSaved.GetNamesOfAllNonemptyCells().Count == 1);
    }

    /// <summary>
    /// Test to ensure that the save method is able to save a spreadsheet with only two cells filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetSave_BasicTwoObjectJSON_CreatesExpectedObject()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5");
        s.SetContentsOfCell("B1", "=4+2");

        File.WriteAllText("values.txt", string.Empty);

        s.Save("values.txt");

        Spreadsheet ss = new Spreadsheet();
        ss.Load("values.txt");

        string expectedContents = "5";

        string? actualContents = ss.GetCellContents("A1").ToString();

        string expectedContents2 = "4+2";

        string? actualContents2 = ss.GetCellContents("B1").ToString();

        Assert.AreEqual(expectedContents, actualContents);
        Assert.AreEqual(expectedContents2, actualContents2);

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 2);
    }

    /// <summary>
    /// Test to ensure that the save method is able to save a spreadsheet with multiple cells filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetSave_MultiObjectJSON_CreatesExpectedObject()
    {
        Spreadsheet s = new Spreadsheet();
        Random r = new Random();
        for (int i = 0; i < 100; i++)
        {
            switch (r.Next(3))
            {
                case 0:
                    s.SetContentsOfCell($"A{i}", $"{i}");
                    break;
                case 1:
                    s.SetContentsOfCell($"B{i}", $"=4+{i}");
                    break;
                case 2:
                    s.SetContentsOfCell($"C{i}", $"Hello{i}");
                    break;
            }
        }

        File.WriteAllText("values.txt", string.Empty);
        s.Save("values.txt");
        Spreadsheet ss = new Spreadsheet();
        ss.Load("values.txt");
        int expectedCount = 100;
        int actualCount = ss.GetNamesOfAllNonemptyCells().Count;
        Assert.AreEqual(expectedCount, actualCount);
    }

    /// <summary>
    /// Test to ensure that the save method is able to throw the proper exception when the file we are attempting to save to
    /// does not exist.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetSave_SavingToFileThatDoesNotExist_ThrowsReadWriteException()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5");
        s.SetContentsOfCell("B1", "=4+2");

        s.Save(@"bleeber//b\labb");
    }

    /// <summary>
    /// Test to ensure that the save method is able to save a spreadsheet that has no filled out cells.
    /// </summary>
    [TestMethod]
    public void SpreadsheetSave_SavingEmptySpreadsheet_CreatesExpectedObject()
    {
        Spreadsheet s = new Spreadsheet();

        File.WriteAllText("values.txt", string.Empty);

        s.Save("values.txt");

        Spreadsheet ss = new Spreadsheet();
        ss.Load("values.txt");

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 0);
    }

    /// <summary>
    /// Test to ensure that a spreadsheet created with the second constructor (with a given name) can still be properly saved
    /// through the save method.
    /// </summary>
    [TestMethod]
    public void SpreadsheetSave_SavingSpreadsheetWithName_CreatesExpectedObject()
    {
        Spreadsheet s = new Spreadsheet("Hey there");

        File.WriteAllText("values.txt", string.Empty);

        s.Save("values.txt");

        Spreadsheet ss = new Spreadsheet("Hi!");
        ss.Load("values.txt");

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 0);
    }

    // TESTS FOR LOADING (load function) -----------------------------

    /// <summary>
    ///  Test to ensure that the load method is able to load a spreadsheet with only one cell filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_BasicOneObjectJSON_CreatesExpectedObject()
    {
        string expectedOutput = @"{""Cells"": { ""A1"": { ""StringForm"": ""5""}}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");

        string expectedContents = "5";

        string? actualContents = ss.GetCellContents("A1").ToString();

        Assert.AreEqual(expectedContents, actualContents);

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 1);
    }

    /// <summary>
    ///  Test to ensure that the load method is able to load a spreadsheet with only two cells filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_BasicTwoObjectJSON_CreatesExpectedObject()
    {
        string expectedOutput = @"{""Cells"": { ""A1"": { ""StringForm"": ""5""},""B1"":{""StringForm"": ""=4+2""}}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");

        string expectedContents = "5";

        string? actualContents = ss.GetCellContents("A1").ToString();

        string expectedContents2 = "4+2";

        string? actualContents2 = ss.GetCellContents("B1").ToString();

        Assert.AreEqual(expectedContents, actualContents);
        Assert.AreEqual(expectedContents2, actualContents2);

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 2);
    }

    /// <summary>
    ///  Test to ensure that the load method is able to load a spreadsheet with multiple cells filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_MultiObjectJSON_CreatesExpectedObject()
    {
        Spreadsheet expectedSpreadsheet = new Spreadsheet();
        StringBuilder jsonStringBuilder = new StringBuilder();
        jsonStringBuilder.Append(@"{""Cells"": {");
        Random r = new Random();
        for(int i = 0; i < 100; i++)
        {
            switch (r.Next(3))
            {
                case 0:
                    jsonStringBuilder.Append($@" ""A{i}"":" + @"{ ""StringForm"": ""5""},");
                    expectedSpreadsheet.SetContentsOfCell($"A{i}", "5");
                    break;
                case 1:
                    jsonStringBuilder.Append($@" ""B{i}"":" + @"{ ""StringForm"": ""=3+11""},");
                    expectedSpreadsheet.SetContentsOfCell($"B{i}", "=3+11");
                    break;
                case 2:
                    jsonStringBuilder.Append($@" ""C{i}"":" + @"{ ""StringForm"": ""Hello""},");
                    expectedSpreadsheet.SetContentsOfCell($"C{i}", "Hello");
                    break;
            }
        }

        jsonStringBuilder.Append(@" ""A101"":" + @"{ ""StringForm"": ""5""}}}");
        expectedSpreadsheet.SetContentsOfCell("A101", "5");
        File.WriteAllText("known_values.txt", jsonStringBuilder.ToString());

        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SetEquals(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 101);
    }

    /// <summary>
    /// Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the file
    /// we are loading from does not exist.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoad_LoadingFromAFileThatDoesNotExist_ThrowsReadWriteException()
    {
        Spreadsheet ss = new Spreadsheet();
        ss.Load("gleebglobblibberblab");
    }

    /// <summary>
    /// Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the file
    /// we are loading from does not exist.
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_LoadingError_DoesNotChangeSpreadsheet()
    {
        Spreadsheet ss = new Spreadsheet();
        ss.SetContentsOfCell("A1", "=2+2");
        try
        {
            ss.Load("gleebglobblibberblab");
        }
        catch (SpreadsheetReadWriteException)
        {
        }

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 1);
        Assert.IsTrue(ss.GetCellContents("A1") is Formula);
        Assert.AreEqual(4.0, ss.GetCellValue("A1"));
    }

    /// <summary>
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when a cell in the
    ///  file contains a formula with invalid syntax.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoad_LoadingAFileThatContainsAnInvalidFormula_ThrowsReadWriteException()
    {
        string expectedOutput = @"{""Cells"": { ""A1"": { ""StringForm"": ""5""},""B1"":{""StringForm"": ""=$$rt&hd + 67&&d*/da""}}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
    }

    /// <summary>
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the file is not a
    ///  JSON readable file. For example just a string of random letters.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoad_LoadingAFileThatIsNotInOurExpectedFormat_ThrowsReadWriteException()
    {
        string expectedOutput = "Hello";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
    }

    /// <summary>
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the file given has
    ///  cells that when loaded would create circular dependencies.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoad_LoadingAFileWithCircularExceptions_ThrowsReadWriteException()
    {
        string expectedOutput = @"{""Cells"": { ""A1"": { ""StringForm"": ""5""},""B1"":{""StringForm"": ""=B1+2""}}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
    }

    /// <summary>
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the cells are not named
    ///  properly.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoading_LoadingAFileWithInvalidNamingPrinciples_ThrowsReadWriteException()
    {
        string expectedOutput = @"{""Cells"": { ""1A"": { ""StringForm"": ""5""},""B"":{""StringForm"": ""=43/2""}}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
    }

    /// <summary>
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the cells are not named
    ///  properly and the state of changed is not changed.
    /// </summary>
    [TestMethod]

    public void SpreadsheetLoad_LoadingAFileWithInvalidNamingPrinciplesDoesNotChangeStateOfChanged_ThrowsReadWriteException()
    {
        string expectedOutput = @"{""Cells"": { ""1A"": { ""StringForm"": ""5""},""B"":{""StringForm"": ""=43/0""}}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.SetContentsOfCell("A1", "hi");
        try
        {
            ss.Load("known_values.txt");
        }
        catch (Exception)
        {
        }

        Assert.IsTrue(ss.Changed);
    }

    /// <summary>
    /// Test to ensure that the load method can properly load a spreadsheet with no cells filled out.
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_SavingEmptySpreadsheet_CreatesExpectedObject()
    {
        string expectedOutput = @"{""Cells"": {}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 0);
    }

    /// <summary>
    /// Test to ensure that the load method is able to load a spreadsheet which was created using the second constructor (a
    /// spreadsheet with a name).
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_LoadingSpreadsheetWithName_CreatesExpectedObject()
    {
        string expectedOutput = @"{""Cells"": {}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet("Money");
        ss.Load("known_values.txt");

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 0);
    }

    // TESTS FOR INDEXER OF SPREADSHEET -----------------------------

    /// <summary>
    /// Ensure the indexer for the spreadsheet can return the proper value in a normal case.
    /// </summary>
    [TestMethod]
    public void SpreadsheetIndexer_RegularIndexerCase_GetValidValue()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "33");

        Assert.AreEqual(s["A1"], 33.0);
    }

    /// <summary>
    /// Ensure the indexer for the spreadsheet can return the proper value in a normal case where we grab the value of an empty cell.
    /// </summary>
    [TestMethod]
    public void SpreadsheetIndexer_GetEmptyCell_GetValidValue()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "33");

        Assert.AreEqual(s["B1"], string.Empty);
    }

    /// <summary>
    /// Ensure the indexer for the spreadsheet can throw the proper InvalidNameException when an invalid name is given to the indexer.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadsheetIndexer_InvalidVariableName_ThrowsException()
    {
        Spreadsheet s = new Spreadsheet();
        object badName = s["afdas"];
    }

    // TESTS FOR NEW CONSTRUCTOR WITH SPREADSHEET NAME ---------------

    /// <summary>
    /// Test to ensure a spreadsheet can be created when it is created with the second constructor (it is given a name).
    /// </summary>
    [TestMethod]
    public void SpreadSheetNameConstructor_GiveName_CreatesValidObject()
    {
        Spreadsheet s = new Spreadsheet("Money");

        s.SetContentsOfCell("A1", "=3+4");

        Assert.IsTrue(s.GetNamesOfAllNonemptyCells().Count == 1);
    }

    // TESTS FOR GetCellValue ----------------------------------------

    /// <summary>
    /// Test to ensure that the GetCellValue method returns a string of text when the value in the cell is just a string.
    /// </summary>
    [TestMethod]
    public void SpreadSheetGetCellValue_StringOfTextInCell_ReturnsString()
    {
        Spreadsheet s = new Spreadsheet();

        s.SetContentsOfCell("A1", "Hello Joe");

        string expectedValue = "Hello Joe";
        if(s.GetCellValue("A1") is string actualValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }
        else
        {
            Assert.Fail();
        }
    }

    /// <summary>
    ///  Test to ensure that the GetCellValue method returns a double when the value in that cell is just a double.
    /// </summary>
    [TestMethod]
    public void SpreadSheetGetCellValue_DoubleInCell_ReturnsDouble()
    {
        Spreadsheet s = new Spreadsheet();

        s.SetContentsOfCell("A1", "3.0");

        double expectedValue = 3.0;
        if (s.GetCellValue("A1") is double actualValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }
        else
        {
            Assert.Fail();
        }
    }

    /// <summary>
    ///  Test to ensure that the GetCellValue method returns a formula evaluated value when the value in a cell is a formula
    ///  that is formatted correctly.
    /// </summary>
    [TestMethod]
    public void SpreadSheetGetCellValue_GoodFormula_ReturnsDouble()
    {
        Spreadsheet s = new Spreadsheet();

        s.SetContentsOfCell("A1", "=3+7");

        double expectedValue = 10.0;
        if (s.GetCellValue("A1") is double actualValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }
        else
        {
            Assert.Fail();
        }
    }

    /// <summary>
    /// Test to ensure that the GetCellValue method returns a FormulaError object if the cell contains a formula which when
    /// evaluated would return a FormulaError.
    /// </summary>
    [TestMethod]
    public void SpreadSheetGetCellValue_FormulaWhichReturnsFormulaError_ReturnsFormulaError()
    {
        Spreadsheet s = new Spreadsheet();

        s.SetContentsOfCell("A1", "=2/0");

        Assert.IsTrue(s.GetCellValue("A1") is FormulaError);
    }

    /// <summary>
    ///  Test to ensure that the GetCellValue method returns an empty string when the cells value is nothing and is an empty cell.
    /// </summary>
    [TestMethod]
    public void SpreadSheetGetCellValue_Empty_ReturnsEmptyString()
    {
        Spreadsheet s = new Spreadsheet();

        s.SetContentsOfCell("A1", "Hello Joe");

        string expectedValue = string.Empty;
        if (s.GetCellValue("B1") is string actualValue)
        {
            Assert.AreEqual(expectedValue, actualValue);
        }
        else
        {
            Assert.Fail();
        }
    }

    /// <summary>
    ///  Test to ensure that the GetCellValue method throws an InvalidNameException when the cell that was passed as a parameter
    ///  is not a valid cell.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SpreadSheetGetCellValue_InvalidName_ThrowsInvalidNameException()
    {
        Spreadsheet s = new Spreadsheet();

        s.SetContentsOfCell("A1", "Hello Joe");

        s.GetCellValue("Hi");
    }

//    /// <summary>
//    /// Tests that the cell naming convention is honored.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("2")]
//    [ExpectedException(typeof(InvalidNameException))]
//    public void GetCellContents_InvalidCellName_Throws()
//    {
//        Spreadsheet s = new();
//        s.GetCellContents("1AA");
//    }

//    /// <summary>
//    ///   Test that an unassigned cell has the default value of an empty string.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("3")]
//    public void GetCellContents_DefaultCellValue_Empty()
//    {
//        Spreadsheet s = new();
//        Assert.AreEqual(string.Empty, s.GetCellContents("A2"));
//    }

//    /// <summary>
//    ///   Try setting an invalid cell to a double.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("5")]
//    [ExpectedException(typeof(InvalidNameException))]
//    public void SetContentsOfCell_InvalidCellName_Throws()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("1A1A", "1.5");
//    }

//    /// <summary>
//    ///   Set a cell to a number and get the number back out.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("6")]
//    public void SetGetCellContents_SetTheNumber_RetrieveTheNumber()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("Z7", "1.5");
//        Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
//    }

//    // SETTING CELL TO A STRING

//    /// <summary>
//    ///   Try to assign a string to an invalid cell.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("9")]
//    [ExpectedException(typeof(InvalidNameException))]
//    public void SetContentsOfCellString_InvalidCellName_Throw()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("1AZ", "newValue");
//    }

//    /// <summary>
//    ///   Simple test of assigning a string to a cell and getting
//    ///   it back out.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("10")]
//    public void SetAndGetCellContents_SetTheString_RetrieveTheString()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("Z7", "newValue");
//        Assert.AreEqual("newValue", s.GetCellContents("Z7"));
//    }

//    // SETTING CELL TO A FORMULA

//    /// <summary>
//    ///   Test that when assigning a formula, an invalid cell name
//    ///   throws an exception.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("13")]
//    [ExpectedException(typeof(InvalidNameException))]
//    public void SetContentsOfCell_InvalidCellNameForFormula_Throws()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("1AZ", "=2");
//    }

//    /// <summary>
//    ///   Set a formula, retrieve the formula.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("14")]
//    public void SetGetCellContents_SetAFormula_RetrieveTheFormula()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("Z7", "=3");
//        Formula f = (Formula)s.GetCellContents("Z7");
//        Assert.AreEqual(new Formula("3"), f);
//        Assert.AreNotEqual(new Formula("2"), f);
//    }

//    // CIRCULAR FORMULA DETECTION

//    /// <summary>
//    ///   Two cell circular dependency check.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("15")]
//    [ExpectedException(typeof(CircularException))]
//    public void SetContentsOfCell_CircularDependency_Throws()
//    {
//        Spreadsheet s = new();

//        s.SetContentsOfCell("A1", "=A2");
//        s.SetContentsOfCell("A2", "=A1");
//    }

//    /// <summary>
//    ///    A four cell circular dependency test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("16")]
//    [ExpectedException(typeof(CircularException))]
//    public void SetContentsOfCell_CircularDependencyMultipleCells_Throws()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("A1", "=A2+A3");
//        s.SetContentsOfCell("A3", "=A4+A5");
//        s.SetContentsOfCell("A5", "=A6+A7");
//        s.SetContentsOfCell("A7", "=A1+A1");
//    }

//    /// <summary>
//    ///  Trying to add a circular dependency should leave the
//    ///  spreadsheet unmodified.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("17")]
//    [ExpectedException(typeof(CircularException))]
//    public void SetContentsOfCell_TestUndoCircular_OriginalSheetRemains()
//    {
//        Spreadsheet s = new();
//        try
//        {
//            s.SetContentsOfCell("A1", "=A2+A3");
//            s.SetContentsOfCell("A2", "15");
//            s.SetContentsOfCell("A3", "30");
//            s.SetContentsOfCell("A2", "=A3*A1");
//        }
//        catch (CircularException)
//        {
//            Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
//            throw;
//        }
//    }

//    /// <summary>
//    ///   After adding the simplest circular dependency, the first cell
//    ///   should still contain the original value, but the second one removed.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("17b")]
//    [ExpectedException(typeof(CircularException))]
//    public void SetContentsOfCell_SimpleCircularUndo_OriginalSheetRemains()
//    {
//        Spreadsheet s = new();
//        try
//        {
//            s.SetContentsOfCell("A1", "=A2");
//            s.SetContentsOfCell("A2", "=A1");
//        }
//        catch (CircularException)
//        {
//            Assert.AreEqual(string.Empty, s.GetCellContents("A2"));
//            Assert.IsTrue(new HashSet<string> { "A1" }.SetEquals(s.GetNamesOfAllNonemptyCells()));
//            throw;
//        }
//    }

//    // NONEMPTY CELLS

//    /// <summary>
//    ///   An empty spreadsheet should have no non-empty cells.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("18")]
//    public void GetNamesOfAllNonEmptyCells_EmptySpreadsheet_EmptyEnumerator()
//    {
//        Spreadsheet s = new();
//        Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
//    }

//    /// <summary>
//    ///   Assigning an empty string into a cell should not create a non-empty cell.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("19")]
//    public void SetContentsOfCell_SetEmptyCell_CellIsEmpty()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("B1", string.Empty);
//        Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
//    }

//    /// <summary>
//    ///   Assigning a string into a cell produces a spreadsheet with one non-empty cell.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("20")]
//    public void GetNamesOfAllNonEmptyCells_AddStringToCell_ThatCellIsNotEmpty()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("B1", "newValue");
//        Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(["B1"]));
//    }

//    /// <summary>
//    ///   Assigning a double into a cell produces a spreadsheet with one non-empty cell.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("21")]
//    public void GetNamesOfAllNonEmptyCells_AddDoubleToCell_ThatCellIsNotEmpty()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("B1", "52.25");
//        Assert.IsTrue(s.GetNamesOfAllNonemptyCells().Matches(["B1"]));
//        Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(["B1"]));
//    }

//    /// <summary>
//    ///   Assigning a Formula into a cell produces a spreadsheet with one non-empty cell.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("22")]
//    public void GetNamesOfAllNonEmptyCells_AddFormulaToCell_ThatCellIsNotEmpty()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("B1", "=3.5");
//        Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(["B1"]));
//    }

//    /// <summary>
//    ///   Assign a double, string, and formula into the sheet and make sure
//    ///   they each have their own cell.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("23")]
//    public void SetContentsOfCell_AssignDoubleStringAndFormula_ThreeCellsExist()
//    {
//        Spreadsheet s = new();

//        s.SetContentsOfCell("A1", "17.2");
//        s.SetContentsOfCell("C1", "newValue");
//        s.SetContentsOfCell("B1", "=3.5");

//        Assert.IsTrue(s.GetNamesOfAllNonemptyCells().Matches(["A1", "B1", "C1"]));
//        Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(["A1", "B1", "C1"]));
//    }

//    // RETURN VALUE OF SET CELL CONTENTS

//    /// <summary>
//    ///   When a cell that has no cells depending on it is changed, then only
//    ///   that cell needs to be reevaluated. (Testing for Double content cells.)
//    /// </summary>
//    [TestMethod]
//    [TestCategory("24")]
//    public void SetContentsOfCell_SettingIndependentCellToDouble_ReturnsOnlyThatCell()
//    {
//        Spreadsheet s = new();

//        s.SetContentsOfCell("B1", "newValue");
//        s.SetContentsOfCell("C1", "=5");
//        var toReevaluate = s.SetContentsOfCell("A1", "17.2");
//        Assert.IsTrue(toReevaluate.Matches(["A1"])); // Note: Matches is not order dependent
//    }

//    /// <summary>
//    ///   When a cell that has no cells depending on it is changed, then only
//    ///   that cell needs to be reevaluated. (Testing for Formula content cells.)
//    /// </summary>
//    [TestMethod]
//    [TestCategory("25")]
//    public void SetContentsOfCell_SettingIndependentCellToString_ReturnsOnlyThatCell()
//    {
//        Spreadsheet s = new();

//        s.SetContentsOfCell("A1", "17.2");
//        s.SetContentsOfCell("C1", "=5");

//        var toReevaluated = s.SetContentsOfCell("B1", "newValue");
//        Assert.IsTrue(toReevaluated.Matches(["B1"]));
//    }

//    /// <summary>
//    ///   When a cell that has no cells depending on it is changed, then only
//    ///   that cell needs to be reevaluated. (Testing for Formula content cells.)
//    /// </summary>
//    [TestMethod]
//    [TestCategory("26")]
//    public void SetContentsOfCell_SettingIndependentCellToFormula_ReturnsOnlyThatCell()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("A1", "17.2");
//        s.SetContentsOfCell("B1", "newValue");
//        var changed = s.SetContentsOfCell("C1", "=5");
//        Assert.IsTrue(changed.Matches(["C1"]));
//    }

//    /// <summary>
//    ///   A chain of 5 cells is created.  When the first cell in the chain
//    ///   is modified, then all the cells have to be recomputed.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("27")]
//    public void SetContentsOfCell_CreateChainModifyFirst_AllAreInNeedOfUpdate()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("A1", "=A2+A3");
//        s.SetContentsOfCell("A2", "6");
//        s.SetContentsOfCell("A3", "=A2+A4");
//        s.SetContentsOfCell("A4", "=A2+A5");

//        var changed = s.SetContentsOfCell("A5", "82.5");

//        Assert.IsTrue(changed.SequenceEqual(["A5", "A4", "A3", "A1"]));
//    }

//    // CHANGING CELLS

//    /// <summary>
//    ///   Test that replacing the contents of a cell (Formula --> double) works.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("28")]
//    public void SetContentsOfCell_ReplaceFormulaWithDouble_CellValueCorrect()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("A1", "=A2+A3");
//        s.SetContentsOfCell("A1", "2.5");
//        Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
//    }

//    /// <summary>
//    ///   Test that replacing a formula in a cell with a string works.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("29")]
//    public void SetContentsOfCell_ReplaceFormulaWithString_CellValueCorrect()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("A1", "=A2+A3");
//        s.SetContentsOfCell("A1", "Hello");
//        Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
//    }

//    /// <summary>
//    ///   Test that replacing a cell containing a string with a new formula works.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("30")]
//    public void SetContentsOfCell_ReplaceStringWithFormula_CellValueCorrect()
//    {
//        Spreadsheet s = new();
//        s.SetContentsOfCell("A1", "Hello");
//        s.SetContentsOfCell("A1", "=23");
//        Assert.AreEqual(new Formula("23"), (Formula)s.GetCellContents("A1"));
//        Assert.AreNotEqual(new Formula("24"), (Formula)s.GetCellContents("A1"));
//    }

//    // STRESS TESTS

//    /// <summary>
//    ///   Create a sheet with 15 cells containing formulas.  Make sure that modifying
//    ///   the end of the chain results in all the cells having to be recomputed.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("31")]
//    public void SetContentsOfCell_LongChainModifyEnd_AllCellsNeedToBeReEvaluated()
//    {
//        Spreadsheet s = new();

//        s.SetContentsOfCell("A1", "=B1+B2");
//        s.SetContentsOfCell("B1", "=C1-C2");
//        s.SetContentsOfCell("B2", "=C3*C4");
//        s.SetContentsOfCell("C1", "=D1*D2");
//        s.SetContentsOfCell("C2", "=D3*D4");
//        s.SetContentsOfCell("C3", "=D5*D6");
//        s.SetContentsOfCell("C4", "=D7*D8");
//        s.SetContentsOfCell("D1", "=E1");
//        s.SetContentsOfCell("D2", "=E1");
//        s.SetContentsOfCell("D3", "=E1");
//        s.SetContentsOfCell("D4", "=E1");
//        s.SetContentsOfCell("D5", "=E1");
//        s.SetContentsOfCell("D6", "=E1");
//        s.SetContentsOfCell("D7", "=E1");
//        s.SetContentsOfCell("D8", "=E1");

//        var cells = s.SetContentsOfCell("E1", "0");
//        Assert.IsTrue(cells.Matches(["A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1"]));
//    }

//    /// <summary>
//    ///    Repeat the stress test for more weight in grading process.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("32")]
//    public void IncreaseGradingWeight1()
//    {
//        SetContentsOfCell_LongChainModifyEnd_AllCellsNeedToBeReEvaluated();
//    }

//    /// <summary>
//    ///    Repeat the stress test for more weight in grading process.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("33")]
//    public void IncreaseGradingWeight2()
//    {
//        SetContentsOfCell_LongChainModifyEnd_AllCellsNeedToBeReEvaluated();
//    }

//    /// <summary>
//    ///    Repeat the stress test for more weight in grading process.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("34")]
//    public void IncreaseGradingWeight3()
//    {
//        SetContentsOfCell_LongChainModifyEnd_AllCellsNeedToBeReEvaluated();
//    }

//    /// <summary>
//    ///   Programmatically create a chain of cells.  Each time we add
//    ///   another element to the chain, we check that the whole chain
//    ///   needs to be reevaluated.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("35")]
//    public void SetContentsOfCell_TwoHundredLongChain_EachTimeReturnsRestOfChain()
//    {
//        Spreadsheet s = new();
//        ISet<string> expectedAnswers = new HashSet<string>();
//        for (int i = 1; i < 200; i++)
//        {
//            string currentCell = "A" + i;
//            expectedAnswers.Add(currentCell);

//            var changed = s.SetContentsOfCell(currentCell, "=A" + (i + 1));

//            Assert.IsTrue(changed.Matches([.. expectedAnswers]));
//        }
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("36")]
//    public void IncreaseGradingWeight4()
//    {
//        SetContentsOfCell_TwoHundredLongChain_EachTimeReturnsRestOfChain();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("37")]
//    public void IncreaseGradingWeight5()
//    {
//        SetContentsOfCell_TwoHundredLongChain_EachTimeReturnsRestOfChain();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("38")]
//    public void IncreaseGradingWeight6()
//    {
//        SetContentsOfCell_TwoHundredLongChain_EachTimeReturnsRestOfChain();
//    }

//    /// <summary>
//    ///   Build a long chain of cells.  Set one of the cells in the middle
//    ///   of the chain to a circular dependency.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("39")]
//    [ExpectedException(typeof(CircularException))]
//    public void SetContentsOfCell_LongChainAddCircularInMiddle_Throws()
//    {
//        Spreadsheet s = new();

//        for (int i = 1; i < 200; i++)
//        {
//            string currentCell = "A" + i;
//            string nextCell = "A" + (i + 1);
//            s.SetContentsOfCell(nextCell, "0");
//            s.SetContentsOfCell(currentCell, "="+nextCell);
//        }

//        s.SetContentsOfCell("A150", "=A50");
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("40")]
//    [ExpectedException(typeof(CircularException))]
//    public void IncreaseGradingWeight7()
//    {
//        SetContentsOfCell_LongChainAddCircularInMiddle_Throws();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("41")]
//    [ExpectedException(typeof(CircularException))]
//    public void IncreaseGradingWeight8()
//    {
//        SetContentsOfCell_LongChainAddCircularInMiddle_Throws();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("42")]
//    [ExpectedException(typeof(CircularException))]
//    public void IncreaseGradingWeight9()
//    {
//        SetContentsOfCell_LongChainAddCircularInMiddle_Throws();
//    }

//    /// <summary>
//    ///   <para>
//    ///     This is a stress test with lots of cells "linked" together.
//    ///   </para>
//    ///   <para>
//    ///     Create 500 cells that are in a chain from A10 to A1499.
//    ///     Then break the chain in the middle by setting A1249 to
//    ///     a number.
//    ///   </para>
//    ///   <para>
//    ///     Then check that there are two separate chains of cells.
//    ///   </para>
//    /// </summary>
//    [TestMethod]
//    [TestCategory("43")]
//    public void SetContentsOfCell_BreakALongChain_TwoIndependentChains()
//    {
//        Spreadsheet s = new();

//        for (int i = 0; i < 500; i++)
//        {
//            string currentCell = "A1" + i;
//            string nextCell = "A1" + (i + 1);
//            s.SetContentsOfCell(nextCell, "0");
//            s.SetContentsOfCell(currentCell, "=" + nextCell);
//        }

//        List<string> firstCells = [];
//        List<string> lastCells = [];

//        for (int i = 0; i < 250; i++)
//        {
//            string firstHalfCell = "A1" + i;
//            string secondHalfCell = "A1" + (i + 250);
//            firstCells.Add(firstHalfCell);
//            lastCells.Add(secondHalfCell);
//        }

//        firstCells.Reverse();
//        lastCells.Reverse();

//        var firstHalfNeedReevaluate = s.SetContentsOfCell("A1249", "25.0");
//        var secondHalfNeedReevaluate = s.SetContentsOfCell("A1499", "0");

//        Assert.IsTrue(firstHalfNeedReevaluate.SequenceEqual(firstCells));
//        Assert.IsTrue(secondHalfNeedReevaluate.SequenceEqual(lastCells));
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("44")]
//    public void IncreaseGradingWeight10()
//    {
//        SetContentsOfCell_BreakALongChain_TwoIndependentChains();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("45")]
//    public void IncreaseGradingWeight11()
//    {
//        SetContentsOfCell_BreakALongChain_TwoIndependentChains();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the above test.
//    /// </summary>
//    [TestMethod]
//    [TestCategory("46")]
//    public void IncreaseGradingWeight12()
//    {
//        SetContentsOfCell_BreakALongChain_TwoIndependentChains();
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the given test.
//    /// </summary>
//    [TestMethod]
//    [Timeout(4000)]
//    [TestCategory("47")]
//    public void IncreaseGradingWeight13()
//    {
//        SetContentsOfCell_1000RandomCells_MatchesPrecomputedSizeValue(47, 2514);
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the given test.
//    /// </summary>
//    [TestMethod]
//    [Timeout(4000)]
//    [TestCategory("48")]
//    public void IncreaseGradingWeight14()
//    {
//        SetContentsOfCell_1000RandomCells_MatchesPrecomputedSizeValue(48, 2519);
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the given test.
//    /// </summary>
//    [TestMethod]
//    [Timeout(4000)]
//    [TestCategory("49")]
//    public void IncreaseGradingWeight15()
//    {
//        SetContentsOfCell_1000RandomCells_MatchesPrecomputedSizeValue(49, 2502);
//    }

//    /// <summary>
//    ///   Add weight to the grading by repeating the given test.
//    /// </summary>
//    [TestMethod]
//    [Timeout(4000)]
//    [TestCategory("50")]
//    public void IncreaseGradingWeight16()
//    {
//        SetContentsOfCell_1000RandomCells_MatchesPrecomputedSizeValue(50, 2515);
//    }

//    /// <summary>
//    ///   Generates a random cell name with a capital letter
//    ///   and number between 1 - 99.
//    /// </summary>
//    /// <param name="rand"> A random number generator. </param>
//    /// <returns> A random cell name like A10, or B50, .... </returns>
//    private static string GenerateRandomCellName(Random rand)
//    {
//        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
//    }

//    /// <summary>
//    ///   Sets random cells to random contents (strings, doubles, formulas)
//    ///   10000 times.  The number of "repeated" cells in the random group
//    ///   has been predetermined based on the given random seed.
//    /// </summary>
//    /// <param name="seed">Random seed.</param>
//    /// <param name="size">
//    ///   The precomputed/known size of the resulting spreadsheet.
//    ///   This size was determined by pre-running the test with the given seed.
//    /// </param>
//    private static void SetContentsOfCell_1000RandomCells_MatchesPrecomputedSizeValue(int seed, int size)
//    {
//        int circularExceptions = 0;
//        int overWritten = 0;
//        string cellName = string.Empty;
//        Spreadsheet s = new();
//        Random rand = new(seed);
//        for (int i = 0; i < 10000; i++)
//        {
//            try
//            {
//                cellName = GenerateRandomCellName(rand);
//                switch (rand.Next(3))
//                {
//                    case 0:
//                        if (s.GetNamesOfAllNonemptyCells().Contains(cellName))
//                        {
//                            overWritten++;
//                        }

//                        s.SetContentsOfCell(cellName, "3.14");
//                        break;
//                    case 1:
//                        if (s.GetNamesOfAllNonemptyCells().Contains(cellName))
//                        {
//                            overWritten++;
//                        }

//                        s.SetContentsOfCell(cellName, "newValue");
//                        break;
//                    case 2:
//                        if (s.GetNamesOfAllNonemptyCells().Contains(cellName))
//                        {
//                            overWritten++;
//                        }

//                        s.SetContentsOfCell(cellName, "=" + GenerateRandomFormula(rand));
//                        break;
//                }
//            }
//            catch (CircularException)
//            {
//                circularExceptions++;
//                if (s.GetNamesOfAllNonemptyCells().Contains(cellName))
//                {
//                    overWritten--;
//                }
//            }
//        }

//        ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());

//        Assert.AreEqual(size, set.Count);
//    }

//    /// <summary>
//    ///   <para>
//    ///     Generates a random Formula.
//    ///   </para>
//    ///   <para>
//    ///     This helper method is used in the randomize test.
//    ///   </para>
//    /// </summary>
//    /// <param name="rand"> A random number generator.</param>
//    /// <returns> A formula referencing random cells in a spreadsheet. </returns>
//    private static string GenerateRandomFormula(Random rand)
//    {
//        string formula = GenerateRandomCellName(rand);
//        for (int i = 0; i < 10; i++)
//        {
//            switch (rand.Next(4))
//            {
//                case 0:
//                    formula += "+";
//                    break;
//                case 1:
//                    formula += "-";
//                    break;
//                case 2:
//                    formula += "*";
//                    break;
//                case 3:
//                    formula += "/";
//                    break;
//            }

//            switch (rand.Next(2))
//            {
//                case 0:
//                    formula += 7.2;
//                    break;
//                case 1:
//                    formula += GenerateRandomCellName(rand);
//                    break;
//            }
//        }

//        return formula;
//    }
//}

///// <summary>
/////   Helper methods for the tests above.
///// </summary>
//public static class IEnumerableExtensions
//{
//    /// <summary>
//    ///   Check to see if the two "sets" (source and items) match, i.e.,
//    ///   contain exactly the same values. Note: we do not check for sequencing.
//    /// </summary>
//    /// <param name="source"> original container.</param>
//    /// <param name="items"> elements to match against.</param>
//    /// <returns> true if every element in source is in items and vice versa. They are the "same set".</returns>
//    public static bool Matches(this IEnumerable<string> source, params string[] items)
//    {
//        return (source.Count() == items.Length) && items.All(item => source.Contains(item));
//    }
}