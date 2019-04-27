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
        const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";
        public Interest AddInterest(string name)
        {
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

        public List<Interest> GetAllInterests()
        {
            var interests = new List<Interest>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllInterestsCommand = connection.CreateCommand();
            getAllInterestsCommand.CommandText = "select * from interests";

            var reader = getAllInterestsCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();

                var interest = new Interest(name) { Id = id };

                interests.Add(interest);
            }

            connection.Close();

            return interests;
        }

        public void DeleteInterest(int id)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var deleteInterestCommand = connection.CreateCommand();
            deleteInterestCommand.CommandText = @"Delete
                                               From Interests
                                               Where id = @id";
            deleteInterestCommand.Parameters.AddWithValue("@id", id);
            var reader = deleteInterestCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}
