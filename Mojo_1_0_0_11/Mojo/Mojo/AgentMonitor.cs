using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Mojo
{
    public class AgentCodes
    {

        public static string Ready = "*52";
        public static string AUX = "*54"; //ACW aux listesinde olacak aksa için
        public static string Login = "*50"; //ACW aux listesinde olacak aksa için
        public static string AutoIn = "*52";
        public static string ManualIn = "*53";
        public static string Logout = "#50";
        

    }
    public class AgentLine
    {
        public string strLine = "Idle";
        public  UInt32  callId = 0;
        public string deviceId = "";
        public UInt32 deviceIdType = 0;
        public string callerId = "";
        public string direction = "";
        public string VDN = "";
    }
    public class AgentMonitor
    {

        public string strExtension;
        public string strAgentId;
        public string strAgentPasswd;
        //SoundPlayer player = new SoundPlayer();
        public bool muted = false;
        public Tsapi session = new Tsapi();

        public string Status = "None";
        /*
        public FButton.FsButton.ConnectionID_t activeCallId1;
        public FButton.FsButton.ConnectionID_t activeCallId2;
        */
        public string activeCallTime1;
        public System.Windows.Forms.ListViewItem lstItem = null;
        public string activeCallTime2;
        
        
        
        public bool transferMode = false;
        public System.Windows.Forms.Form owner = null;
        public System.Threading.Thread runner = null;
        public AgentLine line1 = new AgentLine();
        public AgentLine line2 = new AgentLine();
        public string MonitorType = "AGENT";
        public AgentMonitor(string strServerId, string strLoginId, string strPasswd, string strExtension, string strAgentId, string strAgentPasswd)
        {

            // Read the TServer-specific variables from the app.config
            session.TServerBufferPoll += new Tsapi.TServerBufferEventHandler(checkTServerBuffer);
            

            // Open the TServer session
            bool result = session.open(strLoginId, strPasswd, strServerId);

            this.strExtension = strExtension;
            this.strAgentId = strAgentId;
            this.strAgentPasswd = strAgentPasswd;
        }
        // The event that fires to check for various call events every second

        public void Start()
        {
            runner = new Thread(new ThreadStart(this.Poll));
            runner.Start();
        }
        public void Stop()
        {

            if (runner != null)
            {
                if (runner.IsAlive)
                {
                    runner.Abort();
                }
            }
            if (session != null)
                session.close();
            
        }
        public void Poll()
        {
            while (true)
            {
                this.session.checkTServer();
                System.Threading.Thread.Sleep(500);
            }
        }

        private void checkTServerBuffer(object sender, Tsapi.TServerBufferEventArgs e)
        {

            try
            {
                session.TsHandler(sender, e);
                // Ringing call
                if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_DELIVERED)
                {

                    ringingCall();

                }
                // Connected call
                else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_ESTABLISHED)
                {
                    connectedCall();

                }
                // Disconnected call
                else if (e.EventClass == Csta.CSTAUNSOLICITED && (e.EventType == Csta.CSTA_CONNECTION_CLEARED  || e.EventType == Csta.CSTA_CALL_CLEARED || e.EventType == Csta.CSTA_CALL_COMPLETION))
                {
                    disconnectedCall();

                }
                // Dialed call
                else if (e.EventClass == Csta.CSTAUNSOLICITED && (e.EventType == Csta.CSTA_NETWORK_REACHED || e.EventType == Csta.CSTA_ORIGINATED))
                {
                    dialedCall();

                }
                // Transferred call
                else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_TRANSFERRED)
                {
                    transferredCall();

                }
                else
                {

                }
                UpdateListItem();
            }
            catch (Exception ex)
            {

                ((frmMonitorMain)this.owner).AppendLog(ex.ToString());

            }
        }
        delegate void InvokeUpdateListItem();

        public void UpdateListItem()
        {
            if (!lstItem.ListView.InvokeRequired)
            {
                lstItem.SubItems[0].Text = strExtension;
                lstItem.SubItems[1].Text = strAgentId;
                lstItem.SubItems[2].Text = Status;
                lstItem.SubItems[3].Text = line1.strLine;
                lstItem.SubItems[4].Text = line1.callerId;
                lstItem.SubItems[5].Text = line1.callId.ToString();
                lstItem.SubItems[6].Text = line2.strLine;
                lstItem.SubItems[7].Text = line2.callerId;
                lstItem.SubItems[8].Text = line2.callId.ToString();
                lstItem.SubItems[9].Text = line1.VDN;
                lstItem.SubItems[10].Text = line2.VDN;
            }
            else
            {
                lstItem.ListView.Invoke(new InvokeUpdateListItem( UpdateListItem));
            }
        }
        // The event that fires to check the message waiting indicator every 5 seconds
        private void tmMwiPoll_Tick(object sender, EventArgs e)
        {
            // Check the MWI status boolean
            session.checkMwi();
            //if (session.MessagesWaiting == true)

            //// Feed the event back to the user if there are messages waiting
            //{
            //    this.btnMwi.BackColor = Color.Red;
            //}
            //else
            //{
            //    this.btnMwi.BackColor = Color.Gray;
            //}
        }

        // The actions to take when a call is connected
        private void connectedCall()
        {
            // Feed the event back to the user
            if (line1.strLine == "Ring")
            {
                if (this.line1.callId != 0)
                    this.line1.strLine = "On Call";
                return;
            }
            else if (line2.strLine == "Ring")
            {
                if (this.line2.callId != 0)
                    this.line2.strLine = "On Call";
            }
            else
            {
                //((frmMonitorMain)this.owner).AppendLog("Connected Call:"+ line1.strLine +":"+ line2.strLine);
            }
        }

        private void transferredCall()
        {
            if (transferMode)
            {


                if ("22414;22405;22406;22400".Contains(strAgentId) && strAgentId.Trim() != "") 
                    ((frmMonitorMain)this.owner).AppendLog("Transferred call:"+ strAgentId);
                this.transferMode = false;
                this.line1.strLine = "Idle";
                this.line2.strLine = "Idle";

                this.line1.callerId = "";
                this.line2.callerId = "";
                
                this.line1.callId = 0;
                this.line2.callId = 0;
                this.line1.deviceId = "";
                this.line2.deviceId = "";
                this.line1.deviceIdType = 0;
                this.line2.deviceIdType = 0;
            }
        }
        // The actions to take when an alerting call is present
        private void ringingCall()
        {
            string connectionCallId = session.ConnectionCallId;
            string connectionDeviceId = session.ConnectionDeviceId;
            string connectionDeviceIdType = session.ConnectionDeviceIdType;
            if (Convert.ToUInt32(connectionCallId) == 0) return;
            string callerId = session.CallingDeviceId;

            //((frmMonitorMain)owner).("Ringing: "+ connectionCallId +":" + connectionDeviceId);
            // Pop up the caller ID *** if the calling party isn't the device itself ***            
            if (callerId != strExtension)
            {
                string now = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                this.activeCallTime1 = now;

               /* try
                {
                    DataAccess.Execute.ExecuteNonQuery("update TBIVRLOG set AGENTID='" + strAgentId + "' where AGENTID IS NULL AND DateADD(mi, -10, Current_TimeStamp) AND CALLID='" + connectionCallId + "'");
                }
                catch
                {
                }*/
                // Feed the event back to the user
                if (this.line1.strLine == "Idle" && Convert.ToUInt32(connectionCallId) != 0)
                {
                    line1.direction = "INBOUND";
                    this.line1.strLine = "Ring";
                    this.line1.callerId = callerId;
                    FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                    tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                    FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                    tempDeviceId.device = connectionDeviceId.ToCharArray();
                    tempConnectionId.deviceID = tempDeviceId;
                    tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                    
                    //this.activeCallId1 = tempConnectionId;
                    this.line1.callId = Convert.ToUInt32(connectionCallId);
                    this.line1.deviceId = connectionDeviceId;
                    this.line1.deviceIdType = Convert.ToUInt32(connectionDeviceIdType);

                    //AGENT ISE VDN CAGRI LISTESINDEN ILGILI VDN KODU VE ADINI BUL
                    if (MonitorType == "AGENT")
                    {
                        if (owner != null)
                            
                            try 
	                        {
                                line1.VDN = ((frmMonitorMain)owner).VDNList[line1.callId.ToString()];
                    
	                        }
	                        catch (Exception)
	                        {

                                line1.VDN = "";
	                        }
                    }
                    else if (MonitorType == "VDN")
                    {
                        if (owner != null)

                            try
                            {
                                ((frmMonitorMain)owner).VDNList.Add(line1.callId.ToString(), strAgentId);
                            }
                            catch (Exception)
                            {


                            }
                    }
                    
                }
                else if (this.line2.strLine == "Idle" && Convert.ToUInt32(connectionCallId)!=0)
                {
                    if (this.line1.callId == Convert.ToUInt32(connectionCallId)) return;
                    line2.direction = "INBOUND";
                    
                    this.line2.strLine = "Ring";
                    this.line2.callerId = callerId; 
                    FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                    tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                    FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                    tempDeviceId.device = connectionDeviceId.ToCharArray();
                    tempConnectionId.deviceID = tempDeviceId;
                    tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                  //  this.activeCallId2 = tempConnectionId;

                    this.line2.callId = Convert.ToUInt32(connectionCallId);
                    this.line2.deviceId = connectionDeviceId;
                    this.line2.deviceIdType = Convert.ToUInt32(connectionDeviceIdType);
                    
                    //AGENT ISE VDN CAGRI LISTESINDEN ILGILI VDN KODU VE ADINI BUL
                    if (MonitorType == "AGENT")
                    {
                        if (owner != null)

                            try
                            {
                               line2.VDN = ((frmMonitorMain)owner).VDNList[line1.callId.ToString()];

                            }
                            catch (Exception)
                            {

                                line2.VDN = "";
                            }
                    }
                    else if (MonitorType == "VDN")
                    {
                        if (owner != null)

                            try
                            {
                                ((frmMonitorMain)owner).VDNList.Add(line2.callId.ToString(), strAgentId);
                            }
                            catch (Exception)
                            {


                            }
                    }

                    
                }
                answerCall();
                UpdateListItem();
            }
            else
            {
                if (callerId.StartsWith("*") || callerId.StartsWith("#"))
                {
                    return;
                }
                
                if (this.line1.strLine == "Idle" && Convert.ToUInt32(connectionCallId) != 0)
                {
                    line1.direction = "OUTBOUND"; 
                    this.line1.strLine = "Ring";
                    this.line1.callerId = session.calledDeviceId;
                     //FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                    //tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                    //FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                    //tempDeviceId.device = strExtension.PadRight(64, '\0').ToCharArray();
                    //tempConnectionId.deviceID = tempDeviceId;
                    //tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                    
                    //this.activeCallId1 = tempConnectionId;
                    this.line1.callId = Convert.ToUInt32(connectionCallId); ;
                    this.line1.deviceId = strExtension.PadRight(64, '\0');
                    this.line1.deviceIdType = Convert.ToUInt32(connectionDeviceIdType);
                    return;
                }
                else if (this.line2.strLine == "Idle" && Convert.ToUInt32(connectionCallId) != 0)
                {
                    if (this.line1.callId == Convert.ToUInt32(connectionCallId)) return;

                    line2.direction = "OUTBOUND";
                    this.line2.strLine = "Ring";
                    this.line2.callerId = session.calledDeviceId;
                    //FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                    //tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                    //FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                    //tempDeviceId.device = strExtension.PadRight(64, '\0').ToCharArray();
                    //tempConnectionId.deviceID = tempDeviceId;
                    //tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                    ////this.activeCallId2 = tempConnectionId;

                    this.line2.callId = Convert.ToUInt32(connectionCallId); ;
                    this.line2.deviceId = strExtension.PadRight(64, '\0');
                    this.line2.deviceIdType = Convert.ToUInt32(connectionDeviceIdType);
                }
                if (transferMode)
                {
                    transferCall();
                }
                UpdateListItem();
            }
        }

        // The actions to take when a connection is cleared
        private void disconnectedCall()
        {
            string tempCallId = "";


            try
            {

                ((frmMonitorMain)owner).VDNList.Remove(session.lastDisconectedCallId.ToString());
            }
            catch 
            {
            }
            // Feed the event back to the user
            if (session.lastDisconectedCallId == line1.callId.ToString()) 
            {
                //   this.btnLine1.BackColor = Color.Gray;
                this.line1.strLine = "Idle";
                line1.callerId = "";
                
                tempCallId = line1.callId.ToString();
                //activeCallId1 = new FButton.FsButton.ConnectionID_t();

                line1.callId = 0;
                line1.deviceId = "";
                line1.deviceIdType = 0;
                activeCallTime1 = "";
            }
            else if (session.lastDisconectedCallId == line2.callId.ToString())
            {
                //   this.btnLine2.BackColor = Color.Gray;
                this.line2.strLine = "Idle";
                line2.callerId = "";
            
                tempCallId = line2.callId.ToString();
                //activeCallId2 = new FButton.FsButton.ConnectionID_t();
                activeCallTime2 = "";
                line2.callId = 0;
                line2.deviceId = "";
                line2.deviceIdType = 0;
            }
            else if (this.line1.strLine != "Idle")
            {
                this.line1.strLine = "Idle";
                line1.callerId = "";
            
                tempCallId = line1.callId.ToString();
                //activeCallId1 = new FButton.FsButton.ConnectionID_t();
                line1.callId = 0;
                line1.deviceId = "";
                line1.deviceIdType = 0;

                activeCallTime1 = "";
            }
            else if (this.line2.strLine != "Idle")
            {
                this.line2.strLine = "Idle";
                line2.callerId = "";
            
                tempCallId = line2.callId.ToString();
                //activeCallId2 = new FButton.FsButton.ConnectionID_t();
                line2.callId = 0;
                line2.deviceId = "";
                line2.deviceIdType = 0;
                activeCallTime2 = "";
            }

          /*  if (this.line1.strLine == "Idle" && this.line2.strLine != "Idle")
            {
                this.line2.strLine = "Idle";
                line2.callerId = "";
            
                tempCallId = line2.callId.ToString();
                activeCallId2 = new FButton.FsButton.ConnectionID_t();
                activeCallTime2 = "";
            }*/
           /* try
            {
                DataAccess.Execute.ExecuteNonQuery("update TBIVRLOG set CALLENDTIME=Current_TimeStamp where AGENTID = '" + strAgentId + "' AND ROWDATE >= DateADD(mi, -20, Current_TimeStamp) AND CALLID='" + tempCallId + "'");
            }
            catch
            {
            }
            
            try
            {
                ((frmMonitorMain)owner).VDNList.Remove(tempCallId);
            }
            catch (Exception)
            {
                
                
            }*/
        }

        // The actions to take when an outgoing call has reached telco
        private void dialedCall()
        {


            //((frmMonitorMain)owner).AppendLog("is dialed: " + session.ConnectionCallId);
            //((frmMonitorMain)owner).AppendLog(session.ConnectionDeviceId);
            //((frmMonitorMain)owner).AppendLog(session.ConnectionDeviceIdType);

            //string connectionCallId = session.ConnectionCallId;
            //string connectionDeviceId = session.ConnectionDeviceId;
            //string connectionDeviceIdType = session.ConnectionDeviceIdType;
            
            //if (Convert.ToUInt32(connectionCallId) == 0) return;
            // Feed the event back to the user
            //if (this.line1.strLine == "Idle")
            //{
            //    this.line1.strLine = "On Call";
            //    /*FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
            //    tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
            //    FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
            //    tempDeviceId.device = strExtension.PadRight(64, '\0').ToCharArray();
            //    tempConnectionId.deviceID = tempDeviceId;
            //    tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
            //     * */
            //    this.activeCallId1 = tempConnectionId;

            //    this.line1.callId = Convert.ToUInt32(connectionCallId);
            //    this.line1.deviceId = strExtension.PadRight(64, '\0');
            //    this.line1.deviceIdType = Convert.ToUInt32(connectionDeviceIdType);
                
            //    return;
            //}
            //else if (this.line2.strLine == "Idle")
            //{
            //    this.line2.strLine = "On Call";

            //    /*FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
            //    tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
            //    FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
            //    tempDeviceId.device = strExtension.PadRight(64, '\0').ToCharArray();
            //    tempConnectionId.deviceID = tempDeviceId;
            //    tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);*/

            //    this.activeCallId2 = tempConnectionId;
            //    this.line2.callId = Convert.ToUInt32(connectionCallId);
            //    this.line2.deviceId = strExtension.PadRight(64, '\0');
            //    this.line2.deviceIdType = Convert.ToUInt32(connectionDeviceIdType);
                
            //}
            //if (transferMode)
            //{
            //    ((frmMonitorMain)owner).AppendLog("Transfer Mode:"+ line1.strLine +":" + line2.strLine+":"+session.ConnectionDeviceId);
            //    this.transferCall();
            //}
        }

        public void holdCall()
        {
            
            if ((this.line1.strLine == "On Call" || this.line1.strLine == "Ring") && this.line2.strLine != "Held")
            {
                // Place the active call on hold
                session.holdCall(line1.callId, line1.deviceId.ToCharArray(), (uint)line1.deviceIdType);

                // Feed the event back to the user
                this.line1.strLine = "Held";

            }
            else if (this.line1.strLine == "Held" && this.line2.strLine != "Held")
            {
                // Retrieve the held call
                session.retrieveCall(line1.callId, line1.deviceId.ToCharArray(), (uint)line1.deviceIdType);

                // Feed the event back to the user
                this.line1.strLine = "On Call";
                //this.btnLine1.FlasherButtonStop();
            }
            else if ((this.line2.strLine == "On Call" || this.line2.strLine == "Ring") && this.line1.strLine != "Held")
            {
                // Place the active call on hold
                session.holdCall(line2.callId, line2.deviceId.ToCharArray(), (uint)line2.deviceIdType);

                // Feed the event back to the user
                this.line2.strLine = "Held";
                /* this.btnLine2.FlasherButtonColorOn = Color.Green;
                 this.btnLine2.FlasherButtonColorOff = Color.Red;
                 this.btnLine2.FlasherButtonStart(FButton.FlashIntervalSpeed.BlipMid);*/
            }
            else if (this.line2.strLine == "Held" && this.line1.strLine != "Held")
            {
                // Retrieve the held call
                session.retrieveCall(line2.callId, line2.deviceId.ToCharArray(), (uint)line2.deviceIdType);

                // Feed the event back to the user
                this.line2.strLine = "On Call";
                // this.btnLine2.FlasherButtonStop();
            }
            else
            {
                
            }
        }
   
        public void answerCall()
        {
            session.answerCall(session.activeCallId, session.activeDeviceId, (uint)session.activeDeviceIdType);
        }
        public void hangupCall()
        {
            session.hangupCall(session.activeCallId, session.activeDeviceId, (uint)session.activeDeviceIdType);
        }
        public void transferCall()
        {
            //((frmMonitorMain)owner).ShowCommand(this);
            session.transferCall(line1.callId, strExtension.PadRight(64, '\0').ToCharArray(), (uint)line1.deviceIdType
                ,line2.callId, strExtension.PadRight(64, '\0').ToCharArray(), (uint)line2.deviceIdType);
            
        }
        public void makeCall(string destination)
        {
            session.makeCall(destination);
            //if (destination.StartsWith("*") || destination.StartsWith("#"))
            //{
            //    Thread.Sleep(1000);
            //    hangupCall();
            //}
            /*if (line1.strLine == "Idle")
            {
                line1.strLine = "Ring";
            }
            else
                if (line2.strLine == "Idle")
                {
                    line2.strLine = "Ring";
                }*/
        }
        public void LoginAgent()
        {
            session.SetAgentState(strAgentId, strAgentPasswd, Csta.AgentMode_t.AM_LOG_IN, "");
        }
        public void LogoutAgent()
        {
            session.SetAgentState(strAgentId, strAgentPasswd, Csta.AgentMode_t.AM_LOG_OUT, "");
        }
        public void ReadyAgent()
        {
            session.CstaSetAgentState(strExtension, strAgentId, strAgentPasswd, Csta.AgentMode_t.AM_WORK_READY, Csta.AgentMode_t.AM_READY, 0);
            
        }
        public void NotReadyAgent(string code)
        {
            session.CstaSetAgentState(strExtension, "3001", "1234", Csta.AgentMode_t.AM_WORK_READY, Csta.AgentMode_t.AM_NOT_READY, 0);
        }
    }
}
