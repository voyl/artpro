using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace ArtTechIVRUtils
{
    public partial class AgentStats : System.Web.UI.Page
    {
        string Tarih = "21.11.2014";
        string Tarih2 = "20.11.2014";
        string requestQueryString = "555";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Tarih = DateTime.Today.ToString("dd.MM.yyyy");
                Tarih2 = DateTime.Today.ToString("dd.MM.yyyy");
               LoadAgentData();
            //    LoadAgentSkills(requestQueryString.ToString());

            }
        }
        public void LoadAgentData()
        {
            string result = "";
            /*if (requestQueryString == null || requestQueryString == "")
            {
                result = "Agent kayıtlı değil. Lütfen önce giriş yapın.";
            }
            else
            {*/
                if (Tarih2 == "")
                    Tarih2 = Tarih;

                //ORIGINAL//System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=sql;Password=sql1234;Initial Catalog=tbScript;Data Source=.\SQL2005EXPRESS");
                /*System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("select MAX(LASTUPDATE) as LASTUPDATE2, SUM(cast(ACDCALLS as int)) as ACDCALLS2, SUM(cast(HOLDCALLS as int)) as HOLDCALLS2, SUM(cast(TACDTIME as int)) as TACDTIME2, SUM(cast(THOLDTIME as int)) as THOLDTIME2 , sum(cast ( TACWTIME as int)) as TACWTIME2, sum(cast ( TACWCALLTIME as int)) as TACWCALLTIME2,  "
                    + " SUM(cast(TAUXTIME1 as int)) as TAUXTIME12, SUM(cast(TAUXTIME2 as int)) as TAUXTIME22, SUM(cast(TAUXTIME1 as int)) + SUM(cast(TAUXTIME2 as int)) as TAUXTIMEALL, SUM(cast(TAUXTIME5 as int)) as TAUXTIME52,SUM(cast(SYSTEMACW as int)) as SYSTEMACW2, SUM(cast(MANUALACW as int)) as MANUALACW2, SUM(cast(TACDTIME as int))/dbo.InlineMax(1, SUM(cast(ACDCALLS as int))) as AACDTIME2, SUM(cast(THOLDTIME as int))/dbo.InlineMax(1, SUM(cast(HOLDCALLS as int))) as AHOLDTIME2, SUM(cast(TSTAFFTIME as int)) as TSTAFFTIME2 "
                    + " from AGENTSTATS WHERE LOGID='" + requestQueryString + "' AND convert(datetime, ROW_DATE, 104) between convert(datetime, '" + Tarih + "',104) and convert(datetime, '" + Tarih2 + "', 104)", _sqlConn);
                */
                
                
                //select * from AGENTSTATS WHERE LOGID=" + requestQueryString + " AND ROW_DATE='" + Tarih+ "'", _sqlConn);


                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("select * from AGENTSTATS WHERE LOGID='"+requestQueryString+"'", _sqlConn);
            _sqlConn.Open();
               
                sqlCmd.CommandType = CommandType.Text;

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("", _sqlConn);
                da.SelectCommand = sqlCmd;
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                }
                {


                    result = "<table>"; // <p><b><i>Bu bölüm halen güncellenmektedir.</i></b></p>";
                    //result += "<tr><th colspan=2>Son Veri Güncelleme: " + dt.Rows[0]["LASTUPDATE2"].ToString() + "</th></tr>";
                    result += "<tr><th>" + "Gelen Çağrı" + "</th><td>" + dt.Rows[0]["ACDCALLS"].ToString() + "</td></tr>";
                    result += "<tr><th>" + "TEST" + "</th><td>" + dt.Rows[0]["TEST_HOLDER"].ToString() + "</td></tr>";
                    result += "<tr><th>" + "Top. Çağrı S." + "</th><td>" + TimespanStr(dt.Rows[0]["TACDTIME"].ToString()) + "</td></tr>";
                    //result += "<tr><th>" + "Ort. Çağrı S." + "</th><td>" + TimespanStr(dt.Rows[0]["AACDTIME2"].ToString()) + "</td></tr>";
                    result += "<tr><th>" + "Bekletilen Çağrı" + "</th><td>" + dt.Rows[0]["HOLDCALLS"].ToString() + "</td></tr>";
                    //result += "<tr><th>" + "Ort. Bekletme S." + "</th><td>" + TimespanStr(dt.Rows[0]["AHOLDTIME2"].ToString()) + "</td></tr>";

                    //result += "<tr><th>" + "Yemek S." + "</th><td>" + TimespanStr(dt.Rows[0]["TAUXTIME12"].ToString()) + " (" + DurationPercentage (dt.Rows[0]["TAUXTIME12"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    //result += "<tr><th>" + "Mola S." + "</th><td>" + TimespanStr(dt.Rows[0]["TAUXTIME22"].ToString()) + " (" + DurationPercentage (dt.Rows[0]["TAUXTIME22"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    //result += "<tr><th>" + "Toplam Mola S." + "</th><td>" + TimespanStr(dt.Rows[0]["TAUXTIMEALL"].ToString()) + " (" + DurationPercentage(dt.Rows[0]["TAUXTIMEALL"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    //result += "<tr><th>" + "Sistem ACW" + "</th><td>" + TimespanStr(dt.Rows[0]["SYSTEMACW2"].ToString()) + " (" + DurationPercentage(dt.Rows[0]["SYSTEMACW2"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    //result += "<tr><th>" + "Manual ACW" + "</th><td>" + TimespanStr(dt.Rows[0]["MANUALACW2"].ToString()) + " (" + DurationPercentage(dt.Rows[0]["MANUALACW2"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    //result += "<tr><th>" + "Çağrı ACW" + "</th><td>" + TimespanStr(dt.Rows[0]["TACWCALLTIME2"].ToString()) + " (" + DurationPercentage(dt.Rows[0]["TACWCALLTIME2"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    //result += "<tr><th>" + "Toplantı S." + "</th><td>" + TimespanStr(dt.Rows[0]["TAUXTIME52"].ToString()) + " (" + DurationPercentage(dt.Rows[0]["TAUXTIME52"].ToString(), dt.Rows[0]["TSTAFFTIME2"].ToString()) + "%)</td></tr>";
                    result += "<tr><th>" + "Çalışma S." + "</th><td>" +TimespanStr( dt.Rows[0]["TSTAFFTIME"].ToString()) + "</td></tr>";
                    //result += "<tr><th>" + "Skill Bilgisi:" + "</th><td>" + LoadAgentSkills(requestQueryString.ToString()) +"</td></tr>";
                    //result += "<tr><th>" + "Test Bilgisi:" + "</th><td>" + dt.Rows[0]["Test"].ToString() + "</td></tr>";
                    //  result += "<tr><th>" + "Online Agent"+ "</th><td>" + "0" + "</td></tr>";
                    //  result += "<tr><th>" + "Aktif Agent"+ "</th><td>" + "0" + "</td></tr>";
                    //  result += "<tr><th>" + "Molada Agent"+ "</th><td>" + "0" + "</td></tr>";
                    //       result += "<tr><th colspan=2>Son güncelleme: " + DateTime.Now.ToString() + "</th></tr>";
                    result += "</table>";

               // }
                Response.Write(result);
                _sqlConn.Close();
            }
            divContent.InnerHtml = result;
        }

        public string DurationPercentage(string duration, string total)
        {
            int iduration = 0, itotal = 1;
            try
            {
                iduration = int.Parse(duration);
            }
            catch (Exception ex)
            {
                iduration = 0;
            }
            try
            {
                itotal = int.Parse(total);
            }
            catch (Exception ex)
            {
                itotal = 1;
                
            }
            return String.Format("{0:0.00}", Math.Round( ((double)iduration * 100.0 / (double)itotal), 2)).Replace(",",".");

        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgentStats.aspx", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadAgentData();
        }
        public string TimespanStr(object value)
        {
          if (value == null) return "00:00:00";

          try
          {

              int seconds = int.Parse(value.ToString());

              TimeSpan span = new TimeSpan(0, 0, seconds);
              return string.Format("{0:00}:{1:00}:{2:00}", span.Hours + span.Days * 24, span.Minutes, span.Seconds);
          }
          catch
          {
              return "0";
          }
        }

        protected string  LoadAgentSkills(string agentId)
        {

            string path = @"c:\AVAYA\agent_skill.txt";
            string path2 = @"c:\AVAYA\skills.txt";
            Dictionary<string, string> skills = new Dictionary<string, string>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path2);
            string line = "";
            string[] fields = null;
            while (sr.Peek() != -1)
            {
                line = sr.ReadLine().Trim();
                if (line == "") continue;

                fields = line.Split(';');
                if (fields.Length < 2) continue;
                skills.Add(fields[0], fields[1]);
            }

            
            sr.Close();

            string result = "";

            sr = new System.IO.StreamReader(path);

            while (sr.Peek() != -1)
            {
                line = sr.ReadLine().Trim();
                if (line == "") continue;

                if (!line.StartsWith(agentId)) continue;
                fields = line.Split(';');
                if (fields.Length < 3) continue;
                for (int i = 1; i < fields.Length -1; i=i+2)
                {
                    try
                    {
                        
                         result += skills[fields[i]] +", ";
                            }
                            catch (Exception ex)
                            {
                            }
                        
                }
                break;
            }

            if (result.EndsWith(",")) result = result.Remove(result.Length - 1).Replace(",",", ");
            sr.Close();
            
            return result;

        }
    
    }
}