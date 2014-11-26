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
using System.Threading;

namespace Mojo
{
    public partial class frmMonitorMain : Form
    {
        // The form initialization method
        string strServerId = Properties.Settings.Default.ServerId;
        string strLoginId = Properties.Settings.Default.LoginId;
        string strPasswd = Properties.Settings.Default.Passwd;
        System.Threading.Thread serverThread = null;

        AsynchronousComm.AsynchronousSocketListener listener = new AsynchronousComm.AsynchronousSocketListener();

       public Dictionary<string, string> VDNList = new Dictionary<string, string>();
        public frmMonitorMain()
        {
            InitializeComponent();

            // Add the event handler that checks the TServer monitor event buffer

            // Don't turn on the caller ID timer yet
            // this.TServerBufferPoll += new Tsapi.TServerBufferEventHandler(checkTServerBuffer);
            this.tmBufferPoll.Enabled = false;
        }

        // The method that places an active monitor on the entered extension
        private void btnLogin_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (AgentMonitor dr in agents)
            {
                // Place an active monitor on the entered extension
                bool results  = false;
               if (dr.MonitorType == "VDN") 
                results = dr.session.monitorVDN(dr.strExtension);
                else 

                results = dr.session.monitor(dr.strExtension);
                dr.Start();
                // If action was successful feed the event back to the user
                if (results == true)
                {
                    // The session has an active monitor
                    // Feed back to the user that they are logged in with an active monitor

                    // Start the timers
                    count++;
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
            
            if (count > 0)
            {
                this.tmBufferPoll.Enabled = true;
                this.tmBufferPoll.Start();
               // this.timer1.Enabled = true;
               // this.timer1.Start();
            }

            if (serverThread != null && serverThread.IsAlive)
            {
                
                serverThread.Abort();

            }

            listener.agents = this.agents;
            listener.owner = this;
            System.Threading.ThreadStart st = new System.Threading.ThreadStart(listener.StartListening);

            serverThread = new System.Threading.Thread(st);
            
            serverThread.Start();
        }

        // The various touchtone buttons on the form...
        private void btn1_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "1";
            //if (!muted)
            //{
            //    player.SoundLocation = @"dtmf-1.wav";
            //    player.Play();
            //}
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "2";

        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "3";

        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "4";

        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "5";

        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "6";

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "7";

        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "8";

        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "9";

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            this.tbDialed.Text += "0";

        }

