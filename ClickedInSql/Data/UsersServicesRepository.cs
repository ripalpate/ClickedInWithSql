using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Data
{
    public class UsersServicesRepository
    {
        public UsersServices AddUsersServices(int userId, int serviceId)
        {
            const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserServiceCommand = connection.CreateCommand();
                insertUserServiceCommand.CommandText = @"Insert into UsersService (userId, serviceId)
                                              Output inserted.*
                                              Values(@userId, @serviceId )";
                insertUserServiceCommand.Parameters.AddWithValue("userId", userId);
                insertUserServiceCommand.Parameters.AddWithValue("serviceId", serviceId);

                var reader = insertUserServiceCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUserId = (int)reader["userId"];
                    var insertedServiceId = (int)reader["serviceId"];
                    var insertedId = (int)reader["id"];
                    var newUserService = new UsersServices(insertedUserId, insertedServiceId) { Id = insertedId };

                    connection.Close();

                    return newUserService;
                }
            }
            throw new Exception("No user service found");

        }
    }
}
