using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Media;
using System.Configuration;
using Microsoft.Office.Interop.Outlook;

namespace Mojo
{
    public partial class frmMojoMain : Form
    {
        // The form initialization method
        public frmMojoMain()
        {
            InitializeComponent();

            // Add the event handler that checks the TServer monitor event buffer
            session.TServerBufferPoll += new Tsapi.TServerBufferEventHandler(checkTServerBuffer);

            // Don't turn on the caller ID timer yet
            this.tmBufferPoll.Enabled = false;

            // Read the TServer-specific variables from the app.config
            string strServerId = Properties.Settings.Default.ServerId;
            string strLoginId = Properties.Settings.Default.LoginId;
            string strPasswd = Properties.Settings.Default.Passwd;
            
            // Open the TServer session
            bool result = session.open(strLoginId, strPasswd, strServerId);
            if (result == true)
            {
                // The session was successfully opened
            }
            else
            {
                // The session failed to be opened, so close the form
                this.Close();
            }
        }        
        
        // The method to check caller ID against Outlook contacts
        private void checkOutlook(string callerId)
        {
            // Read the Outlook-specific variable from the app.config
            string strOutlook = Properties.Settings.Default.Outlook;

            // Exit out of the method if Outlook isn't to be connected to
            if (strOutlook == "false")
            {
                return;
            }

            try
            {
                // Define the various Outlook COM items
                Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
                Microsoft.Office.Interop.Outlook.MAPIFolder contacts;
                Microsoft.Office.Interop.Outlook.Items contactItems;
                Microsoft.Office.Interop.Outlook.NameSpace outlookNamespace = application.GetNamespace("MAPI");
                outlookNamespace.Logon("", null, null, null);
                contacts = outlookNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
                contactItems = contacts.Items;

                // Iterate through each contact item
                foreach (Microsoft.Office.Interop.Outlook._ContactItem contact in contactItems)
                {
                    // Match the caller ID against contact home telephone number
                    if (contact.HomeTelephoneNumber != null)
                    {
                        StringBuilder sbHome = new StringBuilder();
                        if (contact.HomeTelephoneNumber.ToString() != string.Empty && contact.HomeTelephoneNumber.ToString() != null)
                        {
                            foreach (char txt in contact.HomeTelephoneNumber.ToString())
                            {
                                // Strip all non-numeric characters from the telephone number
                                if (char.IsDigit(txt))
                                {
                                    sbHome.Append(txt);
                                }
                            }
                        }
                        else
                        {
                            sbHome.Append("");
                        }
                        if (sbHome.ToString() == callerId)
                        {
                            // Display the contact match
                            contact.Display(false);
                            return;
                        }
                    }

                    // Match the caller ID against contact work telephone number
                    if (contact.BusinessTelephoneNumber != null)
                    {
                        StringBuilder sbWork = new StringBuilder();
                        if (contact.BusinessTelephoneNumber.ToString() != string.Empty && contact.BusinessTelephoneNumber.ToString() != null)
                        {
                            foreach (char txt in contact.BusinessTelephoneNumber.ToString())
                            {
                                // Strip all non-numeric characters from the telephone number
                                if (char.IsDigit(txt))
                                {
                                    sbWork.Append(txt);
                                }
                            }
                        }
                        else
                        {
                            sbWork.Append("");
                        }
                        if (sbWork.ToString() == callerId)
                        {
                            // Display the contact match
                            contact.Display(false);
                            return;
                        }
                    }

                    // Match the caller ID against contact cellular telephone number
                    if (contact.MobileTelephoneNumber != null)
                    {
                        StringBuilder sbCell = new StringBuilder();
                        if (contact.MobileTelephoneNumber.ToString() != string.Empty && contact.MobileTelephoneNumber.ToString() != null)
                        {
                            foreach (char txt in contact.MobileTelephoneNumber.ToString())
                            {
                                // Strip all non-numeric characters from the telephone number
                                if (char.IsDigit(txt))
                                {
                                    sbCell.Append(txt);
                                }
                            }
                        }
                        else
                        {
                            sbCell.Append("");
                        }
                        if (sbCell.ToString() == callerId)
                        {
                            // Display the contact match
                            contact.Display(false);
                            return;
                        }
                    }
                }                
            }
            catch
            {
                // An unhandled exception occurs casting the COM object 
                // to the Outlook Contact Item in the event there are no matches
                return;
            } 
        }

