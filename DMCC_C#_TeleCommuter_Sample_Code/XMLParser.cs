/*****************************************************************************
 * Title: XMLParser.cs
 * Author: Avaya
 * Copyright: Copyright 2007 AVAYA, Inc.
 *****************************************************************************
 * Purpose: This class provides the functionality for handling all XML messages that are
 * exchanged between the application and the AE Services server. 
 * 
 * This class does not cover all the XML APIs,
 * it only covers those APIs which are required for the Telecommuter application
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Threading;

namespace TeleCommuter
{
    /* This class provides the wrapper functions for XML APIs and also sends/receives XML messages
     * to/from AE Services server.
     */
    class XMLParser
    {
        /* These are references for the two threads which this class creates. */
        private Timer SessionKeepAliveTimer = null; 
        private Thread readerThread = null;
        private bool StopReadResponse = false;      //set to true when want to stop ReadResponse thread.

        /* This is used to compare with the positive registration "code" element */
        private const int NORMAL_REGISTER = 1;

        /* Event handlers and delegate definitions for various events */
        public delegate void LampModeHandler(object sender, LampModeEventArgs args);
        public delegate void RingerStatusHandler(object sender, RingerStatusEventArgs args);
        public delegate void DisplayUpdatedHandler(object sender, DisplayUpdatedEventArgs args);
        public delegate void TerminalUnregisterEventHandler(object sender, TerminalUnregisteredEventArgs args);
        public delegate void ExceptionHandler(object sender, ExceptionArgs args);

        public event LampModeHandler OnLampModeEvent;
        public event RingerStatusHandler OnRingerStatusEvent;
        public event DisplayUpdatedHandler OnDisplayUpdatedEvent;
        public event TerminalUnregisterEventHandler OnTerminalUnregisteredEvent;
        public event ExceptionHandler OnException;

        SocketHandler socket_handle;
        XMLHandler xmlHandler;

        /* This function starts an application session with the AE Services server and returns a 
         * session ID to uniquely identify the session.
         */
        public string StartApplicationSession(string server_ip, int server_port, bool isSecure, string username, string password, string protocol_version)
        {
            try
            {
                /*
                 * Create TCP socket and attach stream Reader and Writer. This reader and writer 
                 * will be used to read response data and write request data respectively
                 * */
                socket_handle = new SocketHandler();

                Console.WriteLine("Connecting to AE Services Server..");
                if (socket_handle.openSocket(server_ip, server_port, isSecure))
                {
                    Console.WriteLine("Socket open operation performed successfully");
                    xmlHandler = new XMLHandler();
                    xmlHandler.setParameters(socket_handle);
                }
                else
                {
                    Console.WriteLine("Error encountered during socket open operation.\n" +
                                        "Please check IP address and port for the AE Services server" );
                    if(isSecure)
                        Console.WriteLine("Ensure that the secure port is enabled");
                    else
                        Console.WriteLine("Ensure that the non-secure port is enabled");

                    Console.ReadKey();
                    Environment.Exit(1);
                }
                
                // Get the XML string for the "StartApplicationSession" request and send the request
                xmlHandler.sendRequest(XMLFormatter.FormatStartAppSessionRequest(username, password, protocol_version));
                
                // Read the response 
                string response = xmlHandler.readXMLMessage();

                // parse the response and create a document node tree structure for accessing XML data
                XmlDocument doc = new XmlDocument();

                if (response.Contains("StartApplicationSessionPosResponse"))
                {
                    doc.LoadXml(response);
                    XmlElement root = doc.DocumentElement;
                    XmlNodeList NodeList = root.GetElementsByTagName("sessionID");
                    string sessionID = NodeList[0].InnerText;
                    return sessionID;
                }
                else
                {
                    /* Parsing the reason for the reception of "StartApplicationSessionNegResponse" response
                     */
                    doc.LoadXml(response);
                    XmlElement root = doc.DocumentElement;
                    XmlNodeList NodeList = root.GetElementsByTagName("errorCode");
                    if (NodeList.Count > 0)
                    {
                        string errorResponse = NodeList[0].InnerText;
                        Console.WriteLine("Received error response for StartApplicationSession:\n {0}", errorResponse);
                        return null;
                    }
                    return null;
                }              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at StartApplicationSession:\n {0}", ex.Message);
                return null;

            }
        }
        
