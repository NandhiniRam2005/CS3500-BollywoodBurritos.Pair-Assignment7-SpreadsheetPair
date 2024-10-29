// <copyright file="SpreadsheetGUI.razor.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// Ignore Spelling: Spreadsheeeeeeeeee Blazor Interop

namespace SpreadsheetNS;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Diagnostics;
using CS3500.Spreadsheet;
using CS3500.Formula;
using System.Text.RegularExpressions;

/// <summary>
/// Author:    Joel Rodriguez,  Nandhini Ramanathan, and Professor Jim.
/// Partner:   None
/// Date:      October 29, 2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and [Joel Rodriguez and Nandhini Ramanathan] - This work may no
///            be copied for use in Academic Coursework.
///
/// I, Joel Rodriguez and Nandhini Ramanathan, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All
/// references used in the completion of the assignments are cited
/// in my README file.
///
/// File Contents
///
///    This file contains the basic structure and idea of a spreadsheet it contains the idea of cells and ways to fill those cells in an
///    actual spreadsheet. This file is the Model in MVC for our spreadsheet project. We also have ways of handling circular dependencies
///    and invalid names.
/// </summary>

/// <summary>
/// Represents the partial class for the spreadsheet GUI component.
/// It provides the user interface logic for the spreadsheet and communicates with
/// the Spreadsheet model to reflect changes and user interactions.
///  <remarks>
///    <para>
///      This is a partial class because class SpreadsheetGUI is also automatically
///      generated from the SpreadsheetGUI.razor file.  Any code in that file, and variable in
///      that file can be referenced here, and vice versa.
///    </para>
///    <para>
///      It is usually better to put the code in a separate CS isolation file so that Visual Studio
///      can use intellisense better.
///    </para>
///    <para>
///      Note: only GUI related information should go in the sheet. All (Model) spreadsheet
///      operations should happen through the Spreadsheet class API.
///    </para>
///    <para>
///      The "backing stores" are strings that are used to affect the content of the GUI
///      display.  When you update the Spreadsheet, you will then have to copy that information
///      into the backing store variable(s).
///    </para>
///  </remarks>
/// </summary>
public partial class SpreadsheetGUI
{
    /// <summary>
    /// The Spreadsheet object serving as the model in the MVC pattern.
    /// </summary>
    private Spreadsheet spreadsheet = new Spreadsheet();

