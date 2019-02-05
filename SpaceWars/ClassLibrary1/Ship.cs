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
    public class
    Ship
    {
        /// <summary>
        /// The int representing the ID of this ship
        /// </summary>
        [JsonProperty(PropertyName = "ship")]
        private int ID;

        /// <summary>
        /// The location of the ship, as a vector
        /// </summary>
        [JsonProperty]
        private Vector2D loc;

        /// <summary>
        /// The direction of the ship, as a vector
        /// </summary>
        [JsonProperty]
        private Vector2D dir;

        /// <summary>
        /// The velocity of the ship, as a vector
        /// </summary>
        private Vector2D vel;

        /// <summary>
        /// A boolean representing whether or not the ship is thrusting
        /// </summary>
        [JsonProperty]
        private bool thrust;

        /// <summary>
        /// The name of the player who owns this ship
        /// </summary>
        [JsonProperty]
        private string name;

        /// <summary>
        /// The health points of the ship
        /// </summary>
        [JsonProperty]
        private int hp;

        /// <summary>
        /// The score of this ship
        /// </summary>
        [JsonProperty]
        private int score;

        /// <summary>
        /// How long the ship must wait to respawn
        /// </summary>
        private int respawnFrames;

        /// <summary>
        /// The frames between when a ship can shoot again
        /// </summary>
        private int framesBetweenShots;

        /// <summary>
        /// Default constructor needed for JSON serialization
        /// </summary>
        public Ship()
        {
            //Just set default values for each field
            ID = 0;
            loc = new Vector2D();
            dir = new Vector2D();
            vel = new Vector2D(0, 0);
            thrust = false;
            name = "";
            hp = 5;
            score = 0;
        }

        /// <summary>
        /// Creates a ship instance with the Ship ID, location, direction, player name, and score
        /// 
        /// If ID or Score is less than 0 throws an ArgumentException
        /// </summary>
        public Ship(int ID, Vector2D loc, Vector2D dir, string name, int score)
        {
            if (ID < 0 || score < 0) //Check constraint
                throw new ArgumentException("Only positive ID and Score allowed!");

            //Set all fields to provided values
            this.ID = ID;
            this.loc = loc;
            this.dir = dir;
            this.name = name;
            this.score = score;
            vel = new Vector2D(0, 0);
            //Default values
            thrust = false;
            hp = 5;
            this.framesBetweenShots = 0;
        }


        /// <summary>
        /// Gets or sets the ID of the ship
        /// </summary>
        public int IDField { get { return ID; } set { ID = value; } }

        /// <summary>
        /// Gets or sets the location of the ship
        /// </summary>
        public Vector2D Location { get { return loc; } set { loc = value; } }

        /// <summary>
        /// Gets or sets the direction of the ship
        /// </summary>
        public Vector2D Direction { get { return dir; } set { dir = value; } }

        /// <summary>
        /// Gets or sets the Thrust of the ship
        /// </summary>
        public bool Thrust { get { return thrust; } set { thrust = value; } }

        /// <summary>
        /// Gets or sets the owner's name
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Gets or sets the HP of the ship
        /// </summary>
        public int HP { get { return hp; } set { hp = value; } }

        /// <summary>
        /// Gets or sets the Score of the ship
        /// </summary>
        public int Score { get { return score; } set { score = value; } }

        /// <summary>
        /// Gets or sets the Velocity of the ship
        /// </summary>
        public Vector2D Velocity { get { return vel; } set { vel = value; } }

        /// <summary>
        /// Gets or sets the frames before a ship can respawn
        /// </summary>
        public int RespawnFrames { get { return respawnFrames; } set { respawnFrames = value; } }

        /// <summary>
        /// The frames between when a ship can shoot again
        /// </summary>
        public int FramesBetweenShots { get { return framesBetweenShots; } set { framesBetweenShots = value; } }
    }
}
