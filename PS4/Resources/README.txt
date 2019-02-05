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


