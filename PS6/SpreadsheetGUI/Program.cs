// Runs the application
// Authors: Peter Forsling, Pranav Rajan, Professor Daniel Kopta, Professor Joe Zachary
// Version: October 22, 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{

    /// <summary>
    /// Keeps track of how many top-level forms are running
    /// Authors: Daniel Kopta and Joe Zachary
    /// </summary>
    class SpreadsheetApplicationContext : ApplicationContext
    {
        //Number of open forms
        private int FormCount = 0;

        //Singleton ApplicationContext
        private static SpreadsheetApplicationContext AppContext;

        /// <summary>
        /// Private Constructor fo rsingleton pattern
        /// </summary>
        private SpreadsheetApplicationContext()
        {

        }

        /// <summary>
        /// Returns the one SpreadsheetApplicationContext
        /// </summary>
        /// <returns></returns>
        public static SpreadsheetApplicationContext getAppContext()
        {
            if (AppContext == null)
            {
                AppContext = new SpreadsheetApplicationContext();
            }
            return AppContext;
        }

        /// <summary>
        /// Runs the form
        /// </summary>
        /// <param name="form"></param>
        public void RunForm(Form form)
        {
            //One more form is running
            FormCount++;

            //When this form closes, we want to find out
            form.FormClosed += (o, e) => { if (--FormCount <= 0) ExitThread(); };

            //Run the form
            form.Show();
        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Start an application context and and run one form inside it
            SpreadsheetApplicationContext appContext = SpreadsheetApplicationContext.getAppContext();
            appContext.RunForm(new Form1());
            Application.Run(appContext);
        }
    }
}
