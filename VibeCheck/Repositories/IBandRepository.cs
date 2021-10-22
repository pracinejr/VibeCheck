using System.Collections.Generic;
using VibeCheck.Models;


namespace VibeCheck.Repositories
{
    public interface IBandRepository
    {
        void AddBand(Band band);
        void DeleteBand(int bandId);
        List<Band> GetAllBands();
        Band GetBandById(int id);
        void UpdateBand(Band band);
    }
}