        // The actions to take when a call is connected
        private void connectedCall()
        {
            // Feed the event back to the user
            if (btnLine1.Text == "Ring")
            {
                this.btnLine1.Text = "On Call";
                return;
            }
            else if (btnLine2.Text == "Ring")
            {
                this.btnLine2.Text = "On Call";
            }      
        }

        // The actions to take when an alerting call is present
        private void ringingCall()
        {
            string connectionCallId = session.ConnectionCallId;
            string connectionDeviceId = session.ConnectionDeviceId;
            string connectionDeviceIdType = session.ConnectionDeviceIdType;
            string callerId = session.CallingDeviceId;

            // Pop up the caller ID *** if the calling party isn't the device itself ***            
            if (callerId != this.tbExtension.Text)
            {
                this.tbCallerId.Text = callerId;
                string now = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                this.tbCalledAt.Text = now;

                // Feed the event back to the user
                if (this.btnLine1.Text == "Idle")
                {
                    this.btnLine1.Text = "Ring";
                    this.btnLine1.FlasherButtonColorOn = Color.Gray;
                    this.btnLine1.FlasherButtonColorOff = Color.Red;
                    this.btnLine1.FlasherButtonStart(FButton.FlashIntervalSpeed.BlipFast);
                    FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                    tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                    FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                    tempDeviceId.device = connectionDeviceId.ToCharArray();
                    tempConnectionId.deviceID = tempDeviceId;
                    tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                    this.btnLine1.ActiveCallId = tempConnectionId;
                    return;
                 }
                 else if (this.btnLine2.Text == "Idle")
                 {
                    this.btnLine2.Text = "Ring";
                    this.btnLine2.FlasherButtonColorOn = Color.Gray;
                    this.btnLine2.FlasherButtonColorOff = Color.Red;
                    this.btnLine2.FlasherButtonStart(FButton.FlashIntervalSpeed.BlipFast);
                    FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                    tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                    FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                    tempDeviceId.device = connectionDeviceId.ToCharArray();
                    tempConnectionId.deviceID = tempDeviceId;
                    tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);                        
                    this.btnLine2.ActiveCallId = tempConnectionId;
                 }

                 this.WindowState = FormWindowState.Normal;
                 if (!muted)
                 {
                    player.SoundLocation = @"ring.wav";
                    player.Play();
                 }
            }
        }

        // The actions to take when a connection is cleared
        private void disconnectedCall()
        {           
            // Feed the event back to the user
            if (this.btnLine1.Text == "On Call")
            {
                this.btnLine1.BackColor = Color.Gray;
                this.btnLine1.Text = "Idle";
                return;
            }
            else if (this.btnLine2.Text == "On Call")
            {
                this.btnLine2.BackColor = Color.Gray;
                this.btnLine2.Text = "Idle";
            }            
        }
        
        // The actions to take when an outgoing call has reached telco
        private void dialedCall()
        {
            string connectionCallId = session.ConnectionCallId;
            string connectionDeviceId = session.ConnectionDeviceId;
            string connectionDeviceIdType = session.ConnectionDeviceIdType;
            
            // Feed the event back to the user
            if (this.btnLine1.Text == "Idle")
            {
                this.btnLine1.Text = "On Call";
                this.btnLine1.BackColor = Color.Red;
                FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                tempDeviceId.device = strExtension.PadRight(64, '\0').ToCharArray(); //connectionDeviceId.ToCharArray();
                tempConnectionId.deviceID = tempDeviceId;
                tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                this.btnLine1.ActiveCallId = tempConnectionId;
                return;
             }
             else if (this.btnLine2.Text == "Idle")
             {
                this.btnLine2.Text = "On Call";
                this.btnLine2.BackColor = Color.Red;
                FButton.FsButton.ConnectionID_t tempConnectionId = new FButton.FsButton.ConnectionID_t();
                tempConnectionId.callID = Convert.ToUInt32(connectionCallId);
                FButton.FsButton.DeviceID_t tempDeviceId = new FButton.FsButton.DeviceID_t();
                tempDeviceId.device = strExtension.PadRight(64, '\0').ToCharArray(); //connectionDeviceId.ToCharArray();
                tempConnectionId.deviceID = tempDeviceId;
                tempConnectionId.devIDType = (FButton.FsButton.ConnectionID_Device_t)Convert.ToUInt32(connectionDeviceIdType);
                this.btnLine2.ActiveCallId = tempConnectionId;
             }
        }        
                
