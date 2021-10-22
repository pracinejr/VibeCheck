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
    public class BandRepository : BaseRepository, IBandRepository
    {
        public BandRepository(IConfiguration config) : base(config) { }

        public List<Band> GetAllBands()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Band
                            ORDER BY Name";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Band> bands = new List<Band>();

                    while (reader.Read())
                    {
                        Band band = new Band
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                        bands.Add(band);
                    }
                    reader.Close();
                    return bands;
                }
            }
        }
        public Band GetBandById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Band WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Band band = null;

                    if (reader.Read())
                    {
                        band = new Band()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")
                        };
                    }
                    reader.Close();

                    return band;
                }
            }
        }

        public void AddBand(Band band)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Band (Name)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name)";

                    cmd.Parameters.AddWithValue("@Name", band.Name);

                    band.Id = (int)cmd.ExecuteScalar();

                }
            }
        }

        public void UpdateBand(Band band)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Band
                        SET Name = @name
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", band.Name);
                    cmd.Parameters.AddWithValue("@id", band.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBand(int bandId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Band WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", bandId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
