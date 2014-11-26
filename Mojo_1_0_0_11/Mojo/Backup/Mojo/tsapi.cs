using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mojo
{
    public class Tsapi
    {
        public Tsapi()
        {
            // Add the delegate event for checking the TServer buffer
            this.TServerBufferPoll += new TServerBufferEventHandler(TsHandler);
        }
        
        // The public method to open the ACS stream
        public bool open(string strLoginId, string strPasswd, string strServerId)
        {
            // Convert the parameters to character arrays
            char[] serverId = strServerId.ToCharArray();
            char[] loginId = strLoginId.ToCharArray();
            char[] passwd = strPasswd.ToCharArray();

            // Define the initial set of variables used for opening the ACS Stream
            int invokeIdType = 1;
            UInt32 invokeId = 0;
            int streamType = 1;
            char[] appName = "Mojo".ToCharArray();
            int acsLevelReq = 1;
            char[] apiVer = "TS1-2".ToCharArray();
            ushort sendQSize = 0;
            ushort sendExtraBufs = 0;
            ushort recvQSize = 0;
            ushort recvExtraBufs = 0;

            // Define the mandatory (but unused) private data structure
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Define the event buffer pointer that gets data back from the TServer
            ushort numEvents = 0;
            Csta.EventBuf_t eventBuf = new Csta.EventBuf_t();
            ushort eventBufSize = (ushort)Csta.CSTA_MAX_HEAP;

            // Open the ACS stream
            try
            {
                int openStream = Csta.acsOpenStream(ref acsHandle, invokeIdType, invokeId, streamType, serverId, loginId, passwd, appName, acsLevelReq, apiVer, sendQSize, sendExtraBufs, recvQSize, recvExtraBufs, ref privData);
            }
            catch (System.Exception eOpenStream)
            {
                // If we can't open the stream record the error and inform the user
                MessageBox.Show("There was a TServer error. Closing the application and logging the incident.", "TServer Error");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, eOpenStream.Message.ToString(), EventLogEntryType.Warning, 234);
                return false;
            }

            // Wait a second to poll the event buffer
            System.Threading.Thread.Sleep(1000);

            // Poll the event buffer
            try
            {
                int openStreamConf = Csta.acsGetEventPoll(acsHandle, ref eventBuf, ref eventBufSize, ref privData, ref numEvents);
            }
            catch (System.Exception eOpenStreamConf)
            {
                // If we can't get back a confirmation record the error and inform the user
                MessageBox.Show("There was a TServer error. Closing the application and logging the incident.", "TServer Error");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, eOpenStreamConf.Message.ToString(), EventLogEntryType.Warning, 234);
                return false;
            }

            // Parse out the data elements in the event buffer...

            // The event header
            UInt32 numAcsHandle = BitConverter.ToUInt32(eventBuf.data, 0);
            ushort numEventClass = BitConverter.ToUInt16(eventBuf.data, 4);
            ushort numEventType = BitConverter.ToUInt16(eventBuf.data, 6);

            // The remainder of the open stream conf structure
            numInvokeId = BitConverter.ToUInt32(eventBuf.data, 8);
            string strApiVer = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 12, 21);
            string strLibVer = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 33, 21);
            string strTsrvVer = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 54, 21);
            string strDrvrVer = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 75, 21);

            if (numEventClass == Csta.ACSCONFIRMATION && numEventType == Csta.ACS_OPEN_STREAM_CONF)
            {
                // The stream has been successfully opened           
                return true;
            }
            else
            {
                // If we can't get back the open stream confirmation record the error and inform the user
                string strStreamOpenFailed = "The stream was not opened. The Event Class code returned was " + numEventClass + " and the Event Type returned was " + numEventType + ".";
                MessageBox.Show("The stream was not opened.", "Open Stream Failed");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, strStreamOpenFailed, EventLogEntryType.Warning, 234);
                return false;
            }
        }

        // The public method to monitor the provided extension
        public bool monitor(string strExtension)
        {
            // Convert the extension string to a character array
            chExtension = strExtension.ToCharArray();

            // Define the mandatory (unused) private data structure
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Define the various event monitor filters...

            // Any filters NOT added will allow those events to be monitored
            Csta.CSTAMonitorFilter_t monitorFilter = new Csta.CSTAMonitorFilter_t();
            monitorFilter.call = 0xFFFF - Csta.CF_DELIVERED - Csta.CF_CONNECTION_CLEARED - Csta.CF_ESTABLISHED - Csta.CF_NETWORK_REACHED;    // Monitor these call events 
            monitorFilter.feature = 0;                                                                                                  // Monitor everything feature-wise
            monitorFilter.agent = 0;                                                                                                    // Monitor everything agent-wise
            monitorFilter.maintenance = 0;                                                                                              // Monitor everything maintenance-wise
            monitorFilter.privateFilter = 1;                                                                                            // Mandatory but unused

            try
            {
                int monitorDevice = Csta.cstaMonitorDevice(acsHandle, numInvokeId, chExtension, ref monitorFilter, ref privData);
            }
            catch (System.Exception eMonitor)
            {
                // If we can't get back a confirmation record the error and inform the user
                MessageBox.Show("There was a TServer error. Closing the application and logging the incident.", "TServer Error");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, eMonitor.Message.ToString(), EventLogEntryType.Warning, 234);
                return false;
            }

            // Wait a second before polling the event queue
            System.Threading.Thread.Sleep(1000);

            // Define the event buffer that contains data passed back from TServer
            ushort numEvents2 = 0;
            Csta.EventBuf_t eventBuf2 = new Csta.EventBuf_t();
            ushort eventBufSize2 = (ushort)Csta.CSTA_MAX_HEAP;

            try
            {
                int monitorDeviceConf = Csta.acsGetEventPoll(acsHandle, ref eventBuf2, ref eventBufSize2, ref privData, ref numEvents2);
            }
            catch (System.Exception eMonitorConf)
            {
                // If we can't get back a confirmation record the error and inform the user
                MessageBox.Show("There was a TServer error. Closing the application and logging the incident.", "TServer Error");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, eMonitorConf.Message.ToString(), EventLogEntryType.Warning, 234);
                return false;
            }

            // Parse out the data elements in the event buffer...

            // The event header
            UInt32 numAcsHandle3 = BitConverter.ToUInt32(eventBuf2.data, 0);
            ushort numEventClass3 = BitConverter.ToUInt16(eventBuf2.data, 4);
            ushort numEventType3 = BitConverter.ToUInt16(eventBuf2.data, 6);

            // The various elements contained in the rest of the event buffer
            UInt32 numInvokeId3;
            UInt32 numMonitorCrossRefId3;
            ushort numCallFilter3;
            byte numFeatureFilter3;
            byte numAgentFilter3;
            byte numMaintenanceFilter3;
            UInt32 numPrivateFilter3;

            // If the device monitor was successful...
            if (numEventClass3 == Csta.CSTACONFIRMATION && numEventType3 == Csta.CSTA_MONITOR_CONF)
            {
                // Parse the elements in the event buffer
                numInvokeId3 = BitConverter.ToUInt32(eventBuf2.data, 8);
                numMonitorCrossRefId3 = BitConverter.ToUInt32(eventBuf2.data, 12);
                numCallFilter3 = BitConverter.ToUInt16(eventBuf2.data, 16);
                numFeatureFilter3 = (byte)eventBuf2.data.GetValue(18);
                numAgentFilter3 = (byte)eventBuf2.data.GetValue(20);
                numMaintenanceFilter3 = (byte)eventBuf2.data.GetValue(22);
                numPrivateFilter3 = BitConverter.ToUInt32(eventBuf2.data, 24);
                return true;

            }
            else
            {
                string strMonitorDeviceFailed = "The device was not monitored. The Event Class code returned was " + numEventClass3 + " and the Event Type returned was " + numEventType3 + ".";
                MessageBox.Show("The device was not monitored.", "Monitor Device Failed");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, strMonitorDeviceFailed, EventLogEntryType.Warning, 234);
                return false;
            }
        }
        
        // The private method to fire if a CSTA_DELIVERED event type is received
        private void isRinging()
        {
            // Parse out the data elements in the event buffer...

            // The remainder of the delivered call structure
            UInt32 numMonitorCrossRefId;
            UInt32 connectionCallId;
            string connectionDeviceId;
            char[] chConnectionDeviceId;
            int connectionDeviceIdType;
            string alertingDeviceId;
            char[] chAlertingDeviceId;
            int alertingDeviceIdType;
            int alertingDeviceIdStatus;
            string callingDeviceId;
            char[] chCallingDeviceId;
            int callingDeviceIdType;
            int callingDeviceIdStatus;
            string calledDeviceId;
            char[] chCalledDeviceId;
            int calledDeviceIdType;
            int calledDeviceIdStatus;
            string lastDeviceId;
            char[] chLastDeviceId;
            int lastDeviceIdType;
            int lastDeviceIdStatus;
            int localConnectionState;
            int eventCause;

            // The cross reference ID
            numMonitorCrossRefId = BitConverter.ToUInt32(eventBuf.data, 8);

            // The connection ID
            connectionCallId = BitConverter.ToUInt32(eventBuf.data, 12);
            connectionDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 16, 64);
            chConnectionDeviceId = connectionDeviceId.ToCharArray();
            connectionDeviceIdType = BitConverter.ToInt32(eventBuf.data, 80);

            // Define the active call to be referenced elsewhere
            activeCallId = connectionCallId;
            activeDeviceId = chConnectionDeviceId;
            activeDeviceIdType = connectionDeviceIdType;

            // The alerting device
            alertingDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 84, 64);
            chAlertingDeviceId = alertingDeviceId.ToCharArray();
            alertingDeviceIdType = BitConverter.ToInt32(eventBuf.data, 148);
            alertingDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 152);

            // The calling device
            callingDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 156, 64);
            chCallingDeviceId = callingDeviceId.ToCharArray();
            callingDeviceIdType = BitConverter.ToInt32(eventBuf.data, 220);
            callingDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 224);

            // The called device
            calledDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 228, 64);
            chCalledDeviceId = calledDeviceId.ToCharArray();
            calledDeviceIdType = BitConverter.ToInt32(eventBuf.data, 292);
            calledDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 296);

            // The last redirection device
            lastDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 300, 64);
            chLastDeviceId = lastDeviceId.ToCharArray();
            lastDeviceIdType = BitConverter.ToInt32(eventBuf.data, 364);
            lastDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 368);

            // A couple of other state and event describers
            localConnectionState = BitConverter.ToInt32(eventBuf.data, 372);
            eventCause = BitConverter.ToInt32(eventBuf.data, 376);

            activeConnectionCallId = connectionCallId.ToString();
            activeConnectionDeviceId = connectionDeviceId;
            activeConnectionDeviceIdType = connectionDeviceIdType.ToString();
            activeCallingDeviceId = callingDeviceId.Trim('\0');            
        }
        
        // The private method to fire if a CSTA_ESTABLISHED event type is received
        private void isConnected()
        {
            // Parse out the data elements in the event buffer...

            // The remainder of the delivered call structure
            UInt32 monitorCrossRefId;
            UInt32 connectionCallId;
            string connectionDeviceId;
            char[] chConnectionDeviceId;
            int connectionDeviceIdType;
            string answeringDeviceId;
            char[] chAnsweringDeviceId;
            int answeringDeviceIdType;
            int answeringDeviceIdStatus;
            string callingDeviceId;
            char[] chCallingDeviceId;
            int callingDeviceIdType;
            int callingDeviceIdStatus;
            string calledDeviceId;
            char[] chCalledDeviceId;
            int calledDeviceIdType;
            int calledDeviceIdStatus;
            string lastDeviceId;
            char[] chLastDeviceId;
            int lastDeviceIdType;
            int lastDeviceIdStatus;
            int localConnectionState;
            int eventCause;

            // The cross reference ID
            monitorCrossRefId = BitConverter.ToUInt32(eventBuf.data, 8);

            // The connection ID
            connectionCallId = BitConverter.ToUInt32(eventBuf.data, 12);
            connectionDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 16, 64);
            chConnectionDeviceId = connectionDeviceId.ToCharArray();
            connectionDeviceIdType = BitConverter.ToInt32(eventBuf.data, 80);

            // The answering device
            answeringDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 84, 64);
            chAnsweringDeviceId = answeringDeviceId.ToCharArray();
            answeringDeviceIdType = BitConverter.ToInt32(eventBuf.data, 148);
            answeringDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 152);

            // The calling device
            callingDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 156, 64);
            chCallingDeviceId = callingDeviceId.ToCharArray();
            callingDeviceIdType = BitConverter.ToInt32(eventBuf.data, 220);
            callingDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 224);

            // The called device
            calledDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 228, 64);
            chCalledDeviceId = calledDeviceId.ToCharArray();
            calledDeviceIdType = BitConverter.ToInt32(eventBuf.data, 292);
            calledDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 296);

            // The last redirection device
            lastDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 300, 64);
            chLastDeviceId = lastDeviceId.ToCharArray();
            lastDeviceIdType = BitConverter.ToInt32(eventBuf.data, 364);
            lastDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 368);

            // A couple of other state and event describers
            localConnectionState = BitConverter.ToInt32(eventBuf.data, 372);
            eventCause = BitConverter.ToInt32(eventBuf.data, 376);

            activeConnectionCallId = connectionCallId.ToString();
            activeConnectionDeviceId = connectionDeviceId;
            activeConnectionDeviceIdType = connectionDeviceIdType.ToString();
        }

        // The private method to fire if a CSTA_CONNECTION_CLEARED event type is received
        private void isDisconnected()
        {
            // Parse out the data elements in the event buffer...

            // The remainder of the cleared call structure
            UInt32 numMonitorCrossRefId;
            UInt32 connectionCallId;
            string connectionDeviceId;
            char[] chConnectionDeviceId;
            int connectionDeviceIdType;
            string releasingDeviceId;
            char[] chReleasingDeviceId;
            int releasingDeviceIdType;
            int releasingDeviceIdStatus;
            int localConnectionState;
            int eventCause;

            // The cross reference ID
            numMonitorCrossRefId = BitConverter.ToUInt32(eventBuf.data, 8);

            // The connection ID
            connectionCallId = BitConverter.ToUInt32(eventBuf.data, 12);
            connectionDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 16, 64);
            chConnectionDeviceId = connectionDeviceId.ToCharArray();
            connectionDeviceIdType = BitConverter.ToInt32(eventBuf.data, 80);

            // The releasing device
            releasingDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 84, 64);
            chReleasingDeviceId = releasingDeviceId.ToCharArray();
            releasingDeviceIdType = BitConverter.ToInt32(eventBuf.data, 148);
            releasingDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 152);

            // A couple of other state and event describers
            localConnectionState = BitConverter.ToInt32(eventBuf.data, 156);
            eventCause = BitConverter.ToInt32(eventBuf.data, 160);

        }

        // The private method to fire if a CSTA_NETWORK_REACHED event type is received
        private void isDialed()
        {
            // Parse out the data elements in the event buffer...

            // The remainder of the network reached call structure
            UInt32 monitorCrossRefId;
            UInt32 connectionCallId;
            string connectionDeviceId;
            char[] chConnectionDeviceId;
            int connectionDeviceIdType;
            string trunkDeviceId;
            char[] chTrunkDeviceId;
            int trunkDeviceIdType;
            int trunkDeviceIdStatus;
            string calledDeviceId;
            char[] chCalledDeviceId;
            int calledDeviceIdType;
            int calledDeviceIdStatus;
            int localConnectionState;
            int eventCause;

            // The cross reference ID
            monitorCrossRefId = BitConverter.ToUInt32(eventBuf.data, 8);

            // The connection ID
            connectionCallId = BitConverter.ToUInt32(eventBuf.data, 12);
            connectionDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 16, 64);
            chConnectionDeviceId = connectionDeviceId.ToCharArray();
            connectionDeviceIdType = BitConverter.ToInt32(eventBuf.data, 80);

            // The trunk ID
            trunkDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 84, 64);
            chTrunkDeviceId = trunkDeviceId.ToCharArray();
            trunkDeviceIdType = BitConverter.ToInt32(eventBuf.data, 148);
            trunkDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 152);

            // The called device
            calledDeviceId = System.Text.ASCIIEncoding.ASCII.GetString(eventBuf.data, 156, 64);
            chCalledDeviceId = calledDeviceId.ToCharArray();
            calledDeviceIdType = BitConverter.ToInt32(eventBuf.data, 220);
            calledDeviceIdStatus = BitConverter.ToInt32(eventBuf.data, 224);

            // A couple of other state and event describers
            localConnectionState = BitConverter.ToInt32(eventBuf.data, 228);
            eventCause = BitConverter.ToInt32(eventBuf.data, 232);

            activeConnectionCallId = connectionCallId.ToString();
            activeConnectionDeviceId = connectionDeviceId;
            activeConnectionDeviceIdType = connectionDeviceIdType.ToString();
        }

        // The private method to fire if a CSTA_QUERY_MWI_CONF event type is received
        private void mwiStatus()
        {
            // Parse out the data elements in the event buffer...
            UInt32 mwiInvokeId;
            bool boolMwi;

            // Parse the elements in the event buffer
            mwiInvokeId = BitConverter.ToUInt32(eventBuf.data, 8);
            boolMwi = BitConverter.ToBoolean(eventBuf.data, 12);

            messagesWaiting = boolMwi;
        }
        
        // The custom handler for interpreting various event types from TServer
        private void TsHandler(object sender, TServerBufferEventArgs e)
        {
            // Ringing call
            if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_DELIVERED)
            {
                isRinging();                
            }
            // Disconnected call
            else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_CONNECTION_CLEARED)
            {
                isDisconnected();
            }
            // Connected call
            else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_ESTABLISHED)
            {
                isConnected();
            }
            // Dialed call 
            else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_NETWORK_REACHED)
            {
                isDialed();
            }
            // Message waiting indicator update
            else if (e.EventClass == Csta.CSTACONFIRMATION && e.EventType == Csta.CSTA_QUERY_MWI_CONF)
            {
                mwiStatus();
            }
        }

        // Define the two TServer event buffer elements of interest
        public class TServerBufferEventArgs : EventArgs
        {
            private int eventClass, eventType;
            public int EventClass { get { return eventClass; } }
            public int EventType { get { return eventType; } }

            public TServerBufferEventArgs(int EventClass, int EventType)
            {
                eventClass = EventClass;
                eventType = EventType;
            }
        }

        // The public method to place an active call on hold
        public void holdCall(uint callId, char[] device, uint deviceType)
        {
            // Define the mandatory (unused) private data buffer
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Populate a ConnectionID_t struct with the active call elements

            Csta.ConnectionID_t activeCall = new Csta.ConnectionID_t();
            activeCall.callID = callId;
            activeCall.deviceID.device = device;
            activeCall.devIDType = (Csta.ConnectionID_Device_t)deviceType;

            int holdCall = Csta.cstaHoldCall(acsHandle, numInvokeId, ref activeCall, false, ref privData);                
        }

        // The public method to retrieve a held call
        public void retrieveCall(uint callId, char[] device, uint deviceType)
        {
            // Define the mandatory (unused) private data buffer
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Populate a ConnectionID_t struct with the active call elements


            Csta.ConnectionID_t activeCall = new Csta.ConnectionID_t();
            activeCall.callID = callId;
            activeCall.deviceID.device = device;
            activeCall.devIDType = (Csta.ConnectionID_Device_t)deviceType;

            int retrieveCall = Csta.cstaRetrieveCall(acsHandle, numInvokeId, ref activeCall, ref privData);                
        }

        // The public method to pick up an delivered call
        public void answerCall(uint callId, char[] device, uint deviceType)
        {
            // Define the mandatory (unused) private data buffer
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Populate a ConnectionID_t struct with the active call elements
            Csta.ConnectionID_t activeCall = new Csta.ConnectionID_t();
            activeCall.callID = callId;
            activeCall.deviceID.device = device;
            activeCall.devIDType = (Csta.ConnectionID_Device_t)deviceType;

            int answerCall = Csta.cstaAnswerCall(acsHandle, numInvokeId, ref activeCall, ref privData);
        }

        // The public method to clear an active call
        public void hangupCall(uint callId, char[] device, uint deviceType)
        {
            // Define the mandatory (unused) private data buffer
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Populate a ConnectionID_t struct with the active call elements
            Csta.ConnectionID_t activeCall = new Csta.ConnectionID_t();
            activeCall.callID = callId;
            activeCall.deviceID.device = device;
            activeCall.devIDType = (Csta.ConnectionID_Device_t)deviceType;

            int clearConnection = Csta.cstaClearConnection(acsHandle, numInvokeId, ref activeCall, ref privData);
        }

        // The public method to initiate an outgoing call
        public void makeCall(string callee)
        {
            // Define the mandatory (unused) private data buffer 
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            char[] calledDevice = callee.ToCharArray();

            try
            {
                int makeCall = Csta.cstaMakeCall(acsHandle, numInvokeId, chExtension, calledDevice, ref privData);
            }
            catch (System.Exception eMakeCall)
            {
                // If we can't get back a confirmation record the error and inform the user
                MessageBox.Show("There was a TServer error. Logging the incident and continuing the application.", "TServer Error");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, eMakeCall.Message.ToString(), EventLogEntryType.Warning, 234);
                return;
            }            
        }

        // The public method to check the message waiting indicator (MWI) status
        public void checkMwi()
        {
            // Define the mandatory (unused) private data buffer
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            int pollMwi = Csta.cstaQueryMsgWaitingInd(acsHandle, numInvokeId, chExtension, ref privData);
        }
        
        // Define the generic TServer buffer event handler
        public delegate void TServerBufferEventHandler(object sender, TServerBufferEventArgs e);

        // Define the event for polling the TServer event buffer
        public event TServerBufferEventHandler TServerBufferPoll;

        // Check the TServer event buffer
        public void checkTServer()
        {
            // Define the mandatory (unused) private data buffer
            Csta.PrivateData_t privData = new Csta.PrivateData_t();
            privData.vendor = "MERLIN                          ".ToCharArray();
            privData.length = 4;
            privData.data = "N".ToCharArray();

            // Define the event buffer that contains data passed back from TServer
            eventBuf = new Csta.EventBuf_t();
            ushort numEvents = 0;
            ushort eventBufSize = (ushort)Csta.CSTA_MAX_HEAP;

            // Poll the event queue to see if any call events are occurring
            try
            {
                int polledEvent = Csta.acsGetEventPoll(acsHandle, ref eventBuf, ref eventBufSize, ref privData, ref numEvents);
            }
            catch (System.Exception eEventPoll)
            {
                // If we can't get back a confirmation record the error and inform the user
                MessageBox.Show("There was a TServer error. Logging the incident and continuing the application.", "TServer Error");
                if (!EventLog.SourceExists(strSource))
                {
                    EventLog.CreateEventSource(strSource, strLog);
                }
                EventLog.WriteEntry(strSource, eEventPoll.Message.ToString(), EventLogEntryType.Warning, 234);
                return;
            }

            // Parse out the data elements in the event buffer...

            // The event header
            UInt32 numAcsHandle = BitConverter.ToUInt32(eventBuf.data, 0);
            ushort numEventClass = BitConverter.ToUInt16(eventBuf.data, 4);
            ushort numEventType = BitConverter.ToUInt16(eventBuf.data, 6);

            TServerBufferEventArgs args = new TServerBufferEventArgs(numEventClass, numEventType);
            this.TServerBufferPoll(this, args);
        }

        // Define the public properties available for the class
        public string ConnectionCallId { get { return activeConnectionCallId; } }
        public string ConnectionDeviceId { get { return activeConnectionDeviceId; } }
        public string ConnectionDeviceIdType { get { return activeConnectionDeviceIdType; } }
        public string CallingDeviceId { get { return activeCallingDeviceId; } }
        public bool MessagesWaiting { get { return messagesWaiting; } }

        // Define the instance variables referenced throughout the class
        UInt32 acsHandle = 0;
        UInt32 numInvokeId;
        char[] chExtension;
        string strSource = "Mojo";
        string strLog = "Application";
        Csta.EventBuf_t eventBuf = new Csta.EventBuf_t();
        UInt32 activeCallId;
        char[] activeDeviceId;
        int activeDeviceIdType;
        string activeConnectionCallId;
        string activeConnectionDeviceId;
        string activeConnectionDeviceIdType;
        string activeCallingDeviceId;
        bool messagesWaiting;    
    }
}
