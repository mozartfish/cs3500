using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWars
{

    /// <summary>
    /// A world object represents all entities contained within a game,
    /// and of a specific size
    /// </summary>
    public class World
    {
        /// <summary>
        /// A list of all ships contained in the world
        /// </summary>
        private Dictionary<int, Ship> ships;

        /// <summary>
        /// A list of all projectiles contained in the world
        /// </summary>
        private Dictionary<int, Projectile> projectiles;

        /// <summary>
        /// A list of all stars contained in the world
        /// </summary>
        private Dictionary<int, Star> stars;

        /// <summary>
        /// The size of the world, both width and height are the same
        /// </summary>
        private int size;


        ///////////////////////////////////////////////
        ///                                         ///
        ///     All instance variables below are    ///
        ///         meant for server use only       ///
        ///                                         ///
        ///////////////////////////////////////////////


        /// <summary>
        /// The HP value a ship will have upon spawn
        /// </summary>
        private int setHP;

        /// <summary>
        /// The speed a projectile will have when in the game
        /// </summary>
        private int projectileSpeed;

        /// <summary>
        /// The rate at which a ship can turn each frame
        /// </summary>
        private double turnRateDegrees;

        /// <summary>
        /// The strength of an engine which is applied to a thrusting ship to determine acceleration
        /// </summary>
        private double engineStrength;

        /// <summary>
        /// The radius of a circle around any ship, which is used for hit detection
        /// </summary>
        private int shipRadiusSize;

        /// <summary>
        /// The radius of a circle around any star, which is used for hit detection
        /// </summary>
        private int starRadiusSize;

        /// <summary>
        /// The frames required between a ship firing a projectile
        /// </summary>
        private int framesBetweenShots;

        /// <summary>
        /// The frames between a ship's death and respawn
        /// </summary>
        private int respawnFrames;

        /// <summary>
        /// Represents whether the extra feature is enabled or not for the instance of this world
        /// </summary>
        private bool extraContent;

        /// <summary>
        /// Dictionary that contains the commands associated with each player
        /// Key: The PlayerID
        /// Value: The UserCommands
        /// </summary>
        private Dictionary<int, String> PlayerCommands;

        /// <summary>
        /// An int representing the next ID available for a projectile
        /// </summary>
        private int currentProjID;

        /// <summary>
        /// These two ships represent teams so that when sent, the client can have the total team score
        /// </summary>
        private Ship team1;
        private Ship team2;

        /// <summary>
        /// Makes the world of a given size (Client Constructor)
        /// </summary>
        public World(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Only positive sizes accepted");

            //Create new lists for all game entities
            ships = new Dictionary<int, Ship>();
            projectiles = new Dictionary<int, Projectile>();
            stars = new Dictionary<int, Star>();
            this.size = size;
        }

        /// <summary>
        /// Constructs a world of a given size (Server Constructor)
        /// </summary>
        /// <param name="size">The size of the world</param>
        /// <param name="setHP">The amount of HP</param>
        /// <param name="projectileSpeed">The speed of the projectile</param>
        /// <param name="turnRateDegrees">The turn rate of the ship</param>
        /// <param name="engineStrength">The strength of the engine</param>
        /// <param name="shipCollisionRadius">The collision radius for ships</param>
        /// <param name="starCollisionRadius">The collision radius for stars</param>
        public World(int size, int setHP, int projectileSpeed, double turnRateDegrees, double engineStrength,
            int shipCollisionRadius, int starCollisionRadius, int framesBetweenShots, int respawnFrames, bool extraContent)
        {

            if (size <= 0 || shipCollisionRadius <= 0 || starCollisionRadius <= 0) // check to make sure that the collision radius is greater than 0
                throw new ArgumentException("Only positive size allowed");

            if (setHP <= 0) // check to make sure that the hp is positive
                throw new ArgumentException("Only positive HP allowed");


            ships = new Dictionary<int, Ship>();
            projectiles = new Dictionary<int, Projectile>();
            stars = new Dictionary<int, Star>();
            this.size = size;
            this.setHP = setHP;
            this.projectileSpeed = projectileSpeed;
            this.turnRateDegrees = turnRateDegrees;
            this.engineStrength = engineStrength;
            this.shipRadiusSize = shipCollisionRadius;
            this.starRadiusSize = starCollisionRadius;
            this.framesBetweenShots = framesBetweenShots;
            this.respawnFrames = respawnFrames;
            this.extraContent = extraContent;
            PlayerCommands = new Dictionary<int, string>();
            currentProjID = 0;

            if (extraContent) // check if extraContent mode is enabled
            {
                //"spawn" team ships off screen so death animations if they exist are not drawn by the client
                team1 = new Ship(int.MaxValue, new Vector2D(Size/2 + 50, 0), new Vector2D(), "Team 1", 0); // check to ensure that players and teams do not have same ID
                team1.HP = 0;

                team2 = new Ship(int.MaxValue - 1, new Vector2D(Size / 2 + 50, 0), new Vector2D(), "Team 2", 0); //check to ensure that players and teams do not have same ID
                team2.HP = 0;
            }
        }

        /// <summary>
        /// Property that gets the size of the world
        /// </summary>
        public int Size { get { return size; } private set { } }

        /// <summary>
        /// Returns of IEnumerable of all ships in the world
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Ship> getShips()
        {
            return ships;
        }

        /// <summary>
        /// Adds a ship to the world
        /// </summary>
        /// <param name="ship">A ship object</param>
        public void AddShip(Ship ship)
        {
            if (setHP > 0) //Change HP to reflect world HP when adding to world
                ship.HP = setHP;

            if (extraContent) //If extra content add on team name (server use only)
            {
                if (ship.IDField % 2 == 0)
                    ship.Name += ", Team 1";
                else
                    ship.Name += ", Team 2";
            }

            ships.Add(ship.IDField, ship);
        }

        /// <summary>
        /// Removes a ship from the world
        /// Returns false if there is no ship object with the specified ID
        /// Otherwise returns true
        /// </summary>
        /// <param name="shipID">The ID of the ship</param>
        /// <returns></returns>
        public bool RemoveShip(int shipID)
        {
            if (!ships.ContainsKey(shipID))
                return false;

            ships.Remove(shipID);
            return true;
        }

        /// <summary>
        /// Adds a projectile to the world
        /// </summary>
        /// <param name="proj">A projectile object</param>
        public void AddProjectile(Projectile proj)
        {
            projectiles.Add(proj.IDField, proj);
        }

        /// <summary>
        /// Removes a projectile from the world
        /// Returns false if there is no projectile with the specified ID
        /// Otherwise returns true
        /// </summary>
        /// <param name="projID">The ID of the projectile</param>
        /// <returns></returns>
        public bool RemoveProjectile(int projID)
        {
            if (!projectiles.ContainsKey(projID))
                return false;

            projectiles.Remove(projID);
            return true;
        }

        /// <summary>
        /// Adds a star object to the world
        /// </summary>
        /// <param name="star"></param>
        public void AddStar(Star star)
        {
            stars.Add(star.IDField, star);
        }

        /// <summary>
        /// Removes a star object from the world
        /// Returns false if there is no star with the specified ID
        /// Otherwise returns true
        /// </summary>
        /// <param name="starID">The ID of the star</param>
        /// <returns></returns>
        public bool RemoveStar(int starID)
        {
            if (!stars.ContainsKey(starID))
                return false;

            stars.Remove(starID);
            return true;
        }

        /// <summary>
        /// Gets a ship based on the ID provided, returns null if no ship with provided ID
        /// </summary>
        /// <param name="ID">The ID of the ship</param>
        /// <returns>The ship of the related ID</returns>
        public Ship getShip(int ID)
        {
            if (!ships.ContainsKey(ID)) //If no ship with ID return null
                return null;

            return ships[ID];
        }

        /// <summary>
        /// Returns of IEnumerable of all projectiles in the world
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Projectile> getProjectiles()
        {
            return projectiles; //Just return all ships in the dictionary
        }

        /// <summary>
        /// Gets a projectile based on the ID provided, returns null if no projectile with provided ID
        /// </summary>
        /// <param name="ID">The ID of the projectile</param>
        /// <returns>The projectile of the related ID</returns>
        public Projectile getProjectile(int ID)
        {
            if (!projectiles.ContainsKey(ID)) //If no projectile with ID return null
                return null;

            return projectiles[ID];
        }

        /// <summary>
        /// Returns of IEnumerable of all stars in the world
        /// </summary>
        public Dictionary<int, Star> getStars()
        {
            return stars; //Just return all stars in the dictionary
        }

        /// <summary>
        /// Gets a star based on the ID provided, returns null if no star with provided ID
        /// </summary>
        /// <param name="ID">The ID of the star</param>
        /// <returns>The star of the related ID</returns>
        public Star getStar(int ID)
        {
            if (!stars.ContainsKey(ID)) //If no star with ID return null
                return null;

            return stars[ID];
        }

        /// <summary>
        /// Marks the player as "removed" from the game world
        /// </summary>
        /// <param name="ID"></param>
        public void RemovePlayer(int ID)
        {
            if (!ships.ContainsKey(ID)) //Do nothing if ID doesn't exist
                return;

            //Set the ship to never respawn
            ships[ID].HP = 0;
            ships[ID].RespawnFrames = -1;
        }

        /// <summary>
        /// Generates a random spawn location as a vector2D, guaranteed to be safe based on how many stars are in the world and
        /// how large the detection for said stars is
        /// </summary>
        /// <returns></returns>
        public Vector2D GenerateRandomSafeSpawn()
        {
            Random r = new Random(); // random number generator for use in respawning ships in random locations

            bool tryAgain = false;

            double safeBounds = starRadiusSize * 1.5; //Make a safe bound for spawn 150% beyond a star's radius

            while (true)
            {
                double spawnX = r.NextDouble() * Size - Size / 2; //Get a random location within the screen
                double spawnY = r.NextDouble() * Size - Size / 2;

                foreach (Star star in stars.Values) //Make sure the spawn is safely away from each star
                {
                    double distance = Math.Sqrt(Math.Pow((star.Location.GetX() - spawnX), 2) +
                            Math.Pow((star.Location.GetY() - spawnY), 2));

                    if (distance <= safeBounds)
                    {
                        tryAgain = true; //If the distance is not beyond the bounds try again
                        break;
                    }
                    else
                        tryAgain = false;

                }

                if (tryAgain)
                    continue;

                return new Vector2D(spawnX, spawnY); //Return the spawn location\
            }
        }

        /// <summary>
        /// Takes in an int representing the PlayerID and a string representing the player commands
        /// and stores it for use for when the world updates
        /// </summary>
        /// <param name="PlayerID">An integer representing the PlayerID</param>
        /// <param name="Commands">A string representing the playercommands</param>
        public void EnterCommands(int PlayerID, String Commands)
        {
            PlayerCommands[PlayerID] = Commands;
        }

        /// <summary>
        /// This method causes each movable object in the world to be translated and rotated based on various world factors
        /// </summary>
        public string ApplyWorldChanges()
        {
            StringBuilder updatedEntities = new StringBuilder();

            updatedEntities.Append(UpdateShipsInWorld()); //Update the ships

            updatedEntities.Append(UpdateProjectilesInWorld()); // Update projectiles

            RemoveDeadProjectiles(); //Remove any projectiles that may have collided/died

            if (extraContent) // if extraContent mode is enabled, then add the team ships to the world accordingly
            {
                updatedEntities.Append(Newtonsoft.Json.JsonConvert.SerializeObject(team1) + "\n");
                updatedEntities.Append(Newtonsoft.Json.JsonConvert.SerializeObject(team2) + "\n");
            }

            return updatedEntities.ToString();
        }

        /// <summary>
        /// Helper method to apply world changes that updates the location of each ship in the world and performs any requested commands
        /// </summary>
        private StringBuilder UpdateShipsInWorld()
        {

            StringBuilder updatedShips = new StringBuilder(); // stringbuilder that stores all the serialized ships for the updatedShips in the world

            lock (this)
            {
                foreach (Ship ship in ships.Values)
                {
                    if (ship.HP <= 0 && ship.RespawnFrames != 0) // If the ship is dead we want to do nothing
                    {
                        ship.RespawnFrames -= 1; //Decrement respawn time

                        if (ship.RespawnFrames == 0) //Once it is time to respawn set health
                        {
                            //Reset the values and spawn randomly
                            ship.HP = setHP;
                            ship.Location = GenerateRandomSafeSpawn();
                            ship.Velocity = new Vector2D(0, 0);
                            ship.FramesBetweenShots = 0;

                            //Generate a random direction for the ship
                            Random dir = new Random();
                            Vector2D direction = new Vector2D(dir.NextDouble() * 2 - 1, dir.NextDouble() * 2 - 1);
                            direction.Normalize();
                            ship.Direction = direction;
                        }

                        updatedShips.Append(Newtonsoft.Json.JsonConvert.SerializeObject(ship) + "\n");
                        continue;
                    }

                    if (ship.FramesBetweenShots != 0) //Decrement time to shoot
                        ship.FramesBetweenShots -= 1;

                    ApplyPlayerCommands(ship); //Apply player commands

                    Vector2D acceleration; // create a local variable that stores the ship acceleration
                    if (ship.Thrust)
                        acceleration = ship.Direction * engineStrength; // if the ship thrust is true, set the acceleration accordingly
                    else
                        acceleration = new Vector2D(0, 0); // if the ship thrust is false, create a new 2d vector that represents zero thrust

                    foreach (Star star in stars.Values)
                    {
                        acceleration += GetStarGravity(star, ship); // calculate the acceleration using the thrust and the gravity of each star
                    }


                    ship.Velocity += acceleration; //Update the velocity and location
                    ship.Location += ship.Velocity;

                    //Solves the wrap around issue if the player flies offscreen

                    //Fixes the right hand side wrap around
                    if (ship.Location.GetX() >= Size / 2)
                        ship.Location = new Vector2D(-Size / 2, ship.Location.GetY());

                    //Fixes the left side wrap around
                    else if (ship.Location.GetX() <= -Size / 2)
                        ship.Location = new Vector2D(Size / 2, ship.Location.GetY());

                    //Fixes the bottom wrap around
                    if (ship.Location.GetY() >= Size / 2)
                        ship.Location = new Vector2D(ship.Location.GetX(), -Size / 2);

                    //Fixes the top side wrap around
                    else if (ship.Location.GetY() <= -Size / 2)
                        ship.Location = new Vector2D(ship.Location.GetX(), Size / 2);

                    CheckShipSunCollision(ship);


                    updatedShips.Append(Newtonsoft.Json.JsonConvert.SerializeObject(ship) + "\n");
                    ship.Thrust = false; //Do not want infinite thrust

                }
            }

            return updatedShips;
        }

        /// <summary>
        /// A method that checks for whether or not a ship currently is within the radius of a sun, and changing the ship if necessary
        /// </summary>
        /// <param name="ship"></param>
        private void CheckShipSunCollision(Ship ship)
        {
            foreach (Star star in stars.Values)
            {
                //Calculate the distance between the star and ship
                int distance = (int)Math.Sqrt(Math.Pow((star.Location.GetX() - ship.Location.GetX()), 2) +
                    Math.Pow((star.Location.GetY() - ship.Location.GetY()), 2));

                if (distance <= starRadiusSize) //check to see whether the distance 
                {
                    ship.HP = 0; //When ship hits sun set HP to 0 and begin respawn timer
                    ship.Velocity = new Vector2D(0, 0);
                    ship.RespawnFrames = respawnFrames;
                }
            }
        }

        /// <summary>
        /// Method that updates where projectiles are in the world and makes them dead if necessary
        /// </summary>
        /// <returns></returns>
        private StringBuilder UpdateProjectilesInWorld()
        {
            StringBuilder updatedProj = new StringBuilder(); // stringbuilder that stores the serialized strings for the updatedProjectiles in the world

            foreach (Projectile proj in projectiles.Values)
            {
                if (proj.Alive)
                {
                    if (!extraContent)
                        proj.Location += (proj.Direction * projectileSpeed); //Simply add the speed multiplied by direction

                    else //Apply gravity when extra content is enabled
                    {
                        Vector2D acceleration = new Vector2D(0, 0);
                        foreach (Star star in stars.Values)
                        {
                            acceleration += GetStarGravity(star, proj); // calculate the acceleration using gravity of each star
                        }

                        proj.Velocity += acceleration;
                        proj.Location += proj.Velocity;
                    }

                    //Mark projectile as dead if it has reached the edge of the screen
                    if (!extraContent && (proj.Location.GetX() >= Size / 2 || proj.Location.GetX() <= -Size / 2 || proj.Location.GetY() >= Size / 2 || proj.Location.GetY() <= -Size / 2))
                        proj.Alive = false;

                    else if (extraContent) // if extra content is enabled, projectile wrap around logic is implemented
                    {
                        //Fixes the right hand side wrap around
                        if (proj.Location.GetX() >= Size / 2)
                            proj.Location = new Vector2D(-Size / 2, proj.Location.GetY());

                        //Fixes the left side wrap around
                        else if (proj.Location.GetX() <= -Size / 2)
                            proj.Location = new Vector2D(Size / 2, proj.Location.GetY());

                        //Fixes the bottom wrap around
                        if (proj.Location.GetY() >= Size / 2)
                            proj.Location = new Vector2D(proj.Location.GetX(), -Size / 2);

                        //Fixes the top side wrap around
                        else if (proj.Location.GetY() <= -Size / 2)
                            proj.Location = new Vector2D(proj.Location.GetX(), Size / 2);
                    }

                    CheckProjectileCollisions(proj);

                    if (extraContent) //This append for proj-on-proj collisions is in the case that a projectile has already been interated over
                        updatedProj.Append(CheckProjOnProjCollisions(proj));
                }

                updatedProj.Append(Newtonsoft.Json.JsonConvert.SerializeObject(proj) + "\n"); //Add this projectile to the string
            }

            return updatedProj;
        }

        /// <summary>
        /// A helper method to check for whether or not a projectile has collided with a star or ship, and applying any necessary changes for collisions
        /// </summary>
        /// <param name="proj"></param>
        private void CheckProjectileCollisions(Projectile proj)
        {
            foreach (Star star in stars.Values)
            {
                //Calculate the distance between the star and proj
                int distance = (int)Math.Sqrt(Math.Pow((star.Location.GetX() - proj.Location.GetX()), 2) +
                    Math.Pow((star.Location.GetY() - proj.Location.GetY()), 2));

                if (distance <= starRadiusSize) //If within star radius, projectile dies
                    proj.Alive = false;
            }

            foreach (Ship ship in ships.Values)
            {
                if (ship.HP <= 0 || ship.IDField == proj.Owner) //No collisions on dead ships or owned projectiles
                    continue;

                else if (extraContent && ship.IDField % 2 == proj.Owner % 2) //If same team for extra content, continue
                    continue;

                //Calculate the distance between the proj and ship
                int distance = (int)Math.Sqrt(Math.Pow((ship.Location.GetX() - proj.Location.GetX()), 2) +
                    Math.Pow((ship.Location.GetY() - proj.Location.GetY()), 2));

                if (distance <= shipRadiusSize) //If within ship radius, projectile dies
                {
                    proj.Alive = false;
                    ship.HP -= 1;

                    if (ship.HP == 0) //Increment score of owner if hit kills
                    {
                        ship.Velocity = new Vector2D(0, 0);
                        ships[proj.Owner].Score += 1;
                        ship.RespawnFrames = respawnFrames;

                        if (extraContent) //Add to team score if team is enabled
                        {
                            if (proj.Owner % 2 == 0)
                                team1.Score += 1;
                            else
                                team2.Score += 1;
                        }
                    }
                }
            }

        }

        private StringBuilder CheckProjOnProjCollisions(Projectile proj)
        {
            foreach (Projectile projCollide in projectiles.Values)
            {
                if (proj.Equals(projCollide)) //Ensure projectiles do not collide with themselves
                    continue;

                //Calculate the distance between the proj and ship
                int distance = (int)Math.Sqrt(Math.Pow((projCollide.Location.GetX() - proj.Location.GetX()), 2) +
                    Math.Pow((projCollide.Location.GetY() - proj.Location.GetY()), 2));

                if (distance <= 10) //If within radius of 10, collision occurs
                {
                    proj.Alive = false;
                    projCollide.Alive = false;

                    return new StringBuilder().Append(Newtonsoft.Json.JsonConvert.SerializeObject(projCollide) + "\n"); //Let it know for the collision
                }
            }

            return new StringBuilder(); //Return empty stringbuilder for no collisions
        }

        /// <summary>
        /// A method that safely removes all dead projectiles from the world without modifying the container while enumerating it
        /// </summary>
        private void RemoveDeadProjectiles()
        {
            HashSet<int> projIDs = new HashSet<int>(projectiles.Keys); //Make a copy of the keys

            foreach (int ID in projIDs) //Enumerate the copy to safely be able to modify the projectile container
            {
                if (!projectiles[ID].Alive)
                    projectiles.Remove(ID);
            }
        }

        /// <summary>
        /// Takes a star object and a ship object and calculates the gravity that the star exerts on the ship
        /// The star gravity is represented as a 2D vector
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        private Vector2D GetStarGravity(Star star, Ship ship)
        {
            Vector2D gravity = star.Location - ship.Location; //Get the direction to the star, normalize, then return the gravity
            gravity.Normalize();
            return gravity * star.Mass;
        }

        /// <summary>
        /// Takes a star object and a projectile object and calculates the gravity that the star exerts on the projectile
        /// The star gravity is represented as a 2D vector
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        private Vector2D GetStarGravity(Star star, Projectile proj)
        {
            Vector2D gravity = star.Location - proj.Location; //Get the direction to the star, normalize, then return the gravity
            gravity.Normalize();
            return gravity * star.Mass;
        }

        /// <summary>
        /// A method that applies any command that a player has requested onto a frame
        /// </summary>
        /// <param name="ship">The players ship</param>
        private void ApplyPlayerCommands(Ship ship)
        {
            if (!PlayerCommands.ContainsKey(ship.IDField) || PlayerCommands[ship.IDField].Equals("()\n"))
                return;

            String command = PlayerCommands[ship.IDField];

            if (command.Contains('L')) //Turn left
            {
                ship.Direction.Rotate(-1 * turnRateDegrees);
            }
            else if (command.Contains('R')) //Turn right
                ship.Direction.Rotate(turnRateDegrees);

            if (command.Contains('T')) //Let the ship know to thrust
                ship.Thrust = true;

            if (command.Contains('F') && ship.FramesBetweenShots == 0) //Shoot if possible
            {
                ship.FramesBetweenShots = framesBetweenShots;
                Projectile p = new Projectile(currentProjID++, new Vector2D(ship.Location), new Vector2D(ship.Direction), ship.IDField);

                if (extraContent) //If extra content is enabled, set the velocity to the initial speed
                    p.Velocity = p.Direction * projectileSpeed;

                AddProjectile(p);
            }

            PlayerCommands[ship.IDField] = ""; // Once we have updated the player commands, clear the player commands
        }
    }
}
