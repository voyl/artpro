/*****************************************************************************
 * Title: TeleCommuter.cs
 * Author: Avaya
 * Copyright: Copyright 2007 AVAYA, Inc.
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;

/**
 * 
 * Purpose:
 *      This application demonstrates the initialization sequence, and elementary
 * event handling using the Avaya Communication Manager Device Media and Call 
 * Control (DMCC) XML API using the C# programming language.
 * 
 * The application utilizes a Communication Manager feature termed "Telecommuter Mode".
 * The application signals to Communication Manager that it is 
 * invoking telecommuter mode when it registers. The application gets
 * information about which device (extension) it will register for, and which 
 * phone number it will supply as the telecommuter extension along with other 
 * configuration information from a file containing XML descriptions of these
 * configuration items.
 * 
 * The flow of the application is as follows:
 * 
 * - Read the information in the configuration file
 * - Open Sockets to the Applications Enablement Services server
 * - Send the initialization messages
 *     + Start Application Session
 *     + Get Device Identifier
 *     + Start Monitors
 *     + Register Device
 * - Handle events that are sent related to the requested monitor 
 *    These events indicate that calls are being offered to the extension
 *    or that calls are being released from the extension.
 * - Wait for input on the console indicating that the user wishes to
 *     shut down the application.
 *     When such input is received, initiate the application shutdown sequence.
 *          + unRegister
 *          + release Monitor(s)
 *          + release Device Identifier(s)
 *          + Stop Application Session
 *          + close sockets.
 * 
 * 
 * Registration is done asynchronously using Asynchronous Services.
 * 
 * This sample application uses the XML API for DMCC but the application is not
 * production code. For example there is incomplete handling of error conditions.
 * However, it should give the developer a good idea about the necessary steps to
 * write a simple TeleCommuter application.
 * 
 * The code has not been thoroughly tested.
 * The developer is free to utilize this code and modify it as per their requirements.
 *
 * Test configuration:
 * SERVER SIDE:
 * CMAPI Server (4.0) was running on RedHat Enterprise Linux ES 4.0 update 3
 * CLIENT SIDE:
 * Windows XP SP2, .NET Framework 2.0
 *
 * Note : This application uses configuration file, "TeleCommuter.exe.config", to read all required data.
 * It is necessary that the user modifies this file with valid information and places it in the same 
 * directory where the application "exe" is located.
 * However, if the user is using Microsoft Visual Studio (MSVS) to run the application (debug or release mode) then the 
 * configuration file is "app.config" and user should make changes in this file. MSVS takes care of copying 
 * this file as "TeleCommuter.exe.config" while running the application.
 * 
 */
namespace TeleCommuter
{
    class TeleCommuter
    {
        // The following are the set of keys that needs to be present in the configuration file
        // along with their values.

        // Avaya Communication Manager IP address
        private const string CALL_SERVER_IP = "callserver";
        // Extension number for which the telecommuter number is being set up
        private const string EXTENSION = "extension";
        // password for the extension
        private const string EXT_PASSWORD = "extension.password";
        // Application Enablement Services (AE Services) IP Address
        private const string DMCC_SERVER_IP = "DMCC.server_ip";
        // AE Services user name
        private const string DMCC_USERNAME = "DMCC.username";
        // Password for the AES user
        private const string DMCC_PASSWORD = "DMCC.password";
        // AE Services port number 
        private const string DMCC_SERVER_PORT = "DMCC.server_port";
        // Set to true if secure connection is required
        private const string DMCC_SECURE = "DMCC.secure";
        // Telecommuter number
        private const string TELECOMM_NUMBER = "telecommuter.number";
        // Protocol version
        private const string DMCC_PROTOCOL_VERSION = "DMCC.protocol_version";

        // All the configuration details required by the application are read from a
        // configuration file when the application starts up and are stored in propertyList
        private Hashtable propertyList;

        XMLParser xmlParser = null;               // This provides XML handling support.

        /* These variables are populated based on the response from AE Services server */
        private string SessionID = null;
        private string DeviceID = null;
        private string MonitorRefID = null;

        /* The TerminalRegistered flag is used to hold registration status for the terminal. 
         * This will be used in CleanUp() routine to avoid an unnecessary call to 'UnregisterTerminal' 
         * This flag is set to true when the application successfully registers. 
         * When the application receives an UnregisterTerminal event this flag is set to false.
         */
        private bool isTerminalRegistered = false;     

        ~TeleCommuter()
        {
        }

