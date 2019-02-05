using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    /// <summary>
    /// This class represents a series of agnostic static methods for creating a connection, sending, and receiving data
    /// </summary>
    public static class NetworkController
    {
        /// <summary>
        /// The port number that will be used for any connection made by this class
        /// </summary>
        private const int PORT_NUM = 11000;

        /// <summary>
        /// Takes in a callMe delegate, and starts a loop for connecting clients on TCP, invoking the callMe upon connection to a client
        /// </summary>
        /// <param name="callMe">An OnReceiveCall delegate to Invoke post-connection</param>
        public static void ServerAwaitingClientLoop(OnReceiveCall callMe)
        {
            TcpListener connectionListen = new TcpListener(IPAddress.Any, PORT_NUM); //Create a new listener for incoming connections
            connectionListen.Start();

            ServerSocketState serverConnector = new ServerSocketState(connectionListen, callMe); //Create a new ServerSocketState with the listener and the callMe

            connectionListen.BeginAcceptSocket(AcceptNewClient, serverConnector); //Start accepting clients

        }

        /// <summary>
        /// The callback method upon connection to a client, that invokes the callMe stored in the state and begins listening for any new incoming connections
        /// </summary>
        /// <param name="stateAsArObject">The ServerSocketState, stored in the stateAsArObject</param>
        private static void AcceptNewClient(IAsyncResult stateAsArObject)
        {
            ServerSocketState serverState = (ServerSocketState)stateAsArObject.AsyncState; //Get the server state
            Socket clientSocket = serverState.ConnectionListener.EndAcceptSocket(stateAsArObject); //Get the socket we are connected on

            serverState.CallMe(new SocketState(clientSocket)); //Invoke the call me with the new socket state for the client

            serverState.ConnectionListener.BeginAcceptSocket(AcceptNewClient, serverState); //Start accepting new clients
        }

        /// <summary>
        /// Attempts to connect to a server given an IP as a string, and a delegate to invoke upon connection. Returns the socket made by connection attempt
        /// </summary>
        /// <param name="callMe">A delegate to invoke upon connection to the server</param>
        /// <param name="hostname">The url/numeric IP address to connect to</param>
        /// <returns></returns>
        public static Socket ConnectToServer(OnReceiveCall callMe, string hostname)
        {
            //IP address and socket variables
            IPAddress addr;
            Socket servSocket;

            //Make the socket and IP given the hostname
            MakeSocket(hostname, out servSocket, out addr);

            //Create the socket state to pass between server and client
            SocketState servState = new SocketState(servSocket, callMe);

            // Try to begin connection with the server
            servState.SocketProp.BeginConnect(addr, PORT_NUM, ConnectedCallback, servState);

            //Return the socket
            return servSocket;
        }

        /// <summary>
        /// A method that is invoked upon a connection being established between both the client and server,
        /// and begins to receive byte data
        /// </summary>
        /// <param name="stateAsAreObject">Contains a SocketState object</param>
        private static void ConnectedCallback(IAsyncResult stateAsAreObject)
        {
            //Convert the AR to a socket state, end connection
            SocketState state = (SocketState)stateAsAreObject.AsyncState;
            try
            {
                state.SocketProp.EndConnect(stateAsAreObject);
                state.IsConnected = true;
            }
            catch (Exception)
            {
                state.IsConnected = false; //Set to false so the callMe can know that the connection failed
            }

            state.CallMe(state);
        }

        /// <summary>
        /// Helper function that the client code calls whenever it wants more data
        /// </summary>
        /// <param name="state"></param>
        public static void GetData(SocketState state)
        {
            try
            {
                state.SocketProp.BeginReceive(state.MessageData, 0, state.MessageData.Length, SocketFlags.None, ReceiveCallback, state);
            }
            catch (Exception)
            {
                state.IsConnected = false;
                state.CallMe(state); //Call the call me with IsConnected now false
            }
        }

        /// <summary>
        /// The callback for receiving data, ends the receive and then handles the data received
        /// </summary>
        /// <param name="stateAsArObject"></param>
        private static void ReceiveCallback(IAsyncResult stateAsArObject)
        {
            SocketState theSocketState = (SocketState)stateAsArObject.AsyncState;
            int bytesRead = 0;

            try
            {
                bytesRead = theSocketState.SocketProp.EndReceive(stateAsArObject);
            }
            catch (Exception)
            {
                theSocketState.IsConnected = false;
            }

            // If the socket is still open
            if (bytesRead > 0)
            {
                string theMessage = Encoding.UTF8.GetString(theSocketState.MessageData, 0, bytesRead);
                // Append the received data to the growable buffer.
                // It may be an incomplete message, so we need to start building it up piece by piece
                theSocketState.TranslatedData.Append(theMessage);
            }
            theSocketState.CallMe(theSocketState);
        }

        /// <summary>
        /// Sends a string of data through the socket by converting it to byte data
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        public static void Send(Socket socket, String data)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(data);

            try
            {
                socket.BeginSend(messageBytes, 0, messageBytes.Length, SocketFlags.None, SendCallback, socket);
            }

            catch (Exception)
            {
            }
        }

        /// <summary>
        /// The callback for sending data that just ends the send
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            Socket theSocket = (Socket)ar.AsyncState;
            try
            {
                theSocket.EndSend(ar);
            }

            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Creates a Socket object for the given host string
        /// </summary>
        /// <param name="hostName">The host name or IP address</param>
        /// <param name="socket">The created Socket</param>
        /// <param name="ipAddress">The created IPAddress</param>
        public static void MakeSocket(string hostName, out Socket socket, out IPAddress ipAddress)
        {
            ipAddress = IPAddress.None;
            socket = null;
            try
            {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo;

                // Determine if the server address is a URL or an IP
                try
                {
                    ipHostInfo = Dns.GetHostEntry(hostName);
                    bool foundIPV4 = false;
                    foreach (IPAddress addr in ipHostInfo.AddressList)
                        if (addr.AddressFamily != AddressFamily.InterNetworkV6)
                        {
                            foundIPV4 = true;
                            ipAddress = addr;
                            break;
                        }
                    // Didn't find any IPV4 addresses
                    if (!foundIPV4)
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid addres: " + hostName);
                        throw new ArgumentException("Invalid address");
                    }
                }
                catch (Exception)
                {
                    // see if host name is actually an ipaddress, i.e., 155.99.123.456
                    System.Diagnostics.Debug.WriteLine("using IP");
                    ipAddress = IPAddress.Parse(hostName);
                }

                // Create a TCP/IP socket.
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);

                // Disable Nagle's algorithm - can speed things up for tiny messages, 
                // such as for a game
                socket.NoDelay = true;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create socket. Error occured: " + e);
                throw new ArgumentException("Invalid address");
            }
        }
    }
}
