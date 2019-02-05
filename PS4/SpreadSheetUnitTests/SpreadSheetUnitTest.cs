using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;
using SpreadsheetUtilities;
using System.Collections.Generic;

namespace SpreadSheetUnitTests
{
    [TestClass]
    public class SpreadSheetUnitTest
    {
        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestGetCellContentsInvalidName()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.GetCellContents(null);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestGetCellContentsInvalidNameTest2()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.GetCellContents("*3");
        //}

        //[TestMethod]
        //public void TestGetCellContentsCellNotInSpreadSheet()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Assert.AreEqual("", sheet.GetCellContents("A1"));
        //}
        //[TestMethod]
        //public void TestGetCellContentsTestDouble()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("A1", "3.0");
        //    Assert.AreEqual(3.0, sheet.GetCellContents("A1"));
        //}

        //[TestMethod]
        //public void TestGetCellContentsTestString()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("A1", "Bonsoir");
        //    Assert.AreEqual("Bonsoir", sheet.GetCellContents("A1"));
        //}

        //[TestMethod]
        //public void TestGetCellContentsTestFormula()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");

        //    sheet.SetContentsOfCell("A1", formula.ToString());
        //    Assert.AreEqual(formula, sheet.GetCellContents("A1"));
        //}

        //[TestMethod]
        //public void TestGetNamesOfAllNonEmptyCells()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");

        //    sheet.SetContentsOfCell("A1", formula.ToString());
        //    sheet.SetContentsOfCell("B42", "9.1");

        //    HashSet<String> s = new HashSet<string>(sheet.GetNamesOfAllNonemptyCells());
        //    Assert.AreEqual(2, s.Count);
        //    Assert.IsTrue(s.Contains("A1"));
        //    Assert.IsTrue(s.Contains("B42"));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestSetCellContentsStringDoubleInvalidNameTest1()
        //{
        //    String s = null;
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell(null, "31.0");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestSetCellContentsStringDoubleInvalidNameTest2()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("2x", "31.0");
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringDoubleCellDoesNotExist()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("b1", "31.0");
        //    Assert.AreEqual(31.0, sheet.GetCellContents("b1"));
        //}

        // [TestMethod]
        //public void TestSetCellContentsStringDoubleCellOverrideString()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("b1", "bonsoir");
        //    sheet.SetContentsOfCell("b1", 32.0.ToString());
        //    Assert.AreEqual(32.0, sheet.GetCellContents("b1"));
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringDoubleCellOverrideFormula()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    sheet.SetContentsOfCell("A1", formula.ToString());
        //    sheet.SetContentsOfCell("A1", 32.0.ToString());
        //    Assert.AreEqual(32.0, sheet.GetCellContents("A1"));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestSetCellContentsStringTextInvalidNameTest1()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell(null, "Get Rekt");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void TestSetCellContentsStringTextNullText()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    String s = null;
        //    sheet.SetContentsOfCell("a1", s);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestSetCellContentsStringTextInvalidNameTest2()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("2x", "GetRekt");
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringTextCellDoesNotExist()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("b1", "Get Rekt");
        //    Assert.AreEqual("Get Rekt", sheet.GetCellContents("b1"));
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringTextCellOverrideString()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("b1", "bonsoir");
        //    sheet.SetContentsOfCell("b1", "Get Rekt");
        //    Assert.AreEqual("Get Rekt", sheet.GetCellContents("b1"));
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringTextCellOverrideFormula()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    sheet.SetContentsOfCell("A1", formula.ToString());
        //    sheet.SetContentsOfCell("A1", "Get Rekt");
        //    Assert.AreEqual("Get Rekt", sheet.GetCellContents("A1"));
        //}


        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestSetCellContentsStringFormulaInvalidNameTest1()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    sheet.SetContentsOfCell(null, formula.ToString());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void TestSetCellContentsStringFormulaNull()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = null;
        //    sheet.SetContentsOfCell(null, formula.ToString());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(InvalidNameException))]
        //public void TestSetCellContentsStringFormulaInvalidNameTest2()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    sheet.SetContentsOfCell("2x", formula.ToString());
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringFormulaCellDoesNotExist()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    sheet.SetContentsOfCell("b1", formula.ToString());
        //    Assert.AreEqual(formula, sheet.GetCellContents("b1"));
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringFormulaCellOverrideString()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    sheet.SetContentsOfCell("b1", "Get Rekt");
        //    sheet.SetContentsOfCell("b1", formula.ToString());
        //    Assert.AreEqual(formula, sheet.GetCellContents("b1"));
        //}

