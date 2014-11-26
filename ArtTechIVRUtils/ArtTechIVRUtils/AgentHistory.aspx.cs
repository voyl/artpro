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
            if (Request.QueryString["agentId"] == null || Request.QueryString["agentId"] == "")
            {
                result = "Agent kayıtlı değil. Lütfen önce giriş yapın.";
            }
            else
            {

                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                _sqlConn.Open();
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("SELECT CALLERID, STATIONID, CALLTIME, DURATION, SKILL FROM CALLRECORD WHERE     (AGENTID = " + Request.QueryString["agentId"] + ") AND (CONVERT(datetime, ROW_DATE, 104) > GETDATE() - 1) AND (CALLERID <> '') AND (CALLERID NOT LIKE '%V%') ORDER BY CALLTIME DESC", _sqlConn);

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
                result += "<tr><th>Saat</th><th>&nbsp;</th><th>Arayan</th><th>Aranan</th><th>Kuyruk</th><th>Süre</th></tr>";
                foreach (DataRow dr in dt.Rows)
                {

                    result += "<tr><td>" + dr["CALLTIME"].ToString().PadLeft(4,'0').Insert(2,":") + "</td><td>"+(dr["CALLERID"].ToString().Length <= 5 ? "<img src='out.png' alt='Dış Arama'/>" : "<img src='in.jpg' alt='Gelen Çağrı'/>" ) +"</td><td>" + dr["CALLERID"].ToString()+ "</td><td>" + dr["STATIONID"].ToString() + "</td><td>" + dr["SKILL"].ToString()+ "</td><td>" + dr["DURATION"].ToString()+ "</td></tr>";

                }
                result += "</table></div>";

                _sqlConn.Close();
            }
            divContent.InnerHtml =  (result);
        }
    }
}