        // The event that fires when the Call button is clicked
        private void btnCall_Click(object sender, EventArgs e)
        {
            // If an outcall number defined...
            if (this.tbDialed.Text != "")
            {
                // Define the callee for passing to the TServer                
                string callee = this.tbDialed.Text;
                AgentMonitor agt = FindByExtension(txtExtn.Text.Trim());
                if (agt != null)
                    // Make the call
                    agt.session.makeCall(callee);
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
            //if (muted)
            //{
            //    this.btnMute.Text = "On";
            //    this.btnMute.BackColor = System.Drawing.Color.ForestGreen;
            //    muted = false;
            //}
            //else
            //{
            //    this.btnMute.Text = "Off";
            //    this.btnMute.BackColor = System.Drawing.Color.DarkRed;
            //    muted = true;
            //}
        }

        // The event that fires when line one is clicked
        private void btnLine1_Click(object sender, EventArgs e)
        {
            //if (this.btnLine1.Text == "Ring")
            //{
            //    // Pick up the ringing call and feed the event back to the user
            //    session.answerCall(btnLine1.ActiveCallId.callID, btnLine1.ActiveCallId.deviceID.device, (uint)btnLine1.ActiveCallId.devIDType);
            //    this.btnLine1.FlasherButtonStop();

            //    // Check caller ID against Outlook contacts
            //    checkOutlook(session.CallingDeviceId);
            //}
            //else if (this.btnLine1.Text == "On Call")
            //{
            //    // Hangup the active call
            //    session.hangupCall(btnLine1.ActiveCallId.callID, btnLine1.ActiveCallId.deviceID.device, (uint)btnLine1.ActiveCallId.devIDType);
            //}
        }

        // The event that fires when line two is clicked
        private void btnLine2_Click(object sender, EventArgs e)
        {
            /*  if (this.btnLine2.Text == "Ring")
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
              */
        }

        // The event that fires when the hold button is clicked
        private void btnHold_Click(object sender, EventArgs e)
        {
            AgentMonitor agt = FindByExtension(txtExtn.Text.Trim());
            if (agt != null)
            {
                agt.holdCall();
                
            }
        }

        List<AgentMonitor> agents = new List<AgentMonitor>();

        // Define the instance variables referenced throughout the class

        private void frmMojoMain_Load(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            System.IO.StreamReader sr = new System.IO.StreamReader("extensions.txt");
            string temp = "";
            AgentMonitor temp2 = null;
            while (sr.Peek() != -1)
            {
                temp = sr.ReadLine().Trim();
                if (temp == "") continue;
                temp2 = new AgentMonitor(strServerId, strLoginId, strPasswd, temp, "", "");
                temp2.MonitorType = "AGENT";
                temp2.owner = this;
                temp2.lstItem = new ListViewItem(new string[]{temp,"","","","","","","","","",""});
                listView1.Items.Add(temp2.lstItem);
                agents.Add(temp2); 
            }
            sr.Close();

            sr = new System.IO.StreamReader("vdns.txt");
            while (sr.Peek() != -1)
            {
                temp = sr.ReadLine().Trim();
                if (temp == "") continue;
                if (temp.Split(';').Length < 2) continue;
                temp2 = new AgentMonitor(strServerId, strLoginId, strPasswd, temp.Split(';')[0], temp.Split(';')[1], "");
                temp2.MonitorType = "VDN";
                temp2.owner = this;
                temp2.lstItem = new ListViewItem(new string[] { temp.Split(';')[0], temp.Split(';')[1], "", "", "", "", "", "", "","","" });
                //temp2.lstItem.BackColor = System.Drawing.Color.LightSalmon;
                listView1.Items.Add(temp2.lstItem);
                agents.Add(temp2);
            }
            sr.Close();
            DataAccess.Execute.ConnectionString = Properties.Settings.Default["connStr"].ToString();
            btnLogin_Click(null, null);
            btnLogin.Enabled = false;
        }

        public void AddAgent(string ext, string agentId, string pwd)
        {
            Mojo.AgentMonitor temp2 = new Mojo.AgentMonitor(strServerId, strLoginId, strPasswd, ext, agentId, pwd);
            temp2.MonitorType = "AGENT";
            temp2.owner = this;
            temp2.lstItem = new ListViewItem(new string[] { ext, agentId, "", "", "", "", "", "", "","","" });
            
            agents.Add(temp2);
            AddListItem(temp2.lstItem);

            bool result = temp2.session.monitor(temp2.strExtension);
            temp2.Start();
        }
        public delegate void _AddListItem(ListViewItem item);
        public void AddListItem(ListViewItem item)
        {
            if (listView1.InvokeRequired)
            {
                this.Invoke(new _AddListItem(AddListItem), item);
            }
            else
            {
                listView1.Items.Add(item);  
            }
        }
        public delegate void _RemoveListItem(ListViewItem item);
        public void RemoveListItem(ListViewItem item)
        {
            if (listView1.InvokeRequired)
            {
                this.Invoke(new _RemoveListItem(RemoveListItem), item);
            }
            else
            {
                listView1.Items.Remove(item);
            }
        }
        private void tmBufferPoll_Tick(object sender, EventArgs e)
        {
            /*foreach (AgentMonitor agt in agents)
            {
                agt.session.checkTServer();
            }*/
            listView1.Refresh();
        }
        private AgentMonitor FindByExtension(string keyExtension)
        {
            foreach (AgentMonitor agt in agents)
                if (agt.strExtension == keyExtension) return agt;

            return null;
        }

        private void lblDialed_Click(object sender, EventArgs e)
        {

        }

        private void tbDialed_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMwi_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            agents[0].answerCall();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // agents[0].LoginAgent();
            agents[0].transferMode = true;
            agents[0].holdCall();
            agents[0].makeCall("1021");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tmBufferPoll.Stop();
            
            foreach (AgentMonitor mon in agents)
            {
                mon.Stop();
            }
            
            if (serverThread != null && serverThread.IsAlive)
            {
                listener.sw.Close();
                serverThread.Abort();
            }
           //  timer1.Stop();
        }

        
        private void frmMonitorMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Lütfen bu uygulamayý kapatmayýnýz. Kapatmak için onaylayýn.", "Uyarý", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
            else
            {
                button3_Click(null, null);
            }
        }
        delegate void InvokeShowCommand(AgentMonitor mon );
        public void ShowCommand(AgentMonitor mon)
        {
            if (!txtLog.InvokeRequired)
            {
                txtLog.Text +=System.Environment.NewLine+ "Agent:"+ mon.strAgentId + ", Line1:" + mon.line1.strLine + ", Line2:" + mon.line2.strLine + ", Call 1:" + mon.line1.callId + ", Call 2:" + mon.line2.callId +
                    ", Device 1: " + mon.line1.deviceId + ", Device 2: " + mon.line2.deviceId;
            }
            else
                txtLog.Invoke(new InvokeShowCommand(ShowCommand), mon);

        }
        delegate void InvokeAppendLog(string message);
        public void AppendLog(string message)
        {
            if (!txtLog.InvokeRequired)
            {
                txtLog.Text += message+ System.Environment.NewLine;
            }
            else
                txtLog.Invoke(new InvokeAppendLog(AppendLog), message);
        }

