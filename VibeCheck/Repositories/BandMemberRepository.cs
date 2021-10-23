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
    public class BandMemberRepository : BaseRepository, IBandMemberRepository
    {
        public BandMemberRepository(IConfiguration configuration) : base(configuration) { }

        public List<BandMember> GetAllBandMembers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT b.id AS BandId, b.name AS BandName, u.id AS UserId, u.FirebaseUserId, 
                                               u.name AS UserName, u.email, u.imageLocation, bm.id, 
                                               bm.bandId, bm.userId
                                        FROM bandMember bm
                                        JOIN [user] u ON u.id = bm.userId
                                        JOIN band b ON b.id = bm.bandId";

                    var bandMembers = new List<BandMember>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bandMembers.Add(NewBandMember(reader));
                    }

                    reader.Close();

                    return bandMembers;
                }
            }
        }

        public List<BandMember> GetBandMemberByBandId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT b.id AS BandId, b.name AS BandName, u.id AS UserId, u.FirebaseUserId, 
                                               u.name AS UserName, u.email, u.imageLocation, bm.id, 
                                               bm.bandId, bm.userId
                                        FROM bandMember bm
                                        JOIN [user] u ON u.id = bm.userId
                                        JOIN band b ON b.id = bm.bandId
                                        WHERE b.id = @Id";

                    var bandMembers = new List<BandMember>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bandMembers.Add(NewBandMember(reader));
                    }

                    reader.Close();

                    return bandMembers;
                }
            }
        }

        public void AddBandMember(BandMember bandMember)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO BandMember (BandId, UserId)
                                        OUTPUT INSERTED.ID
                                        VALUES ( @BandId, @UserId)";
                    DbUtils.AddParameter(cmd, "@BandId", bandMember.BandId);
                    DbUtils.AddParameter(cmd, "@UserId", bandMember.UserId);
               



                    bandMember.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateBandMember(BandMember bandMember)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE BandMember
                                        SET BandId = @BandId,
                                            UserId = @UserId,    
                                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@BandId", bandMember.BandId);
                    DbUtils.AddParameter(cmd, "@UserId", bandMember.UserId);
                    DbUtils.AddParameter(cmd, "@Id", bandMember.Id);



                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBandMember(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM BandMember WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private BandMember NewBandMember(SqlDataReader reader)
        {
            return new BandMember()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                BandId = DbUtils.GetInt(reader, "BandId"),
                UserId = DbUtils.GetInt(reader, "UserId"),
                User = new User()
                {
                    Id = DbUtils.GetInt(reader, "UserId"),
                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                    Name = DbUtils.GetString(reader, "UserName"),
                    Email = DbUtils.GetString(reader, "Email"),
                    ImageLocation = DbUtils.GetString(reader, "ImageLocation")
                },
                Band = new Band()
                {
                    Id = DbUtils.GetInt(reader, "BandId"),
                    Name = DbUtils.GetString(reader, "BandName")
                }
            };
        }
    }
}
