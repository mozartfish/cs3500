using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceWars
{
    /// <summary>
    /// A panel that draws the game and its objects on it
    /// </summary>
    class GamePanel : Panel
    {
        /// <summary>
        /// The world object containing all entities and the size
        /// </summary>
        private World world;

        /// <summary>
        /// A list containing all ship images
        /// </summary>
        private List<Image> shipImages;

        /// <summary>
        /// A list containing all thrusting ship images
        /// </summary>
        private List<Image> thrustImages;

        /// <summary>
        /// A list of all projectile images
        /// </summary>
        private List<Image> projImages;

        /// <summary>
        /// The list containing all (but usually only one) star images
        /// </summary>
        private List<Image> starImages;

        /// <summary>
        /// The image for a ship's death animation
        /// </summary>
        private Image deathImage;

        /// <summary>
        /// Creates a new game panel with an instance of the game world
        /// </summary>
        /// <param name="world"></param>
        public GamePanel(World world)
        {
            DoubleBuffered = true;
            this.world = world;

            //Get all images from the resources image folder
            string[] imagePaths = Directory.GetFiles("..\\..\\..\\Resources\\Images\\");

            //Instanciate the lists
            shipImages = new List<Image>();
            thrustImages = new List<Image>();
            projImages = new List<Image>();
            starImages = new List<Image>();


            foreach (string imageName in imagePaths)
            {
                Image image = Image.FromFile(imageName);

                //Add to either ship, projectile, or star list based on name of file
                if (imageName.Contains("ship"))
                    if (imageName.Contains("coast"))
                        shipImages.Add(image);
                    else
                        thrustImages.Add(image); //If image of thrust ship put in different list

                else if (imageName.Contains("shot"))
                    projImages.Add(image);
                else if (imageName.Contains("Explosion"))
                    deathImage = image;
                else
                    starImages.Add(image);
            }

            this.Size = new Size(world.Size, world.Size); //Game world is square so set the size to the world's size
            base.BackColor = Color.Black; //Set the background color to black
        }

        /// <summary>
        /// Helper method for DrawObjectWithTransform
        /// </summary>
        /// <param name="size">The world (and image) size</param>
        /// <param name="w">The worldspace coordinate</param>
        /// <returns></returns>
        private static int WorldSpaceToImageSpace(int size, double w)
        {
            return (int)w + size / 2;
        }

        // A delegate for DrawObjectWithTransform
        // Methods matching this delegate can draw whatever they want using e  
        public delegate void ObjectDrawer(object o, PaintEventArgs e);


        /// <summary>
        /// This method performs a translation and rotation to drawn an object in the world.
        /// </summary>
        /// <param name="e">PaintEventArgs to access the graphics (for drawing)</param>
        /// <param name="o">The object to draw</param>
        /// <param name="worldSize">The size of one edge of the world (assuming the world is square)</param>
        /// <param name="worldX">The X coordinate of the object in world space</param>
        /// <param name="worldY">The Y coordinate of the object in world space</param>
        /// <param name="angle">The orientation of the objec, measured in degrees clockwise from "up"</param>
        /// <param name="drawer">The drawer delegate. After the transformation is applied, the delegate is invoked to draw whatever it wants</param>
        private void DrawObjectWithTransform(PaintEventArgs e, object o, int worldSize, double worldX, double worldY, double angle, ObjectDrawer drawer)
        {
            // Perform the transformation
            int x = WorldSpaceToImageSpace(worldSize, worldX);
            int y = WorldSpaceToImageSpace(worldSize, worldY);
            e.Graphics.TranslateTransform(x, y);
            e.Graphics.RotateTransform((float)angle);
            // Draw the object 
            drawer(o, e);
            // Then undo the transformation
            e.Graphics.ResetTransform();
        }

        /// <summary>
        /// This method is called as a handler to handle any paint events for this GamePanel
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            lock (world) //Lock the method to stop from causing issues with iterating in the loops
            {

                // Draw the projectiles
                foreach (Projectile pro in world.getProjectiles().Values)
                {
                    DrawObjectWithTransform(e, pro, this.Size.Width, pro.Location.GetX(), pro.Location.GetY(), pro.Direction.ToAngle(), ProjectileDrawer);
                }
            }

            lock (world) //Lock the method to stop from causing issues with iterating in the loops
            {
                // Draw the stars
                foreach (Star star in world.getStars().Values)
                {
                    DrawObjectWithTransform(e, star, this.Size.Width, star.Location.GetX(), star.Location.GetY(), 0, StarDrawer);
                }
            }

            lock (world) //Lock the method to stop from causing issues with iterating in the loops
            {
                // Draw the ships
                foreach (Ship ship in world.getShips().Values)
                {
                    if (ship.HP > 0) //Only draw live ships
                        DrawObjectWithTransform(e, ship, this.Size.Width, ship.Location.GetX(), ship.Location.GetY(), ship.Direction.ToAngle(), ShipDrawer);
                    else
                        DrawObjectWithTransform(e, ship, this.Size.Width, ship.Location.GetX(), ship.Location.GetY(), ship.Direction.ToAngle(), DeathDrawer);
                }
            }

            // Do anything that Panel (from which we inherit) needs to do
            base.OnPaint(e);
        }

        /// <summary>
        /// Draws a ship provided as an object in the parameters as an image
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        private void ShipDrawer(object o, PaintEventArgs e)
        {
            //Convert object to a ship
            Ship ship = o as Ship;

            //Get the ship color ID
            int shipColorNum = ship.IDField % shipImages.Count;

            //Represents the the image for a ship
            Image shipImage = ship.Thrust ? thrustImages[shipColorNum] : shipImages[shipColorNum];

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Get a rectangle centered on the ship image, and draw it (divied by a constant because images are large)
            Rectangle r = new Rectangle(-(shipImage.Width / 6), -(shipImage.Height / 6), shipImage.Width / 3, shipImage.Height / 3);
            e.Graphics.DrawImage(shipImage, r);
        }

        /// <summary>
        /// Draws a projectile provided as an object in the parameters as an image
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        private void ProjectileDrawer(object o, PaintEventArgs e)
        {
            Projectile pro = o as Projectile;

            //Get the proj color ID, based off of the owner ship ID
            int projColorNum = pro.Owner % projImages.Count;

            //Represents the the image for a ship
            Image projImage = projImages[projColorNum];

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Get a rectangle centered on the ship image, and draw it (divided by a constant because images are large)
            Rectangle r = new Rectangle(-(projImage.Width / 6), -(projImage.Height / 6), projImage.Width / 3, projImage.Height / 3);
            e.Graphics.DrawImage(projImage, r);
        }

        /// <summary>
        /// Draws a star provided as an object in the parameters as an image
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        private void StarDrawer(object o, PaintEventArgs e)
        {
            Star star = o as Star;

            //Get the proj color ID, based off of the owner ship ID
            int starColorNum = star.IDField % starImages.Count;

            //Represents the the image for a ship
            Image starImage = starImages[starColorNum];

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Get a rectangle centered on the ship image, and draw it (divided by a constant because images are large)
            Rectangle r = new Rectangle(-(starImage.Width / 6), -(starImage.Height / 6), starImage.Width / 3, starImage.Height / 3);
            e.Graphics.DrawImage(starImage, r);
        }

        /// <summary>
        /// Draws a death animation with a given image
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        private void DeathDrawer(object o, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Get a rectangle centered on the ship image, and draw it (divided by a constant because images are large)
            Rectangle r = new Rectangle(-(deathImage.Width / 4), -(deathImage.Height / 4), deathImage.Width / 2, deathImage.Height / 2);
            e.Graphics.DrawImage(deathImage, r);
        }
    }
}
