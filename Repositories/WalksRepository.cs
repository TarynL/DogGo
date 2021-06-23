﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetAllWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, o.Name as Client
                                       FROM Walks w
                                       LEFT JOIN Dog d on w.DogId = d.Id
                                       JOIN Owner o on d.OwnerId = o.Id
                                       WHERE w.WalkerId = @walkerId
                                       ORDER BY o.Name";
                    cmd.Parameters.AddWithValue("@walkerId", walkerId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();

                    while (reader.Read())
                    {
                        Walks walk = new Walks()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")).ToShortDateString(),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration"))/60,
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Owner = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("Client")),
                            }
                        };
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }
    }
}