        //[TestMethod]
        //public void TestSetCellContentsStringFormulaCellOverrideFormula()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + 42 * 8 - 1");
        //    Formula newFormula = new Formula("3 + 42 * 80 + 653 * a7");
        //    sheet.SetContentsOfCell("A1", formula.ToString());
        //    sheet.SetContentsOfCell("A1", newFormula.ToString());
        //    Assert.AreEqual(newFormula, sheet.GetCellContents("A1"));
        //}

        //[TestMethod]
        //[ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        //public void TestGetDirectDependentsNull()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    PrivateObject spreadSheet = new PrivateObject(sheet);

        //    spreadSheet.Invoke("GetDirectDependents", new String[1] { null });

        //}

        //[TestMethod]
        //[ExpectedException(typeof(System.Reflection.TargetInvocationException))]
        //public void TestGetDirectDependentsInvalidName()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    PrivateObject spreadSheet = new PrivateObject(sheet);

        //    spreadSheet.Invoke("GetDirectDependents", new String[1] {"2x"});

        //}

        //[TestMethod]
        //[ExpectedException(typeof(CircularException))]
        //public void TestCircularDependency()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula = new Formula("3 + B1");
        //    Formula newFormula = new Formula("4 + A1");
        //    sheet.SetContentsOfCell("A1", formula.ToString());
        //    sheet.SetContentsOfCell("B1", newFormula.ToString());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(CircularException))]
        //public void TestCircularDependencyZacharyTest()
        //{
        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    Formula formula1 = new Formula("B1 * 2");
        //    Formula formula2 = new Formula("C1 * 2");
        //    Formula formula3 = new Formula("A1 * 2");
        //    sheet.SetContentsOfCell("A1", formula1.ToString());
        //    sheet.SetContentsOfCell("B1", formula2.ToString());
        //    sheet.SetContentsOfCell("C1", formula3.ToString());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(CircularException))]
        //public void TestSetCellContentsFormulaCircularExceptionElseCase()
        //{
        //    Formula formula1 = new Formula("A2 + 3");
        //    Formula formula2 = new Formula("B1 - 3");
        //    Formula formula3 = new Formula("C2 + 10");

        //    AbstractSpreadsheet sheet = new Spreadsheet();
        //    sheet.SetContentsOfCell("A2", 3.0.ToString());
        //    sheet.SetContentsOfCell("B1", formula1.ToString());
        //    sheet.SetContentsOfCell("C2", formula2.ToString());
        //    sheet.SetContentsOfCell("B1", formula3.ToString());
        //}

        // EMPTY SPREADSHEETS
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestEmptyGetNull()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestEmptyGetContents()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("1AA");
        }