        // The method that places an active monitor on the entered extension
        private void btnLogin_Click(object sender, EventArgs e)
        {
            strExtension = this.tbExtension.Text;
            
            // Place an active monitor on the entered extension
            bool results = session.monitor(strExtension);
            
            // If action was successful feed the event back to the user
            if (results == true)
            {
                // The session has an active monitor
                // Feed back to the user that they are logged in with an active monitor
                this.tbExtension.ReadOnly = true;
                this.btnLogin.Text = "Logged In";
                this.btnLogin.Enabled = false;

                // Start the timers
                this.tmBufferPoll.Enabled = true;
                this.tmBufferPoll.Start();

                // Don't start the MWI poll if the PBX doesn't support this feature, 
                // which would be traced as CSTA Universal Failure 15

                // this.tmMwiPoll.Enabled = true;
                // this.tmMwiPoll.Start();
            }
            else
            {
                // The session failed to be actively monitored, so close the form
                this.Close();
            }            
        }        

        // The various touchtone buttons on the form...
        private void btn1_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "1";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-1.wav";
                player.Play();
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "2";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-2.wav";
                player.Play();
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "3";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-3.wav";
                player.Play();
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "4";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-4.wav";
                player.Play();
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "5";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-5.wav";
                player.Play();
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "6";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-6.wav";
                player.Play();
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "7";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-7.wav";
                player.Play();
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "8";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-8.wav";
                player.Play();
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "9";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-9.wav";
                player.Play();
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "0";
            if (!muted)
            {
                player.SoundLocation = @"dtmf-0.wav";
                player.Play();
            }
        }
        
        // The event that fires when the Call button is clicked
        private void btnCall_Click(object sender, EventArgs e)
        {
            // If an outcall number defined...
            if (this.tbDialed.Text != "")
            {
                // Define the callee for passing to the TServer                
                string callee = this.tbDialed.Text;

                // Make the call
                session.makeCall(callee);
            }
                
        }

        // The method alternately shows and hides the call control portion of the form
        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (this.Size.Height == 140)
            {
                this.Size = new Size(this.Size.Width, 325);
            }
            else
            {
                this.Size = new Size(this.Size.Width, 140);
            }
        }

        // Ignore all keystrokes except for numbers, backspaces, and tabs.
        private void tbDialed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != '\b') && (e.KeyChar != '\t'))
            {
                e.Handled = true;
            }
        }

        // Ignore all keystrokes except for numbers, backspaces, and tabs.
        private void tbExtension_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != '\b') && (e.KeyChar != '\t'))
            {
                e.Handled = true;
            }
        }

        // The event that fires when the mute button is clicked
        private void btnMute_Click(object sender, EventArgs e)
        {
            if (muted)
            {
                this.btnMute.Text = "On";
                this.btnMute.BackColor = System.Drawing.Color.ForestGreen;
                muted = false;
            }
            else
            {
                this.btnMute.Text = "Off";
                this.btnMute.BackColor = System.Drawing.Color.DarkRed;
                muted = true;
            }
        }
        
        // The event that fires when line one is clicked
        private void btnLine1_Click(object sender, EventArgs e)
        {
            if (this.btnLine1.Text == "Ring")
            {
                // Pick up the ringing call and feed the event back to the user
                session.answerCall(btnLine1.ActiveCallId.callID, btnLine1.ActiveCallId.deviceID.device, (uint)btnLine1.ActiveCallId.devIDType);
                this.btnLine1.FlasherButtonStop();
                
                // Check caller ID against Outlook contacts
                checkOutlook(session.CallingDeviceId);
            }
            else if (this.btnLine1.Text == "On Call")
            {
                // Hangup the active call
                session.hangupCall(btnLine1.ActiveCallId.callID, btnLine1.ActiveCallId.deviceID.device, (uint)btnLine1.ActiveCallId.devIDType);
            }
        }

        // The event that fires when line two is clicked
        private void btnLine2_Click(object sender, EventArgs e)
        {
            if (this.btnLine2.Text == "Ring")
            {
                // Pick up the ringing call and feed the event back to the user
                session.answerCall(btnLine2.ActiveCallId.callID, btnLine2.ActiveCallId.deviceID.device, (uint)btnLine2.ActiveCallId.devIDType);
                this.btnLine2.FlasherButtonStop();

                // Check caller ID against Outlook contacts
                checkOutlook(session.CallingDeviceId);
            }
            else if (this.btnLine2.Text == "On Call")
            {
                // Hangup the active call
                session.hangupCall(btnLine2.ActiveCallId.callID, btnLine2.ActiveCallId.deviceID.device, (uint)btnLine2.ActiveCallId.devIDType);
            }
        }

        // The event that fires when the hold button is clicked
        private void btnHold_Click(object sender, EventArgs e)
        {
            if (this.btnLine1.Text == "On Call" && this.btnLine2.Text == "Idle")
            {
                // Place the active call on hold
                session.holdCall(btnLine1.ActiveCallId.callID, btnLine1.ActiveCallId.deviceID.device, (uint)btnLine1.ActiveCallId.devIDType);

                // Feed the event back to the user
                this.btnLine1.Text = "Held";
                this.btnLine1.FlasherButtonColorOn = Color.Green;
                this.btnLine1.FlasherButtonColorOff = Color.Red;
                this.btnLine1.FlasherButtonStart(FButton.FlashIntervalSpeed.BlipMid);
            }
            else if (this.btnLine1.Text == "Held" && this.btnLine2.Text == "Idle")
            {
                // Retrieve the held call
                session.retrieveCall(btnLine1.ActiveCallId.callID, btnLine1.ActiveCallId.deviceID.device, (uint)btnLine1.ActiveCallId.devIDType);
                
                // Feed the event back to the user
                this.btnLine1.Text = "On Call";
                this.btnLine1.FlasherButtonStop();
            }
            else if (this.btnLine2.Text == "On Call" && this.btnLine1.Text == "Idle")
            {
                // Place the active call on hold
                session.holdCall(btnLine2.ActiveCallId.callID, btnLine2.ActiveCallId.deviceID.device, (uint)btnLine2.ActiveCallId.devIDType);

                // Feed the event back to the user
                this.btnLine2.Text = "Held";
                this.btnLine2.FlasherButtonColorOn = Color.Green;
                this.btnLine2.FlasherButtonColorOff = Color.Red;
                this.btnLine2.FlasherButtonStart(FButton.FlashIntervalSpeed.BlipMid);
            }
            else if (this.btnLine2.Text == "Held" && this.btnLine1.Text == "Idle")
            {
                // Retrieve the held call
                session.retrieveCall(btnLine2.ActiveCallId.callID, btnLine2.ActiveCallId.deviceID.device, (uint)btnLine2.ActiveCallId.devIDType);

                // Feed the event back to the user
                this.btnLine2.Text = "On Call";
                this.btnLine2.FlasherButtonStop();
            }
        }

        // The event that fires to check for various call events every second
        private void tmBufferPoll_Tick(object sender, EventArgs e)
        {
            session.checkTServer();
        }

        private void checkTServerBuffer(object sender, Tsapi.TServerBufferEventArgs e)
        {
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
            else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_CONNECTION_CLEARED)
            {
                disconnectedCall();
            }
            // Dialed call
            else if (e.EventClass == Csta.CSTAUNSOLICITED && e.EventType == Csta.CSTA_NETWORK_REACHED)
            {
                dialedCall();
            }
        }
        
        // The event that fires to check the message waiting indicator every 5 seconds
        private void tmMwiPoll_Tick(object sender, EventArgs e)
        {
            // Check the MWI status boolean
            session.checkMwi();
            if (session.MessagesWaiting == true)

            // Feed the event back to the user if there are messages waiting
            {
                this.btnMwi.BackColor = Color.Red;
            }
            else
            {
                this.btnMwi.BackColor = Color.Gray;
            }
        }

        // Define the instance variables referenced throughout the class
        string strExtension;
        SoundPlayer player = new SoundPlayer();
        bool muted = false;
        Tsapi session = new Tsapi();      
            
    }

    
}