// Implements the view of the Spreadsheet
// Authors: Peter Forsling, Pranav Rajan, Professor Daniel Kopta, Professor Joe Zachary
// Version: October 22, 2018

using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {

        //-----DATA MEMBERS-----
        /// <summary>
        /// The internal functions of a Spreadsheet
        /// </summary>
        private SS.Spreadsheet model;

        /// <summary>
        /// The ASCII value representing 'A', used for
        /// building a cell name from selection
        /// </summary>
        private const int ALPHABETASCII = 65;

        /// <summary>
        /// The filepath of where the model came from, if it exists
        /// </summary>
        private string filepath;

        //-----CONSTRUCTOR-----

        /// <summary>
        /// Constructor for a new form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            cellGrid.SelectionChanged += OnSelectionChanged;
            model = new SS.Spreadsheet(CellNameValidator, s => s.ToUpper(), "ps6");
            filepath = null;
            this.ActiveControl = SelectedCellContentsTextBox;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(cellGrid_KeyDown);
            CellNameTextBox.Text = "A1";
        }

        /// <summary>
        /// Determines if a cell name is valid or not, to be used in the
        /// constructor of a backing spreadsheet. A valid cell name is one capital
        /// letter followed by a number ranging from 1 to 99.
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        private bool CellNameValidator(string cellName)
        {
            //If the cellName isn't length 2 or 3, it isn't a valid cell name.
            if (cellName.Length < 2 || cellName.Length > 3)
            {
                return false;
            }
            //If the first character of cellName is not a capital letter, it isn't a valid cell name.
            if (cellName[0] < 65 || cellName[0] > 90)
            {
                return false;
            }
            //If the substring following the first character is a number, it must be in between 1 and 99.
            if (Int32.TryParse(cellName.Substring(1), out int cellNumber))
            {
                return cellNumber >= 1 && cellNumber <= 99;
            }
            else //The substring following the first character is not a number, so it is not a valid cell name.
            {
                return false;
            }


        }

        //-----EVENTS AND EVENT HANDLERS-----

        /// <summary>
        /// The event handler for when a new cell is selected
        /// </summary>
        /// <param name="panel">The panel the event is called on</param>
        private void OnSelectionChanged(SpreadsheetPanel panel)
        {
            //Update the text boxes at the top of the screen
            String cellName = getSelectedCellName(panel);
            CellNameTextBox.Text = cellName;
            CellValueTextBox.Text = model.GetCellValue(cellName).ToString();

            //If the selected cell is a formula, edit the cell contents text box to have an '=' in front
            if (model.GetCellContents(cellName) is Formula)
            {
                SelectedCellContentsTextBox.Text = "=" + model.GetCellContents(cellName).ToString();
            }
            else //If the selected cell is not a formula, just set the text box to the contents.ToString()
            {
                SelectedCellContentsTextBox.Text = model.GetCellContents(cellName).ToString();
            }

            //Place focus on the Cell Contents Text Box
            this.ActiveControl = SelectedCellContentsTextBox;
        }

        /// <summary>
        /// Method that handles moving between cells
        /// </summary>
        /// <param name="e">The event that occurs</param>
        private void cellGrid_KeySelection(EventArgs e)
        {
            int row;
            int col;
            cellGrid.GetSelection(out col, out row);

            if (e is KeyEventArgs)
            {
                //Record the key input
                KeyEventArgs keyInput = (KeyEventArgs)e;

                //Move the selected cell up/down/left/right
                switch (keyInput.KeyCode)
                {
                    //Up key moves the selection up
                    case Keys.Up:
                        cellGrid.SetSelection(col, row - 1);
                        break;

                    //Down key moves the selection down
                    case Keys.Down:
                        cellGrid.SetSelection(col, row + 1);
                        break;

                    //Right key moves the selection right
                    case Keys.Right:
                        cellGrid.SetSelection(col + 1, row);
                        break;

                    //Left key moves the selection left
                    case Keys.Left:
                        cellGrid.SetSelection(col - 1, row);
                        break;

                    case Keys.Enter:
                        UpdateCell();
                        //Stop the sound from happening when you press enter
                        keyInput.Handled = true;
                        keyInput.SuppressKeyPress = true;
                        break;
                }
                //Update the cell name text box
                CellNameTextBox.Text = getSelectedCellName(cellGrid);
            }

        }

        /// <summary>
        /// Detects key presses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cellGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up
                || e.KeyCode == Keys.Down
                || e.KeyCode == Keys.Left
                || e.KeyCode == Keys.Right
                || e.KeyCode == Keys.Enter)
            {
                cellGrid_KeySelection(e);
            }
        }

        //-----CELL HELPERS, CELL EDITING, CELL SELECTION-----

        /// <summary>
        /// Method that returns the name of the cell
        /// </summary>
        /// <param name="panel">The spreadsheet panel object</param>
        /// <returns>the selected cell name</returns>
        private String getSelectedCellName(SpreadsheetPanel panel)
        {
            //Get the row and column of selected cell
            int row;
            int col;
            panel.GetSelection(out col, out row);

            //Convert the 0 based row and col to the form of a cell name
            char cellNameLetter = (char)(ALPHABETASCII + col);
            int cellNameNumber = row + 1;
            string cellName = cellNameLetter + cellNameNumber.ToString();

            return cellName;
        }

        /// <summary>
        /// Updates the cell with the "Go" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetContentsButton_Click(object sender, EventArgs e)
        {
            UpdateCell();
        }

        /// <summary>
        /// Updates the contents and values of all affected cells
        /// </summary>
        private void UpdateCell()
        {
            //Disable the button to avoid Background Thread errors
            SetContentsButton.Enabled = false;

            //Run the Cell Updater Worker
            CellUpdaterThread.RunWorkerAsync();

            //update the text box and reset the focus towards the cell content text box
            CellValueTextBox.Text = model.GetCellValue(CellNameTextBox.Text).ToString();
            this.ActiveControl = SelectedCellContentsTextBox;
        }

        /// <summary>
        /// Method that returns the row of a cell
        /// </summary>
        /// <param name="cellName">The name of the cell</param>
        /// <returns>The row of the cell</returns>
        private int GetRowOfCell(String cellName)
        {
            Int32.TryParse(cellName.Substring(1), out int col);
            return col - 1;
        }

        /// <summary>
        /// Method that returns the column of a cell
        /// </summary>
        /// <param name="cellName">The name of the cell</param>
        /// <returns>The column of the cell</returns>
        private int GetColOfCell(String cellName)
        {
            Char x = cellName[0];
            x = (char)(x - ALPHABETASCII);
            return x;
        }

        /// <summary>
        /// Updates the view of the SpreadsheetGUI when cells have been updated
        /// </summary>
        /// <param name="cellNames">the cells that need to be updated</param>
        private void UpdateCellGrid(HashSet<string> cellNames)
        {
            foreach (string name in cellNames)
            {
                int cellRow = GetRowOfCell(name);
                int cellCol = GetColOfCell(name);

                //If the value is a FormulaError, build a FormulaError Code
                //Possible FormulaErrorCodes:
                //!DIV0 - Divide by Zero
                //!INVC - Invalid Cell
                if (model.GetCellValue(name) is FormulaError)
                {
                    FormulaError fe = (FormulaError)model.GetCellValue(name);
                    string errorCode = "";

                    //If the FormulaError reason is a divide by zero
                    if (fe.Reason == "Cannot divide by zero")
                    {
                        errorCode = "!DIV0";
                    }
                    else //The only other reason is invalid cell
                    {
                        errorCode = "!INVC";
                    }
                    //Update the cell grid with the FormulaError Code
                    cellGrid.SetValue(cellCol, cellRow, errorCode);
                }
                else //If the value is not FormulaError, represent the value regularly.
                {
                    cellGrid.SetValue(cellCol, cellRow, model.GetCellValue(name).ToString());
                }
            }
        }

        /// <summary>
        /// Performs the work of updating the cell values
        /// on a separate thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellUpdaterThread_DoWork(object sender, DoWorkEventArgs e)
        {
            int row;
            int col;
            cellGrid.GetSelection(out col, out row);

            //Attempt to set the value of the selected cell. If an error occurs, show an error message
            try
            {
                //pull out the dependents
                HashSet<String> dependents = new HashSet<string>(model.SetContentsOfCell(CellNameTextBox.Text, SelectedCellContentsTextBox.Text));

                //Update the view
                UpdateCellGrid(dependents);
            }
            catch (Exception ex)
            {
                //A CircularException doesn't have a message, so a custom one is entered.
                if (ex is CircularException)
                {
                    MessageBox.Show("Invalid Formula.\nThe entered formula has a circular dependency.", "Invalid Formula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Use the exception message for all other exceptions
                else
                {
                    MessageBox.Show("Invalid Formula.\n" + ex.Message, "Invalid Formula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        /// <summary>
        /// Reenables the Set Contents Button after the 
        /// Cell Updater Thread has completed its work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellUpdaterThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Reenable the Set Contents button after the worker is finished
            SetContentsButton.Enabled = true;
        }

        //-----SAVING A SPREADSHEET-----

        /// <summary>
        /// Saves the spreadsheet with a SaveFileDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveSpreadsheetAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSpreadsheetAs();
        }

        /// <summary>
        /// Saves the spreadsheet using a SaveFileDialog if it doesn't
        /// already have an existing filepath
        /// </summary>
        private void SaveSpreadsheetAs()
        {
            //Initialize the save file dialog
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Spreadsheet As:";
            sfd.Filter = "Spreadsheet Files (*.sprd)|*.sprd|All files (*.*)|*.*";

            //If the user cancels during save file, return.
            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //If option 1 is selected and the file name does not have .sprd at
            //the end, add .sprd to the end of the file name
            if (sfd.FilterIndex == 0 && !sfd.FileName.EndsWith(".sprd"))
            {
                sfd.FileName = sfd.FileName + ".sprd";
            }

            //save the file
            model.Save(sfd.FileName);
            filepath = sfd.FileName;
        }

        /// <summary>
        /// Saves the spreadsheet to the filepath that exists
        /// </summary>
        private void SaveSpreadsheet()
        {
            model.Save(filepath);
        }

        /// <summary>
        /// Determines which save for unsaved changes needs to be done
        /// when an action could cause a loss to unsaved data
        /// </summary>
        private void SaveUnsavedChanges()
        {
            //If the filepath exists, save
            if (filepath != null)
            {
                SaveSpreadsheet();
            }
            else //if the filepath does not exist, Save as
            {
                SaveSpreadsheetAs();
            }
        }

        /// <summary>
        /// Determines which save needs to be used, then saves accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the filepath does not exist, save as
            if (filepath == null)
            {
                SaveSpreadsheetAs();
            }
            else //if the filepath exists, save
            {
                model.Save(filepath);
            }
        }

        //-----OPENING A SPREADSHEET-----

        /// <summary>
        /// Opens the spreadsheet using an OpenFileDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If there are unsaved changes, ask if the user wants to save changes
            if (model.Changed)
            {
                DialogResult result = MessageBox.Show("Your current spreadsheet has unsaved changes. Would you like to save?",
                                                      "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                switch (result)
                {
                    //if they hit yes, save the spreadsheet, then finish open
                    case DialogResult.Yes:
                        SaveUnsavedChanges();
                        break;

                    //If they hit no, proceed to open
                    case DialogResult.No:
                        break;

                    //If they hit cancel, do not save, do not proceed to open
                    case DialogResult.Cancel:
                        return;
                }

            }

            //Initialize the open file dialog
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Spreadsheet";
            ofd.Filter = "Spreadsheet Files (*.sprd)|*.sprd|All files (*.*)|*.*";

            //If the user cancels while opening a file, return.
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //Open the file
            model = new SS.Spreadsheet(ofd.FileName, CellNameValidator, s => s.ToUpper(), "ps6");
            filepath = ofd.FileName;

            //Update the view
            cellGrid.Clear();
            HashSet<string> cellNames = new HashSet<string>(model.GetNamesOfAllNonemptyCells());
            UpdateCellGrid(cellNames);
            SelectedCellContentsTextBox.Text = "";
        }

        //-----NEW SPREADSHEET-----

        /// <summary>
        /// Opens a new window with an empty spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetApplicationContext.getAppContext().RunForm(new Form1());
        }

        //-----CLOSING A SPREADSHEET-----

        /// <summary>
        /// Implements the red 'X' close button, checks to verify
        /// any unsaved changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If there are unsaved changes
            if (model.Changed)
            {
                DialogResult result = MessageBox.Show("There are Unsaved changes. Would you like to save?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                switch (result)
                {
                    //Save spreadsheet then close
                    case DialogResult.Yes:
                        SaveUnsavedChanges();
                        break;

                    //Don't save spreadsheet then close
                    case DialogResult.No:
                        break;

                    //Don't save spreadsheet, don't close
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Closes spreadsheet from the file menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //-----HELP MENU-----

        /// <summary>
        /// Opens the help menu regarding selecting cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectingACellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disable the button to avoid the background worker invalid operation exception
            selectingACellToolStripMenuItem.Enabled = false;

            //Open the help menu
            HelpMenuSelectCellThread.RunWorkerAsync();
        }

        /// <summary>
        /// Opens the help menu for Selecting a Cell on a separate thread
        /// so the user can work on the spreadsheet while having the help
        /// menu open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuSelectCellThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("Selecting a Cell:\n" +
                            "There are two ways to select a cell. The first way is to use the mouse and click on the desired cell.\n" +
                            "The second way is to use the arrow keys to select any cell directly adjacent to the current selected cell."
                            , "Spreadsheet Help: Selecting a Cell");
        }

        /// <summary>
        /// Reenables the button after the user has closed the 
        /// help menu for Selecting a Cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuSelectCellThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            selectingACellToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Opens the help menu regarding editing cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editingACellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disable the button to avoid the background worker invalid operation exception
            editingACellToolStripMenuItem.Enabled = false;

            //Open the help menu
            HelpMenuEditCellThread.RunWorkerAsync();
        }

        /// <summary>
        /// Opens the help menu for editing a cell on a separate thread
        /// so the user can work on the Spreadsheet and have the menu open
        /// at the same time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuEditCellThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("Editing a Cell:\n" +
                            "The contents of the cell can be edited by clicking on the Cell Contents Text Box at the top of the program, editing" +
                            " the contents field to what you wish to update a cell to, and either click the Go button, or press the enter key. The" +
                            " contents and the value of the cell will be updated accordingly, as long as the contents are valid.\nValid contents include " +
                            "any number, text, or formula. A valid formula can be written with an \"=\" before the expression. Example: \"=A1 * 7\"\nIf " +
                            "the formula entered is a valid formula, then the value of the formula will be displayed in the spreadsheet, and you can" +
                            " select the cell to see the formula contents in the Cell Contents Text Box.\n An Invalid Formula will result in an error" +
                            " and your spreadsheet will not keep that value in the cell.\nAn otherwise valid formula could still be entered and result" +
                            " in a Formula Error. These Formula Errors have codes to help you determine the source of the error.\n\nFormula Errors:" +
                            "\n!DIV0: The formula at any point attempts to divide by zero. This can be fixed by removing any 0s or Cells representing" +
                            " 0s as the divisor.\n !INVC: The Formula you entered contains a cell where either does not have a value, or has an invalid" +
                            " value. Possible invalid values include text or other Formula Errors. These can be fixed by editing the value inside of" +
                            " the cell that is causing the error.", "Spreadsheet Help: Editing a Cell");
        }

        /// <summary>
        /// Reenables the button after the user closes the help menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuEditCellThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            editingACellToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Opens the help menu regarding saving spreadsheets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void savingASpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disable the button to avoid threading errors
            savingASpreadsheetToolStripMenuItem.Enabled = false;

            //Open the menu
            HelpMenuSavingSpreadsheetThread.RunWorkerAsync();
        }

        /// <summary>
        /// Opens the Saving Spreadsheet help menu window
        /// on a seperate thread so the user can work on the
        /// spreadsheet while reading the help menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuSavingSpreadsheetThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("Saving a Spreadsheet:\n" +
                            "In the top left, select the file menu, then select \"Save Spreadsheet\". The save file dialogue will open for you to save." +
                            " Enter your desired file name. It will be saved as a .sprd file in the location it was saved."
                             , "Spreadsheet Help: Saving a Spreadsheet");
        }

        /// <summary>
        /// Reenables the button after the help menu is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuSavingSpreadsheetThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            savingASpreadsheetToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// The help menu for opening a spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openingASpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disable the button to avoid threading errors
            openingASpreadsheetToolStripMenuItem.Enabled = false;

            //Open the menu
            HelpMenuOpeningSpreadsheetThread.RunWorkerAsync();
        }

        /// <summary>
        /// Runs the help menu for opening a spreadsheet on a separate
        /// thread so the user can read the help menu while working on
        /// the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuOpeningSpreadsheetThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("Opening A Spreadsheet\nTo open a spreadsheet, open the File menu and select Open Spreadsheet." +
                            " If you have unsaved changes, it will prompt you to save. If you select Yes," +
                            " the spreadsheet will be saved, and then you may select a new spreadsheet to open." +
                            " If you select No, the saved changes will be lost when you open a different spreadsheet." +
                            " If you select Cancel, a new spreadsheet will not be opened.", "Spreadsheet Help: Opening a Spreadsheet");
        }


        /// <summary>
        /// Reenables the button after the user closes the help menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuOpeningSpreadsheetThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            openingASpreadsheetToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// The help meny for additional features in this program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfAdditionalFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disable the button to prevent any threading exception
            listOfAdditionalFeaturesToolStripMenuItem.Enabled = false;

            //Open the help menu
            HelpMenuAdditionalFeaturesThread.RunWorkerAsync();
        }

        /// <summary>
        /// Runs the help menu for additional features in this program on a separate thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuAdditionalFeaturesThread_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("List of Additional Features:\n- Key event movement: Allows a user to use up, down, left, and right keys to move between cells\n" +
                            "- Enter Key event: Allows a user to enter cell content by using the enter key\n" +
                            "- Color for Spreadsheet: We provided a red background color for our menu and spreadsheet application program in the spirit of " +
                            "the Utah Utes\n" +
                            "- Multi-Threading: We implemented multi-threading for our help menu and recalculating cells. Using multi-threading " +
                            "we gave users the ability to view the help menu and interact with the spreadsheet application at the same time\n" +
                            "- Spreadsheet Application Icon: We added a logo for the spreadsheet application\n" +
                            "- Formula Error Codes: Similar to the way Excel and Google Sheets provide error messages for incorrect mathematical expressions," +
                            "we provided error codes which we define in the help menu\n", "Spreadsheet Help: List of Additional Features\n");
        }

        /// <summary>
        /// Reenable the button after the help menu is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpMenuAdditionalFeaturesThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listOfAdditionalFeaturesToolStripMenuItem.Enabled = true;
        }
    }

}

