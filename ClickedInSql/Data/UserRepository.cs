using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Data
{
    public class UserRepository
    {
        public User AddUser(string name, DateTime releaseDate, int age, bool isPrisoner)
        {
            const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = @"Insert into users (name, releaseDate, age, isPrisoner)
                                              Output inserted.*
                                              Values(@name, @releaseDate, @age, @isPrisoner )";
                insertUserCommand.Parameters.AddWithValue("name", name);
                insertUserCommand.Parameters.AddWithValue("releaseDate", releaseDate);
                insertUserCommand.Parameters.AddWithValue("age", age);
                insertUserCommand.Parameters.AddWithValue("isPrisoner", isPrisoner);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedName = reader["name"].ToString();
                    var insertedReleaseDate = (DateTime)reader["releaseDate"];
                    var insertedAge = (int)reader["age"];
                    var insertedIsPrisoner = (bool)reader["isPrisoner"];
                    var insertedId = (int)reader["id"];
                    var newUser = new User(insertedName, insertedReleaseDate, insertedAge, insertedIsPrisoner) { Id = insertedId };

                    connection.Close();

                    return newUser;
                }
            }
            throw new Exception("No user found");

        }
    }

}
