/*****************************************************************************
 * Title: XMLHandler.cs
 * Author: Avaya
 * Copyright: Copyright 2007 AVAYA, Inc.
 *****************************************************************************
 * Purpose: This class provides the functionality for sending and reading the XML messages
 * between the application and the AE Services server. The readXMLMessage function reads the XML
 * messages from the AE Services server. The contents of it i.e. the version, length, invokeID and
 * payload are seperated here. The sendRequest function send the XML message over the
 * connection. It constructs the message in a particular format and sends over the 
 * writer stream.
 */

using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Net.Security;

namespace TeleCommuter
{
    /* This class provides the wrapper functions for XML APIs and also sends/receives XML messages
    * to/from AE Services server.
    */
    class XMLHandler
    {
        private TcpClient client;
        private BinaryWriter writer;
        private BinaryReader reader;

        private const int XML_HEADER_LEN = 8;
        private const int INVOKE_ID_LEN = 4;
        private const int VERSION_LEN = 2;
        private int InvokeID = 1;   // Holds the unique Invoke ID for each XML request

        /* Sets the client, writer and reader variables of XMLHandler class with the respective SocketHandler class variables*/
        public void setParameters(SocketHandler socket_handle)
        {
            client = socket_handle.TcpClient;
            writer = socket_handle.SocketWriter;
            reader = socket_handle.SocketReader;
        }

        /* Utility function to get an unique invoke ID */
        public string getInvokeID()
        {
            if (InvokeID == 9998) InvokeID = 1;
            string format = InvokeID.ToString().PadLeft(INVOKE_ID_LEN, '0');

            InvokeID++;
            return format;
        }

        /* This function converts the character string into a byte array*/
        public static byte[] ToByteArray(string sourcestring)
        {
            byte[] byteArray = new byte[sourcestring.Length];
            for (int index = 0; index < sourcestring.Length; index++)
                byteArray[index] = (byte)sourcestring[index];
            return byteArray;
        }

        /* Function to send request to AE Services server */
        public void sendRequest(string request)
        {
            /*
             * According to CSTA-ECMA 323 Standard ANNEX G
             * Section G.2.  CSTA XML without SOAP
             * 
             * The Header is  8 bytes long.
             * | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |   
             * |VERSION|LENGTH |   INVOKE ID   |   XML PAYLOAD
             * 
             * VERSION: 2 bytes
             * LENGTH: 2 bytes information that contains the total size 
             * (XML payload + Header)
             * INVOKE ID: 4 bytes.  
             * 
             */

            if (writer == null)
                return;
            try
            {
                // HEADER : VERSION
                short version = IPAddress.HostToNetworkOrder((short)0);
                byte[] verHeader = BitConverter.GetBytes(version);

                writer.Write(verHeader); // using the BinaryWriter

                // HEADER : LENGTH (PAYLOAD + HEADER(8))
                short totalLength = IPAddress.HostToNetworkOrder((short)(request.Length + XML_HEADER_LEN));
                byte[] lengthHeader = BitConverter.GetBytes(totalLength);
                writer.Write(lengthHeader); // using the BinaryWriter

                // HEADER : INVOKE ID
                byte[] invokeIdByte = ToByteArray(getInvokeID());
                writer.Write(invokeIdByte); // using the BinaryWriter

                // MESSAGE : XML PAYLOAD
                byte[] requestMsg = ToByteArray(request);
                writer.Write(requestMsg); // using the BinaryWriter
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred in SendRequest.\n");
                Console.WriteLine(e.Message);
            }
            writer.Flush();
            return;
        }

        /* Function to receive request from AE Services server */
        public string readXMLMessage()
        {
            /*
             * Removing the Header information.
             * 
             * | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 |   
             *  VERSION|LENGTH |   INVOKE ID   |   XML PAYLOAD
             * 
             * VERSION: 2 bytes
             * LENGTH: 2 bytes
             * INVOKE ID: 4 bytes
             */

            String responseData = String.Empty;

            System.Threading.Thread.Sleep(150);
            try
            {
                // Skip the CSTA version in byte 1, 2
                reader.ReadBytes(VERSION_LEN);
                // Read the length of the response in byte 3, 4
                long responseLength = IPAddress.NetworkToHostOrder(reader.ReadInt16());
                // String to store the response ASCII representation.
                byte[] data = new byte[responseLength];

                // Read ahead in the header. Invoke ID: byte 5,6,7,8	
                string invokeID = new string(reader.ReadChars(INVOKE_ID_LEN));

                // Read the XML Payload which is found at offset 8.
                // The length of XML payload = XML message length - XML header length.
                int bytes = reader.Read(data, 0, data.Length - XML_HEADER_LEN);

                // Get the ASCII representation of XML payload.
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                /* When user presses 'any key' to stop the application, reader thread is aborted to immediately
                 * stop the processing. This results in receiveing ThreadAbortException here.
                 * Just clean the response data varaible and return as application is about to terminate.*/
                Console.WriteLine("Threadabortexception");
                responseData = String.Empty;
            }
            catch (System.IO.IOException e)
            {

                /* In case of Secured connection, ThreadAbortException is wrapped in IOException
                 * so check before printing exception message*/ 

                if (!(e.InnerException is System.Threading.ThreadAbortException))
                    Console.WriteLine("IOException occurred in readXMLMessage.\n{0}", e);
              
               responseData = String.Empty;
            }
            catch (Exception e1)
            {
                Console.WriteLine("Exception occurred in readXMLMessage.\n" + e1);
                responseData = String.Empty;
            }
            return responseData;    
        }
    }
}