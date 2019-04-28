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

        public void DeleteService(int id)
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var deleteServiceCommand = connection.CreateCommand();
            deleteServiceCommand.CommandText = @"Delete
                                               From Services
                                               Where id = @id";
            deleteServiceCommand.Parameters.AddWithValue("@id", id);
            var reader = deleteServiceCommand.ExecuteNonQuery();

            connection.Close();
        }

        public bool UpdateService(int id, string name, string description, decimal price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var updateServiceCommand = connection.CreateCommand();
                updateServiceCommand.Parameters.AddWithValue("@id", id);
                updateServiceCommand.CommandText = @"Update Services
                                                  Set Name = @name,
                                                  Description = @description,
                                                  Price = @price
                                                  Where id = @id";
                updateServiceCommand.Parameters.AddWithValue("name", name);
                updateServiceCommand.Parameters.AddWithValue("description", description);
                updateServiceCommand.Parameters.AddWithValue("price", price);

                var numberOfRowsUpdated = updateServiceCommand.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsUpdated > 0)
                {
                    return true;
                }
                return false;
            }
            throw new Exception("service is not updated");
        }
    }
}
