using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using SpreadsheetUtilities;

/// <summary>
/// Version 1.1
/// This class implements the updated version of abstractspreadsheet.cs
/// Author: Pranav Rajan
/// Date:10/05/2018
/// 
/// Version 1.2
/// There were 14 bugs found in Version 1.0 as found by the PS5 Staff Tests
/// This version corrects 3 of the bugs while there are 11 other bugs somewhere
/// Author: Pranav Rajan
/// Date:10/11/18
/// 
/// Version 1.3
/// Corrected all the bugs in Version 1.2
/// Renamed some of the variables, updated comments and cleaned up the logical
/// ordering of the code
/// Author: Pranav Rajan
/// Date:10/11/18
/// 
/// Version 1.4
/// Renamed some the variables in Version 1.3
/// Author: Pranav Rajan
/// Date:10/14/18
/// </summary>
namespace SS
{
    /// <summary>
    /// This class provides the "smarts" for a spreadsheet
    /// Author: Pranav Rajan
    /// Date: 10/13/18
    /// 
    /// 
    /// A Spreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// 
    /// A string is a cell name if and only if it consists of one or more letters,
    /// followed by one or more digits AND it satisfies the predicate IsValid.
    /// For example, "A15", "a15", "XY032", and "BC7" are cell names so long as they
    /// satisfy IsValid.  On the other hand, "Z", "X_", and "hello" are not cell names,
    /// regardless of IsValid.
    /// 
    /// Any valid incoming cell name, whether passed as a parameter or embedded in a formula,
    /// must be normalized with the Normalize method before it is used by or saved in 
    /// this spreadsheet.  For example, if Normalize is s => s.ToUpper(), then
    /// the Formula "x3+a5" should be converted to "X3+A5" before use.
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  
    /// In addition to a name, each cell has a contents and a value.  The distinction is
    /// important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In a new spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Provides the backing data structure for a spreadsheet
        /// String: Represents the name of a cell
        /// Cell: Represents the cell that corresponds to the name of a cell
        /// </summary>
        private Dictionary<String, Cell> spreadsheet;

        /// <summary>
        ///Provides the backing data structure to keep track of the relationship between cells
        /// </summary>
        private DependencyGraph DG;

        /// <summary>
        /// A zero-argument constructor that creates an empty spreadsheet that imposes no extra validity conditions, 
        /// normalizes every cell name to itself and has version "default".
        /// </summary>
        public Spreadsheet() :
           base(s => true, s => s, "default")
        {
            this.spreadsheet = new Dictionary<string, Cell>();
            this.DG = new DependencyGraph();
        }

        /// <summary>
        /// A three-argument constructor that creates an empty sp
        /// sheet. It allows the user to provide a validity delegate,
        /// a normalization delegate and a version.
        /// </summary>
        /// <param name="isValid">A validity delegate that allows the user to define a valid variable based on certain criteria</param>
        /// <param name="normalize">A normalization delegate that allows the user to standardize variables</param>
        /// <param name="version">A string that represents the version of the spreadsheet</param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, String version) :
            base(isValid, normalize, version)
        {
            this.spreadsheet = new Dictionary<string, Cell>();
            this.DG = new DependencyGraph();
        }

