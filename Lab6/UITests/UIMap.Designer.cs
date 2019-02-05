﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 15.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace UITests
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// ComputeTipForTenDollarsFifteenPercentTip - Use 'ComputeTipForTenDollarsFifteenPercentTipParams' to pass parameters into this method.
        /// </summary>
        public void ComputeTipForTenDollarsFifteenPercentTip()
        {
            #region Variable Declarations
            WinEdit uIBillFieldEdit = this.UIForm1Window.UIBillFieldWindow.UIBillFieldEdit;
            WinEdit uITipPercentFieldEdit = this.UIForm1Window.UIItem15Window.UITipPercentFieldEdit;
            WinButton uIComputeTipButton = this.UIForm1Window.UIComputeTipWindow.UIComputeTipButton;
            #endregion

            // Launch '%USERPROFILE%\Source\repos\u1136324\Lab6\TipCalculator\bin\Debug\TipCalculator.exe'
            ApplicationUnderTest uIForm1Window = ApplicationUnderTest.Launch(this.ComputeTipForTenDollarsFifteenPercentTipParams.UIForm1WindowExePath, this.ComputeTipForTenDollarsFifteenPercentTipParams.UIForm1WindowAlternateExePath);

            // Type '10.00' in 'billField' text box
            uIBillFieldEdit.Text = this.ComputeTipForTenDollarsFifteenPercentTipParams.UIBillFieldEditText;

            // Click 'tipPercentField' text box
            Mouse.Click(uITipPercentFieldEdit, new Point(23, 3));

            // Click 'Compute Tip' button
            Mouse.Click(uIComputeTipButton, new Point(38, 13));
        }
        
        /// <summary>
        /// AssertMethod1 - Use 'AssertMethod1ExpectedValues' to pass parameters into this method.
        /// </summary>
        public void AssertMethod1()
        {
            #region Variable Declarations
            WinEdit uITipFieldEdit = this.UIForm1Window.UITipFieldWindow.UITipFieldEdit;
            #endregion

            // Verify that the 'Text' property of 'tipField' text box equals '1.5'
            Assert.AreEqual(this.AssertMethod1ExpectedValues.UITipFieldEditText, uITipFieldEdit.Text);
        }
        
        #region Properties
        public virtual ComputeTipForTenDollarsFifteenPercentTipParams ComputeTipForTenDollarsFifteenPercentTipParams
        {
            get
            {
                if ((this.mComputeTipForTenDollarsFifteenPercentTipParams == null))
                {
                    this.mComputeTipForTenDollarsFifteenPercentTipParams = new ComputeTipForTenDollarsFifteenPercentTipParams();
                }
                return this.mComputeTipForTenDollarsFifteenPercentTipParams;
            }
        }
        
        public virtual AssertMethod1ExpectedValues AssertMethod1ExpectedValues
        {
            get
            {
                if ((this.mAssertMethod1ExpectedValues == null))
                {
                    this.mAssertMethod1ExpectedValues = new AssertMethod1ExpectedValues();
                }
                return this.mAssertMethod1ExpectedValues;
            }
        }
        
        public UIForm1Window UIForm1Window
        {
            get
            {
                if ((this.mUIForm1Window == null))
                {
                    this.mUIForm1Window = new UIForm1Window();
                }
                return this.mUIForm1Window;
            }
        }
        #endregion
        
        #region Fields
        private ComputeTipForTenDollarsFifteenPercentTipParams mComputeTipForTenDollarsFifteenPercentTipParams;
        
        private AssertMethod1ExpectedValues mAssertMethod1ExpectedValues;
        
        private UIForm1Window mUIForm1Window;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'ComputeTipForTenDollarsFifteenPercentTip'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class ComputeTipForTenDollarsFifteenPercentTipParams
    {
        
        #region Fields
        /// <summary>
        /// Launch '%USERPROFILE%\Source\repos\u1136324\Lab6\TipCalculator\bin\Debug\TipCalculator.exe'
        /// </summary>
        public string UIForm1WindowExePath = "C:\\Users\\Pranav\\Source\\repos\\u1136324\\Lab6\\TipCalculator\\bin\\Debug\\TipCalculator." +
            "exe";
        
        /// <summary>
        /// Launch '%USERPROFILE%\Source\repos\u1136324\Lab6\TipCalculator\bin\Debug\TipCalculator.exe'
        /// </summary>
        public string UIForm1WindowAlternateExePath = "%USERPROFILE%\\Source\\repos\\u1136324\\Lab6\\TipCalculator\\bin\\Debug\\TipCalculator.ex" +
            "e";
        
        /// <summary>
        /// Type '10.00' in 'billField' text box
        /// </summary>
        public string UIBillFieldEditText = "10.00";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertMethod1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class AssertMethod1ExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that the 'Text' property of 'tipField' text box equals '1.5'
        /// </summary>
        public string UITipFieldEditText = "1.5";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIForm1Window : WinWindow
    {
        
        public UIForm1Window()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "Form1";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Form1");
            #endregion
        }
        
        #region Properties
        public UIBillFieldWindow UIBillFieldWindow
        {
            get
            {
                if ((this.mUIBillFieldWindow == null))
                {
                    this.mUIBillFieldWindow = new UIBillFieldWindow(this);
                }
                return this.mUIBillFieldWindow;
            }
        }
        
        public UIItem15Window UIItem15Window
        {
            get
            {
                if ((this.mUIItem15Window == null))
                {
                    this.mUIItem15Window = new UIItem15Window(this);
                }
                return this.mUIItem15Window;
            }
        }
        
        public UIComputeTipWindow UIComputeTipWindow
        {
            get
            {
                if ((this.mUIComputeTipWindow == null))
                {
                    this.mUIComputeTipWindow = new UIComputeTipWindow(this);
                }
                return this.mUIComputeTipWindow;
            }
        }
        
        public UITipFieldWindow UITipFieldWindow
        {
            get
            {
                if ((this.mUITipFieldWindow == null))
                {
                    this.mUITipFieldWindow = new UITipFieldWindow(this);
                }
                return this.mUITipFieldWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIBillFieldWindow mUIBillFieldWindow;
        
        private UIItem15Window mUIItem15Window;
        
        private UIComputeTipWindow mUIComputeTipWindow;
        
        private UITipFieldWindow mUITipFieldWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIBillFieldWindow : WinWindow
    {
        
        public UIBillFieldWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "billField";
            this.WindowTitles.Add("Form1");
            #endregion
        }
        
        #region Properties
        public WinEdit UIBillFieldEdit
        {
            get
            {
                if ((this.mUIBillFieldEdit == null))
                {
                    this.mUIBillFieldEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIBillFieldEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Enter Total Bill";
                    this.mUIBillFieldEdit.WindowTitles.Add("Form1");
                    #endregion
                }
                return this.mUIBillFieldEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIBillFieldEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIItem15Window : WinWindow
    {
        
        public UIItem15Window(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "tipPercentField";
            this.WindowTitles.Add("Form1");
            #endregion
        }
        
        #region Properties
        public WinEdit UITipPercentFieldEdit
        {
            get
            {
                if ((this.mUITipPercentFieldEdit == null))
                {
                    this.mUITipPercentFieldEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITipPercentFieldEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Tip%";
                    this.mUITipPercentFieldEdit.WindowTitles.Add("Form1");
                    #endregion
                }
                return this.mUITipPercentFieldEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITipPercentFieldEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIComputeTipWindow : WinWindow
    {
        
        public UIComputeTipWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "computeButton";
            this.WindowTitles.Add("Form1");
            #endregion
        }
        
        #region Properties
        public WinButton UIComputeTipButton
        {
            get
            {
                if ((this.mUIComputeTipButton == null))
                {
                    this.mUIComputeTipButton = new WinButton(this);
                    #region Search Criteria
                    this.mUIComputeTipButton.SearchProperties[WinButton.PropertyNames.Name] = "Compute Tip";
                    this.mUIComputeTipButton.WindowTitles.Add("Form1");
                    #endregion
                }
                return this.mUIComputeTipButton;
            }
        }
        #endregion
        
        #region Fields
        private WinButton mUIComputeTipButton;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UITipFieldWindow : WinWindow
    {
        
        public UITipFieldWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "tipField";
            this.WindowTitles.Add("Form1");
            #endregion
        }
        
        #region Properties
        public WinEdit UITipFieldEdit
        {
            get
            {
                if ((this.mUITipFieldEdit == null))
                {
                    this.mUITipFieldEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITipFieldEdit.WindowTitles.Add("Form1");
                    #endregion
                }
                return this.mUITipFieldEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITipFieldEdit;
        #endregion
    }
}