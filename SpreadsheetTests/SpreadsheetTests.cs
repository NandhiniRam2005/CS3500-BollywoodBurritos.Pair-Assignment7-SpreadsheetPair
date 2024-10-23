// <copyright file="SpreadsheetTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

namespace CS3500.SpreadsheetTests;

using System;
using System.Text;
using System.Text.Json;
using CS3500.Formula;
using CS3500.Spreadsheet;

/// <summary>
/// Author:    Joel Rodriguez,  Profs Joe, Danny, and Jim.
/// Partner:   None
/// Date:      October 19, 2024
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
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas that if a circular exception is encountered we revert
    /// back to proper contents and changed does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_EmptyStringAfterACircularException_ChangedShouldNotChange()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("A1", "=B1+1");
        spreadsheet.SetContentsOfCell("C1", "=A1 +1");
        spreadsheet.Save("values.txt");
        try
        {
            spreadsheet.SetContentsOfCell("B1", "=C1+2");
        }
        catch (Exception)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("B1"), string.Empty);
        Assert.IsFalse(spreadsheet.Changed);
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas that if a circular exception is encountered we revert
    /// back to proper contents and changed does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_DoubleAfterACircularException_ChangedShouldNotChange()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("A1", "=B1+1");
        spreadsheet.SetContentsOfCell("C1", "=A1 +1");
        spreadsheet.SetContentsOfCell("B1", "2.0");
        spreadsheet.Save("values.txt");
        try
        {
            spreadsheet.SetContentsOfCell("B1", "=C1+2");
        }
        catch (Exception)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("B1"), 2.0);
        Assert.IsFalse(spreadsheet.Changed);
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas that if a circular exception is encountered we revert
    /// back to proper contents and changed does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_FormulaAfterACircularException_ChangedShouldNotChange()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("A1", "=B1+1");
        spreadsheet.SetContentsOfCell("C1", "=A1 +1");
        spreadsheet.SetContentsOfCell("B1", "=B2+2");
        spreadsheet.Save("values.txt");
        try
        {
            spreadsheet.SetContentsOfCell("B1", "=C1+2");
        }
        catch (Exception)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("B1"), new Formula("B2+2"));
        Assert.IsFalse(spreadsheet.Changed);
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas that if a circular exception is encountered we revert
    /// back to proper contents and changed does not change and that nothing else changes. We test this by making sure chains are still in tact.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_BigSpreadsheetFormulaAfterACircularException_NothingIsChanged()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("A1", "=B1+1");
        spreadsheet.SetContentsOfCell("C1", "=A1 +1");
        spreadsheet.SetContentsOfCell("B1", "=B2+2");
        for (int i = 0; i < 20; i++)
        {
            spreadsheet.SetContentsOfCell($"B{i + 3}", $"=B1 + 3");
        }

        spreadsheet.Save("values.txt");
        try
        {
            spreadsheet.SetContentsOfCell("B1", "=C1+2");
        }
        catch (Exception)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("B1"), new Formula("B2+2"));
        Assert.IsFalse(spreadsheet.Changed);
        spreadsheet.SetContentsOfCell("B1", "2");
        for (int i = 0; i < 20; i++)
        {
            Assert.AreEqual(spreadsheet.GetCellValue($"B{i + 3}"), 5.0);
        }
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas that if a circular exception is encountered we revert
    /// back to proper contents. We test this by making sure chains are still in tact and that the Dependees of the exception thrown cell are still
    /// the same (we fill out the variables and make sure the cell updates).
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_BigSpreadsheetFormulaAfterACircularException_DependencyGraphIsNotChanged()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("A1", "=B1+1");
        spreadsheet.SetContentsOfCell("C1", "=A1 +1");
        spreadsheet.SetContentsOfCell("B1", "=B2+V3+B6+E3+D7+H8+J11");
        for (int i = 0; i < 20; i++)
        {
            spreadsheet.SetContentsOfCell($"B{i + 7}", $"=B1 + 3");
        }

        spreadsheet.Save("values.txt");
        try
        {
            spreadsheet.SetContentsOfCell("B1", "=C1+2");
        }
        catch (Exception)
        {
        }

        HashSet<string> expectedDependees = new HashSet<string>();
        if (spreadsheet.GetCellContents("B1") is Formula formula)
        {
            foreach (string dependee in formula.GetVariables())
            {
                spreadsheet.SetContentsOfCell(dependee, "2.0");
            }

            Assert.AreEqual(spreadsheet.GetCellContents("B1"), new Formula("B2+V3+B6+E3+D7+H8+J11"));
            Assert.AreEqual(spreadsheet.GetCellValue("B1"), 14.0);
        }
        else
        {
            Assert.Fail();
        }
    }

    /// <summary>
    /// Test that ensures that when adding to an empty sheet the SetContentsOfCell method for formulas that if a circular exception is encountered we revert
    /// back to proper contents and changed does not change.
    /// </summary>
    [TestMethod]
    public void SpreadSheetSetContentsOfCellFormula_StringAfterACircularExceptionChangedShouldNotChange_ReturnsListOfOneElement()
    {
        Spreadsheet spreadsheet = new Spreadsheet();
        spreadsheet.SetContentsOfCell("A1", "=B1+1");
        spreadsheet.SetContentsOfCell("C1", "=A1 +1");
        spreadsheet.SetContentsOfCell("B1", "Mama mia");
        spreadsheet.Save("values.txt");
        try
        {
            spreadsheet.SetContentsOfCell("B1", "=C1+2");
        }
        catch (Exception)
        {
        }

        Assert.AreEqual(spreadsheet.GetCellContents("B1"), "Mama mia");
        Assert.IsFalse(spreadsheet.Changed);
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
    public void SetContentsOfCell_UpdatingAChainAffectsValuesFormulaToFormula_UpdatesDependents()
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
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents and its dependents upon
    /// the updating of a cell.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_UpdatingAChainAffectsValuesFormulaToDouble_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+3");
        s.SetContentsOfCell("B1", "=A1+3");
        s.SetContentsOfCell("C1", "=B1+2");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        Assert.AreEqual(s.GetCellValue("C1"), 10.0);
        s.SetContentsOfCell("A1", "3.0");
        Assert.AreEqual(s.GetCellValue("B1"), 6.0);
        Assert.AreEqual(s.GetCellValue("C1"), 8.0);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents and its dependents upon
    /// the updating of a cell.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_UpdatingAChainAffectsValuesFormulaToString_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "=2+3");
        s.SetContentsOfCell("B1", "=A1+3");
        s.SetContentsOfCell("C1", "=B1+2");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        Assert.AreEqual(s.GetCellValue("C1"), 10.0);
        s.SetContentsOfCell("A1", "hello");
        Assert.IsTrue(s.GetCellValue("B1") is FormulaError);
        Assert.IsTrue(s.GetCellValue("C1") is FormulaError);
    }

    /// <summary>
    /// Test to ensure that the SetContentsOfCell is able to successfully update all of its dependents and its dependents upon
    /// the updating of a cell.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_UpdatingAChainAffectsValuesDoubleToDifferentDouble_UpdatesDependents()
    {
        Spreadsheet s = new Spreadsheet();
        s.SetContentsOfCell("A1", "5.0");
        s.SetContentsOfCell("B1", "=A1+3");
        s.SetContentsOfCell("C1", "=B1+2");
        Assert.AreEqual(s.GetCellValue("B1"), 8.0);
        Assert.AreEqual(s.GetCellValue("C1"), 10.0);
        s.SetContentsOfCell("A1", "3.0");
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
        catch (Exception)
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
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SetEquals(s.GetNamesOfAllNonemptyCells()));
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SequenceEqual(s.GetNamesOfAllNonemptyCells()));
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
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SequenceEqual(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));

        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Count == 101);
    }

    /// <summary>
    ///  Test to ensure that the load method is able to load a spreadsheet with multiple cells filled out. And that the cells values are what we expect them to
    ///  be.
    /// </summary>
    [TestMethod]
    public void SpreadsheetLoad_MultiObjectJSON_CreatesExpectedObjectAndCells()
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
                    jsonStringBuilder.Append($@" ""A{i}"":" + @"{ ""StringForm"": ""=5""},");
                    expectedSpreadsheet.SetContentsOfCell($"A{i}", "=5");
                    break;
                case 1:
                    jsonStringBuilder.Append($@" ""B{i}"":" + @"{ ""StringForm"": ""=5""},");
                    expectedSpreadsheet.SetContentsOfCell($"B{i}", "=5");
                    break;
                case 2:
                    jsonStringBuilder.Append($@" ""C{i}"":" + @"{ ""StringForm"": ""=5""},");
                    expectedSpreadsheet.SetContentsOfCell($"C{i}", "=5");
                    break;
            }
        }

        jsonStringBuilder.Append(@" ""A101"":" + @"{ ""StringForm"": ""=5""}}}");
        expectedSpreadsheet.SetContentsOfCell("A101", "=5");
        File.WriteAllText("known_values.txt", jsonStringBuilder.ToString());

        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SetEquals(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SequenceEqual(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));

        foreach (string cell in ss.GetNamesOfAllNonemptyCells())
        {
            Assert.AreEqual(ss.GetCellValue(cell), 5.0);
            Assert.AreEqual(ss.GetCellContents(cell), new Formula("5"));
        }

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
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the file is not a
    ///  JSON readable file. For example the cells keyword is missed in the Json string.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoad_LoadingAFileWithoutCellsDetailed_ThrowsReadWriteException()
    {
        string expectedOutput = @"{ ""A1"": { ""StringForm"": ""5""},""B1"":{""StringForm"": ""=2+B1""}}";

        File.WriteAllText("known_values.txt", expectedOutput);

        // Now Read that file
        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
    }

    /// <summary>
    ///  Test to ensure that the load method is able to correctly throw a SpreadsheetReadWriteException when the file is not a
    ///  JSON readable file. For example the stringform keyword is missed in the json file.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void SpreadsheetLoad_LoadingAFileWithoutStringFormDetailed_ThrowsReadWriteException()
    {
        string expectedOutput = @"{""Cells"": { ""A1"": { ""Striorm"": ""5""},""B1"":{""Striorm"": ""=4+2""}}}";

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
    public void SpreadsheetLoad_LoadingEmptySpreadsheet_CreatesExpectedObject()
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

    // STRESS TESTS ---------------------------------

    /// <summary>
    ///  Test to ensure that the load method is able to load a spreadsheet with multiple cells filled out and chains the chain is 1000 and we then
    ///  change the one that leads to the most changes. Since both visit and my recompute cell values are recursive I cannot.
    /// </summary>
    [TestMethod]
    [Timeout(30000)]
    public void SpreadsheetLoad_MultiObjectJSONWithChains_CreatesExpectedObject()
    {
        Spreadsheet expectedSpreadsheet = new Spreadsheet();
        StringBuilder jsonStringBuilder = new StringBuilder();
        jsonStringBuilder.Append(@"{""Cells"":{ ""A1"": {""StringForm"": ""5"" },");
        expectedSpreadsheet.SetContentsOfCell("A1", "5");
        Random r = new Random();
        char prevLetter = 'A';
        int prevI = 1;
        for (int i = 2; i < 1000; i++)
        {
            int letterInt = Math.Clamp(i, 65, 90);
            char letter = (char)letterInt;
            jsonStringBuilder.Append($@"""{letter}{i}"":" + @"{ ""StringForm"":" + $@" ""={prevLetter}{prevI}""" + "},");
            expectedSpreadsheet.SetContentsOfCell($"{letter}{i}", $"={prevLetter}{prevI}");
            prevLetter = letter;
            prevI = i;
        }

        jsonStringBuilder.Append(@" ""A101"":" + @"{ ""StringForm"": ""3.0""}}}");
        string jsonString = jsonStringBuilder.ToString();
        expectedSpreadsheet.SetContentsOfCell("A101", "3.0");
        File.WriteAllText("known_values.txt", jsonString);

        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SetEquals(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SequenceEqual(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));

        ss.SetContentsOfCell("A1", "3.0");
        foreach (string cellName in ss.GetNamesOfAllNonemptyCells())
        {
            Assert.AreEqual(ss.GetCellValue(cellName), 3.0);
        }
    }

    /// <summary>
    ///  Test to ensure that the load method is able to load a spreadsheet with multiple cells filled out and chains the chain is 1000 and we then
    ///  change the one that leads to the most changes. Since both visit and my recompute cell values are recursive I cannot. This test specifically makes
    ///  sure that I do not change spreadsheet.
    /// </summary>
    [TestMethod]
    [Timeout(30000)]
    public void SpreadsheetLoad_MultiObjectJSONWithChainsChangesBadObject_CreatesExpectedObject()
    {
        Spreadsheet expectedSpreadsheet = new Spreadsheet();
        StringBuilder jsonStringBuilder = new StringBuilder();
        jsonStringBuilder.Append(@"{""Cells"":{ ""A1"": {""StringForm"": ""5"" },");
        expectedSpreadsheet.SetContentsOfCell("A1", "5");
        Random r = new Random();
        char prevLetter = 'A';
        int prevI = 1;
        for (int i = 2; i < 1000; i++)
        {
            int letterInt = Math.Clamp(i, 65, 90);
            char letter = (char)letterInt;
            jsonStringBuilder.Append($@"""{letter}{i}"":" + @"{ ""StringForm"":" + $@" ""={prevLetter}{prevI}""" + "},");
            expectedSpreadsheet.SetContentsOfCell($"{letter}{i}", $"={prevLetter}{prevI}");
            prevLetter = letter;
            prevI = i;
        }

        jsonStringBuilder.Append(@" ""A101"":" + @"{ ""StringForm"": ""5.0""}}}");
        string jsonString = jsonStringBuilder.ToString();
        expectedSpreadsheet.SetContentsOfCell("A101", "5.0");
        File.WriteAllText("known_values.txt", jsonString);

        Spreadsheet ss = new Spreadsheet();
        ss.Load("known_values.txt");
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SetEquals(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));
        Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().SequenceEqual(expectedSpreadsheet.GetNamesOfAllNonemptyCells()));

        try
        {
            ss.SetContentsOfCell("A1", "=A1");
        }
        catch (Exception)
        {
        }

        foreach (string cellName in ss.GetNamesOfAllNonemptyCells())
        {
            Assert.AreEqual(ss.GetCellValue(cellName), 5.0);
        }
    }

    /// <summary>
    ///   Test that we can create a spreadsheet and can add and retrieve a value.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("1")]
    public void SetGetCellContents_SingleStringAdded_HasOneString()
    {
        Spreadsheet s = new();
        s.SetContentsOfCell("A1", "x");
        Assert.AreEqual(s.GetNamesOfAllNonemptyCells().Count, 1);
        Assert.AreEqual("x", s.GetCellContents("A1"));
    }

    /// <summary>
    ///   An empty sheet should not contain values.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("2")]
    public void Constructor_Default_ShouldBeEmpty()
    {
        Spreadsheet ss = new();

        var results = new Dictionary<string, object>
        {
            { "A1", string.Empty },
            { "B10", string.Empty },
            { "CC101", string.Empty },
        };

        ss.CompareToExpectedValues(results);
        ss.CompareCounts(results);
    }

    /// <summary>
    ///    Test that you can add one string to a spreadsheet.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> If used alone, check the count as well as the values. </param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("3")]
    [DataRow(null, true)]
    public void SetContentsOfCell_AddStringToCell_CellContainsString(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "B1", "hello" },
            { "B2", string.Empty },
        };

        ss.SetContentsOfCell("B1", "hello");
        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that you can add one string to a spreadsheet.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("4")]
    [DataRow(null, true)]
    public void SetContentsOfCell_AddNumberToCell_CellContainsNumber(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "C1", 17.5 },
            { "C2", string.Empty },
        };

        ss.SetContentsOfCell("C1", "17.5");
        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that you can add a simple formula ("=5") to a spreadsheet.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("5")]
    [DataRow(null, true)]
    public void SetContentsOfCell_AddSimpleFormulaToCell_CellEvaluatesCorrectly(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 5.0 },
            { "A2", string.Empty },
        };

        ss.SetContentsOfCell("A1", "=5");
        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///   Test that you can add a formula that depends on one other cell.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("6")]
    [DataRow(null, true)]
    public void SetContentsOfCell_FormulaBasedOnSingleOtherCell_EvaluatesCorrectly(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.1 },
            { "C1", 8.3 },
        };

        ss.SetContentsOfCell("A1", "4.1");
        ss.SetContentsOfCell("C1", "=A1+4.2");
        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that you can add one formula to a spreadsheet that depends on two other cells.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("7")]
    [DataRow(null, true)]
    public void SetContentsOfCell_FormulaDependsOnTwoCells_AllCellsComputeCorrectly(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.1 },
            { "B1", 5.2 },
            { "C1", 9.3 },
        };

        ss.SetContentsOfCell("A1", "4.1");
        ss.SetContentsOfCell("B1", "5.2");
        ss.SetContentsOfCell("C1", "=A1+B1");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that formulas work for addition, subtraction, multiplication, and division.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("8")]
    [DataRow(null, true)]
    public void SetContentsFormulas_AddSubtractMultiplyDivide_AllWorkAsExpected(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.4 },
            { "B1", 2.2 },
            { "C1", 6.6 },
            { "D1", 2.2 },
            { "E1", 4.4 * 2.2 },
            { "F1", 2.0 },
        };

        ss.SetContentsOfCell("A1", "4.4");
        ss.SetContentsOfCell("B1", "2.2");
        ss.SetContentsOfCell("C1", "= A1 + B1");
        ss.SetContentsOfCell("D1", "= A1 - B1");
        ss.SetContentsOfCell("E1", "= A1 * B1");
        ss.SetContentsOfCell("F1", "= A1 / B1");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Increase score for grading tests.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("9")]
    public void IncreaseGradingWeight1()
    {
        SetContentsFormulas_AddSubtractMultiplyDivide_AllWorkAsExpected(null, true);
    }

    /// <summary>
    ///    Increase score for grading tests.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("10")]
    public void IncreaseGradingWeight2()
    {
        SetContentsFormulas_AddSubtractMultiplyDivide_AllWorkAsExpected(null, true);
    }

    /// <summary>
    ///    Increase score for grading tests.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("11")]
    public void IncreaseGradingWeight3()
    {
        SetContentsFormulas_AddSubtractMultiplyDivide_AllWorkAsExpected(null, true);
    }

    /// <summary>
    ///    Test that division by an empty cell is an error.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("12")]
    [DataRow(null, true)]
    public void DivisionByEmptyCell(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.1 },
            { "B1", string.Empty },
            { "C1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
        };

        ss.SetContentsOfCell("A1", "4.1");
        ss.SetContentsOfCell("C1", "=A1/B1");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that you cannot add a number to a string.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("13")]
    [DataRow(null, true)]
    public void AddNumberToString(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.1 },
            { "B1", "hello" },
            { "C1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
        };

        ss.SetContentsOfCell("A1", "4.1");
        ss.SetContentsOfCell("B1", "hello");
        ss.SetContentsOfCell("C1", "=A1+B1");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that a formula computed from a cell with a formula error value
    ///    is also a formula error.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("14")]
    [DataRow(null, true)]
    public void FormulaFromFormulaError(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", "hello" },
            { "B1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
            { "C1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
        };

        ss.SetContentsOfCell("A1", "hello");
        ss.SetContentsOfCell("B1", "=A1");
        ss.SetContentsOfCell("C1", "=B1");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Test that direct division by 0 in a formula is caught.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("15")]
    [DataRow(null, true)]
    public void DivisionByZero1(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.1 },
            { "B1", string.Empty },
            { "C1", new FormulaError( "Division by zero" ) },
        };

        ss.SetContentsOfCell("A1", "4.1");
        ss.SetContentsOfCell("B1", string.Empty);
        ss.SetContentsOfCell("C1", "=A1/0.0");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///    Check that division by a cell which contains zero is caught.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("16")]
    [DataRow(null, true)]
    public void DivisionByZero2(Spreadsheet? ss, bool verifyCounts)
    {
        ss ??= new();

        var results = new Dictionary<string, object>
        {
            { "A1", 4.1 },
            { "B1", 0.0 },
            { "C1", new FormulaError( "Division by zero") },
        };

        ss.SetContentsOfCell("A1", "4.1");
        ss.SetContentsOfCell("B1", "0.0");
        ss.SetContentsOfCell("C1", "= A1 / B1");

        ss.CompareToExpectedValues(results);

        if (verifyCounts)
        {
            ss.CompareCounts(results);
        }
    }

    /// <summary>
    ///   Repeats the simple tests all together.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("17")]
    public void SpreadsheetOverall_CombineMultipleTests_AllCorrect()
    {
        Spreadsheet ss = new();
        var results = new Dictionary<string, object>
        {
            { "A1", "hello" },
            { "B1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
            { "C1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
        };

        ss.SetContentsOfCell("A1", "17.32");
        ss.SetContentsOfCell("B1", "This is a test");
        ss.SetContentsOfCell("C1", "=C2+C3");

        SetContentsOfCell_AddStringToCell_CellContainsString(ss, false);
        SetContentsOfCell_AddNumberToCell_CellContainsNumber(ss, false);
        SetContentsOfCell_FormulaDependsOnTwoCells_AllCellsComputeCorrectly(ss, false);

        DivisionByZero1(ss, false);
        DivisionByZero2(ss, false);

        AddNumberToString(ss, false);
        FormulaFromFormulaError(ss, false);

        ss.CompareToExpectedValues(results);
    }

    /// <summary>
    ///    Five cells related to each other.  Make sure original values are
    ///    correctly computed (Formula Errors), then change end cells, then check that the
    ///    new values are correct.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("18")]
    [DataRow(null)]
    public void SetContentsOfCell_SimpleFormulas_ComputeCorrectResults(Spreadsheet? ss)
    {
        var expectedResults = new Dictionary<string, object>
        {
            { "A1", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
            { "A2", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
        };

        ss ??= new();

        ss.SetContentsOfCell("A1", "= A2 + A3");
        ss.SetContentsOfCell("A2", "= B1 + B2");

        ss.CompareToExpectedValues(expectedResults);
        ss.CompareCounts(expectedResults);

        ss.SetContentsOfCell("A3", "5.0");
        ss.SetContentsOfCell("B1", "2.0");
        ss.SetContentsOfCell("B2", "3.0");

        expectedResults["A1"] = 10.0;
        expectedResults["A2"] = 5.0;
        expectedResults["A3"] = 5.0;
        expectedResults["B1"] = 2.0;
        expectedResults["B2"] = 3.0;

        ss.CompareToExpectedValues(expectedResults);
        ss.CompareCounts(expectedResults);

        ss.SetContentsOfCell("B2", "4.0");

        expectedResults["A1"] = 11.0;
        expectedResults["A2"] = 6.0;
        expectedResults["B2"] = 4.0;

        ss.CompareToExpectedValues(expectedResults);
        ss.CompareCounts(expectedResults);
    }

    /// <summary>
    ///    Change the end cell of a three cell chain and check for
    ///    the correct computation of results.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("20")]
    [DataRow(null, true)]
    public void SetContentsOfCell_ThreeCellChainFormula_ComputesCorrectResults(Spreadsheet? ss, bool verifyCounts)
    {
        var expectedResults = new Dictionary<string, object>
        {
            { "A1", 12.0 },
            { "A2",  6.0 },
            { "A3",  6.0 },
        };

        ss ??= new();

        ss.SetContentsOfCell("A1", "= A2 + A3");
        ss.SetContentsOfCell("A2", "= A3");
        ss.SetContentsOfCell("A3", "6.0");

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }

        ss.SetContentsOfCell("A3", "5.0");
        expectedResults["A1"] = 10.0;
        expectedResults["A2"] = 5.0;
        expectedResults["A3"] = 5.0;

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }
    }

    /// <summary>
    ///    Five cells chained together.  Make sure initial values are
    ///    computed correctly, then change end cells, then make sure
    ///    new values are computed correctly.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("21")]
    [DataRow(null, true)]
    public void SetContentsOfCell_FiveCellsWithValues_CorrectValuesComputed(Spreadsheet? ss, bool verifyCounts)
    {
        var expectedResults = new Dictionary<string, object>
        {
            { "A1", 18.0 },
            { "A2", 18.0 },
            { "A3",  9.0 },
            { "A4",  9.0 },
            { "A5",  9.0 },
        };

        ss ??= new();

        ss.SetContentsOfCell("A1", "= A3 + A5");
        ss.SetContentsOfCell("A2", "= A5 + A4");
        ss.SetContentsOfCell("A3", "= A5");
        ss.SetContentsOfCell("A4", "= A5");
        ss.SetContentsOfCell("A5", "9.0");

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }

        ss.SetContentsOfCell("A5", "8.0");
        expectedResults["A1"] = 16.0;
        expectedResults["A2"] = 16.0;
        expectedResults["A3"] = 8.0;
        expectedResults["A4"] = 8.0;
        expectedResults["A5"] = 8.0;

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }
    }

    /// <summary>
    ///    Combine the other tests and make sure that they all work
    ///    in combination with each other.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("22")]
    public void SpreadsheetOverall_CombineOtherTests_CorrectValuesComputed()
    {
        var expectedResults = new Dictionary<string, object>
        {
            { "A1", 16.0 },
            { "A2", 16.0 },
            { "A3",  8.0 },
            { "A4",  8.0 },
            { "A5",  8.0 },
            { "B1",  2.0 },
            { "B2",  4.0 },
        };

        Spreadsheet ss = new();
        SetContentsOfCell_SimpleFormulas_ComputeCorrectResults(ss);
        SetContentsOfCell_ThreeCellChainFormula_ComputesCorrectResults(ss, false);
        SetContentsOfCell_FiveCellsWithValues_CorrectValuesComputed(ss, false);

        ss.CompareToExpectedValues(expectedResults);
        ss.CompareCounts(expectedResults);
    }

    /// <summary>
    ///   Increase the grading weight of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("23")]
    public void IncreaseGradingWeight4()
    {
        SpreadsheetOverall_CombineOtherTests_CorrectValuesComputed();
    }

    /// <summary>
    ///   Check that the base case (empty cell) index works.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("24")]
    public void Indexer_EmptyCell_EmptyStringValue()
    {
        Spreadsheet ss = new();

        Assert.AreEqual(ss["A1"], string.Empty);
        Assert.AreEqual(ss["A1"], ss.GetCellValue("A1"));
    }

    /// <summary>
    ///   Check that a double value can be returned.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("25")]
    public void Indexer_DoubleValue_Returns5()
    {
        Spreadsheet ss = new();

        ss.SetContentsOfCell("A1", "5");
        Assert.AreEqual((double)ss["A1"], 5.0, .001);
        Assert.AreEqual((double)ss["A1"], (double)ss.GetCellValue("A1"), .001);
    }

    /// <summary>
    ///   Check that a string can be returned.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("26")]
    public void Indexer_StringValue_ReturnsHelloWorld()
    {
        Spreadsheet ss = new();

        ss.SetContentsOfCell("A1", "hello world");
        Assert.AreEqual(ss["A1"], "hello world");
        Assert.AreEqual(ss["A1"], ss.GetCellValue("A1"));
    }

    /// <summary>
    ///   Check that a formulas computed value can be returned,
    ///   both as a FormulaError and as a double.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("27")]
    public void Indexer_GetFormulaValue_ReturnsErrorThenValue()
    {
        Spreadsheet ss = new();

        ss.SetContentsOfCell("A1", "=A2");
        Assert.IsInstanceOfType<FormulaError>(ss["A1"]);

        ss.SetContentsOfCell("A2", "1.234");
        Assert.AreEqual((double)ss["A1"], 1.234, 0.0000001);
        Assert.AreEqual((double)ss["A1"], (double)ss.GetCellValue("A1"), 0.00000001);
    }
}

