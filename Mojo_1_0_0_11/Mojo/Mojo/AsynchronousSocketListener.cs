using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AsynchronousComm
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.
        public  ManualResetEvent allDone = new ManualResetEvent(false);
        public System.Windows.Forms.Form owner = null;
        public System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\AVAYA\log"+DateTime.Now.ToString("yyyyMMddhhmm")+".txt", true);
        public  List<Mojo.AgentMonitor> agents;
        public AsynchronousSocketListener()
        {
        }

        public  void StartListening()
        {
           
            
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            
            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".

            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public  void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public  void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    try
                    {

                        content = content.Replace("<EOF>", "");
                        // All the data has been read from the 
                        // client. Display it on the console.
                        //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        //    content.Length, content);
                        //MessageBox.Show(content);
                        string result = "";
                        //sw.WriteLine(content);
                        Mojo.AgentMonitor mon = null;
                        string[] fields = content.Split(':');
                        sw.WriteLine(DateTime.Now.ToString() + ">>" + content);
                        switch (fields[0])
                        {
                            case "00": //device status
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                {
                                    result = mon.Status + ":" + mon.line1.strLine + ":" + mon.line1.callerId.Trim().Replace("#", "_").Replace("\0", "") + ":" + mon.line1.VDN.Trim() + ":" + mon.line2.strLine + ":" + mon.line2.callerId.Trim().Replace("#", "_").Replace("\0", "") + ":" + mon.line2.VDN.Trim() + ":" + mon.strExtension + ":" + mon.strAgentId;
                                    sw.WriteLine("***" + result);
                                }
                                else
                                    sw.WriteLine("***Extension Not Found");
                                break;
                            case "01": //agent login
                                mon = FindByExtension(fields[1]);
                                if (mon == null)
                                {
                                    //mon ekle
                                    ((Mojo.frmMonitorMain)owner).AddAgent(fields[1], fields[2], fields[3]);
                                }
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                {
                                    mon.strAgentId = fields[2];
                                    mon.strAgentPasswd = fields[3];
                                    //mon.LoginAgent();
                                    mon.makeCall(Mojo.AgentCodes.Login + fields[2] + fields[3]);
                                    mon.Status = "LoggedIn";
                                    mon.UpdateListItem();
                                    result = "OK";

                                }
                                break;
                            case "02": //agent logout
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                {
                                    mon.Status = "None";
                                    mon.makeCall(Mojo.AgentCodes.Logout);
                                    mon.strAgentId = "";
                                    mon.strAgentPasswd = "";
                                    mon.UpdateListItem();
                                   ((Mojo.frmMonitorMain)owner).RemoveListItem(mon.lstItem);
                                    mon.Stop();
                                    agents.Remove(mon);
                                }
                                result = "OK";
                                break;
                            case "03": //agent ready
                                sw.WriteLine(DateTime.Today.ToString() + "\tAgentId:" + fields[1] + "\tMode:" + fields[2]);
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                {
                                    //mon.makeCall(Mojo.AgentCodes.Ready);
                                    if (fields[2] == "1")
                                    {
                                        mon.Status = "AutoIn";
                                        mon.makeCall(Mojo.AgentCodes.AutoIn);
                                    }
                                    else
                                    {
                                        mon.Status = "ManualIn";
                                        mon.makeCall(Mojo.AgentCodes.ManualIn);

                                    }
                                    mon.UpdateListItem();


                                }
                                result = "OK";
                                break;
                            case "04": //agent acw, aksada yok
                                /*mon = FindByAgentId(fields[1]);
                                if (mon != null)
                                {
                                    mon.makeCall(Mojo.AgentCodes.ACW);
                                }*/
                                result = "OK";
                                break;
                            case "05": //agent aux
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                {
                                    mon.makeCall(Mojo.AgentCodes.AUX + fields[2]);
                                    //mon.NotReadyAgent(fields[2]);
                                    mon.Status = "AUX";
                                    mon.UpdateListItem();
                                    
                                }
                                result = "OK";
                                break;

                            case "06": //answer call
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                    mon.answerCall();
                                result = "OK";
                                break;
                            case "07": //hangup call
                                mon = FindByExtension(fields[1]);
                                if (mon != null)
                                {
                                    /*   if (owner != null)
                                     {
                                         ((Mojo.frmMonitorMain)owner).ShowCommand(mon);
                                     }
                                 */
                                    mon.hangupCall();
                                }
                                result = "OK";
                                break;
                            case "08":
                            case "09"://hold call
                                mon = FindByExtension(fields[1]);
                                mon.transferMode = false;
                                if (mon != null)
                                {
                                   /* if (owner != null)
                                    {
                                        ((Mojo.frmMonitorMain)owner).ShowCommand(mon);
                                    }
                                    */
                                    mon.holdCall();
                                }
                                result = "OK";
                                break;
                            case "10": //make call
                                mon = FindByExtension(fields[1]);
                                mon.makeCall(fields[2]);
                                result = "OK";
                                break;
                            case "11": //transfer call
                                mon = FindByExtension(fields[1]);
                                mon.holdCall();
                                mon.transferMode = true;
                                mon.makeCall(fields[2]);
                                result = "OK";
                                break;
                            case "20": //transfer call
                                mon = FindByExtension(fields[1]);
                                mon.holdCall();
                                mon.transferMode = true;
                                mon.makeCall(fields[2]);
                                result = "OK";
                                break;
                            case "12": //caller Id
                                mon = FindByExtension(fields[1]);
                                if (mon.line1.callId != 0)
                                    result = mon.line1.callerId;
                                else
                                    if (mon.line2.callId != 0)
                                        result = mon.line2.callerId;
                                break;
                            case "13": //caller Id by extension
                                mon = FindByExtension(fields[1]);
                                if (mon.line1.callId != 0)
                                    result = mon.line1.callerId;
                                else
                                    if (mon.line2.callId != 0)
                                        result = mon.line2.callerId;
                                break;
                            default:
                                MessageBox.Show(content);
                                result = "FAIL";
                                break;
                        }

                        mon.UpdateListItem();
                        // Process content
                        // Echo the data back to the client.
                        //  sw.WriteLine(result);
                        if (fields[0] == "00")
                            Send(handler, result);
                    }
                    catch (Exception ex)
                    {
                        sw.WriteLine(ex.ToString());
                    }
                }
                else
                { 
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private  void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private  void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private  Mojo.AgentMonitor FindByExtension(string keyExtension)
        {
            foreach (Mojo.AgentMonitor agt in agents)
                if (agt.strExtension == keyExtension) return agt;

            return null;
        }
        private  Mojo.AgentMonitor FindByAgentId(string keyId)
        {
            foreach (Mojo.AgentMonitor agt in agents)
                if (agt.strAgentId == keyId) return agt;
            return null;
        }

    }
}