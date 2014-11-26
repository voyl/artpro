using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DataAccess
{
    public class Execute
    {
        #region Pro
        public static string ConnectionString = @"User ID=sa;Password=1234;Initial Catalog=atech;Data Source=CELLWE-PC\SQLEXPRESS";
        //public static string ConnectionString = @"User ID=sa;Password=1234qqQ;Initial Catalog=ctp00;Data Source=WIN-SA8IL71C6U7\BAHCE";
        private static SqlConnection cn;
        private static SqlCommand cmd;
        private static SqlDataReader rd;
        public static string ConStr
        {
            get {
                return ConnectionString;
            }
        }
        #endregion

        #region Cons

        static Execute()
        {
            try
            {
                cn = new SqlConnection(ConnectionString);
                //OpenConnection();
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Methods

        #region Open,Close

        private static void OpenConnection()
        {
            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }
        private static void CloseConnection()
        {
            if (cn != null)
            {
                if ((cn.State != System.Data.ConnectionState.Closed))
                {
                    try
                    {
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region ExecuteNonQuery,ExecuteScalar,StoredProcedure

        public static int ExecuteNonQuery(string sql)
        {
            //Bağlantı Açılır.
            OpenConnection();

            cmd = new SqlCommand(sql, cn);
            int r = 0;
            try
            {
                r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection();
            }
            return r;
        }
        public static DataTable  ExecuteTable(string sql)
        {
            //Bağlantı Açılır.
            OpenConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataTable sonuc = new DataTable();
            
            try
            {
                da.Fill(sonuc);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                da.Dispose();
                CloseConnection();
            }
            return sonuc;
        }

        public static object ExecuteScalar(string sql)
        {
            //Bağlantı Açılır.
            OpenConnection();

            cmd = new SqlCommand(sql, cn);
            object r = null;
            try
            {
                r = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection();
            }
            return r;
        }
        public static object StoredProcedure(string ProcedureName, string[] ParameterNames, object[] Parameters)
        {
            //Bağlantı Açılır.
            OpenConnection();

            //parametre adları ve değerleri kontrol edilir
            if (ParameterNames.Length != Parameters.Length)
                throw new Exception("Verilen Parametre Sayısı Ýle Değer Sayısı Tutmuyor! Kontrol Ediniz.");

            cmd = new SqlCommand(ProcedureName, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            object r = false;
            try
            {

                for (int i = 0; i < ParameterNames.Length; i++)
                {
                    cmd.Parameters.Add(ParameterNames[i].ToString(), Parameters[i]);
                }
                r = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                CloseConnection();
            }
            return r;
        }
        public IDataReader ExecuteReader(string selectSql)
        {
            //Bağlantı Açılır.
            OpenConnection();

            rd = null;
            try
            {
                cmd = new SqlCommand(selectSql, cn);
                rd = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rd;
        }
        public void ExecuteReaderClose()
        {
            if (!rd.IsClosed)
            {
                rd.Close();
            }
            CloseConnection();
        }

        #endregion

        #endregion

        #region Desc

        ~Execute()
        {
            if (!rd.IsClosed)
            {
                rd.Close();
            }
            CloseConnection();
        }

        #endregion
    }
    
}
