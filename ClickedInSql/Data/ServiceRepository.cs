using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Data
{
    public class ServiceRepository
    {
        const string ConnectionString = "Server = localhost; Database = ClinckedIn; Trusted_Connection = True;";

        public Service AddService(string name, string description, decimal price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertServiceCommand = connection.CreateCommand();
                insertServiceCommand.CommandText = @"Insert into services (name, description, price)
                                              Output inserted.*
                                              Values(@name, @description, @price)";
                insertServiceCommand.Parameters.AddWithValue("name", name);
                insertServiceCommand.Parameters.AddWithValue("description", description);
                insertServiceCommand.Parameters.AddWithValue("price", price);

                var reader = insertServiceCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedServiceName = reader["name"].ToString();
                    var insertedServiceDescription = reader["description"].ToString();
                    var insertedServicePrice = (decimal)reader["price"];
                    var insertedInterestId = (int)reader["id"];
                    var newInterest = new Service(insertedServiceName, insertedServiceDescription, insertedServicePrice) { Id = insertedInterestId };

                    connection.Close();

                    return newInterest;
                }
            }
            throw new Exception("No service found");
        }
        public List<Service> GetAllServices()
        {
            var services = new List<Service>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllServicesCommand = connection.CreateCommand();
            getAllServicesCommand.CommandText = @"select * from services";

            var reader = getAllServicesCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = reader["Name"].ToString();
                var description = reader["Description"].ToString();
                var price = (decimal)reader["Price"];

                var service = new Service(name, description, price) { Id = id };

                services.Add(service);
            }

            connection.Close();

            return services;
        }
    }
}