        /* This is the starting point of the application which does the following:
         * Reads the configuration file and updates the propertyList
         * Calls the Start() function which starts the application session
         * When the application shuts down, calls CleanUp() to release all the 
         * resources used by the application.
         */
        static void Main(string[] args)
        {
            TeleCommuter AppObj = new TeleCommuter();
            AppObj.LoadProperties();
            AppObj.Start();
            AppObj.CleanUp();
        }

        /*
         * This function reads the configurable data from "app.config" file and populates the property
         * list for easy retrieval. Configurable parameter values are validated before storing.
         */
        public void LoadProperties()
        {
            try
            {
                propertyList = new Hashtable();
                String temp = null;
                temp = ConfigurationSettings.AppSettings[CALL_SERVER_IP];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", CALL_SERVER_IP);
                    Environment.Exit(1);
                }
                propertyList.Add(CALL_SERVER_IP, temp);

                temp = ConfigurationSettings.AppSettings[EXTENSION];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", EXTENSION);
                    Environment.Exit(1);
                }
                propertyList.Add(EXTENSION, temp);


                temp = ConfigurationSettings.AppSettings[EXT_PASSWORD];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", EXT_PASSWORD);
                    Environment.Exit(1);
                }
                propertyList.Add(EXT_PASSWORD, temp);


