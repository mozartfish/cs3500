using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    /// <summary>
    /// A class containing necessary state data for a socket to be passed back and forth,
    /// such as the socket it is using and the message data as a byte array
    /// </summary>
    class ServerSocketState
    {

        /// <summary>
        /// The tcp listener for incoming connections
        /// </summary>
        public TcpListener ConnectionListener { get; set; }

        /// <summary>
        /// The delegate that may be invoked by any class using this object for doing some work on this (or some other) socket state object
        /// </summary>
        public OnReceiveCall CallMe { get; set; }

        /// <summary>
        /// Creates a new socket state with a socket input parameter, and the callMe delegate
        /// </summary>
        /// <param name="servSocket"></param>
        public ServerSocketState(TcpListener tcpListener, OnReceiveCall callMe)
        {
            //Set the two values for the state
            CallMe = callMe;
            ConnectionListener = tcpListener;
        }

    }
}
