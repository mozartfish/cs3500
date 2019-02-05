using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Controllers
{
    /// <summary>
    /// A delegate representing some method that should be invoked by the network controller on receiving data
    /// </summary>
    /// <param name="state">The socket state that the method invoked should have access to via parameter</param>
    public delegate void OnReceiveCall(SocketState state);

    /// <summary>
    /// A class containing necessary state data for a socket to be passed back and forth,
    /// such as the socket it is using and the message data as a byte array
    /// </summary>
    public class SocketState
    {
        /// <summary>
        /// The socket that should be connected to the server
        /// </summary>
        public Socket SocketProp { get; set; }

        /// <summary>
        /// A bool representing if the stored socket in this has an established connection
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// The array containing byte data on the messages received (4kB size)
        /// </summary>
        public byte[] MessageData { get; set; }

        /// <summary>
        /// The delegate that may be invoked by any class using this object for doing some work on this (or some other) socket state object
        /// </summary>
        public OnReceiveCall CallMe { get; set; }

        /// <summary>
        /// The StringBuilder that holds all byte data translated to strings
        /// </summary>
        public StringBuilder TranslatedData { get; private set; }

        /// <summary>
        /// The ID field of this socket ( of server use only)
        /// </summary>
        public int SocketID { get; set; }

        /// <summary>
        /// Creates a new socket state with a socket input parameter, and the callMe delegate
        /// </summary>
        /// <param name="servSocket"></param>
        public SocketState(Socket servSocket, OnReceiveCall callMe)
        {
            CallMe = callMe;
            SocketProp = servSocket;
            MessageData = new byte[4096];
            TranslatedData = new StringBuilder();
            IsConnected = false;
        }

        /// <summary>
        /// Creates a new socket state with a socket input parameter (Server use only)
        /// </summary>
        /// <param name="servSocket"></param>
        public SocketState(Socket servSocket)
        {
            SocketProp = servSocket;
            IsConnected = false;
            MessageData = new byte[4096];
            TranslatedData = new StringBuilder();
        }
    }
}
