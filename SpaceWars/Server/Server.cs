using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SpaceWars;
using Controllers;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
    /// <summary>
    /// This class represents a server for running a spacewars game
    /// </summary>
    class Server
    {
        /// <summary>
        /// The world object that contains all entities and computes logic for the game
        /// </summary>
        private World theWorld;

        /// <summary>
        /// The rate at which the game server should send any changes, in milliseconds
        /// </summary>
        private int frameRate;

        /// <summary>
        /// The list of connections this server has to clients
        /// </summary>
        private List<SocketState> clientConnections;

        /// <summary>
        /// The next available ID for when a client connects
        /// </summary>
        private int nextClientID;

        /// <summary>
        /// A stringbuilder containing all data to send to the clients on an update
        /// </summary>
        private string updatedData;

        /// <summary>
        /// Creates the server and begins awaiting new clients
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Create an instance of the server in main for use
            Server theServer = new Server();
            Console.WriteLine("Server up and awaiting clients");

            NetworkController.ServerAwaitingClientLoop(theServer.HandleNewConnection); //Start client connection loop with the new client callMe
            theServer.StartEventLoop();
            Console.Read();
        }

        /// <summary>
        /// Default constructor for the server
        /// </summary>
        public Server()
        {
            try
            {
                ProcessXmlSettings(); //Simply processes the settings file
                clientConnections = new List<SocketState>();
                updatedData = "";
                nextClientID = 0;
            }
            catch (Exception e) //Catch any exceptions while reading
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// A method to be invoked upon a new connection between client and server
        /// </summary>
        /// <param name="newClient"></param>
        public void HandleNewConnection(SocketState newClient)
        {
            
            newClient.CallMe = OnClientNameReceive; //Set the callMe to receive the name
            newClient.IsConnected = true;
            NetworkController.GetData(newClient); //Await data from the client
        }

        /// <summary>
        /// A method to be invoked on data receival from a client, this establishes the name of the client
        /// </summary>
        /// <param name="clientState"></param>
        private void OnClientNameReceive(SocketState clientState)
        {
            if (!clientState.IsConnected) //Return if no connection
                return;

            string totalData = clientState.TranslatedData.ToString();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");
            string name = "";

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

                name = p.Substring(0, p.Length - 1); //Get the name
                clientState.TranslatedData.Remove(0, p.Length); //Remove the data
                break;
            }

            int ID = nextClientID++; //The ID will be the next available one
            clientState.SocketID = ID;
            lock (theWorld)
            {
                //Add a ship for the client to the world
                Random dir = new Random();
                Vector2D direction = new Vector2D(dir.NextDouble() * 2 - 1, dir.NextDouble() * 2 - 1);
                direction.Normalize();
                theWorld.AddShip(new Ship(ID, theWorld.GenerateRandomSafeSpawn(), direction, name, 0)); //Add the ship to the world
            }

            Console.WriteLine("Connection finalized for client ID: " + ID);
            string IDAndWorldSize = ID + "\n" + theWorld.Size + "\n"; //Make a string to send the ID and world size
            NetworkController.Send(clientState.SocketProp, IDAndWorldSize); //Send the data

            clientConnections.Add(clientState); //Add the client to the list of connections

            clientState.CallMe = OnClientCommandReceive; //Set the callMe to now accept client commands
            NetworkController.GetData(clientState);

            SendStars(clientState); //Send the stars to the client
        }

        /// <summary>
        /// A helper method that sends the stars after establishing a world to the client, since
        /// </summary>
        /// <param name="clientState"></param>
        private void SendStars(SocketState clientState)
        {
            StringBuilder stars = new StringBuilder();
            foreach (Star star in theWorld.getStars().Values) //Serialize each star and send them out
                stars.Append(JsonConvert.SerializeObject(star) + "\n");

            NetworkController.Send(clientState.SocketProp, stars.ToString()); //Send the data
        }

        /// <summary>
        /// A method to be invoked when commands are received from a client's socket, which will turn into changes within the world
        /// </summary>
        /// <param name="clientState"></param>
        private void OnClientCommandReceive(SocketState clientState)
        {
            if (!clientState.IsConnected) //Return if no connection
                return;

            string totalData = ""; //Default to empty string to prevent null object exception
            totalData += clientState.TranslatedData.ToString();
            theWorld.EnterCommands(clientState.SocketID, totalData);

            clientState.TranslatedData.Remove(0, totalData.Length); //Remove the data we receive
            NetworkController.GetData(clientState);
        }

        /// <summary>
        /// Starts an event loop that infinitely runs until close of the program,
        /// on which the game will send updates every frame
        /// </summary>
        public void StartEventLoop()
        {
            Thread thread = new Thread(EventLoopWorker);
            thread.Start();
            thread.Join();
        }

        /// <summary>
        /// A private helper method that runs the event loop on a separate thread, and is in charge of invoking methods to send data to the clients
        /// </summary>
        private void EventLoopWorker()
        {
                Stopwatch watch = new Stopwatch();
                while (true) //Set up an infinite loop
                {
                    watch.Start();
                    while (watch.ElapsedMilliseconds < frameRate) {} //For each frame

                    watch.Reset();
                    updatedData = theWorld.ApplyWorldChanges();
                    SendUpdates();  //Update each client
                }
        }

        /// <summary>
        /// Sends updates of any changes out to all of the clients
        /// </summary>
        private void SendUpdates()
        {

            foreach (SocketState client in new List<SocketState>(clientConnections)) //Send to each client
            {
                if (!client.SocketProp.Connected) //Gracefully handle a disconnect
                {
                    client.IsConnected = false;
                    clientConnections.Remove(client);
                    theWorld.RemovePlayer(client.SocketID);
                    Console.WriteLine("Client disconnected, connection removed for socket No. " + client.SocketID);
                }

                NetworkController.Send(client.SocketProp, updatedData.ToString()); //Send out the updated data
            }

            updatedData = "";
        }

        /// <summary>
        /// This helper method processes the settings file that should be contained in a Resources project to set the proper defaults for the game
        /// </summary>
        private void ProcessXmlSettings()
        {
            //These values are all game rules which must be passed to the world upon creation, but are read out of the settings.xml file
            //They are initially set to defaults, and changed if provided in an xml file
            int size = 750;
            int setHP = 5;
            int projectileSpeed = 15;
            double turnRateDegrees = 2;
            double engineStrength = 0.08;
            int shipCircleSize = 20;
            int starCircleSize = 35;
            frameRate = 16;
            int framesBetweenShots = 6;
            int respawnFrames = 300;
            bool extraContent = false;

            List<Star> settingsStars = new List<Star>(); //The list of stars that may be in the settings file


            using (XmlReader settings = XmlTextReader.Create("..\\..\\..\\Resources\\settings.xml")) //Create a reader from the settings file in resources
            {
                settings.MoveToContent(); //Move to first element and check for right head
                if (!settings.Name.Equals("SpaceSettings"))
                    throw new FormatException("Error: starting element of file not SpaceSettings");

                while (settings.Read()) //Read all nodes in the file
                {
                    if (settings.IsStartElement()) //Skip file header, whitespace, and end elements
                        switch (settings.Name)
                        {
                            case "UniverseSize":
                                settings.Read();
                                size = int.Parse(settings.Value); //Parse the size, must be greater than 0
                                if (size <= 0)
                                    throw new FormatException("Error: <= 0 size invalid in settings");
                                break;

                            case "MSPerFrame":
                                settings.Read();
                                frameRate = int.Parse(settings.Value); //Parse the frame rate, must be greater than 0
                                if (frameRate <= 0)
                                    throw new FormatException("Error: <= 0 MSPerFrame invalid in settings");
                                break;

                            case "FramesPerShot":
                                settings.Read();
                                framesBetweenShots = int.Parse(settings.Value); //Parse the frames between shots, must be greater than 0
                                if (framesBetweenShots <= 0)
                                    throw new FormatException("Error: <= 0 FramesPerShot invalid in settings");
                                break;

                            case "RespawnRate":
                                settings.Read();
                                respawnFrames = int.Parse(settings.Value); //Parse the respawn frames, must be greater than 0
                                if (respawnFrames <= 0)
                                    throw new FormatException("Error: <= 0 RespawnRate invalid in settings");
                                break;

                            case "ShipHP":
                                settings.Read();
                                setHP = int.Parse(settings.Value); //Parse the Ship HP value, must be greater than 0
                                if (setHP <= 0)
                                    throw new FormatException("Error: <= 0 ShipHP invalid in settings");
                                break;

                            case "ProjectileVelocity":
                                settings.Read();
                                projectileSpeed = int.Parse(settings.Value); //Parse the projectile value (can be any value)
                                break;

                            case "EngineStrength":
                                settings.Read();
                                engineStrength = double.Parse(settings.Value); //Parse the engine rate, must be non-negative
                                if (engineStrength < 0)
                                    throw new FormatException("Error: < 0 EngineStrength invalid in settings");
                                break;

                            case "TurningRate":
                                settings.Read();
                                turnRateDegrees = double.Parse(settings.Value); //Parse the turning rate rate, can be any value
                                break;

                            case "ShipRadius":
                                settings.Read();
                                shipCircleSize = int.Parse(settings.Value); //Parse the ship detection radius, must be greater than 0
                                if (shipCircleSize <= 0)
                                    throw new FormatException("Error: <= 0 ShipRadius invalid in settings");
                                break;

                            case "StarRadius":
                                settings.Read();
                                starCircleSize = int.Parse(settings.Value); //Parse the star detection radius, must be greater than 0
                                if (starCircleSize <= 0)
                                    throw new FormatException("Error: <= 0 StarRadius invalid in settings");
                                break;

                            case "ExtraContent":
                                settings.Read();
                                extraContent = settings.Value.Equals("enabled") ? true : false;
                                break;

                            case "Star":
                                settings.Read();
                                settingsStars.Add(GetStarFromXml(settings, settingsStars.Count)); //Get the star from the xml settings and add to the list
                                break;

                            default: //Any other node is not valid
                                throw new Exception("Error: Xml node " + settings.Name +
                                    " not valid for server settings");
                        }
                }

                theWorld = new World(size, setHP, projectileSpeed, turnRateDegrees, engineStrength, shipCircleSize, 
                    starCircleSize, framesBetweenShots, respawnFrames, extraContent); //Create the world with the rules

                foreach (Star star in settingsStars) //Add every established star in the xml file
                    theWorld.AddStar(star);
            }
        }

        /// <summary>
        /// A helper method when reading the settings.xml file, which reads the nodes within a star node to convert it into a star
        /// </summary>
        /// <returns>A star with defaults, or values established in the xml file</returns>
        private Star GetStarFromXml(XmlReader settings, int starID)
        {
            //The three values that define a star's location and mass, initially set to defaults
            int x = 0;
            int y = 0;
            double mass = 0.01;

            while (!settings.Name.Equals("Star")) //Check if we have reached end element for a star
            {
                if (settings.IsStartElement()) //Skip whitespace and end elements
                    switch (settings.Name)
                    {
                        case "x":
                            settings.Read(); //Move to next node
                            x = int.Parse(settings.Value); //Assign x value
                            settings.Read(); //Move to the next start element
                            settings.Read();
                            break;

                        case "y":
                            settings.Read(); //Move to next node
                            y = int.Parse(settings.Value); //Assign y value
                            settings.Read(); //Move to the next start element
                            settings.Read();
                            break;

                        case "mass":
                            settings.Read(); //Move to next node
                            mass = double.Parse(settings.Value); //Assign mass
                            settings.Read(); //Move to the next start element
                            settings.Read();
                            break;

                        default: //If node is not a name or contents throw an exception
                            throw new FormatException("Error: Node " + settings.Name + " not valid node within a Star");
                    }
            }

            return new Star(starID, new Vector2D(x, y), mass); //Create a star with the established values and ID
        }
    }
}