/// <summary>
///    Check cell name normalization (up-casing).
/// </summary>
[TestClass]
public class SpreadSheetNormalizationTests
{
    /// <summary>
    ///   Check name normalization. Given a lower case
    ///   cell name, it should work and be up-cased.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("28")]
    public void SetGetCellContents_LowerCaseCellName_NormalizedToUpper()
    {
        Spreadsheet s = new();

        s.SetContentsOfCell("B1", "hello");
        Assert.AreEqual("hello", s.GetCellContents("B1"));
        s.GetNamesOfAllNonemptyCells().Matches(["B1"]);
    }

    /// <summary>
    ///   Check name normalization. Given a formula with a mis-cased
    ///   cell name, it should still work.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("29")]
    public void SetContentsGetValue_MisCasedVariableNamesInFormula_ShouldStillWork()
    {
        Spreadsheet s = new();

        s.SetContentsOfCell("A1", "6");
        s.SetContentsOfCell("B1", "= A1");

        Assert.AreEqual(6.0, (double)s.GetCellValue("B1"), 1e-9);
    }
}

/// <summary>
///    Test some simple mistakes that a user might make,
///    including invalid formulas resulting in the appropriate contents and values,
///    as well as invalid names.
/// </summary>
[TestClass]
public class SimpleInvalidSpreadsheetTests
{
    /// <summary>
    ///   Test that a formula can be added and retrieved.  The
    ///   contents of the cell are a Formula and the value of
    ///   the cell is a Formula Error.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("30")]
    public void SetContentsOfCell_InvalidFormula_CreatedFormulaValueIsErrorType()
    {
        Spreadsheet s = new();
        s.SetContentsOfCell("B1", "=A1+C1");
        Assert.AreEqual(s.GetNamesOfAllNonemptyCells().Count, 1);
        Assert.IsInstanceOfType<Formula>(s.GetCellContents("B1"));
        Assert.IsInstanceOfType<FormulaError>(s.GetCellValue("B1"));
    }

