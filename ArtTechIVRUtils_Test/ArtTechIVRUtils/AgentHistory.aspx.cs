using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace ArtTechIVRUtils
{
    public partial class AgentHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Timer1.Enabled  = true;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string result = "";
            /*if (requestQueryString == null || requestQueryString == "")
            {
                result = "Agent kayıtlı değil. Lütfen önce giriş yapın.";
            }
            else
            {*/

                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=sql;Password=sql1234;Initial Catalog=tbScript;Data Source=.\SQL2005EXPRESS");
                _sqlConn.Open();
                //ORIGINAL//System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("SELECT CALLERID, STATIONID, CALLTIME, DURATION, SKILL FROM CALLRECORD WHERE     (AGENTID = " + requestQueryString + ") AND (CONVERT(datetime, ROW_DATE, 104) > GETDATE() - 1) AND (CALLERID <> '') AND (CALLERID NOT LIKE '%V%') ORDER BY CALLTIME DESC", _sqlConn);
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("Select * from AGENTSTATS",_sqlConn);
                sqlCmd.CommandType = CommandType.Text;

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("", _sqlConn);
                da.SelectCommand = sqlCmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                }
                result = "<div style=\"font-family: courier new; font-size: 12px;\"><p><b><i>Tarih:"+DateTime.Today.ToString("dd.MM.yyyy")+"</i></b></p><table border=0 width=100%>";
                //ORIGINAL//result += "<tr><th>Saat</th><th>&nbsp;</th><th>Arayan</th><th>Aranan</th><th>Kuyruk</th><th>Süre</th></tr>";
                result += "<tr><th>HOLDCALLS</th><th>ROWDATE</th><th>TACDTIME</th><th>THOLDTIME</th><th>TACWTIME</th><th>TSTAFFTIME</th><th>TEST</th></tr>";
                foreach (DataRow dr in dt.Rows)
                {
                    //ORIGINAL//result += "<tr><td>" + dr["CALLTIME"].ToString().PadLeft(4,'0').Insert(2,":") + "</td><td>"+(dr["CALLERID"].ToString().Length <= 5 ? "<img src='out.png' alt='Dış Arama'/>" : "<img src='in.jpg' alt='Gelen Çağrı'/>" ) +"</td><td>" + dr["CALLERID"].ToString()+ "</td><td>" + dr["STATIONID"].ToString() + "</td><td>" + dr["SKILL"].ToString()+ "</td><td>" + dr["DURATION"].ToString()+ "</td></tr>";
                    result += "<tr><td>" + dr["HOLDCALLS"].ToString()+"</td><td>"+dr["ROW_DATE"].ToString() + "</td><td>" + dr["TACDTIME"].ToString() + "</td><td>" + dr["THOLDTIME"] + "</td><td>" + dr["TACWTIME"] + "</td><td>" + dr["TSTAFFTIME"] +"</td><td>"+dr["TEST_HOLDER"].ToString()+ "</td></tr>";
                }
                result += "</table></div>";

                _sqlConn.Close();
            //}
            divContent.InnerHtml =  (result);
        }
    }
}