using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VibeCheck.Models;
using VibeCheck.Utils;
using Microsoft.Data.SqlClient;

namespace VibeCheck.Repositories
{
    public class VenueRepository : BaseRepository
    {
        public VenueRepository(IConfiguration config) : base(config) { }

        public List<Venue> GetAllVenues()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Venue
                            ORDER BY Name";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Venue> venues = new List<Venue>();

                    while (reader.Read())
                    {
                        Venue venue = new Venue
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                        venues.Add(venue);
                    }
                    reader.Close();
                    return venues;
                }
            }
        }
        public Venue GetVenueById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Venue WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Venue venue = null;

                    if (reader.Read())
                    {
                        venue = new Venue()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                    }
                    reader.Close();

                    return venue;
                }
            }
        }

        public void AddVenue(Venue venue)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Venue (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name)";

                    cmd.Parameters.AddWithValue("@Name", venue.Name);

                    venue.Id = (int)cmd.ExecuteScalar();

                }
            }
        }

        public void UpdateVenue(Venue venue)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Venue
                        SET Name = @name
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", venue.Name);
                    cmd.Parameters.AddWithValue("@id", venue.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteVenue(int venueId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Venue WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", venueId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
