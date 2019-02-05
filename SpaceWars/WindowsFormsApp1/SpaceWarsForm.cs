using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controllers;

namespace SpaceWars
{
    public partial class SpaceWarsForm : Form
    {
        /// <summary>
        /// The controller for the game
        /// </summary>
        private GameController controller;

        /// <summary>
        /// The game panel where gameplay takes place
        /// </summary>
        private GamePanel gamepanel;

        /// <summary>
        /// The scoreboard that displays all user scores
        /// </summary>
        private Scoreboard scores;

        public SpaceWarsForm()
        {
            InitializeComponent();
            nameTextBox.KeyDown += TextBoxKeyPress; //Establish the handlers for pressing enter in a text box
            serverTextBox.KeyDown += TextBoxKeyPress;
            FormClosing += OnClose;

        }

        /// <summary>
        /// Method that handles button clicks for trying to start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, EventArgs e)
        {
            TryStartup();
        }

        /// <summary>
        /// Method that finishes running all threads 
        /// </summary>
        private void OnClose(Object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        /// <summary>
        /// Handler for when enter is pressed in some item to then try and boot up the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TryStartup();
            }

        }

        /// <summary>
        /// A helper method that attempts to start the game with the provided strings in the two text boxes,
        /// if connection fails or server address is invalid, keeps the current items displayed with an error message,
        /// otherwise makes these items invisible
        /// </summary>
        private void TryStartup()
        {
            //If no text to use just return
            if (serverTextBox.Text.Equals("") || nameTextBox.Text.Equals(""))
                return;

            try
            {
                //Try to boot up the game
                controller = new GameController(nameTextBox.Text, serverTextBox.Text);
                controller.ConnectionLost += ConnectionLost;
                controller.DataProcessed += InitializeGamePanels; //Subscribe the handler that creates panels, because it must wait for the world to be created
            }
            catch (ArgumentException e) //Catch bad server address input and display message
            {
                badServerLabel.Text = e.Message;
                badServerLabel.Visible = true;
                return;
            }

            //Set all of the pre-game boxes and labels to invisible
            serverLabel.Visible = false;
            serverTextBox.Visible = false;
            nameLabel.Visible = false;
            nameTextBox.Visible = false;
            connectButton.Visible = false;
            badServerLabel.Visible = false;
            connectButton.TabStop = false;
            this.Focus();
        }

        /// <summary>
        /// A method that should be invoked once the GameController has a world size, so we can then make the GamePanel of a proper size
        /// </summary>
        private void InitializeGamePanels()
        {
            this.Invoke(new MethodInvoker(AddGamePanelToControls)); //Add the game panel to the controls
            this.Invoke(new MethodInvoker(AddScoreboardToControlsAndResize)); //Add the game panel to the controls
            controller.DataProcessed -= InitializeGamePanels;
            controller.DataProcessed += UpdateWorld;
            this.KeyDown += OnKeyPress; //Allow for user input
            this.KeyUp += OnKeyRelease;
        }

        /// <summary>
        /// Helper method for adding a game panel to the controls, to prevent thread crossing
        /// </summary>
        private void AddGamePanelToControls()
        {
            gamepanel = new GamePanel(controller.GameWorld); //Create the panel that draws the game using the world from game world
            gamepanel.Location = new Point(0, 0); //Anchor it to the top left
            this.Controls.Add(gamepanel);
            gamepanel.Visible = true; //Make it visible
        }

        /// <summary>
        /// Helper method for adding a game panel to the controls, to prevent thread crossing (also resizes the window to the proper size)
        /// </summary>
        private void AddScoreboardToControlsAndResize()
        {
            scores = new Scoreboard(controller.GameWorld); //Create the panel that draws the scores using the world from game world
            this.Controls.Add(scores);
            scores.Visible = true; //Make it visible

            this.Size = new Size(gamepanel.Width + scores.Width, gamepanel.Height); //Size the window properly
        }

        /// <summary>
        /// A method that on the press of a key, will tell the controller if a specific key
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnKeyPress(Object o, KeyEventArgs e)
        {
            //Change the key flag property based on key pressed
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                controller.KeyLeft = true;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                controller.KeyRight = true;
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                controller.KeyThrust = true;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
                controller.KeyFire = true;
        }

        /// <summary>
        /// A method that on the release of a key, will tell the controller if a specific key
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnKeyRelease(Object o, KeyEventArgs e)
        {
            //Change the key flag property based on key pressed
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                controller.KeyLeft = false;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                controller.KeyRight = false;
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                controller.KeyThrust = false;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down || e.KeyCode == Keys.Space)
                controller.KeyFire = false;
        }

        /// <summary>
        /// Updates the world so that any changes are repainted
        /// </summary>
        private void UpdateWorld()
        {
            this.Invoke(new MethodInvoker(gamepanel.Invalidate)); //Invalidate the panel to redraw it
            this.Invoke(new MethodInvoker(scores.Invalidate));
        }

        /// <summary>
        /// A message to invoke when connection to a server is lost
        /// </summary>
        private void ConnectionLost()
        {
            this.Invoke(new MethodInvoker(ConnectionLostHelper));
        }

        /// <summary>
        /// A helper method so that the work needed for ConnectionLost is invoked on its own thread
        /// </summary>
        private void ConnectionLostHelper()
        {
            // Show the connection boxes again
            serverLabel.Visible = true;
            serverTextBox.Visible = true;
            nameLabel.Visible = true;
            nameTextBox.Visible = true;
            connectButton.Visible = true;

            //Give a useful message
            badServerLabel.Text = "Connection lost or not found, please try again";
            badServerLabel.Visible = true;

            controller = null;
            this.Controls.Remove(gamepanel);
            this.Controls.Remove(scores);
        }

        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Disable the button to avoid the background worker invalid operation exception
            controlsToolStripMenuItem.Enabled = false;

            //Open the Help menu
            GameControlMenu.RunWorkerAsync();
        }

        private void GameControlMenu_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("UP / W: Fire Thrusters\n" +
                "LEFT / A: Rotate Left\n" +
                "RIGHT / D: Rotate Right\n" +
                "DOWN / S / SPACE: Fire");
        }

        private void GameControlMenu_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            controlsToolStripMenuItem.Enabled = true;
        }
    }
}
