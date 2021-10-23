using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VibeCheck.Models;

namespace VibeCheck.Repositories
{
    public interface IBandMemberRepository
    {
        List<BandMember> GetAllBandMembers();
        List<BandMember> GetBandMemberByBandId(int id);
        void AddBandMember(BandMember bandMemebr);
        void UpdateBandMember(BandMember bandMember);
        void DeleteBandMember(int id);
    }
}
