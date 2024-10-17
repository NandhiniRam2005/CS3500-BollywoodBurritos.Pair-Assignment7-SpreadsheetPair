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

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents and its dependents upon
    /// the updating of a cell.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_UpdatingAChainAffectsValues_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+3");
        s.SetContentsOfCell("B1", "=A1+3");
        s.SetContentsOfCell("C1", "=B1+2");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        Assert.AreEqual(s.GetCellValue("C1"), 10.0);
        s.SetContentsOfCell("A1", "=2+1");
        Assert.AreEqual(s.GetCellValue("B1"), 6.0);
        Assert.AreEqual(s.GetCellValue("C1"), 8.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to revert back to double after encountering a circular exception.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_RevertingToDoubleAfterBuriedCircular_RevertsCorrectly()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "3.0");
        s.SetContentsOfCell("B1", "=A1+3");
        try
        {
            s.SetContentsOfCell("A1", "=B1+1");
        }
        catch(Exception)
        {
        }

        Assert.AreEqual(s.GetCellValue("A1"), 3.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to revert back to double after encountering a circular exception.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_RevertingToDoubleAfterImmediateCircular_RevertsCorrectly()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "3.0");
        s.SetContentsOfCell("B1", "=A1+3");
        try
        {
            s.SetContentsOfCell("A1", "=A1+1");
        }
        catch (Exception)
        {
        }

        Assert.AreEqual(s.GetCellValue("A1"), 3.0);
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
        for (int i = 0; i < 100; i++)
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
        if (s.GetCellValue("A1") is string actualValue)
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
}