        [TestMethod()]
        public void TestGetEmptyContents()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual("", s.GetCellContents("A2"));
        }

        // SETTING CELL TO A DOUBLE
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNullDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "1.5");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetInvalidNameDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1A1A", "1.5");
        }

        [TestMethod()]
        public void TestSimpleSetDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "1.5");
            Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
        }

        // SETTING CELL TO A STRING
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetNullStringVal()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A8", (string)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNullStringName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "hello");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetSimpleString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1AZ", "hello");
        }

        [TestMethod()]
        public void TestSetGetSimpleString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "hello");
            Assert.AreEqual("hello", s.GetCellContents("Z7"));
        }

        // SETTING CELL TO A FORMULA
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetNullFormVal()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A8", null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNullFormName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "=" + new Formula("2").ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetSimpleForm()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1AZ", "=" + new Formula("2").ToString());
        }

        [TestMethod()]
        public void TestSetGetForm()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "=" + new Formula("3").ToString());
            Formula f = (Formula)s.GetCellContents("Z7");
            Assert.AreEqual("=" + new Formula("3").ToString(), "=" + f.ToString());
            Assert.AreNotEqual("=" + new Formula("2").ToString(), "=" + f.ToString());
        }

        // CIRCULAR FORMULA DETECTION
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestSimpleCircular()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=" + new Formula("A2").ToString());
            s.SetContentsOfCell("A2", "=" + new Formula("A1").ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestComplexCircular()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=" + new Formula("A2+A3").ToString());
            s.SetContentsOfCell("A3", "=" + new Formula("A4+A5").ToString());
            s.SetContentsOfCell("A5", "=" + new Formula("A6+A7").ToString());
            s.SetContentsOfCell("A7", "=" + new Formula("A1+A1").ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestUndoCircular()
        {
            Spreadsheet s = new Spreadsheet();
            try
            {
                s.SetContentsOfCell("A1", "=" + new Formula("A2+A3").ToString());
                s.SetContentsOfCell("A2", "15");
                s.SetContentsOfCell("A3", "30");
                s.SetContentsOfCell("A2", "=" + new Formula("A3*A1").ToString());
            }
            catch (CircularException e)
            {
                Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
                throw e;
            }
        }

        // NONEMPTY CELLS
        [TestMethod()]
        public void TestEmptyNames()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void TestExplicitEmptySet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "");
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void TestSimpleNamesString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSimpleNamesDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "52.25");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSimpleNamesFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "=" + new Formula("3.5").ToString());
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestMixedNames()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("C1", "hello");
            s.SetContentsOfCell("B1", "=" + new Formula("3.5").ToString());
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
        }

        // RETURN VALUE OF SET CELL CONTENTS
        [TestMethod()]
        public void TestSetSingletonDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            s.SetContentsOfCell("C1", "=" + new Formula("5").ToString());
            Assert.IsTrue(s.SetContentsOfCell("A1", "17.2").SetEquals(new HashSet<string>() { "A1" }));
        }

        [TestMethod()]
        public void TestSetSingletonString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("C1", "=" + new Formula("5").ToString());
            Assert.IsTrue(s.SetContentsOfCell("B1", "hello").SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSetSingletonFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("B1", "hello");
            Assert.IsTrue(s.SetContentsOfCell("C1", "=" + new Formula("5").ToString()).SetEquals(new HashSet<string>() { "C1" }));
        }

        [TestMethod()]
        public void TestSetChain()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=" + new Formula("A2+A3").ToString());
            s.SetContentsOfCell("A2", "6");
            s.SetContentsOfCell("A3", "=" + new Formula("A2+A4").ToString());
            s.SetContentsOfCell("A4", "=" + new Formula("A2+A5").ToString());
            Assert.IsTrue(s.SetContentsOfCell("A5", "82.5").SetEquals(new HashSet<string>() { "A5", "A4", "A3", "A1" }));
        }

        // CHANGING CELLS
        [TestMethod()]
        public void TestChangeFtoD()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A1", "2.5");
            Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
        }

        [TestMethod()]
        public void TestChangeFtoS()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=" + new Formula("A2+A3").ToString());
            s.SetContentsOfCell("A1", "Hello");
            Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
        }

        [TestMethod()]
        public void TestChangeStoF()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "Hello");
            s.SetContentsOfCell("A1", "=" + new Formula("23").ToString());
            Assert.AreEqual(new Formula("23"), (Formula)s.GetCellContents("A1"));
            Assert.AreNotEqual(new Formula("24").ToString(), (Formula)s.GetCellContents("A1"));
        }

        // STRESS TESTS
        [TestMethod()]
        public void TestStress1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=" + new Formula("B1+B2").ToString());
            s.SetContentsOfCell("B1", "=" + new Formula("C1-C2").ToString());
            s.SetContentsOfCell("B2", "=" + new Formula("C3*C4").ToString());
            s.SetContentsOfCell("C1", "=" + new Formula("D1*D2").ToString());
            s.SetContentsOfCell("C2", "=" + new Formula("D3*D4").ToString());
            s.SetContentsOfCell("C3", "=" + new Formula("D5*D6").ToString());
            s.SetContentsOfCell("C4", "=" + new Formula("D7*D8").ToString());
            s.SetContentsOfCell("D1", "=" + new Formula("E1").ToString().ToString());
            s.SetContentsOfCell("D2", "=" + new Formula("E1").ToString());
            s.SetContentsOfCell("D3", "=" + new Formula("E1").ToString());
            s.SetContentsOfCell("D4", "=" + new Formula("E1").ToString());
            s.SetContentsOfCell("D5", "=" + new Formula("E1").ToString());
            s.SetContentsOfCell("D6", "=" + new Formula("E1").ToString());
            s.SetContentsOfCell("D7", "=" + new Formula("E1").ToString());
            s.SetContentsOfCell("D8", "=" + new Formula("E1").ToString());
            ISet<String> cells = s.SetContentsOfCell("E1", "0");
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }

        // Repeated for extra weight
        [TestMethod()]
        public void TestStress1a()
        {
            TestStress1();
        }
        [TestMethod()]
        public void TestStress1b()
        {
            TestStress1();
        }
        [TestMethod()]
        public void TestStress1c()
        {
            TestStress1();
        }

        [TestMethod()]
        public void TestStress2()
        {
            Spreadsheet s = new Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=" + new Formula("A" + (i + 1)).ToString())));
            }
        }
        [TestMethod()]
        public void TestStress2a()
        {
            TestStress2();
        }
        [TestMethod()]
        public void TestStress2b()
        {
            TestStress2();
        }
        [TestMethod()]
        public void TestStress2c()
        {
            TestStress2();
        }

        [TestMethod()]
        public void TestStress3()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 1; i < 200; i++)
            {
                s.SetContentsOfCell("A" + i, "=" + new Formula("A" + (i + 1)).ToString());
            }
            try
            {
                s.SetContentsOfCell("A150", "=" + new Formula("A50").ToString());
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }

        [TestMethod()]
        public void TestStress3a()
        {
            TestStress3();
        }
        [TestMethod()]
        public void TestStress3b()
        {
            TestStress3();
        }
        [TestMethod()]
        public void TestStress3c()
        {
            TestStress3();
        }

        [TestMethod()]
        public void TestStress4()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 0; i < 500; i++)
            {
                s.SetContentsOfCell("A1" + i, "=" + new Formula("A1" + (i + 1)).ToString());
            }
            HashSet<string> firstCells = new HashSet<string>();
            HashSet<string> lastCells = new HashSet<string>();
            for (int i = 0; i < 250; i++)
            {
                firstCells.Add("A1" + i);
                lastCells.Add("A1" + (i + 250));
            }
            Assert.IsTrue(s.SetContentsOfCell("A1249", "25.0").SetEquals(firstCells));
            Assert.IsTrue(s.SetContentsOfCell("A1499", "0").SetEquals(lastCells));
        }
        [TestMethod()]
        public void TestStress4a()
        {
            TestStress4();
        }
        [TestMethod()]
        public void TestStress4b()
        {
            TestStress4();
        }
        [TestMethod()]
        public void TestStress4c()
        {
            TestStress4();
        }

        [TestMethod()]
        public void TestStress5()
        {
            RunRandomizedTest(47, 2519);
        }

        [TestMethod()]
        public void TestStress6()
        {
            RunRandomizedTest(48, 2521);
        }

        [TestMethod()]
        public void TestStress7()
        {
            RunRandomizedTest(49, 2526);
        }

        [TestMethod()]
        public void TestStress8()
        {
            RunRandomizedTest(50, 2521);
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("a1", "=" + new Formula("a1 + 4 - 18").ToString());
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestThreeArgumentConstructorInvalidCellName()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("_hello", "123.25");
        }

        /// A string is a cell name if and only if it consists of one or more letters,
        /// followed by one or more digits AND it satisfies the predicate IsValid.
        /// For example, "A15", "a15", "XY032", and "BC7" are cell names so long as they
        /// satisfy IsValid.  On the other hand, "Z", "X_", and "hello" are not cell names,
        /// regardless of IsValid.
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void KoptaZacharyTestsForInvalidValidCellNameTest1ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("Z", "123.25");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void KoptaZacharyTestsForInvalidValidCellNameTest2ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("X_", "123.25");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void KoptaZacharyTestsForInvalidValidCellNameTest3ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("hello", "123.25");
        }

        [TestMethod()]
        public void KoptaZacharyTestsForValidValidCellNameTest1ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("A15", "123.25");
        }

        [TestMethod()]
        public void KoptaZacharyTestsForValidValidCellNameTest2ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("a15", "123.25");
        }

        [TestMethod()]
        public void KoptaZacharyTestsForValidValidCellNameTest3ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("XY037", "123.25");
        }

        [TestMethod()]
        public void KoptaZacharyTestsForValidValidCellNameTest4ThreeArgumentConstructor()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("BC7", "123.25");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThreeArgumentConstructorTestInvalidVariablesInAFormula()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("BC7", "=" + new Formula("GET_REKT + 42 * 5 - 1").ToString());
            sheet.SetContentsOfCell("GET_REKT", ("24"));
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameExceptionForGetContentsTest1()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameExceptionForGetContentsTest2()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetCellValue("GetRekt");
        }

        [TestMethod()]
        public void TestGetCellContentsThatWorks()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "GetRekt");
            Assert.AreEqual("GetRekt", sheet.GetCellValue("a1"));
        }

        [TestMethod()]
        public void TestSaveSpreadSheet()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "GetRekt");
            sheet.SetContentsOfCell("a3", "=" + new Formula("a4 * 5 + 3 - 2").ToString());
            sheet.SetContentsOfCell("a2", "hellomoto");
            sheet.SetContentsOfCell("a9", "bongo drums");
            sheet.SetContentsOfCell("a6", "=" + new Formula("72 * 81 + 9").ToString());
            sheet.SetContentsOfCell("a10", "76");
            sheet.SetContentsOfCell("a41", "42");
            sheet.SetContentsOfCell("a22", "320");

            sheet.Save("TestSaveSpreadSheet");
        }

        [TestMethod()]
        public void TestSaveSpreadSheetExceptionsSetContentsOfFormula()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", "GetRekt");
            sheet.SetContentsOfCell("a3", "=" + new Formula("a4 * 5 + 3 - 2").ToString());
            sheet.SetContentsOfCell("a2", "hellomoto");
            sheet.SetContentsOfCell("a9", "bongo drums");
            sheet.SetContentsOfCell("a6", "=" + new Formula("72 * 81 + 9").ToString());
            sheet.SetContentsOfCell("a10", "76");
            sheet.SetContentsOfCell("a41", "42");
            sheet.SetContentsOfCell("a22", "320");

            sheet.Save("TestSaveSpreadSheet");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExceptionsForFormulaNullText()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell("a1", null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestExceptionsForFormulaNullName()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "bonsoir");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestExceptionsForDoubleNullName()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.SetContentsOfCell(null, "32.0");
        }

        [TestMethod()]

        public void ReadFromFile()
        {
            Spreadsheet sheet = new Spreadsheet(p => true, s => s.ToUpper(), "default");
            sheet.SetContentsOfCell("a1", "GetRekt");
            sheet.SetContentsOfCell("a3", "=" + new Formula("a4 * 5 + 3 - 2").ToString());
            sheet.SetContentsOfCell("a2", "hellomoto");
            sheet.SetContentsOfCell("a9", "bongo drums");
            sheet.SetContentsOfCell("a6", "=" + new Formula("72 * 81 + 9").ToString());
            sheet.SetContentsOfCell("a10", "76");
            sheet.SetContentsOfCell("a41", "42");
            sheet.SetContentsOfCell("a22", "320");

            sheet.Save("TestSaveSpreadSheet2");

            Spreadsheet otherSheet = new Spreadsheet("TestSaveSpreadSheet2", p => true, s => s.ToUpper(), "default");

            PrivateObject originalSheet = new PrivateObject(sheet);
            PrivateObject readSheet = new PrivateObject(otherSheet);

            Dictionary<String, Cell> originalSheetDictionary = (Dictionary<String, Cell>)originalSheet.GetField("spreadSheet");
            Dictionary<String, Cell> otherReadSheetDictionary = (Dictionary<String, Cell>)readSheet.GetField("spreadSheet");

            foreach (String s in originalSheetDictionary.Keys)
            {
                Assert.AreEqual(originalSheetDictionary[s].GetCellContents(), otherReadSheetDictionary[s].GetCellContents());
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]

        public void GetSavedVersionException()
        {
            Spreadsheet sheet = new Spreadsheet();
            sheet.GetSavedVersion("hellomoto");
        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]

        public void SavedDifferentVersions()
        {
            Spreadsheet sheet = new Spreadsheet("TestSaveSpreadSheet2", p => true, s => s.ToUpper(), "3.5");
        }

        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]

        public void SavedDifferentWrongName()
        {
            Spreadsheet sheet = new Spreadsheet("TestSaveSpreadSheet2", p => true, s => s.ToUpper(), "3.5");
        }
































        /// <summary>
        /// Sets random contents for a random cell 10000 times
        /// </summary>
        /// <param name="seed">Random seed</param>
        /// <param name="size">The known resulting spreadsheet size, given the seed</param>
        public void RunRandomizedTest(int seed, int size)
        {
            Spreadsheet s = new Spreadsheet();
            Random rand = new Random(seed);
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    switch (rand.Next(3))
                    {
                        case 0:
                            s.SetContentsOfCell(randomName(rand), "3.14");
                            break;
                        case 1:
                            s.SetContentsOfCell(randomName(rand), "hello");
                            break;
                        case 2:
                            s.SetContentsOfCell(randomName(rand), randomFormula(rand));
                            break;
                    }
                }
                catch (CircularException)
                {
                }
            }
            ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(size, set.Count);
        }

        /// <summary>
        /// Generates a random cell name with a capital letter and number between 1 - 99
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        private String randomName(Random rand)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
        }

        /// <summary>
        /// Generates a random Formula
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        private String randomFormula(Random rand)
        {
            String f = randomName(rand);
            for (int i = 0; i < 10; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        f += "+";
                        break;
                    case 1:
                        f += "-";
                        break;
                    case 2:
                        f += "*";
                        break;
                    case 3:
                        f += "/";
                        break;
                }
                switch (rand.Next(2))
                {
                    case 0:
                        f += 7.2;
                        break;
                    case 1:
                        f += randomName(rand);
                        break;
                }
            }
            return f;
        }
    }
}