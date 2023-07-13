using CommonLayer.Models;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class DbConnect
    {
       public SqlConnection connection { get; set; }
        public DbConnect()
        {
            connection = new SqlConnection(ConnectionConfig.dbConnectConfig);
        }
    }
}
