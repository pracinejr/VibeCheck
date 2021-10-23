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
    public class BandMemberRepository : BaseRepository, IBandRepository
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

        public List<BandMember> GetBandMembersByBandId(int id)
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
