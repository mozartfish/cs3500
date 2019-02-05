using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SpaceWars
{
    /// <summary>
    /// This class represents a scoreboard showing all active players with their health and scores
    /// </summary>
    public class Scoreboard : Panel
    {
        /// <summary>
        /// The world object containing all entities and the size
        /// </summary>
        private World world;

        /// <summary>
        /// Constructor that creates an instance of a scoreboard, at a pixel offset from the world's size
        /// for information reference
        /// </summary>
        public Scoreboard(World world)
        {
            DoubleBuffered = true;
            this.world = world;
            this.Location = new Point(world.Size, 0); //Set the location to the correct offset
            this.Size = new Size(150, world.Size);
        }

        /// <summary>
        /// This method is called as a handler to handle any paint events for this Scoreboard
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            int offset = 20; //Start 20 pixels from the top of the

            SortedDictionary<double, Ship> orderedShips = new SortedDictionary<double, Ship>();

            lock (world) //Lock the method to stop from causing issues with iterating in the loops
            {

                // Draw the scores based on info in the ships
                foreach (Ship ship in world.getShips().Values)
                {
                    string IDandScore = ship.Score + "." + ship.IDField; //Make unique 'double' value of a combo of score and ID
                    orderedShips[double.Parse(IDandScore)] = ship; //Add the ship with its unique score/id
                    offset += 40; //Increment offset for each player
                }

            }

            foreach (Ship ship in orderedShips.Values)
            {
                offset -= 40; //Decrement offset so we draw in order of low to high, bottom to top
                ScoreDrawer(ship.Name, ship.Score, ship.HP, offset, e);
            }

            // Do anything that Panel (from which we inherit) needs to do
            base.OnPaint(e);
        }

        /// <summary>
        /// Draws the scoreboard according to the ship name, player score, player health, and offset
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="score">The player score</param>
        /// <param name="health">The player health</param>
        /// <param name="offsetFromTop">The offset amount from the top</param>
        /// <param name="e"></param>
        private void ScoreDrawer(string name, int score, int health, int offsetFromTop, PaintEventArgs e)
        {

            int width = 100; //Health bar defaults
            int height = 10;
            int leftOffset = 10; //Offset from panel edge

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            using (System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green))
            using (System.Drawing.SolidBrush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
            {
                //Draw the two overlayed health bars
                Rectangle back = new Rectangle(leftOffset - 2, offsetFromTop - 2 + 23, width + 4, height + 4);
                Rectangle healthUnderlay = new Rectangle(leftOffset, offsetFromTop + 23, width, height);
                Rectangle currentHealth = new Rectangle(leftOffset, offsetFromTop + 23, health * 20, height);
                e.Graphics.FillRectangle(blackBrush, back);
                e.Graphics.FillRectangle(redBrush, healthUnderlay);
                e.Graphics.FillRectangle(greenBrush, currentHealth);

                //Draw the name and score
                e.Graphics.DrawString(name + " : " + score, new Font(new FontFamily("Times New Roman"), 12), blackBrush, new PointF(leftOffset - 2, offsetFromTop));
            }


        }
    }
}
