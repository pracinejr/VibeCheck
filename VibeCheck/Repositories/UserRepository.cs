using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VibeCheck.Models;
using VibeCheck.Utils;
using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;

namespace VibeCheck.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public User GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, FirebaseUserId, [Name], Email, ImageLocation
                        FROM [User] 
                        WHERE FirebaseUserId = @FirebaseUserId;";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    User user = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                        };
                    }
                    reader.Close();

                    return user;
                }
            }
        }

        public void Add(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO User (FirebaseUserId, Name, Email, ImageLocation)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @Name, @Email, @ImageLocation)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", user.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Name", user.Name);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@ImageLocation", user.ImageLocation);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection conn = Connection)
            {
                List<User> users = new List<User>();
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT u.id, u.FirebaseUserId, u.FirstName, u.LastName, u.DisplayName, u.Email,
                                    u.CreateDateTime, u.ImageLocation, u.UserTypeId,
                                    ut.[Name] AS UserTypeName
                                FROM User u
                                LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                                ORDER BY DisplayName
                                ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                            };
                            users.Add(user);
                        }
                    }
                    return users;
                }
            }
        }
    }
}
