using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using SpaceWars;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Timers;
using System.Text;

namespace Controllers
{
    /// <summary>
    /// This class represents an intermediate controller between the view, model, and network connection
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// The ID of the player of this controller, sent by the server
        /// </summary>
        private int ID;

        /// <summary>
        /// The personalized name of the user of this controller
        /// </summary>
        private string username;

        /// <summary>
        /// The socket that the controllers will use to connect to the server
        /// </summary>
        private Socket theSocket;

        /// <summary>
        /// A bool representing a right turn key being pressed
        /// </summary>
        public bool KeyRight { get; set; }

        /// <summary>
        /// A bool representing a left turn key being pressed
        /// </summary>
        public bool KeyLeft { get; set; }

        /// <summary>
        /// A bool representing a thrust key being pressed
        /// </summary>
        public bool KeyThrust { get; set; }

        /// <summary>
        /// A bool representing a fire key being pressed
        /// </summary>
        public bool KeyFire { get; set; }

        /// <summary>
        /// The game world, containing all entities and the size of the game world
        /// </summary>
        public World GameWorld { get; private set; }

        /// <summary>
        /// The delegate for handling all data that has been processed
        /// </summary>
        public delegate void DataProcessedHandler();

        /// <summary>
        /// The event that will invoke all methods registered to this event
        /// </summary>
        public event DataProcessedHandler DataProcessed;

        /// <summary>
        /// A delegate that is invoked when connection is lost to a server
        /// </summary>
        public delegate void ConnectionLostHandler();

        /// <summary>
        /// An event that fires when connection is lost to a server
        /// </summary>
        public event ConnectionLostHandler ConnectionLost;

        /// <summary>
        /// Creates an instance of a game controller, with a username and connects to a server with the given
        /// address, throws an illegal argument exception if the server address is incorrect
        /// </summary>
        public GameController(string username, string serverAddr)
        {
            //Try to connect to the server
            theSocket = NetworkController.ConnectToServer(SendNameOnConnect, serverAddr);
            this.username = username;

            //Set defaults for pre-connection, to later be changed upon connection
            ID = -1;
            GameWorld = null;

            //Set all flags to false
            KeyRight = false;
            KeyLeft = false;
            KeyThrust = false;
            KeyFire = false;
        }

        /// <summary>
        /// Sends user input to a server based on the key flag properties of this
        /// </summary>
        private void SendUserInput()
        {
            StringBuilder inputs = new StringBuilder();

            //Append the commands based on the key flags
            if (KeyLeft)
                inputs.Append('L');
            if (KeyRight)
                inputs.Append('R');
            if (KeyThrust)
                inputs.Append('T');
            if (KeyFire)
                inputs.Append('F');

            if (KeyLeft || KeyRight || KeyFire || KeyThrust)
                NetworkController.Send(theSocket, "(" + inputs.ToString() + ")\n"); //Simply send the data using the network controller and with the correct protocol
        }

        /// <summary>
        /// The method that should be invoked on connection to the server, which will send the player's name to the server and await a response
        /// </summary>
        /// <param name="state">The socket state to pass between client and server</param>
        private void SendNameOnConnect(SocketState state)
        {
            NetworkController.Send(state.SocketProp, username + "\n");
            state.CallMe = ReceiveIDandWorldSize;
            NetworkController.GetData(state);
        }

