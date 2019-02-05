namespace SpreadsheetGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cellGrid = new SS.SpreadsheetPanel();
            this.SelectedCellContentsTextBox = new System.Windows.Forms.TextBox();
            this.CellContentsLabel = new System.Windows.Forms.Label();
            this.CellNameTextBox = new System.Windows.Forms.TextBox();
            this.CellNameLabel = new System.Windows.Forms.Label();
            this.CellValueTextBox = new System.Windows.Forms.TextBox();
            this.CellValueLabel = new System.Windows.Forms.Label();
            this.SetContentsButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpreadsheetAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectingACellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editingACellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savingASpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openingASpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CellUpdaterThread = new System.ComponentModel.BackgroundWorker();
            this.HelpMenuSelectCellThread = new System.ComponentModel.BackgroundWorker();
            this.HelpMenuEditCellThread = new System.ComponentModel.BackgroundWorker();
            this.HelpMenuSavingSpreadsheetThread = new System.ComponentModel.BackgroundWorker();
            this.HelpMenuOpeningSpreadsheetThread = new System.ComponentModel.BackgroundWorker();
            this.saveSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfAdditionalFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuAdditionalFeaturesThread = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cellGrid
            // 
            this.cellGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cellGrid.AutoScroll = true;
            this.cellGrid.BackColor = System.Drawing.Color.DarkGray;
            this.cellGrid.Location = new System.Drawing.Point(9, 38);
            this.cellGrid.Margin = new System.Windows.Forms.Padding(2);
            this.cellGrid.Name = "cellGrid";
            this.cellGrid.Size = new System.Drawing.Size(779, 376);
            this.cellGrid.TabIndex = 0;
            // 
            // SelectedCellContentsTextBox
            // 
            this.SelectedCellContentsTextBox.Location = new System.Drawing.Point(412, 15);
            this.SelectedCellContentsTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SelectedCellContentsTextBox.Name = "SelectedCellContentsTextBox";
            this.SelectedCellContentsTextBox.Size = new System.Drawing.Size(121, 20);
            this.SelectedCellContentsTextBox.TabIndex = 1;
            // 
            // CellContentsLabel
            // 
            this.CellContentsLabel.AutoSize = true;
            this.CellContentsLabel.BackColor = System.Drawing.Color.Red;
            this.CellContentsLabel.Location = new System.Drawing.Point(409, 0);
            this.CellContentsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CellContentsLabel.Name = "CellContentsLabel";
            this.CellContentsLabel.Size = new System.Drawing.Size(69, 13);
            this.CellContentsLabel.TabIndex = 2;
            this.CellContentsLabel.Text = "Cell Contents";
            // 
            // CellNameTextBox
            // 
            this.CellNameTextBox.Location = new System.Drawing.Point(320, 14);
            this.CellNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CellNameTextBox.Name = "CellNameTextBox";
            this.CellNameTextBox.ReadOnly = true;
            this.CellNameTextBox.Size = new System.Drawing.Size(53, 20);
            this.CellNameTextBox.TabIndex = 3;
            // 
            // CellNameLabel
            // 
            this.CellNameLabel.AutoSize = true;
            this.CellNameLabel.BackColor = System.Drawing.Color.Red;
            this.CellNameLabel.Location = new System.Drawing.Point(317, -1);
            this.CellNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CellNameLabel.Name = "CellNameLabel";
            this.CellNameLabel.Size = new System.Drawing.Size(55, 13);
            this.CellNameLabel.TabIndex = 4;
            this.CellNameLabel.Text = "Cell Name";
            // 
            // CellValueTextBox
            // 
            this.CellValueTextBox.Location = new System.Drawing.Point(635, 14);
            this.CellValueTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CellValueTextBox.Name = "CellValueTextBox";
            this.CellValueTextBox.ReadOnly = true;
            this.CellValueTextBox.Size = new System.Drawing.Size(76, 20);
            this.CellValueTextBox.TabIndex = 5;
            // 
            // CellValueLabel
            // 
            this.CellValueLabel.AutoSize = true;
            this.CellValueLabel.BackColor = System.Drawing.Color.Red;
            this.CellValueLabel.Location = new System.Drawing.Point(632, -1);
            this.CellValueLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CellValueLabel.Name = "CellValueLabel";
            this.CellValueLabel.Size = new System.Drawing.Size(54, 13);
            this.CellValueLabel.TabIndex = 6;
            this.CellValueLabel.Text = "Cell Value";
            // 
            // SetContentsButton
            // 
            this.SetContentsButton.BackColor = System.Drawing.SystemColors.Control;
            this.SetContentsButton.Location = new System.Drawing.Point(537, 14);
            this.SetContentsButton.Margin = new System.Windows.Forms.Padding(2);
            this.SetContentsButton.Name = "SetContentsButton";
            this.SetContentsButton.Size = new System.Drawing.Size(56, 19);
            this.SetContentsButton.TabIndex = 7;
            this.SetContentsButton.Text = "Go";
            this.SetContentsButton.UseVisualStyleBackColor = false;
            this.SetContentsButton.Click += new System.EventHandler(this.SetContentsButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Red;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSpreadsheetToolStripMenuItem,
            this.openSpreadsheetToolStripMenuItem,
            this.saveSpreadsheetToolStripMenuItem,
            this.saveSpreadsheetAsToolStripMenuItem,
            this.closeSpreadsheetToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newSpreadsheetToolStripMenuItem
            // 
            this.newSpreadsheetToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.newSpreadsheetToolStripMenuItem.Name = "newSpreadsheetToolStripMenuItem";
            this.newSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.newSpreadsheetToolStripMenuItem.Text = "New Spreadsheet";
            this.newSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.newSpreadsheetToolStripMenuItem_Click);
            // 
            // openSpreadsheetToolStripMenuItem
            // 
            this.openSpreadsheetToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.openSpreadsheetToolStripMenuItem.Name = "openSpreadsheetToolStripMenuItem";
            this.openSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openSpreadsheetToolStripMenuItem.Text = "Open Spreadsheet";
            this.openSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.openSpreadsheetToolStripMenuItem_Click);
            // 
            // saveSpreadsheetAsToolStripMenuItem
            // 
            this.saveSpreadsheetAsToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.saveSpreadsheetAsToolStripMenuItem.Name = "saveSpreadsheetAsToolStripMenuItem";
            this.saveSpreadsheetAsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveSpreadsheetAsToolStripMenuItem.Text = "Save Spreadsheet As";
            this.saveSpreadsheetAsToolStripMenuItem.Click += new System.EventHandler(this.saveSpreadsheetAsToolStripMenuItem_Click);
            // 
            // closeSpreadsheetToolStripMenuItem
            // 
            this.closeSpreadsheetToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.closeSpreadsheetToolStripMenuItem.Name = "closeSpreadsheetToolStripMenuItem";
            this.closeSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.closeSpreadsheetToolStripMenuItem.Text = "Close Spreadsheet";
            this.closeSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.closeSpreadsheetToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectingACellToolStripMenuItem,
            this.editingACellToolStripMenuItem,
            this.savingASpreadsheetToolStripMenuItem,
            this.openingASpreadsheetToolStripMenuItem,
            this.listOfAdditionalFeaturesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // selectingACellToolStripMenuItem
            // 
            this.selectingACellToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.selectingACellToolStripMenuItem.Name = "selectingACellToolStripMenuItem";
            this.selectingACellToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.selectingACellToolStripMenuItem.Text = "Selecting a Cell";
            this.selectingACellToolStripMenuItem.Click += new System.EventHandler(this.selectingACellToolStripMenuItem_Click);
            // 
            // editingACellToolStripMenuItem
            // 
            this.editingACellToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.editingACellToolStripMenuItem.Name = "editingACellToolStripMenuItem";
            this.editingACellToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.editingACellToolStripMenuItem.Text = "Editing a Cell";
            this.editingACellToolStripMenuItem.Click += new System.EventHandler(this.editingACellToolStripMenuItem_Click);
            // 
            // savingASpreadsheetToolStripMenuItem
            // 
            this.savingASpreadsheetToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.savingASpreadsheetToolStripMenuItem.Name = "savingASpreadsheetToolStripMenuItem";
            this.savingASpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.savingASpreadsheetToolStripMenuItem.Text = "Saving a Spreadsheet";
            this.savingASpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.savingASpreadsheetToolStripMenuItem_Click);
            // 
            // openingASpreadsheetToolStripMenuItem
            // 
            this.openingASpreadsheetToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.openingASpreadsheetToolStripMenuItem.Name = "openingASpreadsheetToolStripMenuItem";
            this.openingASpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.openingASpreadsheetToolStripMenuItem.Text = "Opening a Spreadsheet";
            this.openingASpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.openingASpreadsheetToolStripMenuItem_Click);
            // 
            // CellUpdaterThread
            // 
            this.CellUpdaterThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CellUpdaterThread_DoWork);
            this.CellUpdaterThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CellUpdaterThread_RunWorkerCompleted);
            // 
            // HelpMenuSelectCellThread
            // 
            this.HelpMenuSelectCellThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HelpMenuSelectCellThread_DoWork);
            this.HelpMenuSelectCellThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HelpMenuSelectCellThread_RunWorkerCompleted);
            // 
            // HelpMenuEditCellThread
            // 
            this.HelpMenuEditCellThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HelpMenuEditCellThread_DoWork);
            this.HelpMenuEditCellThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HelpMenuEditCellThread_RunWorkerCompleted);
            // 
            // HelpMenuSavingSpreadsheetThread
            // 
            this.HelpMenuSavingSpreadsheetThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HelpMenuSavingSpreadsheetThread_DoWork);
            this.HelpMenuSavingSpreadsheetThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HelpMenuSavingSpreadsheetThread_RunWorkerCompleted);
            // 
            // HelpMenuOpeningSpreadsheetThread
            // 
            this.HelpMenuOpeningSpreadsheetThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HelpMenuOpeningSpreadsheetThread_DoWork);
            this.HelpMenuOpeningSpreadsheetThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HelpMenuOpeningSpreadsheetThread_RunWorkerCompleted);
            // 
            // saveSpreadsheetToolStripMenuItem
            // 
            this.saveSpreadsheetToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.saveSpreadsheetToolStripMenuItem.Name = "saveSpreadsheetToolStripMenuItem";
            this.saveSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.saveSpreadsheetToolStripMenuItem.Text = "Save Spreadsheet";
            this.saveSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.saveSpreadsheetToolStripMenuItem_Click);
            // 
            // listOfAdditionalFeaturesToolStripMenuItem
            // 
            this.listOfAdditionalFeaturesToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.listOfAdditionalFeaturesToolStripMenuItem.Name = "listOfAdditionalFeaturesToolStripMenuItem";
            this.listOfAdditionalFeaturesToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.listOfAdditionalFeaturesToolStripMenuItem.Text = "List of Additional Features";
            this.listOfAdditionalFeaturesToolStripMenuItem.Click += new System.EventHandler(this.listOfAdditionalFeaturesToolStripMenuItem_Click);
            // 
            // HelpMenuAdditionalFeaturesThread
            // 
            this.HelpMenuAdditionalFeaturesThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HelpMenuAdditionalFeaturesThread_DoWork);
            this.HelpMenuAdditionalFeaturesThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HelpMenuAdditionalFeaturesThread_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(797, 424);
            this.Controls.Add(this.SetContentsButton);
            this.Controls.Add(this.CellValueLabel);
            this.Controls.Add(this.CellValueTextBox);
            this.Controls.Add(this.CellNameLabel);
            this.Controls.Add(this.CellNameTextBox);
            this.Controls.Add(this.CellContentsLabel);
            this.Controls.Add(this.SelectedCellContentsTextBox);
            this.Controls.Add(this.cellGrid);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel cellGrid;
        private System.Windows.Forms.TextBox SelectedCellContentsTextBox;
        private System.Windows.Forms.Label CellContentsLabel;
        private System.Windows.Forms.TextBox CellNameTextBox;
        private System.Windows.Forms.Label CellNameLabel;
        private System.Windows.Forms.TextBox CellValueTextBox;
        private System.Windows.Forms.Label CellValueLabel;
        private System.Windows.Forms.Button SetContentsButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSpreadsheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSpreadsheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSpreadsheetAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectingACellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editingACellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeSpreadsheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savingASpreadsheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openingASpreadsheetToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker CellUpdaterThread;
        private System.ComponentModel.BackgroundWorker HelpMenuSelectCellThread;
        private System.ComponentModel.BackgroundWorker HelpMenuEditCellThread;
        private System.ComponentModel.BackgroundWorker HelpMenuSavingSpreadsheetThread;
        private System.ComponentModel.BackgroundWorker HelpMenuOpeningSpreadsheetThread;
        private System.Windows.Forms.ToolStripMenuItem saveSpreadsheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listOfAdditionalFeaturesToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker HelpMenuAdditionalFeaturesThread;
    }
}