    /// <summary>
    ///   Test that an invalid cell name doesn't affect spreadsheet.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("31")]
    public void SetGetCellContents_InvalidNameAdded_StillEmpty()
    {
        Spreadsheet s = new();

        try
        {
            s.SetContentsOfCell("1A1", "x");
        }
        catch
        {
            Assert.AreEqual(s.GetNamesOfAllNonemptyCells().Count, 0);
            Assert.AreEqual(string.Empty, s.GetCellContents("A1"));
            return;
        }

        Assert.Fail();
    }
}

/// <summary>
///    Test the changed property of the spreadsheet.
/// </summary>
[TestClass]
public class SpreadsheetChangedTests
{
    /// <summary>
    ///   After setting a cell, the spreadsheet is changed.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("32")]
    public void Changed_AfterModification_IsTrue()
    {
        Spreadsheet ss = new();
        Assert.IsFalse(ss.Changed);
        ss.SetContentsOfCell("C1", "17.5");
        Assert.IsTrue(ss.Changed);
    }

    /// <summary>
    ///   After saving the spreadsheet to a file, the spreadsheet is not changed.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("33")]
    public void Changed_AfterSave_IsFalse()
    {
        Spreadsheet ss = new();
        ss.SetContentsOfCell("C1", "17.5");
        ss.Save("changed.txt");
        Assert.IsFalse(ss.Changed);
    }
}

