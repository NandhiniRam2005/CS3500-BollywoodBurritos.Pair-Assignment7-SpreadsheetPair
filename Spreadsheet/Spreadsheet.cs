// <copyright file="Spreadsheet.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <summary>
// Author:    Joel Rodriguez,  Profs Joe, Professor Kopta, and Professor Jim.
// Partner:   None
// Date:      September 27, 2024
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
//    This file contains ...
//
// </summary>

namespace CS3500.Spreadsheet;

using CS3500.Formula;
using CS3500.DependencyGraph;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Net.Mime.MediaTypeNames;

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
    private const string VariableRegExPattern = @"[a-zA-Z]+\d+";
    private DependencyGraph dependencyGraph;
    private Dictionary<string, Cell> nonEmptyCells;

    /// <summary>
    ///  Initializes a new instance of the <see cref="Spreadsheet"/> class.
    /// </summary>
    public Spreadsheet()
    {
        this.dependencyGraph = new DependencyGraph();
        this.nonEmptyCells = new Dictionary<string, Cell>();
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
            namesOfNonEmpty.Add(nameOfCell);
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
        if (!IsVar(name))
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
    public IList<string> SetCellContents(string name, double number)
    {
        if (!IsVar(name))
        {
            throw new InvalidNameException();
        }

        string nameOfCell = NormalizeToken(name);
        if (!this.nonEmptyCells.ContainsKey(nameOfCell))
        {
            this.nonEmptyCells.Add(nameOfCell, new Cell(nameOfCell, number));
        }
        else
        {
            this.nonEmptyCells[nameOfCell].SetContent(number);
        }

        return this.GetCellsToRecalculate(nameOfCell).ToList();
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
    public IList<string> SetCellContents(string name, string text)
    {
        string nameOfCell = NormalizeToken(name);
        if (!IsVar(name))
        {
            throw new InvalidNameException();
        }

        // If the given string is empty that means the cell is empty and we need to remove it from our dependency graph and nonEmptyCells dictionary.
        if (text.Equals(string.Empty))
        {
            this.nonEmptyCells.Remove(nameOfCell);
            this.dependencyGraph.ReplaceDependees(nameOfCell, new HashSet<string>());
            return this.GetCellsToRecalculate(nameOfCell).ToList();
        }

        if (!this.nonEmptyCells.ContainsKey(nameOfCell))
        {
            this.nonEmptyCells.Add(nameOfCell, new Cell(nameOfCell, text));
        }
        else
        {
            this.nonEmptyCells[nameOfCell].SetContent(text);
        }

        return this.GetCellsToRecalculate(nameOfCell).ToList();
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
    public IList<string> SetCellContents(string name, Formula formula)
    {
        if (!IsVar(name))
        {
            throw new InvalidNameException();
        }

        string nameOfCell = NormalizeToken(name);
        if (!this.nonEmptyCells.ContainsKey(nameOfCell))
        {
            // Checks to see if any of the variables in the given formula cause a direct circular exception.
            HashSet<string> formulaVariables = formula.GetVariables().ToHashSet();
            if (formulaVariables.Contains(nameOfCell))
            {
                throw new CircularException();
            }

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
                this.SetCellContents(nameOfCell, string.Empty);
                throw new CircularException();
            }

            this.nonEmptyCells.Add(nameOfCell, new Cell(nameOfCell, formula));
        }
        else
        {
            Formula ogFormula = (Formula)this.nonEmptyCells[nameOfCell].GetContent();

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
                this.SetCellContents(nameOfCell, ogFormula);
                throw new CircularException();
            }

            this.nonEmptyCells[nameOfCell].SetContent(formula);
        }

        // Returns the cells in the order they need to be evaluated in.
        return this.GetCellsToRecalculate(nameOfCell).ToList();
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
    /// A private helper method that "normalizes" tokens. Names such as
    /// such as x1 turn into X1.
    /// </summary>
    /// <param name="nameOfCell"> The token to be normalized.</param>
    /// <returns>A normalized token. Refer to method summary on what normalizing is.</returns>
    private static string NormalizeToken(string nameOfCell)
    {
        string normalizedNameOfCell = nameOfCell.ToUpper();
        return normalizedNameOfCell;
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
        LinkedList<string> changed = new ();
        HashSet<string> visited = new ();
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
        visited.Add(name); // Adds the name of the cell we are currently on to the visited list
        foreach (string dependent in this.GetDirectDependents(name)) // For each cell that is a direct dependent on the name cell.
        {
            if (dependent.Equals(start)) // If that dependent is equal to the dependent that we started on this means a circular exception has occurred.
            {
                throw new CircularException();
            }
            else if (!visited.Contains(dependent)) // Else if the visited list does not contain the dependent that means that it and its dependents have not been checked so we must go down its path.
            {
                this.Visit(start, dependent, visited, changed); // Recursion
            }
        }

        changed.AddFirst(name); // Once we reach this point it means that the cell we are currently on (name) has to be changed due to the new cell contents.

                                // We add it to a linked list so we maintain the order of the dependents needing to be changed.
    }
}

/// <summary>
/// A general idea of what a cell is in a Spreadsheet.
/// </summary>
internal class Cell
{
    private string name;
    private object content;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="name"> The name of the cell.</param>
    /// <param name="content">The content in the cell which can be a double, string, or formula.</param>
    public Cell(string name, object content)
    {
        this.name = name;
        this.content = content;
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
}