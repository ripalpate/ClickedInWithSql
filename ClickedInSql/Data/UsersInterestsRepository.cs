using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Data
{
    public class UsersInterestsRepository
    {
        public UsersInterests AddUsersInterests(int userId, int interestId)
        {
            const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = @"Insert into usersInterests (userId, interestId)
                                              Output inserted.*
                                              Values(@userId, @interestId )";
                insertUserCommand.Parameters.AddWithValue("userId", userId);
                insertUserCommand.Parameters.AddWithValue("interestId", interestId);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUserId = (int)reader["userId"];
                    var insertedInterestId = (int)reader["interestId"];
                    var insertedId = (int)reader["id"];
                    var newUserInterest = new UsersInterests(insertedUserId, insertedInterestId) { Id = insertedId };

                    connection.Close();

                    return newUserInterest;
                }
            }
            throw new Exception("No user interest found");

        }
    }
}
