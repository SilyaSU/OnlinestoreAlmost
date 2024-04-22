using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OnlineStore.models
{
    class DataBase
    {
        public SqlConnection sqlConnection = new SqlConnection(@"Data Source=localhost;Initial Catalog=OnlineStore;Integrated Security=True;TrustServerCertificate=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlDataReader ExecuteQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            return cmd.ExecuteReader();
        }
    }
}