/// <summary>
///    Test that you can have multiple spreadsheet objects
///    and they don't interact with each other (e.g., by using static
///    fields/properties).
/// </summary>
[TestClass]
public class SpreadsheetNonStaticFields
{
    /// <summary>
    ///    Check that two spreadsheet objects are independent.  If we add
    ///    a value to one, it doesn't influence the other.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("34")]
    public void Constructor_SetCellContents_SpreadSheetsAreIndependent()
    {
        Spreadsheet s1 = new();
        Spreadsheet s2 = new();

        var results = new Dictionary<string, object>
        {
            { "X1", "hello" },
            { "A1", string.Empty },
            { "B1", 5.0 },
        };

        s1.SetContentsOfCell("X1", "hello");
        s1.SetContentsOfCell("B1", "5.0");
        s2.SetContentsOfCell("X1", "goodbye");

        s1.CompareToExpectedValues(results);
        s1.CompareCounts(results);

        results["X1"] = "goodbye";
        results.Remove("B1");

        s2.CompareToExpectedValues(results);
        s2.CompareCounts(results);
    }

    /// <summary>
    ///    Increase score for grading tests.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("35")]
    public void IncreaseGradingWeight1()
    {
        Constructor_SetCellContents_SpreadSheetsAreIndependent();
    }

