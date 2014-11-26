using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Services;

namespace ArtTechIVRUtils
{
    /// <summary>
    /// Summary description for AksaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AksaService : System.Web.Services.WebService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeviceID">Agent/Extension Number</param>
        /// <param name="Number">Number to call with outgoing prefix</param>
        /// <returns></returns>
        [WebMethod]
        public int Click2Call(string DeviceID, string Number)
        {
            
            System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n c " + DeviceID + " " + Number);


            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number">Number to call</param>
        /// <param name="Type">Type of alarm announce</param>
        /// <returns></returns>
        [WebMethod]
        public int AlarmUyari(string Number, string Type)
        {
            System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=artpro;Data Source=CALLPHONE\SQLEXPRESS");
            _sqlConn.Open();
            try
            {
                AsynchronousComm.AsynchronousClient client = new AsynchronousComm.AsynchronousClient();
                string result = client.StartClient("00:" + Number);
                string ext = result.Split(':')[1];
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("insert into ALARMLOG (CALLEDDN, ALARMTYPE, ALARMEXTENSION) values ('" + Number + "','" + Type + "','"+ext+"')", _sqlConn);
                sqlCmd.ExecuteNonQuery();

                result =  result.Split(':')[0];
                if (result == "OK") return 1;
                else if (result == "BUSY") return 0;
                else return -1;
            }
            catch
            {
                return -1;
            }
            finally
            {
                _sqlConn.Close();
            }
                
        }
    }
}
