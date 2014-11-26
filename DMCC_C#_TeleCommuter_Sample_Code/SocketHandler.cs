/*****************************************************************************
 * Title: SocketHandler.cs
 * Author: Avaya
 * Copyright: Copyright 2007 AVAYA, Inc.
 *****************************************************************************
/* Purpose: SocketHandler class creates TCP socket and attaches stream Reader and Writer to it. 
 * This reader and writer will be used to read response data and write request data respectively. The 
 * openSocket function gets called in the XMLParser class when the application starts.
 */

using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace TeleCommuter
{

    public class SocketHandler
    {
        /* The variables declared below maintain the TCP connection and its 
         * associated socket reader & writer
         * */

        private NetworkStream nwstream;
        public NetworkStream NWStream
        {
            get { return nwstream; }
        }

        private TcpClient tcpclient = null;
        public TcpClient TcpClient
        {
            get { return tcpclient; }
        }

        /* Secure stream for secure socket implementation. */
        public SslStream sslStream = null;
        public SslStream SslStream
        {
            get { return sslStream; }
        }

        private BinaryWriter socketWriter;
        public BinaryWriter SocketWriter
        {
            get { return socketWriter; }
        }

        private BinaryReader socketReader;
        public BinaryReader SocketReader
        {
            get { return socketReader; }
        }

        /* Specify the callback function that will act as the validation delegate. 
         * This lets the user inspect the certificate to see if it meets the specific
         * validation requirements.
         */
        public static bool ValidateServerCertificate(object sender,
                           X509Certificate certificate,
                           X509Chain chain,
                           SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
                return true;
            return false;
        } 
        
         /* The openSocket function gets invoked in the XMLParser class for the 
          * StartApplicationSession function. It creates a TCPClient, gets the network stream 
          * from it and initializes the reader and writer. If during this operation any
          * exception is encountered, it prints the exception on the console.
          */
        public bool openSocket(string server_ip, int server_port, bool isSecure)
        {
            try
            {
                tcpclient = new TcpClient(server_ip, server_port);

                if (tcpclient == null)
                {
                    Console.WriteLine("Unable to connect to callserver ip: " + server_ip + " port: " + server_port);
                    return false;
                }

                nwstream = tcpclient.GetStream();

                if (isSecure)
                {
                    sslStream = new SslStream(
                        nwstream,
                        false,
                        new RemoteCertificateValidationCallback(ValidateServerCertificate),
                        null
                    );
                    
                    if (sslStream == null)
                    {
                        Console.WriteLine("Unable to connect to callserver ip: " + server_ip + " port: " + server_port);
                        return false;
                    }

                    /* If authentication fails, AuthenticateAsClient throws AuthenticationException */
                    sslStream.AuthenticateAsClient(server_ip);
                    
                    /* Initializes the reader and writer.*/
                    socketWriter = new BinaryWriter(sslStream);
                    socketReader = new BinaryReader(sslStream);
                }
                else
                {
                    socketWriter = new BinaryWriter(nwstream);
                    socketReader = new BinaryReader(nwstream); 
                    
                }
                return true;
            }
            catch (AuthenticationException e)
            {
                /* This exception indicates authentication has failed. Display the reason for failure */
                Console.WriteLine("Authentication failed");
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during open socket: {0}", ex.Message);
                return false;
            }
        }

        /* The closeSocket function gets invoked in the XMLParser class for the 
         * StopApplicationSession function. It creates a TCPClient, gets the network stream 
         * from it and initializes the reader and writer. If during this operation any
         * exception is encountered, it prints the exception on the console.
         */
        public bool closeSocket()
        {
            try
            {
                /* Closing the reader and writer.*/
                if (socketReader != null)
                    socketReader.Close();
                socketReader = null;

                if (socketWriter != null)
                    socketWriter.Close();
                socketWriter = null;

                /* Closing the network stream.*/
                if (nwstream != null)
                    nwstream.Close();
                nwstream = null;

                /* Closing the secure layer stream.*/
                if (sslStream != null)
                    sslStream.Close();
                sslStream = null;

                /* Closing the TCP client connection.*/
                if (tcpclient != null)
                    tcpclient.Close();
                tcpclient = null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during close socket: {0}", ex.Message);
                return false;
            }
        }
    }
}