    /// <summary>
    ///    Increase score for grading tests.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("36")]
    public void IncreaseGradingWeight2()
    {
        Constructor_SetCellContents_SpreadSheetsAreIndependent();
    }

    /// <summary>
    ///    Increase score for grading tests.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("37")]
    public void IncreaseGradingWeight3()
    {
        Constructor_SetCellContents_SpreadSheetsAreIndependent();
    }
}

/// <summary>
///    Test that circular exceptions are handled correctly.
/// </summary>
[TestClass]
public class SpreadsheetCircularExceptions
{
    /// <summary>
    ///    Check that a simple circular exception is thrown.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("38")]
    [ExpectedException(typeof(CircularException))]
    public void SetCellContents_CircularException_Throws()
    {
        Spreadsheet s1 = new();
        s1.SetContentsOfCell("A1", "=A1");
    }

    /// <summary>
    ///    Check that assigning a circular exception doesn't change rest of spreadsheet.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("39")]
    public void SetCellContents_CircularException_DoesNotChangeRestOfSheet()
    {
        var results = new Dictionary<string, object>
        {
            { "A1", "hello" },
            { "A2", 10.0 },
            { "A3", new FormulaError( "Only cells that evaluate to a number are valid for lookup." ) },
            { "A4", 9.0 },
        };

        Spreadsheet s1 = new();
        s1.SetContentsOfCell("A1", "hello");
        s1.SetContentsOfCell("A2", "=5+5");
        s1.SetContentsOfCell("A3", "=A1");
        s1.SetContentsOfCell("A4", "9.0");

        try
        {
            s1.SetContentsOfCell("A1", "=A1");
        }
        catch
        {
            s1.CompareToExpectedValues(results);
            return;
        }

        Assert.Fail();
    }
}

