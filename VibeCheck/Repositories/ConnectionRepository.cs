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
    public class ConnectionRepository : BaseRepository, IConnectionRepository
    {
        public ConnectionRepository(IConfiguration config) : base(config) { }

        public Connection GetConnectionById(int id)
        {
            using (var conn = this.Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.UserId AS UserId, c.VenueId, c.MutualFriendId, c.AcquaintanceId,
	                                           c.DateCreated, c.notes, mf.Id AS FriendId, mf.Name AS FriendName,
	                                           mf.email AS FriendEmail, mf.ImageLocation AS FriendImage,
	                                           a.Id AS AcquaintanceId, a.Name AS AcquaintanceName, a.email
	                                           AS AcquaintanceEmail, a.ImageLocation AS AcquaintanceImage,
	                                           v.id, v.name AS VenueName
                                        FROM connection c
                                        JOIN [user] mf ON c.mutualFriendId = mf.id
                                        JOIN [user] a ON c.acquaintanceId = a.id
                                        JOIN venue v ON c.venueId = v.id
                                        WHERE c.acquaintanceId = @Id";

                    var reader = cmd.ExecuteReader();

                    var connections = new List<Connection>();
                    while (reader.Read())
                    {
                        connections.Add(new Connection()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            VenueId = DbUtils.GetInt(reader, "VenueId"),
                            Venue = new Venue()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "VenueName")
                            },
                            MutualFriendId = DbUtils.GetInt(reader, "MutualFriendId"),
                            MutualFreind = new User()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "MutualFreindName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                            },
                            AcquaintanceId = DbUtils.GetInt(reader, "AcquaintanceId"),
                            Acquaintance = new User()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                            },
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            Notes = DbUtils.GetString(reader, "Notes"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            User = new User()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                            },
                        });
                    }

                    reader.Close();

                    return connections;
                }
            }
        }

        public List<Connection> GetUsersConnections(string firebaseUserId)
        {
            using (var conn = this.Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.UserId AS UserId, c.VenueId, c.MutualFriendId, c.AcquaintanceId,
	                                           c.DateCreated, c.notes, mf.Id AS FriendId, mf.Name AS FriendName,
	                                           mf.email AS FriendEmail, mf.ImageLocation AS FriendImage,
	                                           a.Id AS AcquaintanceId, a.Name AS AcquaintanceName, a.Email
	                                           AS AcquaintanceEmail, a.ImageLocation AS AcquaintanceImage,
	                                           v.Id, v.Name AS VenueName, u.Id, u.FirebaseUserId, u.Name AS UserName, 
                                               u.Email AS UserEmail, u.ImageLocation AS UserImage
                                        FROM connection c
                                        JOIN [user] mf ON c.mutualFriendId = mf.Id
                                        JOIN [user] a ON c.acquaintanceId = a.Id
                                        JOIN [user] u ON c.UserId = u.Id
                                        JOIN venue v ON c.venueId = v.Id
                                        WHERE u.FirebaseUserId = @FirebaseUserId
                                        ORDER BY AcquaintanceName";

                    DbUtils.AddParameter(cmd, "@Id", firebaseUserId);

                    var reader = cmd.ExecuteReader();

                    var connections = new List<Connection>();
                    while (reader.Read())
                    {
                        connections.Add(new Connection()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            VenueId = DbUtils.GetInt(reader, "VenueId"),
                            Venue = new Venue()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "VenueName")
                            },
                            MutualFriendId = DbUtils.GetInt(reader, "MutualFriendId"),
                            MutualFreind = new User()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "MutualFreindName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                            },
                            AcquaintanceId = DbUtils.GetInt(reader, "AcquaintanceId"),
                            Acquaintance = new User()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                            },
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            Notes = DbUtils.GetString(reader, "Notes"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            User = new User()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                            },
                        });
                    }

                    reader.Close();

                    return connections;
                }
            }
        }

        public void AddConnection(Connection connection)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Connection (UserId, VenueId, MutualFriendId,
                                               AcquaintanceId, DateCreated, Notes)
                                        OUTPUT INSERTED.ID
                                        VALUES ( @UserId, @VenueId, @MutualFriendId,
                                               @AcquaintanceId, @DateCreated, @Notes)";

                    DbUtils.AddParameter(cmd, "@UserId", connection.UserId);
                    DbUtils.AddParameter(cmd, "@VenueId", connection.VenueId);
                    DbUtils.AddParameter(cmd, "@MutualFriendId", connection.MutualFriendId);
                    DbUtils.AddParameter(cmd, "@AcquaintanceId", connection.AcquaintanceId);
                    DbUtils.AddParameter(cmd, "@DateCreated", connection.DateCreated);
                    DbUtils.AddParameter(cmd, "@Notes", connection.Notes);

                    connection.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateConnection(Connection connection)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Connection
                                        SET UserId = @UserId,
                                            VenueId = @VenueId,
                                            MutualFriendId = @MutualFriendId,
                                            AcquaintanceId = @AcquaintanceId,
                                            Notes = @Notes
                                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@UserId", connection.UserId);
                    DbUtils.AddParameter(cmd, "@VenueId", connection.VenueId);
                    DbUtils.AddParameter(cmd, "@MutualFriendId", connection.MutualFriendId);
                    DbUtils.AddParameter(cmd, "@AcquaintanceId", connection.AcquaintanceId);
                    DbUtils.AddParameter(cmd, "@Notes", connection.Id);
                    DbUtils.AddParameter(cmd, "Id", connection.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
