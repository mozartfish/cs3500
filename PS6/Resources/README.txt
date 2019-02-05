	Spreadsheet Project

	SUMMARY
	A spreadsheet application similar to Microsoft Excel and Google Sheets that allows users to
	add values to cells including numbers, text, and formulas and perform computation of mathematical
	equations. The application can save its contents as an XML file and can read a saved spreadsheet for
	later use so long as the application version matches that of the application version that was used to create
	the saved spreadsheet.

	AUTHORS
	Professor Joe Zachary, Professor Daniel Kopta, Pranav Rajan

	LIBRARIES AND CLASSES USED

	DependencyGraph - Version 1.5 - Last Updated: October 13, 2018
	Authors: Professor Joe Zachary, Professor Daniel Kopta, Pranav Rajan
	A  library that defines a dependency graph object that keeps track of the relationships
	between individual cells. 
	An example of a dependency between two cells would be the following:
	A1 = 5, A2 = A1 + 10. The dependency graph keeps track of the relationship between A1 and A2
	by defining A1 as a dependent of A2 and A2 as a dependee of A1.

	Formula - Version 1.6 - Last Updated: October 13, 2018
	Authors: Professor Joe Zachary, Professor Daniel Kopta, Pranav Rajan
	A library that defines a formula object that reads  a mathematical equation as a string and
	returns the result of evaluating the mathematical expression. Formulas are written in standard infix
	notation using standard precedence rules(BODMAS). The allowed symbols are 
	non-negative numbers written using double-precision floating-point syntax (without unary preceeding '-' or '+'); 
	variables that consist of a letter or underscore followed by zero or more letters, underscores, or digits; parentheses; 
	and the four operator symbols +, -, *, and /. In the case of the spreadsheet application, the variables are the name of the cells that are used
	in a formula.The formula object can look up values stored in other cells if there are values and then use those values for evaluation of the mathematical
	expression.
	Example: "4 * 3 * 2 * 1", "4 + A2 + 10 / B3".

	AbstractSpreadSheet - Version 1.7 - Last Updated: September 29, 2012
	Author: Professor Joe Zachary
	An interface that provides the structure for the implementation of the "smarts" behind a spreadsheet application.
	The interface provides methods for saving spreadsheets, modifying cell content and cell values, and recalculating cell values
	when a cell has been modified.

	Cell - Version 1.0 - Last Updated: October 11, 2018
	Author: Pranav Rajan
	A class that provides the definition of a "cell" for use in a spreadsheet applications. A cell has a name, contents and value.
	A cell's contents can be a string, double or a formula object. A cell's value is a string, double or a formula error. The difference between
	a cell's content and a cell's value is that a cell's value represents the evaluation of the cell's contents. In addition to defining these properties 
	for a cell object, the cell class provides setter and getter methods for obtaining and modifying the internal data of a cell object.

	Spreadsheet - Version 1.3 - Last Updated: October 13, 2018
	Authors: Professor Joe Zachary, Professor Daniel Kopta, Pranav Rajan
	A library that implements the "smarts" behind a spreadsheet application using the libraries and classes listed above.
	The library defines a spreadsheet object based upon the interface provided by the AbstractSpreadsheet class.
	Spreadsheet objects use the DependencyGraph Library and Formula Library to keep track of relationships among cells and
	to evaluate mathematical expressions. Spreadsheet objects calculate cell values and provide verification of what is a legal 
	cell name. In addition, spreadsheet objects can save their contents to an XML file and can read an XML file for data to use in a 
	spreadsheet.

	SpreadSheet Panel - Version 1.0 - Last Updated: September 2011
	Author: Professor Joe Zachary
	A grid that displays rows from 0-99 and columns from A-Z. The grid was created using C# graphics libray components. Components
	included a scroll bar for scrolling with a mouse, a black box outline that highlights cell A1 and methods that return the row and
	column for a cell, clear the grid, obtaining values and getting and selecting values.

	Spreadsheet GUI - Version 1.0 - Last Updated: October 22, 2018
	Authors: Pranav Rajan and Peter Forsling
	A GUI that provides the interface for a spreadsheet program. The spreadsheet application was implemented using MVC architecture.
	For the model, the spreadsheet was represented using the libraries discussed above. The view was implemented using the SpreadSheet Panel provided 
	by Professor Joe Zachary and the Control was implemented using different action events such as mouse clicking, key events, 
	button events, and display events provided by C#. Below is a list of the special features we implemented
	Special Features:
	1) Key event movement - Allows a user to use up, down, left, and right keys to move between cells
	2) Enter Key event - Allows a user to enter cell content by using the enter key
	3) Color for Spreadsheet - We provided a red background color for our menu and spreadsheet application program in the spirit
	of the Utah Utes
	4) Multi-Threading - We implemented multi-threading for our help menu and recalculating cells. Using multi-threading we gave
	users the ability to view the help menu and interact with the spreadsheet application at the same time
	5) Spreadsheet Application Icon - We added a logo for our spreadsheet application
	6) Formula Error Codes- Similar to the way Excel and Google Sheets provide error messages for incorrect mathematical expressions,
	 we provided error codes which we define in the help menu
	7) Error Message Icons - When an error message occurs and a dialog box displaying the error is created, we implemented logos such as a white
	x in a red circle for an error similar to that for errors on windows machine.

	We attempted to implement Coded UI Tests but there were issues with where the spreadsheet application would open for the spreadsheet
	which resulted in us failing tests that behaved as we expected them to behave as defined by the specs of the assignment, piazza and class.

	Regarding implementation, we implemented two ways of saving a spreadsheet which were save and save as. The difference between save and save as 
	is the following: If a current spreadsheet is already saved, save will save the spreadsheet to that file by default. If the user selects save as,
	the spreadsheet will override the file if it is already saved by default. We also had two different ways of closing a spreadsheet. The user can click
	the red x in the corner of the windows form and close the spreadsheet or select the close button in the menu.
	
	We also implemented a cell name validator to ensure that only the cells that are entered by the user are located on the spreadsheet grid. 