/// <summary>
///    Test file input and output.
/// </summary>
[TestClass]
public class SpreadsheetLoadSaveTests
{
    /// <summary>
    ///   Try to save to a bad file.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("40")]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void Save_InvalidMissingFolder_Throws()
    {
        Spreadsheet s1 = new();
        s1.Save("."); // note: this test was updated and students will all be given a point for it.
    }

    /// <summary>
    ///   Try to save to a folder (i.e., ".").
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("40")]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void Save_ToCurrentFolderPeriod_Throws()
    {
        Spreadsheet s1 = new();
        s1.Save(".");
    }

    /// <summary>
    ///   Try to load from a bad file.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("41")]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void Load_FromMissingFile_Throws()
    {
        // should not be able to read
        Spreadsheet ss = new();
        ss.Load("q:\\missing\\save.txt");
    }

    /// <summary>
    ///   Write a single string to a spreadsheet, save the file,
    ///   load the file, look to see if the value is back.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("42")]
    public void SaveLoad_CreateAVerySimpleSheetSaveAndLoad_GetOriginalBack()
    {
        var results = new Dictionary<string, object>
        {
            { "A1", "hello" },
            { "B1", string.Empty },
        };

        Spreadsheet s1 = new();
        s1.SetContentsOfCell("A1", "hello");
        s1.Save("save1.txt");
        s1.CompareToExpectedValues(results);
        s1.CompareCounts(results);

        Spreadsheet s2 = new();
        s2.Load("save1.txt");

        s2.CompareToExpectedValues(results);
        s2.CompareCounts(results);
    }

    /// <summary>
    ///   Should not be able to read a file that is not correct JSON.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("43")]
    [ExpectedException(typeof(SpreadsheetReadWriteException))]
    public void Load_InvalidXMLFile_Throws()
    {
        using (StreamWriter writer = new("save2.txt"))
        {
            writer.WriteLine("This");
            writer.WriteLine("is");
            writer.WriteLine("a");
            writer.WriteLine("test!");
        }

        Spreadsheet s1 = new();
        s1.Load("save2.txt");
    }

    /// <summary>
    ///   Save a sheet, load the file with it, make sure the new sheet has
    ///   the expected values.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("44")]
    public void Load_ReadValidPreDefinedJSONFile_ContainsCorrectData()
    {
        var results = new Dictionary<string, object>
        {
            { "A1", "hello" },
            { "A2", 5.0 },
            { "A3", 4.0 },
            { "A4", 9.0 },
        };

        var sheet = new
        {
            Cells = new
            {
                A1 = new { StringForm = "hello" },
                A2 = new { StringForm = "5.0" },
                A3 = new { StringForm = "4.0" },
                A4 = new { StringForm = "= A2 + A3" },
            },
        };

        File.WriteAllText("save5.txt", JsonSerializer.Serialize(sheet));

        Spreadsheet ss = new();
        ss.Load("save5.txt");

        ss.CompareToExpectedValues(results);
        ss.CompareCounts(results);
    }

    /// <summary>
    ///    Save a spreadsheet with a string, two numbers, and a formula,
    ///    and see that the saved file contains the proper json.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("45")]
    public void Save_SaveSheetWithDoubleStringAndFormula_GeneratesValidJSONFileSyntax()
    {
        Spreadsheet ss = new();
        ss.SetContentsOfCell("A1", "hello");
        ss.SetContentsOfCell("A2", "5.0");
        ss.SetContentsOfCell("A3", "4.0");
        ss.SetContentsOfCell("A4", "=A2+A3");
        ss.Save("save6.txt");

        string fileContents = File.ReadAllText("save6.txt");

        try
        {
            Dictionary<string, object> root = JsonSerializer.Deserialize<Dictionary<string, object>>(fileContents) ?? [];
            if (!root.TryGetValue("Cells", out object? cellValues))
            {
                Assert.Fail();
            }

            var cells = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(cellValues.ToString() ?? "oops");

            if (cells != null)
            {
                Assert.AreEqual("hello", cells["A1"]["StringForm"]);
                Assert.AreEqual(5.0, double.Parse(cells["A2"]["StringForm"]), 1e-9);
                Assert.AreEqual(4.0, double.Parse(cells["A3"]["StringForm"]), 1e-9);
                Assert.AreEqual("=A2+A3", cells["A4"]["StringForm"].Replace(" ", string.Empty));
            }
            else
            {
                Assert.Fail();
            }
        }
        catch
        {
            Assert.Fail();
        }
    }
}