    /// <summary>
    ///    Gets the alphabet for ease of creating columns.
    /// </summary>
    private static char[ ] Alphabet { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    /// <summary>
    ///   Gets or sets the javascript object for this web page that allows
    ///   you to interact with any javascript in the associated file.
    /// </summary>
    private IJSObjectReference? JSModule { get; set; }

    /// <summary>
    ///  Gets or sets the default name used when saving the spreadsheet.
    /// </summary>
    private string FileSaveName { get; set; } = Regex.Replace("default", @"[\/@#%&*]", string.Empty) + ".sprd";

    /// <summary>
    ///   <para> Gets or sets the data for the Tool Bar Cell Contents text area. </para>
    ///   <remarks>Backing Store for HTML</remarks>
    /// </summary>
    private string ToolBarCellContents { get; set; } = string.Empty;

    /// <summary>
    ///   <para> Gets or sets the backing store for the cell contents displayed in the GUI.</para>
    ///   <remarks>Backing Store for HTML</remarks>
    /// </summary>
    private string[,] CellsBackingStore { get; set; } = new string[ 100, 26 ];

    /// <summary>
    ///   <para> Gets or sets the html class string for all of the cells in the spreadsheet GUI. </para>
    ///   <remarks>Backing Store for HTML CLASS strings</remarks>
    /// </summary>
    private string[,] CellsClassBackingStore { get; set; } = new string[ 100, 26 ];

    /// <summary>
    ///   Gets or sets a value indicating whether we are showing the save "popup" or not.
    /// </summary>
    private bool SaveGUIView { get; set; }

    private string[,] CellsBackingValue { get; set; } = new string[100, 26];

    /// <summary>
    ///   Query the spreadsheet to see if it has been changed.
    ///   <remarks>
    ///     Any method called from JavaScript must be public
    ///     and JSInvokable!
    ///   </remarks>
    /// </summary>
    /// <returns>
    ///   true if the spreadsheet is changed.
    /// </returns>
    [JSInvokable]
    public bool HasSpreadSheetChanged(  )
    {
        Debug.WriteLine( $"{"HasSpreadSheetChanged",-30}: {Navigator.Uri}" );
        return spreadsheet.Changed;
    }

    /// <summary>
    ///   Example of how JavaScript can talk "back" to the C# side.
    /// </summary>
    /// <param name="message"> string from javascript side. </param>
    [JSInvokable]
    public void TestBlazorInterop(string message)
    {
        Debug.WriteLine($"JavaScript has send me a message: {message}");
    }

    /// <summary>
    ///   Set up initial state and event handlers.
    ///   <remarks>
    ///     This is somewhat like a constructor for a Blazor Web Page (object).
    ///   </remarks>
    /// </summary>
    protected override void OnInitialized( )
    {
        Debug.WriteLine( $"{"OnInitialized",-30}: {Navigator.Uri}" );
    }

    /// <summary>
    ///   Called anytime in the lifetime of the web page were the page is re-rendered.
    /// </summary>
    /// <param name="firstRender"> true the very first time the page is rendered.</param>
    protected async override void OnAfterRender( bool firstRender )
    {
        base.OnAfterRender( firstRender );

        Debug.WriteLine( $"{"OnAfterRenderStart",-30}: {Navigator.Uri} - first time({firstRender})" );

        if ( firstRender )
        {
            // The following three lines setup and test the
            // ability for Blazor to talk to javascript and vice versa.
            JSModule = await JS.InvokeAsync<IJSObjectReference>( "import", "./Pages/SpreadsheetGUI.razor.js" ); // create/read the javascript
            await JSModule.InvokeVoidAsync( "SetDotNetInterfaceObject", DotNetObjectReference.Create( this ) ); // tell the javascript about us (dot net)
            Thread.Sleep(10);
            await FormulaContentEditableInput.FocusAsync(); // when we start up, put the focus on the input, done anytime a cell is clicked.
        }

        Debug.WriteLine( $"{"OnAfterRender Done",-30}: {Navigator.Uri} - Remove Me." );
    }

    /// <summary>
    ///  cells should be of the form "A5" or "B1".  The matrix of cells (the backing store) is zero
    ///  based but the first row in the spreadsheet is 1.
    /// </summary>
    /// <param name="cellName"> The name of the cell for example A1. </param>
    /// <param name="row"> The returned conversion between row and zero based index. </param>
    /// <param name="col"> The returned conversion between column letter and zero based matrix index. </param>
    private static void ConvertCellNameToRowCol( string cellName, out int row, out int col )
    {
        char cellNameCol = cellName[0];
        string numberForCell = cellName.Substring( 1 );
        int.TryParse(numberForCell, out row );
        row = row - 1;
        col = (int)(cellNameCol - 65);
    }

    /// <summary>
    ///   Given a row,col such as "(0,0)" turn this into the appropriate
    ///   cell name, such as: "A1".
    /// </summary>
    /// <param name="row"> The row number (0-A, 1-B, ...).</param>
    /// <param name="col"> The column number (0 based).</param>
    /// <returns>A string defining the cell name, where the col is A-Z and row is not zero based.</returns>
    private static string CellNameFromRowCol( int row, int col )
    {
        return $"{Alphabet[col]}{row + 1}";
    }

    /// <summary>
    ///   Called when the input widget (representing the data in a particular cell) is modified.
    ///   Updates the contents of a specific cell in the spreadsheet and the GUI.
    /// </summary>
    /// <param name="newInput"> The new value to put at row/col. </param>
    /// <param name="row"> The matrix row identifier. </param>
    /// <param name="col"> The matrix column identifier. </param>
    private async void HandleUpdateCellInSpreadsheet( string newInput, int row, int col )
    {
        try
        {
            inputWidgetBackingStore = $"{row},{col}";
            ValueWidgetBackingStore = $"{row}, {col}";

            string cellName = CellNameFromRowCol(row, col);
            List<string> cellsToRecalculate = spreadsheet.SetContentsOfCell(cellName, newInput).ToList();

            // Normalize the variable so the contents display shows normalized variables.
            if (newInput.StartsWith("="))
            {
                CellsBackingStore[row, col] = newInput.ToUpper();
                ToolBarCellContents = newInput.ToUpper();
            }
            else
            {
                CellsBackingStore[row, col] = newInput;
                ToolBarCellContents = newInput;
            }

            RevaluateAllCellsInList(cellsToRecalculate);
        }
        catch (Exception e)
        {
            await JS.InvokeVoidAsync( "alert", e.Message );
        }
    }

    /// <summary>
    /// Reevaluates all the cells in a given list.
    /// </summary>
    /// <param name="listOfCellsToReevaluate"> List of cells to recalculates. </param>
    private async void RevaluateAllCellsInList(List<string> listOfCellsToReevaluate)
    {
        try
        {
            foreach (string cellToRecalc in listOfCellsToReevaluate)
            {
                int rowToRecalc;
                int colToRecalc;
                ConvertCellNameToRowCol(cellToRecalc, out rowToRecalc, out colToRecalc);

                string? valueOfCell = spreadsheet.GetCellValue(cellToRecalc).ToString();
                if (spreadsheet.GetCellValue(cellToRecalc) != null && spreadsheet.GetCellValue(cellToRecalc) is FormulaError formulaError)
                {
                    valueOfCell = formulaError.Reason;
                }

                if (valueOfCell != null)
                {
                    if (spreadsheet.GetCellContents(cellToRecalc) is Formula formula)
                    {
                        List<string> formulaVars = formula.GetVariables().ToList();
                        bool successfullyParsed = true;

                        // The following foreach statement is added to see if a formula is outside the users current view. If it is the view will reflect that.
                        foreach (string variable in formulaVars)
                        {
                            // Suspicious variables are variables that could possibly be outside of the current scope of our view.
                            int suspiciousRow;
                            int suspiciousCol;
                            ConvertCellNameToRowCol(variable, out suspiciousRow, out suspiciousCol);
                            if (suspiciousRow + 1 > NumberOfRows || suspiciousCol + 1 > NumberOfCols)
                            {
                                CellsBackingValue[rowToRecalc, colToRecalc] = "Error Attempting to evaluate two invalid things";
                                successfullyParsed = false;
                                break;
                            }
                        }

                        if (successfullyParsed)
                        {
                            CellsBackingValue[rowToRecalc, colToRecalc] = valueOfCell;
                        }
                    }
                    else
                    {
                        CellsBackingValue[rowToRecalc, colToRecalc] = valueOfCell;
                    }
                }
            }
        }
        catch (Exception e)
        {
            await JS.InvokeVoidAsync("alert", e.Message);
        }
    }

    /// <summary>
    ///   <para>
    ///     Using a Web Input ask the user for a file and then process the
    ///     data in the file.
    ///   </para>
    ///   <remarks>
    ///     Unfortunately, this happens after the file is chosen, but we will live with that.
    ///   </remarks>
    /// </summary>
    /// <param name="args"> Information about the file that has been selected. </param>
    private async void HandleLoadFile( EventArgs args )
    {
        try
        {
            bool success = true;
            if (spreadsheet.Changed)
            {
                 success = await JS.InvokeAsync<bool>("confirm", "Do this?");
            }

            if ( !success )
            {
                // user canceled the action.
                return;
            }

            string fileContent = string.Empty;

            InputFileChangeEventArgs eventArgs = args as InputFileChangeEventArgs ?? throw new Exception("that didn't work");
            if ( eventArgs.FileCount == 1 )
            {
                var file = eventArgs.File;
                if ( file is null )
                {
                    // No file found, return/exit.
                    return;
                }

                using var stream = file.OpenReadStream();
                using var reader = new System.IO.StreamReader(stream);
                fileContent = await reader.ReadToEndAsync();

                spreadsheet.InstantiateFromJSON( fileContent );

                this.CellsBackingStore = new string[100, 26];
                this.CellsBackingValue = new string[100, 26];

                int biggestRow = 9;
                int biggestColumn = 9;

                // Although similar this foreach statement is different than the helper method. So we cannot use it.
                foreach (string cellName in spreadsheet.GetNamesOfAllNonemptyCells())
                {
                    int rowToChange;
                    int colToChange;
                    ConvertCellNameToRowCol(cellName, out rowToChange, out colToChange);
                    if(rowToChange > biggestRow)
                    {
                        biggestRow = rowToChange;
                    }

                    if(colToChange > biggestColumn)
                    {
                        biggestColumn = colToChange;
                    }

                    string? valueOfCell = spreadsheet.GetCellValue(cellName).ToString();
                    string? contentsOfCell = spreadsheet.GetCellContents(cellName).ToString();

                    if (contentsOfCell != null && spreadsheet.GetCellContents(cellName) is Formula)
                    {
                        CellsBackingStore[rowToChange, colToChange] = "=" + contentsOfCell;
                    }
                    else if (contentsOfCell != null)
                    {
                        CellsBackingStore[rowToChange, colToChange] = contentsOfCell;
                    }

                    if (spreadsheet.GetCellValue(cellName) != null && spreadsheet.GetCellValue(cellName) is FormulaError formulaError)
                    {
                        valueOfCell = formulaError.Reason;
                    }

                    // Store the evaluated value of the cell.
                    if (valueOfCell != null)
                    {
                        if (spreadsheet.GetCellContents(cellName) is Formula formula)
                        {
                            List<string> formulaVars = formula.GetVariables().ToList();
                            bool successfullyParsed = true;
                            foreach (string var in formulaVars)
                            {
                                // Suspicious variables are variables that could possibly be outside of the current scope of our view.
                                int suspiciousRow;
                                int suspiciousCol;
                                ConvertCellNameToRowCol(cellName, out suspiciousRow, out suspiciousCol);
                                if (suspiciousRow + 1 > NumberOfRows || suspiciousCol + 1 > NumberOfCols)
                                {
                                    CellsBackingValue[rowToChange, colToChange] = "Error Attempting to evaluate two invalid things";
                                    successfullyParsed = false;
                                    break;
                                }
                            }

                            if (successfullyParsed)
                            {
                                CellsBackingValue[rowToChange, colToChange] = valueOfCell;
                            }
                        }
                        else
                        {
                            CellsBackingValue[rowToChange, colToChange] = valueOfCell;
                        }
                    }

                    NameWidgetBackingStore = this.spreadsheet.Name;
                    this.NumberOfCols = biggestColumn + 1;
                    this.NumberOfRows = biggestRow + 1;
                    FocusMainInput(selectedRow, selectedCol);
                    StateHasChanged();
                }
            }
        }
        catch ( Exception e )
        {
            await JS.InvokeVoidAsync("alert", "Bad File" + e.Message);
        }
    }

    /// <summary>
    ///   Switch between the file save view or main view.
    /// </summary>
    /// <param name="show"> if true, show the file save view. </param>
    private void ShowHideSaveGUI(bool show)
    {
        SaveFileName = spreadsheet.Name + ".sprd";

        SaveGUIView = show;
        StateHasChanged();
    }

    /// <summary>
    ///   Call the JavaScript necessary to download the data via the Browser's Download
    ///   Folder.
    /// </summary>
    /// <param name="e"> Ignored. </param>
    private async void HandleSaveFile(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        // this null check is done because Visual Studio doesn't understand
        // the Blazor life cycle and cannot assure of non-null.
        if ( JSModule is not null )
        {
            var success = await JSModule.InvokeAsync<bool>("saveToFile", SaveFileName, spreadsheet.GetJSON());
            if (success)
            {
                ShowHideSaveGUI( false );
                StateHasChanged();
            }
        }
    }

    /// <summary>
    ///   Clear the spreadsheet if not modified.
    /// </summary>
    /// <param name="e"> Mouse Event is Ignored. </param>
    private async void HandleClear(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        if ( JSModule is not null && spreadsheet.Changed )
        {
            bool success = await JS.InvokeAsync<bool>( "confirm", "Clear the sheet?" );
        }

        this.spreadsheet = new Spreadsheet();
        this.CellsBackingStore = new string[100, 26];
        this.CellsBackingValue = new string[100, 26];

        FocusMainInput(selectedRow, selectedCol);
    }
}
