using DogGo.Models;
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

        public List <Walks> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, wr.Name as WalkerName, d.Name as DogName
                                    FROM Walks w 
                                    LEFT JOIN Walker wr on w.WalkerId = wr.Id
                                    LEFT JOIN Dog d on w.DogId = d.Id
                                    ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walks> walks = new List<Walks>();
                    while(reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")).ToShortDateString(),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")) / 60,
                            Walker = new Walker()
                            {
                                Name = reader.GetString(reader.GetOrdinal("WalkerName"))
                            },
                            Dog = new Dog()
                            {
                                Name = reader.GetString(reader.GetOrdinal("DogName"))
                            }
                        };
                        walks.Add(walk);
                        }
                    reader.Close();
                    return walks;
                       
                }
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

        

        public void AddWalk(Walks walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       INSERT INTO Walks (Date, Duration, WalkerId, DogId)
                        OUTPUT INSERTED.ID
                        VALUES (@date, @duration, @walkerId, @dogId)
                    ";

                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);
                    cmd.Parameters.AddWithValue("@walkerId", walk.WalkerId);
                    cmd.Parameters.AddWithValue("@dogId", walk.DogId);


                    int id = (int)cmd.ExecuteScalar();

                    walk.Id = id;
                }
            }
        }
    }
}