/// <summary>
///    Test methods on larger spreadsheets.
/// </summary>
[TestClass]
public class LargerSpreadsheetTests
{
    /// <summary>
    ///    Create 7 cells, put some formulas in, change the values,
    ///    and verify that the final and intermediate results are good.
    /// </summary>
    /// <param name="ss"> For use with other tests, allow passing in of a spreadsheet. </param>
    /// <param name="verifyCounts"> if used alone, check the count as well as the values.</param>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("46")]
    [DataRow(null, true)]
    public void SetContentsOfCell_SevenCellsWithModifications_CorrectValuesProduced(Spreadsheet? ss, bool verifyCounts)
    {
        var expectedResults = new Dictionary<string, object>
        {
            { "A1", 1.0 },
            { "A2", 2.0 },
            { "A3", 3.0 },
            { "A4", 4.0 },
            { "B1", 3.0 },
            { "B2", 12.0 },
            { "C1", 9.0 },
        };

        ss ??= new();

        ss.SetContentsOfCell("A1", "1.0");
        ss.SetContentsOfCell("A2", "2.0");
        ss.SetContentsOfCell("A3", "3.0");
        ss.SetContentsOfCell("A4", "4.0");
        ss.SetContentsOfCell("B1", "= A1 + A2");
        ss.SetContentsOfCell("B2", "= A3 * A4");
        ss.SetContentsOfCell("C1", "= B2 - B1");

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }

