using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Data
{
    public class InterestRepository
    {
        public Interest AddInterest(string name)
        {
            const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertInterestCommand = connection.CreateCommand();
                insertInterestCommand.CommandText = @"Insert into interests (name)
                                              Output inserted.*
                                              Values(@name)";
                insertInterestCommand.Parameters.AddWithValue("name", name);

                var reader = insertInterestCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedInterestName = reader["name"].ToString();
                    var insertedInterestId = (int)reader["id"];
                    var newInterest = new Interest(insertedInterestName) { Id = insertedInterestId };

                    connection.Close();

                    return newInterest;
                }
            }
            throw new Exception("No interest found");

        }
    }
}
