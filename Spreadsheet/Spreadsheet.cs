// <copyright file="Spreadsheet.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

namespace CS3500.Spreadsheet;

using CS3500.Formula;
using CS3500.DependencyGraph;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using System.Collections;

/// <summary>
/// Author:    Joel Rodriguez,  Profs Joe, Professor Kopta, and Professor Jim.
/// Partner:   None
/// Date:      October 18, 2024
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
///    This file contains the basic structure and idea of a spreadsheet it contains the idea of cells and ways to fill those cells in an
///    actual spreadsheet. This file is the Model in MVC for our spreadsheet project. We also have ways of handling circular dependencies
///    and invalid names.
///
/// </summary>

/// <summary>
///   <para>
///     Thrown to indicate that a change to a cell will cause a circular dependency.
///   </para>
/// </summary>
public class CircularException : Exception
{
}

/// <summary>
///   <para>
///     Thrown to indicate that a name parameter was invalid.
///   </para>
/// </summary>
public class InvalidNameException : Exception
{
}

/// <summary>
///   <para>
///     An Spreadsheet object represents the state of a simple spreadsheet.  A
///     spreadsheet represents an infinite number of named cells.
///   </para>
/// <para>
///     Valid Cell Names: A string is a valid cell name if and only if it is one or
///     more letters followed by one or more numbers, e.g., A5, BC27.
/// </para>
/// <para>
///    Cell names are case insensitive, so "x1" and "X1" are the same cell name.
///    Your code should normalize (uppercased) any stored name but accept either.
/// </para>
/// <para>
///     A spreadsheet represents a cell corresponding to every possible cell name.  (This
///     means that a spreadsheet contains an infinite number of cells.)  In addition to
///     a name, each cell has a contents and a value.  The distinction is important.
/// </para>
/// <para>
///     The <b>contents</b> of a cell can be (1) a string, (2) a double, or (3) a Formula.
///     If the contents of a cell is set to the empty string, the cell is considered empty.
/// </para>
/// <para>
///     By analogy, the contents of a cell in Excel is what is displayed on
///     the editing line when the cell is selected.
/// </para>
/// <para>
///     In a new spreadsheet, the contents of every cell is the empty string. Note:
///     this is by definition (it is IMPLIED, not stored).
/// </para>
/// <para>
///     The <b>value</b> of a cell can be (1) a string, (2) a double, or (3) a FormulaError.
///     (By analogy, the value of an Excel cell is what is displayed in that cell's position
///     in the grid.)
/// </para>
/// <list type="number">
///   <item>If a cell's contents is a string, its value is that string.</item>
///   <item>If a cell's contents is a double, its value is that double.</item>
///   <item>
///     <para>
///       If a cell's contents is a Formula, its value is either a double or a FormulaError,
///       as reported by the Evaluate method of the Formula class.  For this assignment,
///       you are not dealing with values yet.
///     </para>
///   </item>
/// </list>
/// <para>
///     Spreadsheets are never allowed to contain a combination of Formulas that establish
///     a circular dependency.  A circular dependency exists when a cell depends on itself.
///     For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
///     A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
///     dependency.
/// </para>
/// </summary>
public class Spreadsheet
{
    /// <summary>
    ///  A regular expression that is used to check if its var.
    /// </summary>
    private const string VariableRegExPattern = @"[a-zA-Z]+\d+";

    /// <summary>
    ///  The dependency graph created that represents this spreadsheet.
    /// </summary>
    private DependencyGraph dependencyGraph;

    /// <summary>
    ///  A representation of all the non empty cells in this spreadsheet it is a hash map where the key is the name of the cell
    /// and the value is a cell object that stores its contents.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("Cells")]
    private Dictionary<string, Cell> nonEmptyCells;

    /// <summary>
    /// The name of the spreadsheet object.
    /// </summary>
    private string name;

    /// <summary>
    /// Represents if the spreadsheet has been changed since its last load and/or save as field.
    /// </summary>
    private bool _changed;

