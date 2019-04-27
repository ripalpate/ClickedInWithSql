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
        const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";
        public User AddUser(string name, DateTime releaseDate, int age, bool isPrisoner)
        {
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

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = "select * from users";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var releaseDate = (DateTime)reader["ReleaseDate"];
                var age = (int)reader["Age"];
                var isPrisoner = (bool)reader["IsPrisoner"];

                var user = new User(name, releaseDate, age, isPrisoner) { Id = id };

                users.Add(user);
            }

            connection.Close();

            foreach (User user in users)
            {
                user.Services = (GetServices(user.Id));
            }

            foreach (User user in users)
            {
                user.Interests = (GetInterests(user.Id));
            }

            return users;
        }


        public List<string> GetInterests(int userId)
        {
            var interests = new List<string>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var getUserWithInterestsCommand = connection.CreateCommand();
            getUserWithInterestsCommand.CommandText = @"Select i.Name InterestName
                                                        From Interests i
														Join UsersInterests ui
														On ui.InterestId = i.Id
                                                        Where ui.UserId = @userId"; 

            getUserWithInterestsCommand.Parameters.AddWithValue("@userId", userId);
            var reader = getUserWithInterestsCommand.ExecuteReader();

            while (reader.Read())
            {
                var interest = reader["InterestName"].ToString();

                interests.Add(interest);
            }

            connection.Close();

            return interests;
        }

       public List<string> GetServices(int userId)
        {
            var services = new List<string>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getUserWithServicesCommand = connection.CreateCommand();
            getUserWithServicesCommand.Parameters.AddWithValue("@userId", userId);
            getUserWithServicesCommand.CommandText = @"Select s.Name ServiceName
                                                        From Services s
														Join UsersService us
														On us.ServiceId = s.Id
                                                        Where us.UserId = @userId";

            var reader = getUserWithServicesCommand.ExecuteReader();

            while (reader.Read())
            {
                var service = reader["ServiceName"].ToString();

                services.Add(service);
            }

            connection.Close();

            return services;
        }




        //public List<User> GetUsersWithInterestsAndServices()
        //{
        //    var users = new List<User>();
        //    var connection = new SqlConnection(ConnectionString);
        //    connection.Open();
        //    var getAllUsersWithInterestCommand = connection.CreateCommand();
        //    getAllUsersWithInterestCommand.CommandText = @"Select u.* , InterestName = i.Name 
        //                                                   from UsersInterests as ui
        //                                                   Join Interests as i
        //                                                   On i.Id = ui.InterestId
        //                                                   Join Users as u
        //                                                   On u.Id = ui.UserId;";

        //    var reader = getAllUsersWithInterestCommand.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        var id = (int)reader["id"];
        //        var username = reader["name"].ToString();
        //        var releaseDate= (DateTime)reader["releaseDate"];
        //        var age= (int)reader["age"];
        //        var isPrisoner = (bool)reader["isPrisoner"];
        //        var interest = reader["InterestName"].ToString();
        //        var user = new User(username, releaseDate, age, isPrisoner, interest) { Id = id };
        //        users.Add(user);
        //    }
        //    reader.Close();

        //    var getAllUsersWithServiceCommand = connection.CreateCommand();
        //    getAllUsersWithServiceCommand.CommandText = @"Select u.* , ServiceName = s.Name
        //                                                 from UsersService as us
        //                                                 Join Services as s
        //                                                 On s.Id = us.ServiceId
        //                                                 Join Users as u
        //                                                 On u.Id = us.UserId
        //                                                 Order by u.Id";

        //    var serviceReader = getAllUsersWithServiceCommand.ExecuteReader();

        //    while (serviceReader.Read())
        //    {
        //        var id = (int)serviceReader["id"];
        //        var service = (string)serviceReader["ServiceName"];
        //        for (var i = 0; i < users.Count; i++) {
        //            if (users[i].Id == id)
        //            {
        //                if (users[i].Service != null && users[i].Service != "")
        //                {
        //                    users[i].Service += ", " + service;
        //                }
        //                else
        //                {
        //                    users[i].Service += service;
        //                }
        //            }
        //        }


        //    }

        //    connection.Close();

        //    var userWithInterests = users.GroupBy(u => new { u.Id, u.Name, u.ReleaseDate, u.Age, u.IsPrisoner, u.Service })
        //        .Select(user => new User()
        //        {
        //            Id = user.Key.Id,
        //            Name = user.Key.Name,
        //            ReleaseDate = user.Key.ReleaseDate,
        //            Age = user.Key.Age,
        //            IsPrisoner = user.Key.IsPrisoner,
        //            Interest =  user.Select(i => i.Interest),
        //            Service = user.Key.Service
        //        }).ToList();

        //    return userWithInterests;
        //}
    }
}