        ss.SetContentsOfCell("A1", "2.0");
        expectedResults["A1"] = 2.0;
        expectedResults["A2"] = 2.0;
        expectedResults["A3"] = 3.0;
        expectedResults["B1"] = 4.0;
        expectedResults["B2"] = 12.0;
        expectedResults["C1"] = 8.0;

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }

        ss.SetContentsOfCell("B1", "= A1 / A2");
        expectedResults["B1"] = 1.0;
        expectedResults["B2"] = 12.0;
        expectedResults["C1"] = 11.0;

        ss.CompareToExpectedValues(expectedResults);

        if (verifyCounts)
        {
            ss.CompareCounts(expectedResults);
        }
    }

    /// <summary>
    ///   Increases the value of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("47")]
    public void IncreaseGradingWeight_MediumSheet1()
    {
        SetContentsOfCell_SevenCellsWithModifications_CorrectValuesProduced(null, true);
    }

    /// <summary>
    ///   See if we can write and read a (slightly) larger spreadsheet.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("48")]
    public void SpreadSheetSaveLoad_OverallTestOfMediumSize_Correct()
    {
        Dictionary<string, object> expectedResults = new()
        {
            ["A1"] = 2.0,
            ["A2"] = 2.0,
            ["A3"] = 3.0,
            ["A4"] = 4.0,
            ["B1"] = 1.0,
            ["B2"] = 12.0,
            ["C1"] = 11.0,
        };

        Spreadsheet s1 = new();
        SetContentsOfCell_SevenCellsWithModifications_CorrectValuesProduced(s1, true);
        s1.Save("save7.txt");

        Spreadsheet s2 = new();
        s2.Load("save7.txt");

        s2.CompareToExpectedValues(expectedResults);
        s2.CompareCounts(expectedResults);
    }

    /// <summary>
    ///   Increases the value of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("49")]
    public void IncreaseGradingWeight_MediumSave1()
    {
        SpreadSheetSaveLoad_OverallTestOfMediumSize_Correct();
    }

    /// <summary>
    ///   <para>
    ///     A long chained formula. Solutions that re-evaluate
    ///     cells on every request, rather than after a cell changes,
    ///     will timeout on this test.
    ///   </para>
    ///   <para>
    ///     A1 = A3+A5
    ///     A2 = A3+A4
    ///     A3 = A5+A6
    ///     A4 = A5+A6
    ///     A5 = A7+A8
    ///     A6 = A7+A8
    ///     etc.
    ///   </para>
    ///   <para>
    ///     In the end we compute the 2^depth.
    ///   </para>
    ///   <para>
    ///     Then we set the end cells to zero and the whole sum goes to 0.
    ///   </para>
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("50")]
    public void SetContentsOfCell_LongChainOfExponentialNumbers_ComputesPowersCorrectly()
    {
        Spreadsheet s1 = new();

        s1.SetContentsOfCell("sum1", "=A1+A2");

        int i;
        int depth = 100;
        for (i = 1; i <= depth * 2; i += 2)
        {
            s1.SetContentsOfCell("A" + i, "= A" + (i + 2) + " + A" + (i + 3));
            s1.SetContentsOfCell("A" + (i + 1), "= A" + (i + 2) + "+ A" + (i + 3));
        }

        s1.SetContentsOfCell("A" + i, "1");
        s1.SetContentsOfCell("A" + (i + 1), "1");
        Assert.AreEqual(Math.Pow(2, depth + 1), (double)s1.GetCellValue("sum1"), 1.0);

        s1.SetContentsOfCell("A" + i, "0");
        Assert.AreEqual(Math.Pow(2, depth), (double)s1.GetCellValue("sum1"), 1.0);

        s1.SetContentsOfCell("A" + (i + 1), "0");
        Assert.AreEqual(0.0, (double)s1.GetCellValue("sum1"), 0.1);
    }

    /// <summary>
    ///   Increases the value of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("51")]
    public void IncreaseGradingWeight_Long1()
    {
        SetContentsOfCell_LongChainOfExponentialNumbers_ComputesPowersCorrectly();
    }

    /// <summary>
    ///   Increases the value of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("52")]
    public void IncreaseGradingWeight_Long2()
    {
        SetContentsOfCell_LongChainOfExponentialNumbers_ComputesPowersCorrectly();
    }

    /// <summary>
    ///   Increases the value of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("53")]
    public void IncreaseGradingWeight_Long3()
    {
        SetContentsOfCell_LongChainOfExponentialNumbers_ComputesPowersCorrectly();
    }

    /// <summary>
    ///   Increases the value of the given test.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    [TestCategory("54")]
    public void IncreaseGradingWeight_Long4()
    {
        SetContentsOfCell_LongChainOfExponentialNumbers_ComputesPowersCorrectly();
    }
}

/// <summary>
///   Helper methods for the tests above.
/// </summary>
public static class TestExtensions
{
    /// <summary>
    ///   Check to see if the two "sets" (source and items) match, i.e.,
    ///   contain exactly the same values. Note: we do not check for sequencing.
    /// </summary>
    /// <param name="source"> original container.</param>
    /// <param name="items"> elements to match against.</param>
    /// <returns> true if every element in source is in items and vice versa. They are the "same set".</returns>
    public static bool Matches(this IEnumerable<string> source, params string[] items)
    {
        return (source.Count() == items.Length) && items.All(item => source.Contains(item));
    }

    /// <summary>
    ///   This function takes in a spreadsheet object and a List
    ///   of expected Cell values which are compared with the spreadsheet.
    ///   Failure to match results in an error.
    ///   <para>
    ///     It is valid to have additional values in the spreadsheet which are not checked.
    ///   </para>
    /// </summary>
    /// <param name="sheet"> The spreadsheet being tested. </param>
    /// <param name="expectedValues"> Key-value pairs for what should be in the spreadsheet. </param>
    public static void CompareToExpectedValues(this Spreadsheet sheet, Dictionary<string, object> expectedValues)
    {
        foreach (var cellName in expectedValues.Keys)
        {
            if (expectedValues[cellName] is double number)
            {
                Assert.AreEqual(number, (double)sheet.GetCellValue(cellName), 1e-9);
            }
            else if (expectedValues[cellName] is string entry)
            {
                Assert.AreEqual(entry, sheet.GetCellValue(cellName));
            }
            else if (expectedValues[cellName] is FormulaError error)
            {
                Assert.IsInstanceOfType(error, typeof(FormulaError));
            }
            else
            {
                throw new Exception("Invalid data in expected value dictionary!");
            }
        }
    }

    /// <summary>
    ///   This function takes in a spreadsheet object and a List
    ///   of expected Cell values and makes sure that there are not
    ///   any extra values in the sheet (e.g., more non-empty cells
    ///   than expected).  Failure to match results in an error.
    ///   <para>
    ///     It is valid to have additional values in the spreadsheet which are not checked.
    ///   </para>
    /// </summary>
    /// <param name="sheet"> The spreadsheet being tested. </param>
    /// <param name="expectedValues"> Key-value pairs for what should be in the spreadsheet. </param>
    public static void CompareCounts(this Spreadsheet sheet, Dictionary<string, object> expectedValues)
    {
        int numberOfExpectedResults = expectedValues.Values.Where(o => o.ToString() != string.Empty).Count();
        int numberOfNonEmptyCells = sheet.GetNamesOfAllNonemptyCells().Count;

        Assert.AreEqual(numberOfExpectedResults, numberOfNonEmptyCells);
    }
}