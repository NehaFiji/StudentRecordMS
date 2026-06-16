using Microsoft.Data.SqlClient;
using StudentRecordMS.Models;
using System.Data;

namespace StudentRecordMS.Repositories
{
    public class UserRepositoryImpl : IUserRepository
    {
        // Field
        private readonly string _connectionString;

        // DI
        public UserRepositoryImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnStrMVC");
        }

        public TblUser AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (connection == null) return null;

                using (SqlCommand command = new SqlCommand("sp_AuthenticateUser", connection))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@UserPassword", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TblUser
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName = reader["UserName"].ToString(),
                                UserPassword = reader["UserPassword"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                Role = new TblRole
                                {
                                    RoleId = Convert.ToInt32(reader["RoleId"]),
                                    RoleName = reader["RoleName"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