        private void frmMonitorMain_Resize(object sender, EventArgs e)
        {
            listView1.Height = this.ClientRectangle.Height - btnLogin.Bottom - 200;
            txtLog.Height = 200;
            txtLog.Top = listView1.Bottom + 10; ;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader("extensions.txt");
            string temp = "";
            AgentMonitor temp2 = null;
            while (sr.Peek() != -1)
            {
                temp = sr.ReadLine().Trim();
                if (FindByExtension(temp) != null) continue;
                if (temp == "") continue;
                temp2 = new AgentMonitor(strServerId, strLoginId, strPasswd, temp, "", "");
                temp2.MonitorType = "AGENT";
                temp2.owner = this;
                temp2.lstItem = new ListViewItem(new string[] { temp, "", "", "", "", "", "", "", "", "", "" });
                listView1.Items.Add(temp2.lstItem);
                agents.Add(temp2);
                temp2.session.monitor(temp2.strExtension);
                temp2.Start();
            }
            sr.Close();

            sr = new System.IO.StreamReader("vdns.txt");
            while (sr.Peek() != -1)
            {
                temp = sr.ReadLine().Trim();
                if (temp == "") continue;
                if (temp.Split(';').Length < 2) continue;
                if (FindByExtension(temp.Split(';')[0]) != null) continue;
                temp2 = new AgentMonitor(strServerId, strLoginId, strPasswd, temp.Split(';')[0], temp.Split(';')[1], "");
                temp2.MonitorType = "VDN";
                temp2.owner = this;
                temp2.lstItem = new ListViewItem(new string[] { temp.Split(';')[0], temp.Split(';')[1], "", "", "", "", "", "", "", "", "" });
              //  temp2.lstItem.BackColor = System.Drawing.Color.LightSalmon;
                listView1.Items.Add(temp2.lstItem);
                agents.Add(temp2);
                temp2.session.monitorVDN(temp2.strExtension);
                temp2.Start();
            }
            sr.Close();
           
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
                AgentMonitor mon =             FindByExtension(textBox1.Text);
                mon.LoginAgent();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgentMonitor mon = FindByExtension(textBox1.Text);
            mon.LogoutAgent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AgentMonitor mon = FindByExtension(textBox1.Text);
            mon.ReadyAgent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AgentMonitor mon = FindByExtension(textBox1.Text);
            mon.NotReadyAgent("0");
        }
        
    }

    
}