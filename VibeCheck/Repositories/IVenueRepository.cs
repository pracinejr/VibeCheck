using System.Collections.Generic;
using VibeCheck.Models;


namespace VibeCheck.Repositories
{
    public interface IVenueRepository
    {
        void AddVenue(Venue venue);
        void DeleteVenue(int venueId);
        List<Venue> GetAllVenues();
        Venue GetVenueById(int id);
        void UpdateVenue(Venue venue);
    }
}
