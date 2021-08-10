using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DBUtility
{
    public class BackupManager
    {
        protected string DBString = @"Data Source=.\SQLEXPRESS;Initial Catalog=moviestore;Integrated Security=True;";

        public BackupManager()
        {
            
        }

        public bool CheckDB()
        {
            SqlConnection con = new SqlConnection(DBString);
            try
            {
                con.Open();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void GenerateDB()
        {
            SqlConnection cn;
            SqlCommand cm;
            try
            {
                string script = null;
                script = MovieStore.Properties.Resources.scriptbackup;
                string[] ScriptSplitter = script.Split(new string[] { "GO" }, StringSplitOptions.None);
                using (cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True"))
                {
                    cn.Open();
                    foreach (string str in ScriptSplitter)
                    {
                        using (cm = cn.CreateCommand())
                        {
                            cm.CommandText = str;
                            cm.ExecuteNonQuery();
                        }
                    }
                }
                cn.Close();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