        /* This function will send the "StopApplicationRequest" and end the application session.
         * It also closes the TCP socket and the reader and writer streams attached to it.
         * */
        public void StopApplicationSession(string session_id)
        {
            try
            {
                // Get the XML string for "StopApplicationRequest" and send the request
                xmlHandler.sendRequest(XMLFormatter.FormatStopApplicationSessionRequest(session_id));
                string response = xmlHandler.readXMLMessage();

                // Close the TCP Socket connection.
                if (socket_handle.closeSocket())
                {
                    Console.WriteLine("Socket close operation performed successfully");
                }
                else
                {
                    Console.WriteLine("Error encountered during socket close operation");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at StopApplicationSession: {0}", ex.Message);
                return ;
            }
        }

        /* This function sets up a device identifier for the phone or extension supplied.
         * The response will contain device ID to uniquely identify a device.
         * 
         * */
        public string GetDeviceID(string callserver, string ext)
        {
            try
            {
                // Get the XML string for the "GetDeviceID" request and send the request
                xmlHandler.sendRequest(XMLFormatter.FormatDeviceIDRequest(callserver, ext));
                                
                // Read the response 
                string response = xmlHandler.readXMLMessage();

                // parse the response and create a document node tree structure for accessing XML data
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlElement root = doc.DocumentElement;
                XmlNodeList NodeList = root.GetElementsByTagName("device");
                if (NodeList == null || NodeList.Count <= 0)
                {
                    Console.WriteLine("Received error response for GetDeviceIdResponse");
                    Console.WriteLine(response);
                    return null;
                }
                else
                {
                    // Extract device ID
                    string ID = NodeList[0].InnerText;
                    return ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at GetDeviceID: {0}", ex.Message);
                return null;
            }
        }
      
        /* This function releases the device or station.
         * */
        public void ReleaseDeviceID(string device_id)
        {
            try
            {
                xmlHandler.sendRequest(XMLFormatter.FormatReleaseDeviceIDRequest(device_id));
                string response = xmlHandler.readXMLMessage();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at GetDeviceID: {0}", ex.Message);
                return ;
            }
        }
        
        /*
         * This function sends a "MonitorStart" request and starts a monitor for telephony events.
         * By starting a monitor, the application indicates the device that it is interested in observing.
         * By default, application starts receiving all the events. An application should state which events 
         * it does not want to receive.
         * */
        public string MonitorStart(string deviceID)
        {
            try
            {
                // Get the XML string for the "MonitorStart" request and send the request
                xmlHandler.sendRequest(XMLFormatter.FormatMonitorStartRequest(deviceID));
                
                // Read response 
                string response = xmlHandler.readXMLMessage();

                // parse the response and create a document node tree structure for accessing XML data
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlElement root = doc.DocumentElement;
                XmlNodeList NodeList = root.GetElementsByTagName("monitorCrossRefID");
                if (NodeList == null || NodeList.Count <= 0)
                {
                    Console.WriteLine("Received error response for MonitorStartResponse");
                    Console.WriteLine(response);
                    return null;
                }
                else
                {
                    // Extract monitor reference ID from response XML 
                    string ID = NodeList[0].InnerText;
                    return ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at MonitorStart: {0}", ex.Message);
                return null;
            }
        }
  
        /* This function stops monitor */
        public void MonitorStop(string monitor_id)
        {
            try
            {
                xmlHandler.sendRequest(XMLFormatter.FormatMonitorStopRequest(monitor_id));
                string response = xmlHandler.readXMLMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at MonitorStop: {0}", ex.Message);
                return ;
            }
        }

        /* This function sends "RegisterTerminalRequest" to register the device */
        public bool RegisterTerminal(string device_id, string password, string telecommuter_number)
        {
            try
            {
                /* Get the XML string for "RegisterTerminalRequest" and 
                 * send the request 
                 */

                xmlHandler.sendRequest(XMLFormatter.FormatRegisterTerminalDeviceRequest(device_id, password, telecommuter_number));

                /* 
                 * Now wait for a response, parse it, and interpert the response
                 */
                string response = xmlHandler.readXMLMessage();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlElement root = doc.DocumentElement;
                XmlNodeList NodeList = root.GetElementsByTagName("code");

                if (NodeList == null || NodeList.Count <= 0)
                {
                    Console.WriteLine("Received error response for RegisterTerminalResponse");
                    Console.WriteLine(response);
                    return false;

                }
                else
                {
                    /* Parsing the inner text message in the "code" element.
                     */
                    int ID = Int32.Parse(NodeList[0].InnerText);

                    if (ID == NORMAL_REGISTER)
                    {
                        /* The value of the data received in the "code" element is "1". 
                         * This indicates normal and successful registration.
                         */
                        return true;
                    }
                    else
                    {
                       /* The value of the data received in the "code" element 
                        *  is an integer other than "1". 
                        *  This indicates erroneous registration. 
                        *  The positive integer gives the error code and the error 
                        *  message is contained in the "reason" element. 
                        */

                        XmlNodeList NodeList_reason = root.GetElementsByTagName("reason");
                        String reason = NodeList_reason[0].InnerText;
                        Console.WriteLine("Reason for RegisterTerminalResponse error= {0}", reason);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at RegisterTerminalDevice: {0}", ex.Message);
                return false ;
            }
        }

        /* This function unregisters the terminal */
        public void UnregisterTerminal(string device_id)
        {
            try
            {
                xmlHandler.sendRequest(XMLFormatter.FormatUnregisterTerminalRequest(device_id));
                string response = xmlHandler.readXMLMessage();
                return ;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at RegisterTerminalDevice: {0}", ex.Message);
                return ;
            }
        }

        /* This function starts a separate thread for sending 
         * "ResetApplicationSessionTimer" requests 
         */
        public void StartAutoKeepAlive(string session_id, int duration)
        {
            if (SessionKeepAliveTimer == null)
                SessionKeepAliveTimer = new Timer(new TimerCallback(SessionKeepAlive), session_id, duration, duration);
            else
                SessionKeepAliveTimer.Change(duration, duration);
        }
 
        /* This function disposes the keep alive thread */
        public void StopAutoKeepAlive()
        {
            if (SessionKeepAliveTimer != null)
            {
                SessionKeepAliveTimer.Dispose();
                SessionKeepAliveTimer = null;
            }
        }

        /* This function sends the Keep Alive messages to keep the session active */
        private void SessionKeepAlive(Object stateInfo)
        {
            Console.WriteLine("\nSending session keep alive request at {0}", DateTime.Now.TimeOfDay);
            xmlHandler.sendRequest(XMLFormatter.FormatSessionKeepAlive((string)stateInfo));
            return;
        }

        /* This function is the thread procedure for reader thread.
         * This reads the responses and appropriately converts them into events.
         * */
        private void HandleReponseThreadProc()
        {
            try
            {
                while (!StopReadResponse)
                {
                    /* Ensure there is some data on the stream */
                    if (socket_handle.NWStream.DataAvailable == false)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                    
                    string response = xmlHandler.readXMLMessage();

                    // parse the response and create a document node tree structure for accessing XML data
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(response);
                    XmlElement root = doc.DocumentElement;
                    Console.WriteLine("Received event : {0}", root.Name);

                    switch (root.Name)
                    {
                        case "LampModeEvent":
                            {
                                LampModeEventArgs lampModeEventArgs = new LampModeEventArgs(root);
                                OnLampModeEvent(this, lampModeEventArgs);
                                break;
                            }
                        case "DisplayUpdatedEvent":
                            {
                                DisplayUpdatedEventArgs displayUpdatedArgs = new DisplayUpdatedEventArgs(root);
                                OnDisplayUpdatedEvent(this, displayUpdatedArgs);
                                break;
                            }
                        case "RingerStatusEvent":
                            {
                                RingerStatusEventArgs ringerStatusEventArgs = new RingerStatusEventArgs(root);
                                OnRingerStatusEvent(this, ringerStatusEventArgs);
                                break;
                            }
                        case "ResetApplicationSessionTimerPosResponse":
                            {
                                Console.WriteLine("ResetApplicationSessionTimerPosResponse received from AE Services server");
                                break;
                            }
                        case "CSTAException":
                            {
                                XmlNodeList NodeList = root.GetElementsByTagName("message");
                                Console.WriteLine(NodeList[0].InnerText);
                            }
                            break;
                        case "TerminalUnregisteredEvent":
                            {
                                TerminalUnregisteredEventArgs terminalUnregisteredEventArgs = new TerminalUnregisteredEventArgs(root);
                                OnTerminalUnregisteredEvent(this, terminalUnregisteredEventArgs);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Unknown event received from AE Services server: {0}", root.Name);
                                // This application ignores unrecognized events.
                                break;
                            }

                    }   // End of switch
                }// End of while
            }

            catch (ThreadAbortException ex)
            {
                /* When user presses 'any key' to stop the application, reader thread is aborted to immediately
                 * stop the processing. This results in receiveing ThreadAbortException here.
                 * Do nothing in this exception handler. */

            }
            catch (Exception ex)
            {
                ExceptionArgs args = new ExceptionArgs(ex.Message);
                OnException(this, args);
            }

            finally
            {
            }

        }
           
        /* This function provides interface for starting reader thread */
        public void Run()
        {
            readerThread = new Thread(new ThreadStart(HandleReponseThreadProc));
            StopReadResponse = false;
            readerThread.Start();
            Console.WriteLine("\nReader Thread started. Press any key to stop the Reader thread.\n");
        }
 
        /* This function provides interface for stopping reader thread */
        public void Stop()
        {
            if (readerThread != null)
            {
                Console.WriteLine("\n Waiting for reader thread to stop..");
                StopReadResponse = true;
                readerThread.Abort();
                readerThread = null;
                Console.WriteLine("\nReader thread stopped. Press any key to exit the console.\n");
            }           
        }
     }
}
