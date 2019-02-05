using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceWars
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Projectile
    {
        /// <summary>
        /// The projectile ID
        /// </summary>
        [JsonProperty(PropertyName = "proj")]
        private int ID;

        /// <summary>
        /// The location of the projectile, as a vector
        /// </summary>
        [JsonProperty]
        private Vector2D loc;

        /// <summary>
        /// The direction of the projectile, as a vector
        /// </summary>
        [JsonProperty]
        private Vector2D dir;

        /// <summary>
        /// The status of the projectile as a boolean
        /// </summary>
        [JsonProperty]
        private bool alive;

        /// <summary>
        /// The player whose ship owns that specific kind of projectile
        /// </summary>
        [JsonProperty]
        private int owner;

        /// <summary>
        /// The default constructor
        /// </summary>
        public Projectile()
        {
            //Set the default values
            this.ID = 0;
            this.loc = new Vector2D();
            this.dir = new Vector2D();
            this.alive = true;
            this.owner = 0;
        }

        /// <summary>
        /// Creates a Projectile instance with the ID, location, direction and owner
        /// If the ID or owner are less than zero, an argument exception is thrown
        /// </summary>
        /// <param name="ID">The Projectile ID</param>
        /// <param name="loc">The Projectile location</param>
        /// <param name="dir">The Projectile direction</param>
        /// <param name="owner">The Projectile owner</param>
        public Projectile(int ID, Vector2D loc, Vector2D dir, int owner)
        {
            if (ID < 0 || owner < 0) // check constraint
                throw new ArgumentException("Only positive ID and Owner allowed!");

            //Set all fields to provided values
            this.ID = ID;
            this.loc = loc;
            this.dir = dir;
            this.owner = owner;

            //Default value
            this.alive = true;
        }


        /// <summary>
        /// Gets or sets the ID of the projectile
        /// </summary>
        public int IDField { get { return ID; } set { ID = value; } }

        /// <summary>
        /// Gets or sets the location of the projectile
        /// </summary>
        public Vector2D Location { get { return loc; } set { loc = value; } }

        /// <summary>
        /// Gets or sets the direction of the projectile
        /// </summary>
        public Vector2D Direction { get { return dir; } set { dir = value; } }

        /// <summary>
        /// Gets or sets the owner of the projectile
        /// </summary>
        public int Owner { get { return owner; } set { owner = value; } }

        /// <summary>
        /// Gets or sets whether the projectile is alive
        /// </summary>
        public bool Alive { get { return alive; } set { alive = value; } }

        /// <summary>
        /// Gets or sets the Velocity of the projectile (for extra feature use only)
        /// </summary>
        public Vector2D Velocity { get; set; }
    }
}