    /// <summary>
    ///  Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    public Spreadsheet()
    {
        this.name = "default";
        this.dependencyGraph = new DependencyGraph();
        this.nonEmptyCells = new Dictionary<string, Cell>();
        this._changed = false;
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Spreadsheet"/> class but with a set name.
    /// </summary>
    /// <param name="name"> The given name of the spreadsheet.</param>
    public Spreadsheet(string name)
    {
        this.name = name;
        this.dependencyGraph = new DependencyGraph();
        this.nonEmptyCells = new Dictionary<string, Cell>();
        this._changed = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether gets or sets _changed.
    /// Gets the value of the field _changed or changes it (set it).
    /// </summary>
    [JsonIgnore]
    public bool Changed
    {
        get { return _changed; }
        set { _changed = value; }
    }

    /// <summary>
    ///   <para>
    ///     Shortcut syntax to for getting the value of the cell
    ///     using the [] operator.
    ///   </para>
    ///   <para>
    ///     See: <see cref="GetCellValue(string)"/>.
    ///   </para>
    ///   <para>
    ///     Example Usage:
    ///   </para>
    ///   <code>
    ///      sheet.SetContentsOfCell( "A1", "=5+5" );
    ///
    ///      sheet["A1"] == 10;
    ///      // vs.
    ///      sheet.GetCellValue("A1") == 10;
    ///   </code>
    /// </summary>
    /// <param name="cellName"> Any valid cell name. </param>
    /// <returns>
    ///   Returns the value of a cell.  Note: If the cell is a formula, the value should
    ///   already have been computed.
    /// </returns>
    /// <exception cref="InvalidNameException">
    ///     If the name parameter is invalid, throw an InvalidNameException.
    /// </exception>
    public object this[string cellName]
    {
        get
        {
            string nameOfCell = NormalizeToken(cellName);
            if (!IsValidName(nameOfCell))
            {
                throw new InvalidNameException();
            }

            return this.GetCellValue(cellName);
        }
    }

    /// <summary>
    ///   <para>
    ///     Writes the contents of this spreadsheet to the named file using a JSON format.
    ///     If the file already exists, overwrite it.
    ///   </para>
    ///   <para>
    ///     The output JSON should look like the following.
    ///   </para>
    ///   <para>
    ///     For example, consider a spreadsheet that contains a cell "A1"
    ///     with contents being the double 5.0, and a cell "B3" with contents
    ///     being the Formula("A1+2"), and a cell "C4" with the contents "hello".
    ///   </para>
    ///   <para>
    ///      This method would produce the following JSON string:
    ///   </para>
    ///   <code>
    ///   {
    ///     "Cells": {
    ///       "A1": {
    ///         "StringForm": "5"
    ///       },
    ///       "B3": {
    ///         "StringForm": "=A1+2"
    ///       },
    ///       "C4": {
    ///         "StringForm": "hello"
    ///       }
    ///     }
    ///   }
    ///   </code>
    ///   <para>
    ///     You can achieve this by making sure your data structure is a dictionary
    ///     and that the contained objects (Cells) have property named "StringForm"
    ///     (if this name does not match your existing code, use the JsonPropertyName
    ///     attribute).
    ///   </para>
    ///   <para>
    ///     There can be 0 cells in the dictionary, resulting in { "Cells" : {} }.
    ///   </para>
    ///   <para>
    ///     Further, when writing the value of each cell...
    ///   </para>
    ///   <list type="bullet">
    ///     <item>
    ///       If the contents is a string, the value of StringForm is that string
    ///     </item>
    ///     <item>
    ///       If the contents is a double d, the value of StringForm is d.ToString()
    ///     </item>
    ///     <item>
    ///       If the contents is a Formula f, the value of StringForm is "=" + f.ToString()
    ///     </item>
    ///   </list>
    ///   <para>
    ///     After saving the file, the spreadsheet is no longer "changed".
    ///   </para>
    /// </summary>
    /// <param name="filename"> The name (with path) of the file to save to.</param>
    /// <exception cref="SpreadsheetReadWriteException">
    ///   If there are any problems opening, writing, or closing the file,
    ///   the method should throw a SpreadsheetReadWriteException with an
    ///   explanatory message.
    /// </exception>
    public void Save(string filename)
    {
       string jsonString = string.Empty;
       try
       {
           jsonString = System.Text.Json.JsonSerializer.Serialize<Spreadsheet>(this);
       }
       catch (Exception e)
       {
           throw new SpreadsheetReadWriteException("Error: " + e);
       }

       File.WriteAllText(filename, jsonString);
       Changed = false;
    }

    /// <summary>
    ///   <para>
    ///     Read the data (JSON) from the file and instantiate the current
    ///     spreadsheet.  See <see cref="Save(string)"/> for expected format.
    ///   </para>
    ///   <para>
    ///     Note: First deletes any current data in the spreadsheet.
    ///   </para>
    ///   <para>
    ///     Loading a spreadsheet should set changed to false.  External
    ///     programs should alert the user before loading over a changed sheet.
    ///   </para>
    /// </summary>
    /// <param name="filename"> The saved file name including the path. </param>
    /// <exception cref="SpreadsheetReadWriteException"> When the file cannot be opened or the json is bad.</exception>
    public void Load(string filename)
    {
        Spreadsheet ogSpreadsheet = new Spreadsheet();
        ogSpreadsheet.nonEmptyCells = new Dictionary<string, Cell>(this.nonEmptyCells);

        this.nonEmptyCells.Clear();
        this.dependencyGraph = new DependencyGraph();

        string jsonText = string.Empty;
        try
        {
            jsonText = File.ReadAllText(filename);
            Spreadsheet? tempSpreadsheeet = System.Text.Json.JsonSerializer.Deserialize<Spreadsheet>(jsonText);

            List<string> loadedKeys = new List<string>();

            // needed to get rid of nullable warning.
            if (tempSpreadsheeet != null)
            {
                loadedKeys = tempSpreadsheeet.nonEmptyCells.Keys.ToList();
                int i = 0;
                foreach (Cell cell in tempSpreadsheeet.nonEmptyCells.Values)
                {
                    this.SetContentsOfCell(loadedKeys[i], cell.StringForm);
                    i++;
                }
            }
        }
        catch (Exception e)
        {
            foreach (KeyValuePair<string, Cell> pair in ogSpreadsheet.nonEmptyCells)
            {
                this.SetContentsOfCell(pair.Key, pair.Value.StringForm);
            }

            throw new SpreadsheetReadWriteException("Error:" + e);
        }

        Changed = false;
    }

    /// <summary>
    ///   <para>
    ///     Return the value of the named cell.
    ///   </para>
    /// </summary>
    /// <param name="cellName"> The cell in question. </param>
    /// <returns>
    ///   Returns the value (as opposed to the contents) of the named cell.  The return
    ///   value's type should be either a string, a double, or a CS3500.Formula.FormulaError.
    ///   If the cell contents are a formula, the value should have already been computed
    ///   at this point.
    /// </returns>
    /// <exception cref="InvalidNameException">
    ///   If the provided name is invalid, throws an InvalidNameException.
    /// </exception>
    public object GetCellValue(string cellName)
    {
        string normalizedCellName = NormalizeToken(cellName);
        if (!IsValidName(normalizedCellName))
        {
            throw new InvalidNameException();
        }

        if (this.nonEmptyCells.ContainsKey(normalizedCellName))
        {
            return this.nonEmptyCells[normalizedCellName].GetValue();
        }

        return string.Empty;
    }

    /// <summary>
    ///   <para>
    ///       Sets the contents of the named cell to the appropriate object
    ///       based on the string in <paramref name="content"/>.
    ///   </para>
    ///   <para>
    ///       First, if the <paramref name="content"/> parses as a double, the contents of the named
    ///       cell becomes that double.
    ///   </para>
    ///   <para>
    ///       Otherwise, if the <paramref name="content"/> begins with the character '=', an attempt is made
    ///       to parse the remainder of content into a Formula.
    ///   </para>
    ///   <para>
    ///       There are then three possible outcomes when a formula is detected:
    ///   </para>
    ///
    ///   <list type="number">
    ///     <item>
    ///       If the remainder of content cannot be parsed into a Formula, a
    ///       FormulaFormatException is thrown.
    ///     </item>
    ///     <item>
    ///       If changing the contents of the named cell to be f
    ///       would cause a circular dependency, a CircularException is thrown,
    ///       and no change is made to the spreadsheet.
    ///     </item>
    ///     <item>
    ///       Otherwise, the contents of the named cell becomes f.
    ///     </item>
    ///   </list>
    ///   <para>
    ///     Finally, if the content is a string that is not a double and does not
    ///     begin with an "=" (equal sign), save the content as a string.
    ///   </para>
    ///   <para>
    ///     On successfully changing the contents of a cell, the spreadsheet will be <see cref="Changed"/>.
    ///   </para>
    /// </summary>
    /// <param name="name"> The cell name that is being changed.</param>
    /// <param name="content"> The new content of the cell.</param>
    /// <returns>
    ///   <para>
    ///     This method returns a list consisting of the passed in cell name,
    ///     followed by the names of all other cells whose value depends, directly
    ///     or indirectly, on the named cell. The order of the list MUST BE any
    ///     order such that if cells are re-evaluated in that order, their dependencies
    ///     are satisfied by the time they are evaluated.
    ///   </para>
    ///   <para>
    ///     For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
    ///     list {A1, B1, C1} is returned.  If the cells are then evaluate din the order:
    ///     A1, then B1, then C1, the integrity of the Spreadsheet is maintained.
    ///   </para>
    /// </returns>
    /// <exception cref="InvalidNameException">
    ///   If the name parameter is invalid, throw an InvalidNameException.
    /// </exception>
    /// <exception cref="CircularException">
    ///   If changing the contents of the named cell to be the formula would
    ///   cause a circular dependency, throw a CircularException.
    ///   (NOTE: No change is made to the spreadsheet.)
    /// </exception>
    public IList<string> SetContentsOfCell(string name, string content)
    {
        string nameOfCell = NormalizeToken(name);
        if (!IsValidName(name))
        {
            throw new InvalidNameException();
        }

        if (content.StartsWith("="))
        {
            string formulaContent = content.Substring(1);

            Formula formulaToBeAdded = new Formula(formulaContent);
            return this.SetCellContents(nameOfCell, formulaToBeAdded);
        }

        bool successfullyParsed = double.TryParse(content, out double doubleToBeAdded);
        if(successfullyParsed)
        {
            return this.SetCellContents(nameOfCell, doubleToBeAdded);
        }

        return this.SetCellContents(nameOfCell, content);
    }

    /// <summary>
    ///   Provides a copy of the names of all of the cells in the spreadsheet
    ///   that contain information (i.e., not empty cells).
    /// </summary>
    /// <returns>
    ///   A set of the names of all the non-empty cells in the spreadsheet.
    /// </returns>
    public ISet<string> GetNamesOfAllNonemptyCells()
    {
        HashSet<string> namesOfNonEmpty = new HashSet<string>();
        foreach (string nameOfCell in this.nonEmptyCells.Keys)
        {
            if (!nameOfCell.Equals(string.Empty))
            {
                namesOfNonEmpty.Add(nameOfCell);
            }
        }

        return namesOfNonEmpty;
    }

    /// <summary>
    ///   Returns the contents (as opposed to the value) of the named cell.
    /// </summary>
    ///
    /// <exception cref="InvalidNameException">
    ///   Thrown if the name is invalid.
    /// </exception>
    ///
    /// <param name="name">The name of the spreadsheet cell to query. </param>
    /// <returns>
    ///   The contents as either a string, a double, or a Formula.
    ///   See the class header summary.
    /// </returns>
    public object GetCellContents(string name)
    {
        string nameOfCell = NormalizeToken(name);
        if (!IsValidName(name))
        {
            throw new InvalidNameException();
        }

        if (!this.nonEmptyCells.ContainsKey(nameOfCell))
        {
            return string.Empty;
        }

        object cellContents = this.nonEmptyCells[nameOfCell].GetContent();
        return cellContents;
    }

    /// <summary>
    ///   Reports whether "token" is a variable.  It must be one or more letters
    ///   followed by one or more numbers.
    /// </summary>
    /// <param name="token"> A token that may be a variable. </param>
    /// <returns> true if the string matches the requirements, e.g., A1 or a1. </returns>
    private static bool IsValidName(string token)
    {
        // notice the use of ^ and $ to denote that the entire string being matched is just the variable
        string standaloneVarPattern = $"^{VariableRegExPattern}$";
        return Regex.IsMatch(token, standaloneVarPattern);
    }

    /// <summary>
    /// A private helper method that "normalizes" tokens. Names such as
    /// such as x1 turn into X1.
    /// </summary>
    /// <param name="nameOfCell"> The token to be normalized.</param>
    /// <returns>A normalized token. Refer to method summary on what normalizing is.</returns>
    private static string NormalizeToken(string nameOfCell)
    {
        return nameOfCell.ToUpper();
    }

    /// <summary>
    ///  Set the contents of the named cell to the given number.
    /// </summary>
    ///
    /// <exception cref="InvalidNameException">
    ///   If the name is invalid, throw an InvalidNameException.
    /// </exception>
    ///
    /// <param name="name"> The name of the cell. </param>
    /// <param name="number"> The new content of the cell. </param>
    /// <returns>
    ///   <para>
    ///     This method returns an ordered list consisting of the passed in name
    ///     followed by the names of all other cells whose value depends, directly
    ///     or indirectly, on the named cell.
    ///   </para>
    ///   <para>
    ///     The order must correspond to a valid dependency ordering for recomputing
    ///     all of the cells, i.e., if you re-evaluate each cell in the order of the list,
    ///     the overall spreadsheet will be correctly updated.
    ///   </para>
    ///   <para>
    ///     For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
    ///     list [A1, B1, C1] is returned, i.e., A1 was changed, so then A1 must be
    ///     evaluated, followed by B1 re-evaluated, followed by C1 re-evaluated.
    ///   </para>
    /// </returns>
    private IList<string> SetCellContents(string name, double number)
    {
        if (!this.nonEmptyCells.ContainsKey(name))
        {
            this.nonEmptyCells.Add(name, new Cell(number.ToString()));
        }

        this.dependencyGraph.ReplaceDependees(name, new HashSet<string>());
        this.nonEmptyCells[name].SetContent(number);
        this.nonEmptyCells[name].SetValue(number);
        this.nonEmptyCells[name].StringForm = number.ToString();

        Changed = true;
        return this.GetCellsToRecalculate(name).ToList();
    }

    /// <summary>
    ///   The contents of the named cell becomes the given text.
    /// </summary>
    ///
    /// <exception cref="InvalidNameException">
    ///   If the name is invalid, throw an InvalidNameException.
    /// </exception>
    /// <param name="name"> The name of the cell. </param>
    /// <param name="text"> The new content of the cell. </param>
    /// <returns>
    ///   The same list as defined in <see cref="SetCellContents(string, double)"/>.
    /// </returns>
    private IList<string> SetCellContents(string name, string text)
    {
        this.dependencyGraph.ReplaceDependees(name, new HashSet<string>());
        if (text.Equals(string.Empty))
        {
            this.nonEmptyCells.Remove(name);
            return this.GetCellsToRecalculate(name).ToList();
        }

        if (!this.nonEmptyCells.ContainsKey(name))
        {
            this.nonEmptyCells.Add(name, new Cell(text));
        }
        else
        {
            this.nonEmptyCells[name].SetContent(text);
            this.nonEmptyCells[name].SetValue(text);
            this.nonEmptyCells[name].StringForm = text;
        }

        Changed = true;
        return this.GetCellsToRecalculate(name).ToList();
    }

    /// <summary>
    ///   Set the contents of the named cell to the given formula.
    /// </summary>
    /// <exception cref="InvalidNameException">
    ///   If the name is invalid, throw an InvalidNameException.
    /// </exception>
    /// <exception cref="CircularException">
    ///   <para>
    ///     If changing the contents of the named cell to be the formula would
    ///     cause a circular dependency, throw a CircularException.
    ///   </para>
    ///   <para>
    ///     No change is made to the spreadsheet.
    ///   </para>
    /// </exception>
    /// <param name="name"> The name of the cell. </param>
    /// <param name="formula"> The new content of the cell. </param>
    /// <returns>
    ///   The same list as defined in <see cref="SetCellContents(string, double)"/>.
    /// </returns>
    private IList<string> SetCellContents(string name, Formula formula)
    {
        if (!this.nonEmptyCells.ContainsKey(name))
        {
            CheckForCircularException(name, formula, string.Empty);

            this.nonEmptyCells.Add(name, new Cell("=" + formula.ToString()));

            this.nonEmptyCells[name].ComputeValue(this);
        }
        else
        {
            object ogContents = this.nonEmptyCells[name].GetContent();
            if (ogContents is Formula)
            {
                this.dependencyGraph.ReplaceDependees(name, new HashSet<string>());
            }

            CheckForCircularException(name, formula, ogContents);

            this.nonEmptyCells[name].SetContent(formula);
            this.nonEmptyCells[name].StringForm = "=" + formula.ToString();
            this.nonEmptyCells[name].ComputeValue(this);
            Changed = true;
        }

        return this.GetCellsToRecalculate(name).ToList();
    }

    /// <summary>
    /// This private helper method checks to see if the addition of a new Formula object to our spreadsheet would cause
    /// a circular dependency to occur. If one does occur we will revert the value in that cell to what it was before the
    /// addition of the new formula object.
    /// </summary>
    /// <param name="nameOfCell">The name of the cell which we will be checking for circular dependencies.</param>
    /// <param name="formula"> The formula we are trying to add to our cell.</param>
    /// <param name="ogContents">The original contents of the cell.</param>
    /// <exception cref="CircularException">
    /// <para>
    ///     If changing the contents of the named cell to be the formula would
    ///     cause a circular dependency, throw a CircularException.
    ///   </para>
    ///   <para>
    ///     No change is made to the spreadsheet.
    ///   </para>
    ///   </exception>
    private void CheckForCircularException(string nameOfCell, Formula formula, object ogContents)
    {
        // Checks if any direct relationships cause a circular exception.
        HashSet<string> formulaVariables = formula.GetVariables().ToHashSet();
        if (formulaVariables.Contains(nameOfCell))
        {
            throw new CircularException();
        }

        this.dependencyGraph.ReplaceDependees(nameOfCell, new HashSet<string>());

        foreach (string dependee in formulaVariables)
        {
            this.dependencyGraph.AddDependency(dependee, nameOfCell);
        }

        // This checks to see if the addition caused a circular dependency and reverts the set content cells if it did.
        try
        {
            this.GetCellsToRecalculate(nameOfCell);
        }
        catch (CircularException)
        {
            if (ogContents is Formula ogFormula)
            {
                this.SetCellContents(nameOfCell, ogFormula);
            }
            else if (ogContents is string ogString)
            {
                this.SetCellContents(nameOfCell, ogString);
            }
            else if (ogContents is double ogDouble)
            {
                this.SetCellContents(nameOfCell, ogDouble);
            }

            throw new CircularException();
        }
}

    /// <summary>
    ///   Returns an enumeration, without duplicates, of the names of all cells whose
    ///   values depend directly on the value of the named cell.
    /// </summary>
    /// <param name="name"> This <b>MUST</b> be a valid name.  </param>
    /// <returns>
    ///   <para>
    ///     Returns an enumeration, without duplicates, of the names of all cells
    ///     that contain formulas containing name.
    ///   </para>
    ///   <para>For example, suppose that: </para>
    ///   <list type="bullet">
    ///      <item>A1 contains 3</item>
    ///      <item>B1 contains the formula A1 * A1</item>
    ///      <item>C1 contains the formula B1 + A1</item>
    ///      <item>D1 contains the formula B1 - C1</item>
    ///   </list>
    ///   <para> The direct dependents of A1 are B1 and C1. </para>
    /// </returns>
    private IEnumerable<string> GetDirectDependents(string name)
    {
        string nameOfCell = NormalizeToken(name);
        return this.dependencyGraph.GetDependents(nameOfCell);
    }

    /// <summary>
    ///   <para>
    ///     This method is implemented for you, but makes use of your GetDirectDependents.
    ///   </para>
    ///   <para>
    ///     Returns an enumeration of the names of all cells whose values must
    ///     be recalculated, assuming that the contents of the cell referred
    ///     to by name has changed.  The cell names are enumerated in an order
    ///     in which the calculations should be done.
    ///   </para>
    ///   <exception cref="CircularException">
    ///     If the cell referred to by name is involved in a circular dependency,
    ///     throws a CircularException.
    ///   </exception>
    ///   <para>
    ///     For example, suppose that:
    ///   </para>
    ///   <list type="number">
    ///     <item>
    ///       A1 contains 5
    ///     </item>
    ///     <item>
    ///       B1 contains the formula A1 + 2.
    ///     </item>
    ///     <item>
    ///       C1 contains the formula A1 + B1.
    ///     </item>
    ///     <item>
    ///       D1 contains the formula A1 * 7.
    ///     </item>
    ///     <item>
    ///       E1 contains 15
    ///     </item>
    ///   </list>
    ///   <para>
    ///     If A1 has changed, then A1, B1, C1, and D1 must be recalculated,
    ///     and they must be recalculated in an order which has A1 first, and B1 before C1
    ///     (there are multiple such valid orders).
    ///     The method will produce one of those enumerations.
    ///   </para>
    ///   <para>
    ///      PLEASE NOTE THAT THIS METHOD DEPENDS ON THE METHOD GetDirectDependents.
    ///      IT WON'T WORK UNTIL GetDirectDependents IS IMPLEMENTED CORRECTLY.
    ///   </para>
    /// </summary>
    /// <param name="name"> The name of the cell.  Requires that name be a valid cell name.</param>
    /// <returns>
    ///    Returns an enumeration of the names of all cells whose values must
    ///    be recalculated.
    /// </returns>
    private IEnumerable<string> GetCellsToRecalculate(string name)
    {
        LinkedList<string> changed = new();
        HashSet<string> visited = new();
        this.Visit(name, name, visited, changed);
        return changed;
    }

    /// <summary>
    /// This recursive private helper method visits all cells which are direct and indirect dependents of some start cell.
    /// While traversing the DependencyGraph this method catches any Circular Dependencies.
    /// </summary>
    /// <param name="start"> The original cell which was visited.</param>
    /// <param name="name"> The cell which is currently being visited, changes on every recursive call.</param>
    /// <param name="visited"> The list of all cells which was visited.</param>
    /// <param name="changed"> The list of all cells which are to be changed due to the changing of the start cell.</param>
    /// <exception cref="CircularException"> This exception is to be thrown if a circular dependency is found. </exception>
    private void Visit(string start, string name, ISet<string> visited, LinkedList<string> changed)
    {
        // Adds the name of the cell we are currently on to the visited list
        visited.Add(name);

        // For each cell that is a direct dependent on the name cell.
        foreach (string dependent in this.GetDirectDependents(name))
        {
            // If that dependent is equal to the dependent that we started on this means a circular exception has occurred.
            {
                if (dependent.Equals(start))
                {
                    throw new CircularException();
                }

                // Else if the visited list does not contain the dependent that means that it and its dependents have not been checked so we must go down its path.
                else if (!visited.Contains(dependent))
                {
                    this.Visit(start, dependent, visited, changed);
                }
            }
        }

        // Once we reach this point it means that the cell we are currently on (name) has to be changed due to the new cell contents
        // We add it to a linked list so we maintain the order of the dependents needing to be changed.
        changed.AddFirst(name);
    }
}

/// <summary>
/// A general idea of what a cell is in a Spreadsheet.
/// </summary>
internal class Cell
{
    /// <summary>
    /// The string value of the cell object for example the formula (2+2) should have a string value of "=2+2".
    /// </summary>
    private string _stringForm;

    /// <summary>
    /// The contents of the string which are either a formula object, a double, or a string.
    /// </summary>
    private object content;

    /// <summary>
    /// The actual value of the cell for example a formula of "2+6" would have a value of 8.
    /// </summary>
    private object value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="stringForm"> The string value of the cell.</param>
    public Cell(string stringForm)
    {
        this._stringForm = stringForm;

        // The following line is temporary and will always be fixed later on.
        this.value = 0;
        bool successfullyParsed = double.TryParse(stringForm, out double doubleContent);
        if (stringForm.StartsWith("="))
        {
            string formulaContent = stringForm.Substring(1);

            Formula formula = new Formula(formulaContent);

            this.content = formula;
        }
        else if (successfullyParsed)
        {
            this.value = doubleContent;
            this.content = doubleContent;
        }
        else
        {
            this.content = stringForm;
            this.value = stringForm;
        }
    }

    /// <summary>
    /// Gets or sets the StringForm property.
    /// </summary>
    public string StringForm
    {
        get { return this._stringForm; }
        set { this._stringForm = value; }
    }

    /// <summary>
    /// Computes the value of the cell and stores that inside this cell object.
    /// </summary>
    /// <param name="spreadsheet">The instance of the spreadsheet in which this cell lives in.</param>
    public void ComputeValue(Spreadsheet spreadsheet)
    {
        double MyVariables(string var)
        {
            if (spreadsheet[var] is double value)
            {
                return value;
            }

            if (spreadsheet[var].Equals(string.Empty))
            {
                return 0;
            }

            throw new ArgumentException("Attempting to add two things that are not numbers!");
        }

        string formulaContent = this.StringForm.Substring(1);

        Formula formula = new Formula(formulaContent);

        this.value = formula.Evaluate(MyVariables);
    }

    /// <summary>
    /// Method to get the content of a cell.
    /// </summary>
    /// <returns> The content of the cell whether it be a formula, string, or double.</returns>
    public object GetContent()
    {
        return this.content;
    }

    /// <summary>
    /// Method to get the content of a cell.
    /// </summary>
    /// <param name="givenContent"> The new content for this cell.</param>
    public void SetContent(object givenContent)
    {
        this.content = givenContent;
    }

    /// <summary>
    /// Method to set the value of a cell.
    /// </summary>
    /// <param name="value">The value which the cell will be set to.</param>
    public void SetValue(object value)
    {
        this.value = value;
    }

    /// <summary>
    /// Method to get the value of the cell (not the contents).
    /// </summary>
    /// <returns> The value of this cell.</returns>
    public object GetValue()
    {
        return this.value;
    }
}

/// <summary>
/// <para>
///   Thrown to indicate that a read or write attempt has failed with
///   an expected error message informing the user of what went wrong.
/// </para>
/// </summary>
public class SpreadsheetReadWriteException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="SpreadsheetReadWriteException"/> class.
    ///   <para>
    ///     Creates the exception with a message defining what went wrong.
    ///   </para>
    /// </summary>
    /// <param name="msg"> An informative message to the user. </param>
    public SpreadsheetReadWriteException(string msg)
    : base(msg)
    {
    }
}