        /// <summary>
        /// The method to be invoked on the first receiving of data, to set the player ID and create the world size, if both are not received at same time continues
        /// to use this method to receive data, then changes once the ID and world have been established
        /// </summary>
        /// <param name="socketState">The object holding all necessary data for the server-client communication</param>
        private void ReceiveIDandWorldSize(SocketState socketState)
        {
            if (!socketState.IsConnected) //If no connection invoke connection lost handle and return
            {
                ConnectionLost();
                return;
            }

            string totalData = socketState.TranslatedData.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            // Loop until we have processed all messages.
            // We may have received more than one.

            foreach (string p in parts)
            {
                // Ignore empty strings added by the regex splitter
                if (p.Length == 0)
                    continue;
                // The regex splitter will include the last string even if it doesn't end with a '\n',
                // So we need to ignore it if this happens. 
                if (p[p.Length - 1] != '\n')
                    break;

                if (ID == -1) //If ID not set, try to set it
                {
                    if (!int.TryParse(p, out ID))
                        throw new ArgumentException("Server sent bad player ID");
                }
                else if (GameWorld == null) //If world not set then create a new world
                {
                    if (!int.TryParse(p, out int size))
                        throw new ArgumentException("Server sent bad world size");
                    GameWorld = new World(size);
                }
                else
                {
                    //INVOKE SOME DELEGATE FROM THE VIEW TO LET IT KNOW TO DRAW THE PANEL
                    socketState.CallMe = GameDataProcessor;
                    break;
                }

                // Then remove it from the SocketState's growable buffer
                socketState.TranslatedData.Remove(0, p.Length);
            }


            if (GameWorld != null && DataProcessed != null) //When both ID and game world are established, send event that data has been processed
                DataProcessed();

            NetworkController.GetData(socketState); //Ask for more data

        }

        /// <summary>
        /// Processes all data stored within the socket state's stringbuilder, and uses it to update the game
        /// </summary>
        /// <param name="state">The object holding all necessary data for the server-client communication</param>
        private void GameDataProcessor(SocketState state)
        {
            if (!state.IsConnected) //If no connection invoke connection lost handle and return
            {
                ConnectionLost();
                return;
            }

            //Split the data into individual messages
            string totalData = state.TranslatedData.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            List<Object> newEntities = new List<object>();

            // Loop until we have processed all messages.
            // We may have received more than one.

            foreach (string p in parts)
            {
                // Ignore empty strings added by the regex splitter
                if (p.Length == 0)
                    continue;
                // The regex splitter will include the last string even if it doesn't end with a '\n',
                // So we need to ignore it if this happens. 
                if (p[p.Length - 1] != '\n')
                    break;

                //Convert the string json into an object
                String jsonObj = p.Substring(0, p.Length - 1);
                JObject obj = JObject.Parse(jsonObj);

                //Check which type it should be based on field inquiries
                if (obj["thrust"] != null)
                {
                    Ship jShip = JsonConvert.DeserializeObject<Ship>(jsonObj); //Deserialize and add it to the list
                    newEntities.Add(jShip);
                }
                else if (obj["alive"] != null)
                {
                    Projectile jProjectile = JsonConvert.DeserializeObject<Projectile>(jsonObj); //Deserialize and add it to the list
                    newEntities.Add(jProjectile);
                }
                else
                {
                    Star jStar = JsonConvert.DeserializeObject<Star>(jsonObj); //Deserialize and add it to the list
                    newEntities.Add(jStar);
                }

                // Then remove it from the SocketState's growable buffer
                state.TranslatedData.Remove(0, p.Length);
            }

            UpdateWorldWithEntities(newEntities); //Update the world

            DataProcessed(); //Invoke the event to tell all subscribers that data has been processed

            SendUserInput(); //Send the user input when data is processed to somewhat sync with server frame rate

            NetworkController.GetData(state); //Ask for more data
        }

        /// <summary>
        /// A helper method called when new data is finished processing, that will update any changed entities contained within the world using
        /// the provided IEnumerable that contains entities as Objects
        /// </summary>
        /// <param name="newEntities"></param>
        private void UpdateWorldWithEntities(IEnumerable<Object> newEntities)
        {
            lock (GameWorld) //Lock the code since we are modifying a container and don't want to cause issues
            {
                foreach (Object entity in newEntities)
                {
                    if (entity is Ship)
                    {
                        Ship ship = entity as Ship;
                        GameWorld.getShips()[ship.IDField] = ship;
                    }
                    else if (entity is Projectile)
                    {
                        Projectile proj = entity as Projectile;
                        GameWorld.RemoveProjectile(proj.IDField); //Remove regardless of being alive or dead

                        if (proj.Alive) //Add the updated projectile back if it is alive
                            GameWorld.getProjectiles()[proj.IDField] = proj;
                    }
                    else
                    {
                        Star star = entity as Star;
                        GameWorld.getStars()[star.IDField] = star;
                    }
                }
            }
        }
    }
}