        /// <summary>
        /// A four-argument constructor that allows the user to provide a string representng a file path to a file
        /// a validity delegate, a normalization delegate, and a version. It should read a saved spreadsheet from a file 
        /// and use it to construct a new spreadsheet. The new spreadsheet uses the provided validity delegate, normalization
        /// delegate and version.
        /// </summary>
        /// <param name="filePath">A string representng a file path to a file</param>
        /// <param name="isValid">A validity delegate that allows the user to define a valid variable based on certain criteria</param>
        /// <param name="normalize">A normalization delegate that allows the user to standardize variables</param>
        /// <param name="version">A string that represents the version of the spreadsheet</param>
        public Spreadsheet(String filePath, Func<string, bool> isValid, Func<string, string> normalize, String version) :
            base(isValid, normalize, version)
        {
            this.spreadsheet = new Dictionary<string, Cell>();
            this.DG = new DependencyGraph();

            try
            {
                // Create an XmlReader inside this block, and automatically Dispose() it at the end.
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "spreadsheet":
                                    string fileVersion = GetSavedVersion(filePath);

                                    //check for the version
                                    if (!(fileVersion.Equals(version)))
                                    {
                                        throw new SpreadsheetReadWriteException("The versions do not match");
                                    }
                                    break;

                                case "cell":
                                    reader.Read();
                                    reader.MoveToContent();

                                    //check to make sure the content is a name
                                    if (!(reader.Name.Equals("name")))
                                    {
                                        throw new SpreadsheetReadWriteException("Expected name tag");
                                    }

                                    reader.Read();
                                    reader.MoveToContent();

                                    //check that the cell name is a valid cell name
                                    if (!(CheckIfValidCellName(reader.Value, this.IsValid, this.Normalize)))
                                    {
                                        throw new SpreadsheetReadWriteException("Invalid Cell Name");
                                    }

                                    String name = this.Normalize(reader.Value);

                                    reader.Read();
                                    reader.MoveToContent();

                                    reader.Read();
                                    reader.MoveToContent();

                                    //check to make sure the content is contents
                                    if (!(reader.Name.Equals("contents")))
                                    {
                                        throw new SpreadsheetReadWriteException("Expected contents tag");
                                    }

                                    reader.Read();
                                    reader.MoveToContent();

                                    SetContentsOfCell(name, reader.Value);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved                  
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed
        {
            get;
            protected set;

        } = false;

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            //check if the name is null or invalid
            if ((name == null) || !(CheckIfValidCellName(name, this.IsValid, this.Normalize)))
            {
                throw new InvalidNameException();
            }

            String verifiedName = this.Normalize(name);

            //check if the spreadsheet contains the name
            if (!(this.spreadsheet.ContainsKey(verifiedName)))
            {
                return "";
            }
            else
            {
                return this.spreadsheet[verifiedName].GetCellContents();
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            //check if the name is null or invalid
            if ((name == null) || !(CheckIfValidCellName(name, this.IsValid, this.Normalize)))
            {
                throw new InvalidNameException();
            }

            String verifiedName = this.Normalize(name);

            //check if the spreadsheet contains the name
            if (!(this.spreadsheet.ContainsKey(verifiedName)))
            {
                return "";
            }
            else
            {
                return this.spreadsheet[verifiedName].GetCellValue();
            }
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return this.spreadsheet.Keys;
        }

        /// <summary>
        /// Returns the version information of the spreadsheet saved in the named file.
        /// If there are any problems opening, reading, or closing the file, the method
        /// should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override string GetSavedVersion(string filename)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        if (reader.Name.Equals("spreadsheet"))
                        {
                            return reader.GetAttribute("version");
                        }
                    }
                    throw new SpreadsheetReadWriteException("No attribute or version was found");
                }  
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>
        /// cell name goes here
        /// </name>
        /// <contents>
        /// cell contents goes here
        /// </contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        public override void Save(string filename)
        {
            // We want some non-default settings for our XML writer.
            // Specifically, use indentation to make it more readable.
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("  ");

            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", this.Version);

                    foreach (String cellName in this.GetNamesOfAllNonemptyCells())
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", cellName);

                        if (this.GetCellContents(cellName) is Formula)
                        {
                            Formula savedFormula = (Formula)this.GetCellContents(cellName);
                            String formulaToString = "=" + savedFormula.ToString();
                            writer.WriteElementString("contents", formulaToString);
                        }
                        else
                        {
                            writer.WriteElementString("contents", this.GetCellContents(cellName).ToString());
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndDocument();
                }

                this.Changed = false;
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        /// 
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        /// 
        ///   (1) If the remainder of content cannot be parsed into a Formula, a 
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///       
        ///   (3) Otherwise, the contents of the named cell becomes f.
        /// 
        /// Otherwise, the contents of the named cell becomes content.
        /// 
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            //check if the content is null
            if (content == null)
            {
                throw new ArgumentNullException();
            }

            //check if the name is null or invalid
            if ((name == null) || !CheckIfValidCellName(name, this.IsValid, this.Normalize))
            {
                throw new InvalidNameException();
            }

            String verifiedName = this.Normalize(name);

            //CASE 1: check if the content parses to a double
            if (Double.TryParse(content, out double result))
            {
                this.Changed = true;
                return SetCellContents(verifiedName, result);
            }

            //CASE 2: check if the content is a formula
            else if (content.IndexOf("=") == 0)
            {
                String contentFormula = content.Substring(1);

                Formula formula = new Formula(contentFormula, this.Normalize, this.IsValid);

                //check to make sure variables in formula follow this definition: 
                //Variables for a Spreadsheet are only valid if they are one or more letters followed by 
                //one or more digits (numbers).This must now be enforced by the spreadsheet.
                foreach (String formulaVariables in formula.GetVariables())
                {
                    if (!(CheckIfValidCellName(formulaVariables, this.IsValid, this.Normalize)))
                    {
                        throw new InvalidNameException();
                    }
                }

                this.Changed = true;
                return SetCellContents(verifiedName, formula);
            }

            //CASE 3: set the contents of a cell to a string
            else
            {
                this.Changed = true;
                return SetCellContents(verifiedName, content);
            }
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            //check if the name is null
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            //check if the name is a valid name
            else if (!(CheckIfValidCellName(name, this.IsValid, this.Normalize)))
            {
                throw new InvalidNameException();
            }
            else
            {
                String verifiedName = this.Normalize(name);
                return DG.GetDependents(verifiedName);
            }
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, double number)
        {
            //CASE 1: check if the cell name does exists
            if (!(this.spreadsheet.ContainsKey(name)))
            {
                Cell newCell = new Cell(name, number);
                newCell.SetCellValue(number);

                this.spreadsheet.Add(name, newCell);
            }

            //CASE 2: The cell name already exists
            else
            {
                this.spreadsheet[name].SetCellContents(number);
                this.spreadsheet[name].SetCellValue(number);
            }

            //reset all the dependees that depend on cell name
            DG.ReplaceDependees(name, new HashSet<String>());

            return RecalculateCells(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, string text)
        {
            //CASE 1: check if the cell name exists
            if (!(this.spreadsheet.ContainsKey(name)))
            {
                Cell newCell = new Cell(name, text);
                this.spreadsheet.Add(name, newCell);
                newCell.SetCellValue(text);
            }

            //CASE 2: The cell name already exists
            else
            {
                this.spreadsheet[name].SetCellContents(text);
                this.spreadsheet[name].SetCellValue(text);
            }

            //check if the contents is an empty string
            //if the contents is an empty string then we have to remove the cell for GetAllNamesOfNonEmptyCells to
            //behave correctly as specified in the AbstractSpreadsheet specs
            if (this.spreadsheet[name].GetCellContents().Equals(""))
            {
                this.spreadsheet.Remove(name);
            }

            //reset all the cell that depends on cell name
            DG.ReplaceDependees(name, new HashSet<String>());

            return RecalculateCells(GetCellsToRecalculate(name));
        }

        /// <summary>
        /// If formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, Formula formula)
        {
            Object contents = null;
            HashSet<String> toRestore = new HashSet<string>();

            //check if the cell name exists
            if (!(this.spreadsheet.ContainsKey(name)))
            {
                Cell newCell = new Cell(name, formula);

                newCell.SetCellValue(formula.Evaluate(LookUp));

                this.spreadsheet.Add(name, newCell);
            }
            else
            {
                contents = this.spreadsheet[name].GetCellContents();

                if (contents is Formula formulacontents)
                {
                    toRestore = new HashSet<string>(formulacontents.GetVariables());
                }

                this.spreadsheet[name].SetCellContents(formula);
            }

            //replace the dependees
            //reset all the cell that depends on cell name
            DG.ReplaceDependees(name, formula.GetVariables());

            IEnumerable<String> cellsToRecalculate;

            try
            {
                cellsToRecalculate = GetCellsToRecalculate(name);
            }
            catch (CircularException ce)
            {
                if (contents is null)
                {
                    this.spreadsheet.Remove(name);
                }
                else
                {
                    this.spreadsheet[name].SetCellContents(contents);
                }

                DG.ReplaceDependees(name, toRestore);

                throw ce;
            }

            return RecalculateCells(cellsToRecalculate);
        }

        /// <summary>
        /// A helper method that recalcalculates cells every time time a cell is modified.
        /// For example, say A1 = A2 + B3 + 42 - 10
        /// If the contents of A2 was originally a double and we change it to a string
        /// the cells that depend on A2 now have to be recalculated to account for the change
        /// </summary>
        /// <param name="s">An IEnumerable that contains a cell and all the cells that depend on that cell</param>
        /// <returns>An ISet containing all the cells that have been recalculated to account for when a cell has been modified</returns>
        private ISet<string> RecalculateCells(IEnumerable<string> s)
        {
            //look up the cell name in the IEnumerable and get its value
            foreach (String cellName in s)
            {
                //CASE 1: Spreadsheet does not contain the cell
                if (!(this.spreadsheet.ContainsKey(cellName)))
                {
                    continue;
                }

                //CASE 2: The contents of the cell is a formula
                else if (this.spreadsheet[cellName].GetCellContents() is Formula f)
                {
                    this.spreadsheet[cellName].SetCellValue(f.Evaluate(LookUp));
                }

                //CASE 3: The contents of the cell is a string or double
                else
                {
                    continue;
                }
            }

            return new HashSet<String>(s);
        }

        /// <summary>
        /// Method that looks up the contents associated with a cell
        /// </summary>
        /// <param name="cellName">The specific cell that we are looking at</param>
        /// <returns>
        /// A double if the cell value is a double
        /// Otherwise returns an argument exception if the cell value is a string or if the cell does not exist in the Spreadsheet
        /// </returns>
        private Double LookUp(String cellName)
        {
            //CASE 1: The cell does not exist in the Spreadsheet
            if (!(spreadsheet.ContainsKey(cellName)))
            {
                throw new ArgumentException();
            }

            Object cellValue = this.spreadsheet[cellName].GetCellValue();

            //CASE 2: The cell value is a double
            if (cellValue is double)
            {
                return (double)cellValue;
            }

            //CASE 3: The cell value is a string
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// A helper method that checks whether a cell name is a variable cell name as defined below:
        /// A string is a valid cell name if and only if:
        /// (1) its first character is an underscore or a letter
        /// (2) its remaining characters (if any) are underscores and/or letters and/or digits
        /// </summary>
        /// <param name="cellName">The cell name that we are looking at</param>
        /// <param name="userValidator">
        /// A function the user passes to provide extra validation if they have specific requirements of what is a valid cell name
        /// </param>
        /// <param name="Normalize">
        /// A function the user passes to provide a way of standardizing cell names if they have specific requirement for 
        /// standardizing cell names
        /// </param>
        /// <returns>A boolean that represents whether a cell name is a valid cell name</returns>
        private static bool CheckIfValidCellName(String cellName, Func<string, bool> userValidator, Func<string, string> Normalize)
        {
            //pattern definition for a legal variable name
            String pattern = @"^[a-zA-Z]+[0-9]+$";

            //the regex object allows for checking whether the input variable name matches
            //the requirement of a valid formula variable name
            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(cellName))
            {
                if (userValidator(Normalize(cellName)))
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Version 1.0
    /// A class that defines a Cell object for use in a spreadsheet
    /// Author: Pranav Rajan
    /// Date: 10/11/2018
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Represents the name of a cell
        /// </summary>
        private String cellName;

        /// <summary>
        /// Represents the contents of a cell
        /// </summary>
        private object cellContents;

        /// <summary>
        /// Represents the value of a cell
        /// </summary>
        private object cellValue;

        /// <summary>
        /// The constructor for a cell
        /// </summary>
        public Cell(String cellName, Object cellContents)
        {
            this.cellName = cellName;
            this.cellContents = cellContents;
            this.cellValue = null;
        }

        /// <summary>
        /// Setter method that sets the contents of a cell
        /// </summary>
        /// <param name="o">The object that we want to set the cell contents to</param>
        public void SetCellContents(Object o)
        {
            this.cellContents = o;
        }

        /// <summary>
        /// Getter method that gets the cell contents
        /// </summary>
        /// <returns>Returns the contents of a cell</returns>
        public object GetCellContents()
        {
            return this.cellContents;
        }

        /// <summary>
        /// Set Cell Value
        /// </summary>
        /// <returns></returns>
        public void SetCellValue(Object o)
        {
            this.cellValue = o;
        }

        /// <summary>
        /// Getter method that gets the cell value
        /// </summary>
        /// <returns></returns>
        public object GetCellValue()
        {
            return this.cellValue;
        }
    }
}