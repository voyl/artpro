using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace ArtTechIVRUtils
{
    /// <summary>
    /// Web servis logu eklenecek.
    /// </summary>
    [WebService(Namespace = "http://172.20.7.59/ArtTechUtils/Utils.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Utils : System.Web.Services.WebService
    {
        [WebMethod]
        public string[] ReadDigits(string value)
        {
            List<string> result = new List<string>();

                  
                foreach (char c in value)
                {
                    if (char.IsDigit(c))
                    {
                        result.Add("http://172.20.7.59/anons/" + c.ToString().PadLeft(3, '0') + ".wav");
                    }
                }
                  
                return result.ToArray();
        }
        [WebMethod]
        public string[] ReadNumber(string value)
        {
            List<string> result = new List<string>();


            string sTutar = value.Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
            string tam, ondalik;
            if (sTutar.IndexOf(',') > 0)
            {

                tam = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
                ondalik = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            }
            else
            {
                tam = sTutar;
                ondalik = "00";
            } 
            string yazi = "";

            string[] birler = { "", "BİR", "İKİ", "Üç", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.
            string[] binlerwav = { "", "", "1000000000", "1000000", "1000", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
            //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            tam = tam.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (tam.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(tam.Substring(i, 1))] + "YÜZ"; //yüzler                

                
                if (grupDegeri == "BİRYÜZ") //biryüz düzeltiliyor.
                    grupDegeri = "YÜZ";

                if (Convert.ToInt32(tam.Substring(i, 1)) != 0)
                    result.Add("http://172.20.7.59/anons/" + Convert.ToInt32(tam.Substring(i, 1)) + "00.wav");
                
               
                if (Convert.ToInt32(tam.Substring(i + 1, 1)) != 0)
                    result.Add("http://172.20.7.59/anons/0" + Convert.ToInt32(tam.Substring(i + 1, 1)) + "0.wav");

                if (Convert.ToInt32(tam.Substring(i + 2, 1)) != 0)
                    result.Add("http://172.20.7.59/anons/00" + Convert.ToInt32(tam.Substring(i + 2, 1)) + ".wav");


                if (Convert.ToInt32(tam.Substring(i + 1, 1)) != 0) 
                    grupDegeri += onlar[Convert.ToInt32(tam.Substring(i + 1, 1))]; //onlar
                if (Convert.ToInt32(tam.Substring(i + 2, 1)) != 0)
                 grupDegeri += birler[Convert.ToInt32(tam.Substring(i + 2, 1))]; //birler                

                if (grupDegeri != "") //binler
                {
                    grupDegeri += binler[i / 3];
                    if (binlerwav[i/3] != "")
                        result.Add("http://172.20.7.59/anons/" + binlerwav[i/3] + ".wav");
                
                }
                if (grupDegeri == "BİRBİN") //birbin düzeltiliyor.
                    grupDegeri = "BİN";

                yazi += grupDegeri;
            }

            int yaziUzunlugu = yazi.Length;

            if (ondalik.Substring(0, 1) != "0") //kuruş onlar
            {
                result.Add("http://172.20.7.59/anons/0"+ Convert.ToInt32(ondalik.Substring(0, 1))+ "0.wav");
            
                yazi += onlar[Convert.ToInt32(ondalik.Substring(0, 1))];
            }

            if (ondalik.Substring(1, 1) != "0") //kuruş birler
            {
                yazi += birler[Convert.ToInt32(ondalik.Substring(1, 1))];
                result.Add("http://172.20.7.59/anons/00" + Convert.ToInt32(ondalik.Substring(1, 1)) + ".wav");
            }

            return result.ToArray();
        }
        [WebMethod]
        public string[] ReadCurrency(string value)
        {
            List<string> result = new List<string>();


            string sTutar = value.Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
            string tam, ondalik;
            if (sTutar.IndexOf(',') > 0)
            {

                tam = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
                ondalik = sTutar.Substring(sTutar.IndexOf(',') + 1);
                ondalik = ondalik.PadRight(2, '0');
            }
            else
            {
                tam = sTutar;
                ondalik = "00";
            }
            
            string yazi = "";

            string[] birler = { "", "BİR", "İKİ", "Üç", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.
            string[] binlerwav = { "", "", "1000000000", "1000000", "1000", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
            //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            tam = tam.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (tam.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(tam.Substring(i, 1))] + "YÜZ"; //yüzler                


                if (grupDegeri == "BİRYÜZ") //biryüz düzeltiliyor.
                    grupDegeri = "YÜZ";

                if (Convert.ToInt32(tam.Substring(i, 1)) != 0)
                    result.Add("http://172.20.7.59/anons/" + Convert.ToInt32(tam.Substring(i, 1)) + "00.wav");


                if (Convert.ToInt32(tam.Substring(i + 1, 1)) != 0)
                    result.Add("http://172.20.7.59/anons/0" + Convert.ToInt32(tam.Substring(i + 1, 1)) + "0.wav");

                grupDegeri += onlar[Convert.ToInt32(tam.Substring(i + 1, 1))]; //onlar
                if (Convert.ToInt32(tam.Substring(i + 2, 1)) != 0)
                    result.Add("http://172.20.7.59/anons/00" + Convert.ToInt32(tam.Substring(i + 2, 1)) + ".wav");
                    grupDegeri += birler[Convert.ToInt32(tam.Substring(i + 2, 1))]; //birler                

                if (grupDegeri != "") //binler
                {
                    grupDegeri += binler[i / 3];
                    if (binlerwav[i / 3] != "")
                        result.Add("http://172.20.7.59/anons/" + binlerwav[i / 3] + ".wav");

                }
                if (grupDegeri == "BİRBİN") //birbin düzeltiliyor.
                    grupDegeri = "BİN";

                yazi += grupDegeri;
            }

            if (yazi != "")
            {

                yazi += " TL ";
                result.Add("http://172.20.7.59/anons/Lira.wav");
            }
            int yaziUzunlugu = yazi.Length;

            if (ondalik.Substring(0, 1) != "0") //kuruş onlar
            {
                result.Add("http://172.20.7.59/anons/0" + Convert.ToInt32(ondalik.Substring(0, 1)) + "0.wav");

                yazi += onlar[Convert.ToInt32(ondalik.Substring(0, 1))];
            }

            if (ondalik.Substring(1, 1) != "0") //kuruş birler
            {
                yazi += birler[Convert.ToInt32(ondalik.Substring(1, 1))];
                result.Add("http://172.20.7.59/anons/00" + Convert.ToInt32(ondalik.Substring(1, 1)) + ".wav");
            }

            if (yazi.Length > yaziUzunlugu)
            {
                yazi += " Kr.";
                result.Add("http://172.20.7.59/anons/Kurus.wav");

            }
            else
            {
                yazi += "SIFIR Kr.";
            }
            return result.ToArray();
        }

        [WebMethod]
        public string[] ReadDate(string value)
        {
            List<string> result = new List<string>();

            DateTime dt = DateTime.ParseExact(value, "d.M.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            string[] gun = ReadNumber(dt.Day.ToString());
            string[] yil = ReadNumber(dt.Year.ToString());
           
            
            result.AddRange(gun);
            result.Add("http://172.20.7.59/anons/ay" + dt.Month.ToString().PadLeft(2,'0') + ".wav");
            result.AddRange(yil);


            return result.ToArray();
        }

        #region AKSA OZEL
        [WebMethod]
        public string ParseXMLFaturaAdet(string xmlStr)
        {

            int ind1 = xmlStr.IndexOf("<fatAdet>");
            int ind2 = xmlStr.IndexOf("</fatAdet>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 9, ind2 - ind1 - 9);
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>
            }
            catch 
            {
                return "0";
            }
            

        }
        [WebMethod]
        public string ParseXMLFaturaTutar(string xmlStr)
        {

            int ind1 = xmlStr.IndexOf("<fatTutar>");
            int ind2 = xmlStr.IndexOf("</fatTutar>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 10, ind2 - ind1 - 10);
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>
            }
            catch
            {
                return "0";
            }


        }
        [WebMethod]
        public string ParseXMLFaturaTarih(string xmlStr)
        {

            int ind1 = xmlStr.IndexOf("<sot>");
            int ind2 = xmlStr.IndexOf("</sot>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 5, ind2 - ind1 - 5);
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>
            }
            catch
            {
                return "0";
            }


        }
        [WebMethod ]
        public string ParseXMLOkumaTarih(string xmlStr)
        {
            //<okumagunu>11/7/2013 12:00:00 AM</okumagunu>

            int ind1 = xmlStr.IndexOf("<okumagunu>");
            int ind2 = xmlStr.IndexOf("</okumagunu>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                string temp =  xmlStr.Substring(ind1 + 11, ind2 - ind1 - 11).Trim();
                temp = temp.Remove(temp.IndexOf(' '));
                DateTime dt = DateTime.ParseExact(temp, "M/d/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return dt.ToString("d.M.yyyy");
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>
               
            }
            catch
            {
                return "0";
            }


        }
        [WebMethod]
        public string ParseXMLAboneDurum(string xmlStr)
        {
            //SONDURUM="7" DURUM="Gaz aÃ§ma iÅŸlemi gerÃ§ek
            xmlStr = xmlStr.ToLower();

            int ind1 = xmlStr.IndexOf("sondurum=");
            int ind2 = xmlStr.IndexOf(" durum=");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "";

            try
            {
                string temp = xmlStr.Substring(ind1 + 9, ind2 - ind1 - 9).Trim();
                temp = temp.Replace("\"", "").Trim();
                return "http://172.20.7.59/anons/DURUM_"+temp +".wav";
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>

            }
            catch
            {
                return "";
            }

        }
        [WebMethod]
        public string SayacOkumaSonGun(string  AboneNo)
        {
            try
            {
                ws1.GasIvrserviceagent sa = new ws1.GasIvrserviceagent();
                return ParseXMLOkumaTarih(sa.SayOkGunuAboneNo(AboneNo));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [WebMethod]
        public string AboneSonDurum(string AboneNo)
        {
            try
            {
                ws1.GasIvrserviceagent sa = new ws1.GasIvrserviceagent();
                return ParseXMLAboneDurum(sa.AboneSonDurum(AboneNo));
            }
            catch
            {
                return "";
            }
        }
        [WebMethod]
        public string AboneBorc(string AboneNo)
        {
            try
            {
                ws1.GasIvrserviceagent sa = new ws1.GasIvrserviceagent();
                
                return sa.FaturaBilgiAbone(AboneNo);
            }
            catch
            {
                return "";
            }
        }
        [WebMethod]
        public string ElektrikAboneBorc(string AboneNo)
        {
            try
            {
                wsElektrik.ElectricIvrserviceagent sa = new wsElektrik.ElectricIvrserviceagent();
                return sa.getAboneBorc(int.Parse(AboneNo));
            }
            catch
            {
                return "";
            }
        }

        [WebMethod]
        public string ElektrikAboneBorcSonuc(string xmlStr)
        {
            int ind1 = xmlStr.IndexOf("<result-code>");
            int ind2 = xmlStr.IndexOf("</result-code>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 13, ind2 - ind1 - 13);
            }
            catch
            {
                return "0";
            }

        }
        //result-string
        [WebMethod]
        public string ElektrikAboneBorcSonucStr(string xmlStr)
        {
            int ind1 = xmlStr.IndexOf("<result-string>");
            int ind2 = xmlStr.IndexOf("</result-string>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 15, ind2 - ind1 - 15);
            }
            catch
            {
                return "0";
            }
            

        }

        [WebMethod]
        public string ParseXMLElektrikFaturaAdet(string xmlStr)
        {

            int ind1 = xmlStr.IndexOf("<adet>");
            int ind2 = xmlStr.IndexOf("</adet>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 6, ind2 - ind1 - 6);
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>
            }
            catch
            {
                return "0";
            }


        }
        [WebMethod]
        public string ParseXMLElektrikFaturaTutar(string xmlStr)
        {

            int ind1 = xmlStr.IndexOf("<tutar>");
            int ind2 = xmlStr.IndexOf("</tutar>");
            if (ind1 < 0 || ind2 < 0 || ind2 <= ind1)
                return "0";

            try
            {
                return xmlStr.Substring(ind1 + 7, ind2 - ind1 - 7);
                //<fatAdet>1</fatAdet><fatTutar>34.01</fatTutar><sot>27.12.2013</sot>
            }
            catch
            {
                return "0";
            }


        }
        
        //<adet>1</adet><tutar>11.55</tutar><tesisat>2006161</tesisat><sot>9/23/2013 12:00:00 AM</sot>
        //<adet>0</adet><tutar>0</tutar><tesisat>6204738</tesisat><sot></sot>
      
        #endregion

        #region Agent State Control Methods (DEVREYE ALINDI)


        public void LogAgentState(string AgentID, string Extension, string State)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\AVAYA\" + AgentID + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\t" + State + "\t" + Extension);
                sw.Close();
            }
            catch
            {
            }
        }
        [WebMethod]
        public int AgentLogin(string Extension, string agentID, string AgentPwd)
        {
            LogAgentState(agentID, Extension, "LOGIN");
            System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n i " + Extension + " " + agentID + " " + AgentPwd);

            
            return 1;
        }
        
        [WebMethod]
        public int AgentLogout(string Extension, string agentID, string AgentPwd)
        {
            LogAgentState(agentID, Extension, "LOGOUT");
            System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n o " + Extension + " " + agentID + " " + AgentPwd);
            return 1;
        }
        [WebMethod]
        public int AgentAUX(string Extension, string agentID, string AgentPwd, string Reason, bool pending)
        {
            LogAgentState(agentID, Extension, "AUX"+Reason +(pending ? "\tPENDING":""));
            if (Reason == "7")
            {
                System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n w " + Extension + " " + agentID + " " + AgentPwd /*+ " " + (pending  ? "1":"0")*/);
            }
            else
            {
                if (pending)
                    System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n a " + Extension + " " + agentID + " " + AgentPwd + " " + Reason + " 1");
                else
                    System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n a " + Extension + " " + agentID + " " + AgentPwd + " " + Reason + " 0");
            }
            return 1;
        }
        [WebMethod]
        public int AgentReady(string Extension, string agentID, string AgentPwd)
        {
            LogAgentState(agentID, Extension, "READY");
            
            System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n r " + Extension + " " + agentID + " " + AgentPwd);
            return 1;
        }
        [WebMethod]
        public int AgentACW(string Extension, string agentID, string AgentPwd)
        {
            LogAgentState(agentID, Extension, "ACW");
            
            System.Diagnostics.Process.Start(@"C:\AVAYA\CommandLine Tools\CMDv02.exe", "n w " + Extension + " " + agentID + " " + AgentPwd);
            return 1;
        }

        [WebMethod]
        public int GetAppSettings(out string ACMLINK, out string UserID, out string Password, out string IVRExtension, out string PopUpPath, out string StatsUrl)
        {

            ACMLINK = "AVAYA#AKSACM#CSTA#ISTAES1";
            UserID = "yuksel";
            Password = "Yuksel12#";
            IVRExtension = "";
            PopUpPath = "";
            StatsUrl = "http://172.20.7.229/AksaUtils/AgentStats.aspx?agentId=";
            return 1;

        }
         [WebMethod]
        public int LogVDN(string VDN, string callerdn)
        {
            System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=cms_collect;Data Source=CALLPHONE\SQLEXPRESS");
            _sqlConn.Open();
            try
            {
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("insert into ALARMLOG (VDN, CALLERDN) values ('"+VDN+"','"+callerdn+"')", _sqlConn);
                return sqlCmd.ExecuteNonQuery();


            }
            catch
            {
                _sqlConn.Close();
                return -1;
            }
        }
         [WebMethod]
         public string GetAlarmType(string ani)
         {
             System.Data.SqlClient.SqlConnection _sqlConn = new System.Data.SqlClient.SqlConnection(@"User ID=artpro;Password=artpro12;Initial Catalog=artpro;Data Source=CALLPHONE\SQLEXPRESS");
             _sqlConn.Open();
             string type = "";
             try
             {
                 System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("select top 1 ALARMTYPE from ALARMLOG where ALARMEXTENSION='"+ani+"' order by ROWDATE desc", _sqlConn);
                 type = sqlCmd.ExecuteScalar().ToString();
                 type = "http://172.20.7.229/alarmanons/" + type + ".wav";
             }
             catch
             {
                 type = "";
             }
             finally
             {
                 _sqlConn.Close();
             }
             return type;
         }

        #endregion
     

    }
}
