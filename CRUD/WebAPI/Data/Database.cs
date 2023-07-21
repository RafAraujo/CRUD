using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Data
{
    public static class Database
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static string GetConnectionString() => _connectionString;

        public static DataTable ExecuteQuery(string sql)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(sql, connection);

                var dt = new DataTable();
                var da = new SqlDataAdapter(command);
                da.Fill(dt);

                return dt;
            }
        }
    }
}