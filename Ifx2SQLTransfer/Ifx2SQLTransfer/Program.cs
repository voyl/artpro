using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBM.Data;
using IBM.Data.Informix;
using System.Data;
namespace Ifx2SQLTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            AgentStatsEndOfDay();
          //  Calls();
           // AgentStats3();
           // ACDGroups();
         //Console.ReadLine();
         //   IntervalBackup();
           // Console.ReadLine();
        }
        static void Calls()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;";
            //string _connStr = "Database=cms;Server=cms_net;Host=192.168.40.210;Service=50001;UID=database;Password=database12;";
            // string _connStr = "DSN=ARTTECH;Uid=database;Pwd=database12;";
            // string _connStr = "DSN=KGSCMS;Uid=teknik;Pwd=Teknik12;";

            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select *  from call_rec "
                + " WHERE (ROW_DATE BETWEEN '" + DateTime.Today.Date.AddDays(-1).ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.AddDays(-1).ToString("MM/dd/yy") + "') and (calling_pty like '%5448832244%')"
                , _connection);
                System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                 System.IO.StreamWriter sw = new System.IO.StreamWriter("temp.txt", false);
                System.Data.DataTable dt = new System.Data.DataTable();


                da.Fill(dt);

               // System.Data.Odbc.OdbcDataReader rdr = cmd.ExecuteReader();

                //System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                //_sqlConn.Open();

                //System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from CALLRECORD WHERE ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy HH:mm:ss") + "'", _sqlConn);

                //sqlCmd.CommandType = CommandType.Text;
                
                

                // System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\vdns.txt");
                string skill = "";
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {

                    foreach (DataColumn dc in dt.Columns)
                        sw.WriteLine(dc.ColumnName +":"+dr[dc.ColumnName].ToString().Trim());
                    
                }
                _connection.Close();
                 sw.Close();
                Console.WriteLine("Done");
            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));
            }


        }

        static void AbandonedCalls()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;"; 
            //string _connStr = "Database=cms;Server=cms_net;Host=192.168.40.210;Service=50001;UID=database;Password=database12;";
           // string _connStr = "DSN=ARTTECH;Uid=database;Pwd=database12;";
           // string _connStr = "DSN=KGSCMS;Uid=teknik;Pwd=Teknik12;";
            
            try
            {
                _connection = new  System.Data.Odbc.OdbcConnection(_connStr);
                
                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select *  from call_rec "
                + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.ToString("MM/dd/yy") + "'"
                , _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

               // System.IO.StreamWriter sw = new System.IO.StreamWriter("temp.csv", false);
                System.Data.DataTable dt = new System.Data.DataTable();


                //da.Fill(dt);

                System.Data.Odbc.OdbcDataReader rdr = cmd.ExecuteReader();

                   System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                    _sqlConn.Open();
                    
                    System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from CALLRECORD WHERE ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy HH:mm:ss") + "'", _sqlConn);

                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.ExecuteNonQuery();
                  
                sqlCmd.CommandText = "delete from CALLRECORD where LASTUPDATE < GETDATE() -2";
                  sqlCmd.ExecuteNonQuery();
                      
                
               // System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\vdns.txt");
                string skill = "";
                int i = 0;
                    while (rdr.Read())
                    {
                        switch (rdr["DISPSPLIT"].ToString().Trim())
                        {
                            case "3":
                                skill = "CORUH";
                                break;
                            case "4":
                                skill = "CORUH_ING";
                                break;
                            case "20":
                                skill = "DGAZ444";
                                break;
                            case "10":
                                skill = "ELEK_SATIS";
                                break;
                            case "23":
                                skill = "FIRAT";
                                break;
                            case "21":
                                skill = "GAZ 187";
                                break;
                            case "8":
                                skill = "JEN KIRALAMA";
                                break;
                            case "80":
                                skill = "DIS ARAMA";
                                break;
                            case "7":
                                skill = "JENERATOR";
                                break;
                            default: skill = "";
                                break;
                        }

                        //sqlCmd.CommandText = "DELETE FROM CALLRECORD WHERE CALLID='"+ rdr["UCID"].ToString() +"'";
                       // sqlCmd.ExecuteNonQuery();
                        sqlCmd.CommandText = "INSERT INTO [cms_collect].[dbo].[CALLRECORD] ([ROW_DATE], [CALLID] ,[AGENTID] ,[STATIONID] ,[CALLERID] ,[DURATION]"
           + ",[CALLTIME], SKILL) VALUES  ('" + rdr["ROW_DATE"].ToString().Trim() + "','" + rdr["UCID"].ToString().Trim() + "','" + rdr["ANSLOGIN"].ToString().Trim() + "','" + rdr["DIALED_NUM"].ToString().Trim() + "','" + rdr["CALLING_PTY"].ToString().Trim() + "','" + TimespanStr(rdr["DURATION"].ToString().Trim()) + "', '" + rdr["ROW_TIME"].ToString().Trim() + "','" + skill + "')";
                        sqlCmd.ExecuteNonQuery();
                        //Console.WriteLine("{0}\t{1}\t{2}", rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString());
                        
                        //sw.WriteLine(rdr[0].ToString()+";"+rdr[0].ToString()+";"+rdr[0].ToString());
                        i++;
                }


                //    sw.Close();

                _connection.Close();
               // sw.Close();
                Console.WriteLine("Done");
            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));
            }

                 
        }

        static void AgentStats2()
        {
            System.Data.Odbc.OdbcConnection _connection;
            //string _connStr = "Database=cms;Server=cms_net;Host=192.168.40.210;Service=50001;UID=database;Password=database12;";
            // string _connStr = "DSN=ARTTECH;Uid=database;Pwd=database12;";
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;";

            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select  '" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "', LOGID, SUM(TI_STAFFTIME) TSTAFFTIME, SUM(TI_AVAILTIME) TAVAILTIME, SUM(TI_AUXTIME) TAUXTIME, SUM(ACDTIME) TACDTIME ,SUM(ACWTIME) TACWTIME,  round(sum(ACDTIME)/ (case when sum(ACDCALLS) = 0 then 1 else sum(ACDCALLS) END)) AS AACDTIME, SUM(ACDCALLS) as ACDCALLS, SUM(HOLDCALLS) as HOLDCALLS, SUM(HOLDTIME) AS THOLDTIME, ROUND(SUM(HOLDTIME) / (case when sum(HOLDCALLS) = 0 then 1 else sum(HOLDCALLS) END)) as AHOLDTIME, SUM(RINGTIME) as TRINGTIME from hagent "
                 + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.ToString("MM/dd/yy") + "'"
                 + " GROUP BY LOGID order by LOGID ", _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                // System.IO.StreamWriter sw = new System.IO.StreamWriter("temp.csv", false);
                System.Data.DataTable dt = new System.Data.DataTable();


                //da.Fill(dt);

                System.Data.Odbc.OdbcDataReader rdr = cmd.ExecuteReader();

                /* System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=arttech;Password=arttech12;Initial Catalog=cms_collect;Data Source=172.20.7.59");
                  _sqlConn.Open();
                    
                  System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from CALLRECORD WHERE ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy HH:mm:ss") + "'", _sqlConn);

                  sqlCmd.CommandType = CommandType.Text;
                  sqlCmd.ExecuteNonQuery();
              */
               // System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\vdns.txt");
                int i = 0;
                while (rdr.Read() && i < 100)
                {
                    //  sqlCmd.CommandText = "DELETE FROM CALLRECORD WHERE CALLID='"+ rdr["UCID"].ToString() +"'";
                    //  sqlCmd.ExecuteNonQuery();
                    //      sqlCmd.CommandText = "INSERT INTO [cms_collect].[dbo].[CALLRECORD] ([ROW_DATE], [CALLID] ,[AGENTID] ,[STATIONID] ,[CALLERID] ,[DURATION]"
                    //+ ",[CALLTIME]) VALUES  ('" + rdr["ROW_DATE"].ToString() + "','" + rdr["UCID"].ToString() + "','" + rdr["ANSLOGIN"].ToString() + "','" + rdr["DIALED_NUM"].ToString() + "','" + rdr["CALLING_PTY"].ToString() + "','" + TimespanStr(rdr["DURATION"].ToString()) + "', '" + rdr["ROW_TIME"].ToString()+ "')";
                    //    sqlCmd.ExecuteNonQuery();
                    //Console.WriteLine("{0}\t{1}\t{2}", rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString());

                 //   sw.WriteLine(rdr[0].ToString() + ";" + rdr[1].ToString() + ";" + rdr[2].ToString());
                    i++;
                }


              //  sw.Close();

                _connection.Close();
                // sw.Close();
            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));
            }


        }
        static void ACDGroups()
        {
            System.Data.Odbc.OdbcConnection _connection;
            //string _connStr = "Database=cms;Server=cms_net;Host=192.168.40.210;Service=50001;UID=database;Password=database12;";
            // string _connStr = "DSN=ATECHCMS;Uid=database;Pwd=database12;";
            string _connStr = "DSN=MEMORIALCMS;Uid=dbuser;Pwd=dbuser12;";


            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select  * from hsplit WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.ToString("MM/dd/yy") + "'", _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;
                System.Data.Odbc.OdbcDataReader dr = cmd.ExecuteReader();
                //System.Data.DataTable dt = new System.Data.DataTable();
                //da.Fill(dt);

                /*
                 * 
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=arttech;Password=arttech12;Initial Catalog=cms_collect;Data Source=AVAYA-EMC\SQLEXPRESSR2");
                _sqlConn.Open();
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from AGENTSTATS WHERE ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "'", _sqlConn);

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();
                */
                while (dr.Read())
                {
                    Console.WriteLine(dr[0].ToString() + ":" + dr[1].ToString() + ":" + dr[2].ToString() + ":" + dr[3].ToString() + ":" + dr[4].ToString() + ":" + dr[5].ToString() + ":" + dr[6].ToString());
                  
                }


                /*foreach (DataColumn dc in dt.Columns)
                {

                    Console.Write(dc.ColumnName + "\t");
                }
                Console.WriteLine ("");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    foreach (DataColumn dc in dt.Columns)
                    {

                        Console.Write(dr[dc.Ordinal].ToString() + "\t");

                        
                    }
                    Console.WriteLine("");
                    if (i > 20) break;
                }
                */
                dr.Close();
              
                _connection.Close();

                Console.WriteLine(String.Format("Done"));

            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));

            }


        }


        static void AgentStats()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;"; 
            
            
            try
            {
                _connection = new  System.Data.Odbc.OdbcConnection(_connStr);
                
                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select  '" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "', LOGID, SUM(Ti_STAFFTIME) TSTAFFTIME, SUM(i_AVAILTIME) TAVAILTIME, SUM(TI_AUXTIME1) TAUXTIME1, SUM(TI_AUXTIME2) TAUXTIME2, SUM(TI_AUXTIME5) TAUXTIME5, SUM(i_ACDTIME) TACDTIME ,SUM(i_ACWTIME) TACWTIME, SUM(TI_AUXTIME7) TAUXTIME7,   round(sum(i_ACDTIME)/ (case when sum(ACDCALLS) = 0 then 1 else sum(ACDCALLS) END)) AS AACDTIME, SUM(ACDCALLS) as ACDCALLS, SUM(HOLDCALLS) as HOLDCALLS, SUM(HOLDTIME) AS THOLDTIME, ROUND(SUM(HOLDTIME) / (case when sum(HOLDCALLS) = 0 then 1 else sum(HOLDCALLS) END)) as AHOLDTIME, SUM(RINGTIME) as TRINGTIME, sum(acwoutcalls) as ACWCALLS, sum(acwouttime) as ACWCALLTIME from hagent "
                + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.ToString("MM/dd/yy") + "'"
                + " GROUP BY LOGID order by LOGID ", _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                //System.Data.DataTable dt = new System.Data.DataTable();
                //da.Fill(dt);

                System.Data.Odbc.OdbcDataReader dr = cmd.ExecuteReader();
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                _sqlConn.Open();
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from AGENTSTATS WHERE ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "'", _sqlConn);

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();

                while (dr.Read())
                {
                    sqlCmd.CommandText = "INSERT INTO [dbo].[AGENTSTATS] ([ROW_DATE] ,[LOGID] ,[ACDCALLS] ,[HOLDCALLS] ,[TACDTIME] ,[THOLDTIME] ,[TACWTIME] ,[TRINGTIME] ,[TSTAFFTIME],[TAUXTIME1],[TAUXTIME2] ,[TAUXTIME5] ,[AACDTIME] ,[AHOLDTIME], [TAUXTIME7],[TACWCALLS], [TACWCALLTIME]) "
                        + " VALUES ('" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "'," + dr["logid"].ToString() + "," + dr["ACDCALLS"].ToString() + "," + dr["HOLDCALLS"].ToString() +
                        ",'" + TimespanStr(dr["TACDTiME"]) + "','" + TimespanStr(dr["THOLDTiME"]) + "','" + TimespanStr(dr["TACWTiME"]) + "','" + TimespanStr(dr["TRiNGTiME"]) + "' ,'" + TimespanStr(dr["TSTAFFTiME"]) + "','" + TimespanStr(dr["TAUXTiME1"]) + "','" + TimespanStr(dr["TAUXTiME2"]) + "' ,'" + TimespanStr(dr["TAUXTiME5"]) + "','" + TimespanStr(dr["AACDTiME"]) + "','" + TimespanStr(dr["AHOLDTiME"]) + "','" + TimespanStr(dr["TAUXTIME7"]) + "'," + dr["ACWCALLS"].ToString() + ",'" + dr["ACWCALLTIME"].ToString() + "')";
                    sqlCmd.ExecuteNonQuery();
                }


                sqlCmd.CommandText = "update AGENTSTATS set SYSTEMACW = ACDCALLS *3, MANUALACW = TACWTIME -  ACDCALLS * 3 -  TACWCALLTIME where ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "'";

                sqlCmd.ExecuteNonQuery();

                sqlCmd.CommandText = "update AGENTSTATS set MANUALACW = 0 where ROW_DATE='" + DateTime.Today.Date.ToString("dd.MM.yyyy") + "' AND MANUALACW < 0 ";

                sqlCmd.ExecuteNonQuery();
                
              
                /*foreach (DataColumn dc in dt.Columns)
                {

                    Console.Write(dc.ColumnName + "\t");
                }
                Console.WriteLine ("");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    foreach (DataColumn dc in dt.Columns)
                    {

                        Console.Write(dr[dc.Ordinal].ToString() + "\t");

                        
                    }
                    Console.WriteLine("");
                    if (i > 20) break;
                }
                */
                dr.Close();
                _sqlConn.Close();
                
                _connection.Close();

                Console.WriteLine(String.Format("Done"));
                
            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));
                
            }

                 
        }

        static void IntervalInfo()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;";

            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select first 1 * from hagent "
                 + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.ToString("MM/dd/yy") + "'"
                 , _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                System.IO.StreamWriter sw = new System.IO.StreamWriter("temp.txt", false);
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd.CommandText, _connection);
                da.Fill(dt);


                //da.Fill(dt);

                // cmd.CommandText = "select * from hagent";
                cmd.CommandText = "select * from hagent WHERE ROW_DATE BETWEEN '" + DateTime.Today.AddDays(-1).Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.AddDays(-1).Date.ToString("MM/dd/yy") + "'";


                System.Data.Odbc.OdbcDataReader rdr = cmd.ExecuteReader();
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                _sqlConn.Open();

                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("", _sqlConn);

                sqlCmd.CommandType = CommandType.Text;


                string sql1 = "", sql2 = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    sql1 += dc.ColumnName + ",";

                }
                Console.WriteLine(sql1);
                sql1 = sql1.Remove(sql1.Length - 1);
                while (rdr.Read())
                {
                    sql2 = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sql2 += "'" + rdr[dc.ColumnName] + "',";

                    }
                    sql2 = sql2.Remove(sql2.Length - 1);
                    sqlCmd.CommandText = "insert into HAGENT (" + sql1 + ") values (" + sql2 + ")";
                    try
                    {
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                }


                //sw.Close();

                _connection.Close();
                // sw.Close();
            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));
            }


        }
        static void AgentStatsEndOfDay()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;";


            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select  '" + DateTime.Today.Date.AddDays(-1).ToString("dd.MM.yyyy") + "', LOGID, SUM(Ti_STAFFTIME) TSTAFFTIME, SUM(i_AVAILTIME) TAVAILTIME, SUM(TI_AUXTIME1) TAUXTIME1, SUM(TI_AUXTIME2) TAUXTIME2, SUM(TI_AUXTIME5) TAUXTIME5, SUM(i_ACDTIME) TACDTIME ,SUM(i_ACWTIME) TACWTIME, SUM(TI_AUXTIME7) TAUXTIME7,   round(sum(i_ACDTIME)/ (case when sum(ACDCALLS) = 0 then 1 else sum(ACDCALLS) END)) AS AACDTIME, SUM(ACDCALLS) as ACDCALLS, SUM(HOLDCALLS) as HOLDCALLS, SUM(HOLDTIME) AS THOLDTIME, ROUND(SUM(HOLDTIME) / (case when sum(HOLDCALLS) = 0 then 1 else sum(HOLDCALLS) END)) as AHOLDTIME, SUM(RINGTIME) as TRINGTIME, sum(acwoutcalls) as ACWCALLS, sum(acwouttime) as ACWCALLTIME from hagent "
                + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.AddDays(-1).ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.AddDays(-1).ToString("MM/dd/yy") + "'"
                + " GROUP BY LOGID order by LOGID ", _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                //System.Data.DataTable dt = new System.Data.DataTable();
                //da.Fill(dt);

                System.Data.Odbc.OdbcDataReader dr = cmd.ExecuteReader();
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                _sqlConn.Open();
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from AGENTSTATS WHERE ROW_DATE='" + DateTime.Today.Date.AddDays(-1).ToString("dd.MM.yyyy") + "'", _sqlConn);

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.ExecuteNonQuery();

                while (dr.Read())
                {
                    sqlCmd.CommandText = "INSERT INTO [dbo].[AGENTSTATS] ([ROW_DATE] ,[LOGID] ,[ACDCALLS] ,[HOLDCALLS] ,[TACDTIME] ,[THOLDTIME] ,[TACWTIME] ,[TRINGTIME] ,[TSTAFFTIME],[TAUXTIME1],[TAUXTIME2] ,[TAUXTIME5] ,[AACDTIME] ,[AHOLDTIME], [TAUXTIME7],[TACWCALLS], [TACWCALLTIME]) "
                        + " VALUES ('" + DateTime.Today.Date.AddDays(-1).ToString("dd.MM.yyyy") + "'," + dr["logid"].ToString() + "," + dr["ACDCALLS"].ToString() + "," + dr["HOLDCALLS"].ToString() +
                        ",'" + TimespanStr(dr["TACDTiME"]) + "','" + TimespanStr(dr["THOLDTiME"]) + "','" + TimespanStr(dr["TACWTiME"]) + "','" + TimespanStr(dr["TRiNGTiME"]) + "' ,'" + TimespanStr(dr["TSTAFFTiME"]) + "','" + TimespanStr(dr["TAUXTiME1"]) + "','" + TimespanStr(dr["TAUXTiME2"]) + "' ,'" + TimespanStr(dr["TAUXTiME5"]) + "','" + TimespanStr(dr["AACDTiME"]) + "','" + TimespanStr(dr["AHOLDTiME"]) + "','" + TimespanStr(dr["TAUXTIME7"]) + "'," + dr["ACWCALLS"].ToString() + ",'" + dr["ACWCALLTIME"].ToString() + "')";
                    sqlCmd.ExecuteNonQuery();
                }


                sqlCmd.CommandText = "update AGENTSTATS set SYSTEMACW = ACDCALLS *3, MANUALACW = TACWTIME -  ACDCALLS * 3 -  TACWCALLTIME where ROW_DATE='" + DateTime.Today.Date.AddDays(-1).ToString("dd.MM.yyyy") + "'";

                sqlCmd.ExecuteNonQuery();

                sqlCmd.CommandText = "update AGENTSTATS set MANUALACW = 0 where ROW_DATE='" + DateTime.Today.Date.AddDays(-1).ToString("dd.MM.yyyy") + "' AND MANUALACW < 0 ";

                sqlCmd.ExecuteNonQuery();


                /*foreach (DataColumn dc in dt.Columns)
                {

                    Console.Write(dc.ColumnName + "\t");
                }
                Console.WriteLine ("");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    foreach (DataColumn dc in dt.Columns)
                    {

                        Console.Write(dr[dc.Ordinal].ToString() + "\t");

                        
                    }
                    Console.WriteLine("");
                    if (i > 20) break;
                }
                */
                dr.Close();
                _sqlConn.Close();

                _connection.Close();

                Console.WriteLine(String.Format("Done"));

            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));

            }


        }


        static void AgentStats3()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;";


            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select  ROW_DATE, LOGID, SUM(Ti_STAFFTIME) TSTAFFTIME, SUM(i_AVAILTIME) TAVAILTIME, SUM(TI_AUXTIME1) TAUXTIME1, SUM(TI_AUXTIME2) TAUXTIME2, SUM(TI_AUXTIME5) TAUXTIME5, SUM(i_ACDTIME) TACDTIME ,SUM(i_ACWTIME) TACWTIME, SUM(TI_AUXTIME7) TAUXTIME7,   round(sum(i_ACDTIME)/ (case when sum(ACDCALLS) = 0 then 1 else sum(ACDCALLS) END)) AS AACDTIME, SUM(ACDCALLS) as ACDCALLS, SUM(HOLDCALLS) as HOLDCALLS, SUM(HOLDTIME) AS THOLDTIME, ROUND(SUM(HOLDTIME) / (case when sum(HOLDCALLS) = 0 then 1 else sum(HOLDCALLS) END)) as AHOLDTIME, SUM(RINGTIME) as TRINGTIME, sum(acwoutcalls) as ACWCALLS, sum(acwouttime) as ACWCALLTIME from hagent "
                + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.Date.AddDays(-5).ToString("MM/dd/yy") + "' and '" + DateTime.Today.Date.ToString("MM/dd/yy") + "'"
                + " GROUP BY ROW_DATE, LOGID order by ROW_DATE, LOGID ", _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                //System.Data.DataTable dt = new System.Data.DataTable();
                //da.Fill(dt);

                System.Data.Odbc.OdbcDataReader dr = cmd.ExecuteReader();
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                _sqlConn.Open();
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from AGENTSTATS", _sqlConn);

                sqlCmd.CommandType = CommandType.Text;
               // sqlCmd.ExecuteNonQuery();

                while (dr.Read())
                {
                    sqlCmd.CommandText = "INSERT INTO [dbo].[AGENTSTATS] ([ROW_DATE] ,[LOGID] ,[ACDCALLS] ,[HOLDCALLS] ,[TACDTIME] ,[THOLDTIME] ,[TACWTIME] ,[TRINGTIME] ,[TSTAFFTIME],[TAUXTIME1],[TAUXTIME2] ,[TAUXTIME5] ,[AACDTIME] ,[AHOLDTIME], [TAUXTIME7],[TACWCALLS], [TACWCALLTIME]) "
                        + " VALUES ('" + dr["ROW_DATE"].ToString() + "'," + dr["logid"].ToString() + "," + dr["ACDCALLS"].ToString() + "," + dr["HOLDCALLS"].ToString() +
                        ",'" + TimespanStr(dr["TACDTiME"]) + "','" + TimespanStr(dr["THOLDTiME"]) + "','" + TimespanStr(dr["TACWTiME"]) + "','" + TimespanStr(dr["TRiNGTiME"]) + "' ,'" + TimespanStr(dr["TSTAFFTiME"]) + "','" + TimespanStr(dr["TAUXTiME1"]) + "','" + TimespanStr(dr["TAUXTiME2"]) + "' ,'" + TimespanStr(dr["TAUXTiME5"]) + "','" + TimespanStr(dr["AACDTiME"]) + "','" + TimespanStr(dr["AHOLDTiME"]) + "','" + TimespanStr(dr["TAUXTIME7"]) + "'," + dr["ACWCALLS"].ToString() + ",'" + dr["ACWCALLTIME"].ToString() + "')";
                    sqlCmd.ExecuteNonQuery();
                }


                /*foreach (DataColumn dc in dt.Columns)
                {

                    Console.Write(dc.ColumnName + "\t");
                }
                Console.WriteLine ("");
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    foreach (DataColumn dc in dt.Columns)
                    {

                        Console.Write(dr[dc.Ordinal].ToString() + "\t");

                        
                    }
                    Console.WriteLine("");
                    if (i > 20) break;
                }
                */
                dr.Close();
                _sqlConn.Close();

                _connection.Close();

                Console.WriteLine(String.Format("Done"));

            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));

            }


        }
        static void IntervalBackup()
        {
            System.Data.Odbc.OdbcConnection _connection;
            string _connStr = "DSN=AKSACMS;Uid=aksadb;Pwd=aksa102030;";

            try
            {
                _connection = new System.Data.Odbc.OdbcConnection(_connStr);

                _connection.Open();
                Console.WriteLine(String.Format("Server Version: {0}", _connection.ServerVersion));

                Console.WriteLine(String.Format("Database: {0}", _connection.Database));
                Console.WriteLine(String.Format("Data Source: {0}", _connection.DataSource));


                System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand("select first 1 * from hagent "
                 + " WHERE ROW_DATE BETWEEN '" + DateTime.Today.AddDays(-90).Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.AddDays(-1).Date.ToString("MM/dd/yy") + "'"
                 , _connection);
                //System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd);
                cmd.CommandType = System.Data.CommandType.Text;

                System.IO.StreamWriter sw = new System.IO.StreamWriter("temp.txt", false);
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.Odbc.OdbcDataAdapter da = new System.Data.Odbc.OdbcDataAdapter(cmd.CommandText, _connection);
                da.Fill(dt);


                //da.Fill(dt);

                // cmd.CommandText = "select * from hagent";
                cmd.CommandText = "select * from hagent WHERE ROW_DATE BETWEEN '" + DateTime.Today.AddDays(-1).Date.ToString("MM/dd/yy") + "' and '" + DateTime.Today.AddDays(-1).Date.ToString("MM/dd/yy") + "'";


                System.Data.Odbc.OdbcDataReader rdr = cmd.ExecuteReader();
                System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
                _sqlConn.Open();

                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("", _sqlConn);

                sqlCmd.CommandType = CommandType.Text;


                string sql1 = "", sql2 = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    sql1 += dc.ColumnName + ",";

                }
                Console.WriteLine(sql1);
                sql1 = sql1.Remove(sql1.Length - 1);
                while (rdr.Read())
                {
                    sql2 = "";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sql2 += "'" + rdr[dc.ColumnName] + "',";

                    }
                    sql2 = sql2.Remove(sql2.Length - 1);
                    sqlCmd.CommandText = "insert into HAGENT (" + sql1 + ") values (" + sql2 + ")";
                    try
                    {
                        sqlCmd.ExecuteNonQuery();
                    }
                    catch
                    {
                    }
                }


                //sw.Close();

                _connection.Close();
                // sw.Close();
            }
            catch (Exception excep)
            {
                Console.WriteLine(String.Format("Connection Error: {0}", excep.Message));
            }


        }
  
        static string TimespanStr(object value)
        {
            if (value == null) return "";

            try
            {
                return int.Parse(value.ToString()).ToString();
            }
            catch { return "0"; }
            //if (value == null) return "00:00:00";

            //int seconds = int.Parse(value.ToString());

            //TimeSpan span = new TimeSpan(0, 0, seconds);
           // return string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
        }
    }
}
