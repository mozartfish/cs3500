using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceWars
{
    /// <summary>
    /// This class represents a star object that has a mass to impose gravity
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Star
    {
        /// <summary>
        /// The ID of this star
        /// </summary>
        [JsonProperty(PropertyName = "star")]
        private int ID;

        /// <summary>
        /// The x and y location of this star
        /// </summary>
        [JsonProperty]
        private Vector2D loc;

        /// <summary>
        /// The mass of this star
        /// </summary>
        [JsonProperty]
        private double mass;

        /// <summary>
        /// The default star constructor
        /// </summary>
        public Star()
        {
            //Set the default values
            this.ID = 0;
            this.loc = new Vector2D();
            this.mass = 0.0;
        }

        /// <summary>
        /// Creates a Star instance with the Star ID, location and mass
        /// If the star ID or mass is less than zero, an argument exception is thrown
        /// </summary>
        /// <param name="ID">An integer representing the id of a star</param>
        /// <param name="loc">A vector representing the location of a star</param>
        /// <param name="mass">A double represeting the mass of a star</param>
        public Star(int ID, Vector2D loc, double mass)
        {
            if (ID < 0 || mass < 0.0) // check constraint
                throw new ArgumentException("Only positive ID and Mass allowed!");

            //Set all fields to provided values
            this.ID = ID;
            this.loc = loc;
            this.mass = mass;
        }

        /// <summary>
        /// Gets or sets the ID of the star
        /// </summary>
        public int IDField { get { return ID; } set { ID = value; } }

        /// <summary>
        /// Gets or sets the location of the star
        /// </summary>
        public Vector2D Location { get { return loc; } set { loc = value; } }

        /// <summary>
        /// Gets or sets the mass of the star
        /// </summary>
        public double Mass { get { return mass; } set { mass = value; } }
    }
}
