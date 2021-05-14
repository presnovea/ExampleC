using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ExapleForPoint.Modelling
{
    public class DbParams
    {
        private string dbName;
        private string dataSource;
        private string port;
        private string userID;
        private string password;

        public string DbName { 
            get { return dbName; }
            set { dbName = value; }
        }
        public string DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

    }

    internal class DataBaseWorker
    {
        internal static string EFConnectionString (DbParams dbParams)
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = dbParams.DataSource + "," + dbParams.Port;
            cs.InitialCatalog = dbParams.DbName;
            cs.UserID = dbParams.UserID;
            cs.Password = dbParams.Password;

            cs.MultipleActiveResultSets = true;

            return cs.ToString();
        }
    }
}
