/*****************************************************************************
 * Title: XMLFormatter.cs
 * Author: Avaya
 * Copyright: Copyright 2007 AVAYA, Inc.
 *****************************************************************************
 * Purpose: This file lists functions for formatting XML requests that 
 * need to be sent to the AE Services server. This file is part of   
 * TeleCommuter application and thus it covers a limited set of XML APIs
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace TeleCommuter
{
    class XMLFormatter
    {
        /* This function returns XML String for "StartApplicationSession" request. */
        public static string FormatStartAppSessionRequest(string username, string password, string protocol_version)
        {
            string message = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<StartApplicationSession xmlns=\"http://www.ecma-international.org/standards/ecma-354/appl_session\">" +
                    "<applicationInfo>" +
                        "<applicationID>TeleCommuterApp</applicationID>" +
                        "<applicationSpecificInfo>" +
                            "<ns1:SessionLoginInfo xsi:type=\"ns1:SessionLoginInfo\" xmlns:ns1=\"http://www.avaya.com/csta\"" +
                                " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                                "<ns1:userName>" + username + "</ns1:userName>" +
                                "<ns1:password>" + password + "</ns1:password>" +
                                "<ns1:sessionCleanupDelay>60</ns1:sessionCleanupDelay>" +
                            "</ns1:SessionLoginInfo>" +
                        "</applicationSpecificInfo>" +
                    "</applicationInfo>" +
                    "<requestedProtocolVersions>" +
                    "<protocolVersion>" + protocol_version + "</protocolVersion>" +
                    "</requestedProtocolVersions>" +
                    "<requestedSessionDuration>180</requestedSessionDuration>" +
                "</StartApplicationSession>";

            return message;
        }
        
        /* This function returns XML String for "StopApplicationSession" request. */
        public static string FormatStopApplicationSessionRequest(string session_id)
        {
            string getStopApplicationSession = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<StopApplicationSession xmlns=\"http://www.ecma-international.org/standards/ecma-354/appl_session\">" +
                    "<sessionID>" + session_id + "</sessionID>" +
                    "<sessionEndReason>"+
                        "<appEndReason>Application Request</appEndReason>"+
                    "</sessionEndReason>"+
                "</StopApplicationSession>";
            return getStopApplicationSession;
        }

        /* This function returns XML String for "GetDeviceID" request.*/
        public static string FormatDeviceIDRequest(string callserver, string ext)
        {
            string getGetDeviceId = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<GetDeviceId xmlns=\"http://www.avaya.com/csta\">" +
                   "<switchIPInterface>" + callserver + "</switchIPInterface>" +
                   "<extension>" + ext + "</extension>" +
                "</GetDeviceId>";

            return getGetDeviceId;
        }
        
        /* This function returns XML String for "ReleaseDeviceID" request. */
        public static string FormatReleaseDeviceIDRequest(string device_id)
        {
            string getReleaseDeviceId = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<ReleaseDeviceId xmlns=\"http://www.avaya.com/csta\">" +
                   "<device typeOfNumber=\"other\" mediaClass=\"notKnown\">"
                   + device_id + "</device>" +
               "</ReleaseDeviceId>";

            return getReleaseDeviceId;
        }

        /* This function returns XML String for "MonitorStart" request. 
         * By default, the application will start receiving all the events.  
         * An application needs to state which events it does not want to receive.
         */
        public static string FormatMonitorStartRequest(string device_id)
        {
            string getMonitorStart =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<MonitorStart xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://www.ecma-international.org/standards/ecma-323/csta/ed3\">" +
                    "<monitorObject>" +
                    "<deviceObject typeOfNumber=\"other\" mediaClass=\"notKnown\">" + device_id + "</deviceObject>" +
                    "</monitorObject>" +
                    "<requestedMonitorFilter>" +
                        "<physicalDeviceFeature>" +
                            "<buttonInformation>true</buttonInformation>" +
                            "<displayUpdated>true</displayUpdated>" +
                            "<hookswitch>true</hookswitch>" +
                            "<lampMode>true</lampMode>" +
                            "<ringerStatus>true</ringerStatus>" +
                        "</physicalDeviceFeature>" +
                    "</requestedMonitorFilter>" +
                    "<extensions>" +
                        "<privateData>" +
                            "<private>" +
                                "<AvayaEvents>" +
                                    "<CallInformationEvents>" +
                                        "<linkDown>true</linkDown> " +
                                        "<linkUp>true</linkUp>" +
                                    "</CallInformationEvents>" +
                                    "<invertFilter>true</invertFilter>" +
                                    "<RegisterTerminalEventsFilter><unregistered>true</unregistered></RegisterTerminalEventsFilter>" +
                                "</AvayaEvents>" +
                            "</private>" +
                        "</privateData>" +
                    "</extensions>" +
                "</MonitorStart>";

            return getMonitorStart;
           
        }
        
        /* This function returns XML String for "MonitorStop" request. */
        public static string FormatMonitorStopRequest(string monitor_id)
        {
            string getMonitorStop = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<MonitorStop xmlns=\"http://www.avaya.com/csta\">" +
                   "<monitorCrossRefID>" + monitor_id + "</monitorCrossRefID>" +
               "</MonitorStop>";
            return getMonitorStop;
        }

        /* This function returns XML String for "RegisterTerminalDevice" request. */
        public static string FormatRegisterTerminalDeviceRequest(string device_id, string password, string telecommuterext)
        {
            string getRegisterTerminalRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<RegisterTerminalRequest xmlns=\"http://www.avaya.com/csta\">" +
                    "<device typeOfNumber=\"other\" mediaClass=\"\" " +
                    "bitRate=\"constant\">" + device_id + "</device>" +
                    "<loginInfo>" +
                        "<forceLogin>true</forceLogin>" +
                        "<sharedControl>false</sharedControl>" +
                        "<password>" + password + "</password>" +
                        "<telecommuterExtension>" + telecommuterext + "</telecommuterExtension>" +
                    "</loginInfo>" +
                "</RegisterTerminalRequest>";

            return getRegisterTerminalRequest;
        }
        
        /* This function returns XML String for "UnregisterTerminalDevice" request. */
        public static string FormatUnregisterTerminalRequest(string device_id)
        {
            string getUnregisterTerminalRequest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<UnregisterTerminalRequest xmlns=\"http://www.avaya.com/csta\">" +
                    "<device typeOfNumber=\"other\" mediaClass=\"\" " +
                        "bitRate=\"constant\">" + device_id +
                    "</device>" +
                    "<forceLogout>true</forceLogout>" +
                "</UnregisterTerminalRequest>";
            return getUnregisterTerminalRequest;
        }

        /* This function returns XML String for "ResetApplicationSessionTimer" request. */
        public static string FormatSessionKeepAlive(string sessionID)
        {
            string keepSessionAlive = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<ResetApplicationSessionTimer xmlns=\"http://www.ecma-international.org/standards/ecma-354/appl_session\">" +
                    "<sessionID>" + sessionID + "</sessionID>" +
                    "<requestedSessionDuration>180</requestedSessionDuration>" +
                "</ResetApplicationSessionTimer>";
            return keepSessionAlive;

        }
    }
}