                temp = ConfigurationSettings.AppSettings[DMCC_SERVER_IP];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", DMCC_SERVER_IP);
                    Environment.Exit(1);
                }
                propertyList.Add(DMCC_SERVER_IP, temp);


                temp = ConfigurationSettings.AppSettings[DMCC_USERNAME];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", DMCC_USERNAME);
                    Environment.Exit(1);
                }
                propertyList.Add(DMCC_USERNAME, temp);


                temp = ConfigurationSettings.AppSettings[DMCC_PASSWORD];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", DMCC_PASSWORD);
                    Environment.Exit(1);
                }
                propertyList.Add(DMCC_PASSWORD, temp);


                temp = ConfigurationSettings.AppSettings[DMCC_SERVER_PORT];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", DMCC_SERVER_PORT);
                    Environment.Exit(1);
                }
                propertyList.Add(DMCC_SERVER_PORT, temp);


                temp = ConfigurationSettings.AppSettings[DMCC_SECURE];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", DMCC_SECURE);
                    Environment.Exit(1);
                }
                propertyList.Add(DMCC_SECURE, temp);

                temp = ConfigurationSettings.AppSettings[TELECOMM_NUMBER];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", TELECOMM_NUMBER);
                    Environment.Exit(1);
                }
                propertyList.Add(TELECOMM_NUMBER, temp);

                temp = ConfigurationSettings.AppSettings[DMCC_PROTOCOL_VERSION];
                if (temp == null)
                {
                    Console.WriteLine("Please specify '{0}' value in configuration file", DMCC_PROTOCOL_VERSION);
                    Environment.Exit(1);
                }
                propertyList.Add(DMCC_PROTOCOL_VERSION, temp);
               
                
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Syntax error in configuration file Telecommuter.exe.config. \n {0}", Ex.InnerException.Message);
                Environment.Exit(1);

            }

        }

        /* This function establishes a connection with the server, performs initial setup 
         * and handles XML requests/responses.
         * Initial setup includes four steps
         * 1) Start application session
         * 2) Get the device ID
         * 3) Starts the monitor
         * 4) Registers the terminal
         */

        public void Start()
        {
            xmlParser = new XMLParser();


            AddEventHandlers();
            string host = (string)propertyList[DMCC_SERVER_IP];
            int port = Int32.Parse((string)propertyList[DMCC_SERVER_PORT]);
            string username = (string)propertyList[DMCC_USERNAME];
            string password = (string)propertyList[DMCC_PASSWORD];
            string protocol_version = (string)propertyList[DMCC_PROTOCOL_VERSION];
            bool secure = System.Boolean.Parse((string)propertyList[DMCC_SECURE]);
            
            Console.WriteLine("To EXIT the application, type a character on the console.\n");

            // Check the configured information for a common configuration mistake
            if (!((port == 4722 && secure) || (port == 4721 && !secure)))
            {
                Console.WriteLine("Improper settings for server port and secure fields\n"
                                + "Please check port is 4722 for secured connectiion and 4721 for non-secured connection");

                // wait for input before closing down the application (and the output window).
                if (Console.ReadKey() != null)
                    Environment.Exit(1);
            }

            // print all the configuration information to help the user identify configuration issues
            Console.WriteLine("Parameter values for StartApplicationSession: \n" +
                                DMCC_SERVER_IP + " = " + host + "\n" +
                                DMCC_SERVER_PORT + " = " + port + "\n" +
                                DMCC_SECURE + " = " + secure + "\n" +
                                DMCC_USERNAME + " = " + username + "\n" +
                                DMCC_PASSWORD + " = " + password + "\n" +
                                DMCC_PROTOCOL_VERSION + " = " + protocol_version + "\n");

            /* Establish an application session between the application and the AE Services server 
             * Note:
             * This application uses only one device and one session. 
             * An application can handle multiple devices with a single  
             * application session and a single socket can be used for
             * sending all related requests.
             */
            SessionID = xmlParser.StartApplicationSession(host, port, secure, username, password, protocol_version);

            if (SessionID == null)
            { return; }
            Console.WriteLine("Application Session started. SessionID = {0}", SessionID);


            /* Obtain a device identifier for each station/device or extension that
             * the application needs to work with. This application works with only one device. 
             */

            string callserver = (string)propertyList[CALL_SERVER_IP];
            string ext = (string)propertyList[EXTENSION];

            Console.WriteLine("Parameter values for GetDeviceID: \n" +
                                CALL_SERVER_IP + " = " + callserver + "\n" +
                                EXTENSION + " = " + ext + "\n");

            DeviceID = xmlParser.GetDeviceID(callserver, ext);
            if (DeviceID == null)
            {return; }
            Console.WriteLine("Device created.");

            /* Start device monitor, this will get the application events for changes occuring
             * at the station.
             */
            MonitorRefID = xmlParser.MonitorStart(DeviceID);
            if (MonitorRefID == null)
            { return; }
            Console.WriteLine("Registered for monitoring services.");

            /* Register the device
             * The application registers the device with exclusive control and specifying Telecommuter
             * media mode by providing the Telecommuter number.
             */

            string ext_password = (string)propertyList[EXT_PASSWORD];
            string telecommuter_number = (string)propertyList[TELECOMM_NUMBER];

            Console.WriteLine("Parameter values for RegisterTerminal: \n" +
                    "deviceID" + " = " + DeviceID + "\n" +
                    EXT_PASSWORD + " = " + ext_password + "\n" +
                    TELECOMM_NUMBER + " = " + telecommuter_number + "\n");
            
            
            if (xmlParser.RegisterTerminal(DeviceID, ext_password, telecommuter_number) == false)
            { 
               return;
            }
            Console.WriteLine("Terminal Registered.");

            // Set the flag on successful registration 
            isTerminalRegistered = true;
            
            // Start sending keep alive messages to keep the session active. This application sends one request every 55 sec.
            xmlParser.StartAutoKeepAlive(SessionID, 55000);
            
            Console.WriteLine("Session Keep Alive started.");
            Console.WriteLine("The application has set up {0} as the Telecommuter number for the extension {1}", (string)propertyList[TELECOMM_NUMBER], (string)propertyList[EXTENSION]);

            Run();
        }

        /* This application defines its own handlers for handling events such as
         * LampMode Events. These event handlers form the core part of the application telephony logic.
         * 
         * Note: This implementation has adapted an event based approach but developers can choose their own
         * way to handle the events.
         * */
        public void AddEventHandlers()
        {
            xmlParser.OnLampModeEvent += new XMLParser.LampModeHandler(OnLampModeHandler);
            xmlParser.OnRingerStatusEvent += new XMLParser.RingerStatusHandler(OnRingerStatusHandler);
            xmlParser.OnDisplayUpdatedEvent += new XMLParser.DisplayUpdatedHandler(OnDisplayUpdatedHandler);
            xmlParser.OnTerminalUnregisteredEvent += new XMLParser.TerminalUnregisterEventHandler(OnTerminalUnregisteredEventHandler);


            /* This is a special handler. It allows the application to handle the exceptions in the 
             * reader thread and call clean up routine.
             */
            xmlParser.OnException += new XMLParser.ExceptionHandler(OnExceptionHandler);
        }

        /* This method starts the thread for reading XML responses and waits for closure.
         * */
        public void Run()
        {
            xmlParser.Run();                // Start the thread for reading responses.    
            Console.ReadKey();
            xmlParser.Stop();
        }

        /* This is a clean up routine to unregister and release all resources.
         * Steps are opposite to that in Start function.
         */
        public void CleanUp()
        {
                
                /* Stop reader thread */ 
                xmlParser.Stop();

                /* Stop the Auto keep alive signal */
                if (xmlParser != null)
                    xmlParser.StopAutoKeepAlive();

                /* Unregister the device with the AE Services server. */
                if (isTerminalRegistered)
                    xmlParser.UnregisterTerminal(DeviceID);
                isTerminalRegistered = false;

                /*  Stop the device monitor */
                if (MonitorRefID != null)
                    xmlParser.MonitorStop(MonitorRefID);
                MonitorRefID = null;

                /*  Release the device */
                if (DeviceID != null)
                    xmlParser.ReleaseDeviceID(DeviceID);
                DeviceID = null;


                /* Stop application session*/
                if (SessionID != null)
                    xmlParser.StopApplicationSession(SessionID);
                SessionID = null;
                
                /* Waits for a key to be pressed. If key detected is true, 
                 * then the console window closes.
                 */
                if(Console.ReadKey()!= null)
                    Environment.Exit(1);
        }

        /*
         * This callback method is invoked in response to the 'LampModeEvent'.  This event 
         * indicates the status of the lamp display for the application. Call state can be
         * inferred from the lamp state of call appearance buttons.
         * 
         * Lamp mode='flash' represents green lamp flashing on the device and indicates an incoming 
         *                   call if the button is a call appearance.
         * Lamp mode='steady' represents green lamp is on and not flashing and indicates the 
         *                    calll has been established if the button is a call appearance.
         * Lamp mode='off' represents green lamp goes dark and indicates that the call 
         *                 has been disconnected if the button is a call appearance.
         * Note that the red lamp (in-use) indicates which call-appearance is selected. 
         * The greep lamp indicates the status of the button (call-appearance/feature). 
         * Triggering behaviors off the button type and the green lamp state is best.
         * 
         */

        public void OnLampModeHandler(object sender, LampModeEventArgs args)
        {
            /*
             * In telecommunter mode the only lamp changes we expect are for a single
             * call appearance button. This application does not check to make sure
             * the changes we are receiving meet these conditions.
             * 
             * This function prints the lamp state information and takes no further action.
             */

            // Checking for only Green lamp modes and ignoring the Red lamp modes
            if (args.LampColor == LampColor.Green)
            {
                if (args.LampMode == LampMode.Flashing) // incoming call
                {
                    // A flashing green call appearance lamp indicates that there is an incoming call 
                    Console.WriteLine("Lamp " + args.Lamp + " is now flashing.");
                    Console.WriteLine("Receiving an Incoming call");
                }
                else if (args.LampMode == LampMode.Off) // Call disconnected
                {
                    Console.WriteLine("Lamp " + args.Lamp + " is now off.");
                    Console.WriteLine("The call has been disconnected");
                }
                else if (args.LampMode == LampMode.Steady) // Call Connected
                {
                    // A steady green call appearance lamp indicates that the call is now established
                    Console.WriteLine("Lamp " + args.Lamp + " is now on.");
                    Console.WriteLine("The call has been answered");
                }
            }
        }

        /**
         * This callback method is invoked in response to the 'RingerStatusEvent' event.
         * Application will check ring pattern and display the status on the console.
         **/ 
        public void OnRingerStatusHandler(object sender, RingerStatusEventArgs args)
        {
            if ((RingPattern)args.RingPattern == RingPattern.RingerOff)
            {
                Console.WriteLine("The ringer is off.");
            }
            else 
            {
                /* 
                 * There are a number of different ringing patterns that can indicate that 
                 * a call has been offered to the destination staition. 
                 * Print out the pattern and expect  that an interested user 
                 * will investigate more throughly
                 */
                Console.WriteLine("The ringer is ringing in pattern:" + (RingPattern)args.RingPattern);
            }
        }

        /**
         * This callback method is invoked in response to the 'DisplayUpdatedEvent'.
         * This sample application displays the contents of device display on the console.
         * This handler can be modified to provide more involved display information handling.
         **/
        public void OnDisplayUpdatedHandler(object sender, DisplayUpdatedEventArgs args)
        {
            if (args.Message != null)
                Console.WriteLine("Display update event: " + args.Message);
        }

        /**
         * This callback method is invoked in response to the 'TerminalUnregisteredEvent'. The application may 
         * receive this event if the terminal if forcefully unregistered by the AE Service Server
         * This application sets the isTerminalRegistered flag to false and displays reason for unregistration.
         * This handler can be modified to provide more involved display information handling.
         **/
        public void OnTerminalUnregisteredEventHandler(object sender, TerminalUnregisteredEventArgs args)
        {
            Console.WriteLine("Application exiting as TerminalUnregisterd event received "); 
            if (args.Reason != null)
                Console.WriteLine("Reason = {0}: ", args.Reason);
            isTerminalRegistered = false;
            CleanUp();
        }
        
        /* This function handles exception in xmlParser and does the cleanup */
        public void OnExceptionHandler(object sender, ExceptionArgs args)
        {
            Console.WriteLine("Exception: " + args.Message); 
            CleanUp();
        }
    }
}
