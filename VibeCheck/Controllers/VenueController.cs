using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VibeCheck.Models;
using VibeCheck.Repositories;

namespace VibeCheck.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueRepository _venueRepo;

        public VenueController(IVenueRepository venueRepository)
        {
            _venueRepo = venueRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_venueRepo.GetAllVenues());
        }

        [HttpPost]

        public IActionResult Create(Venue venue)
        {
            List<Venue> venues = _venueRepo.GetAllVenues();
            if (venues.Any(t => t.Name == venue.Name))
            {
                ModelState.AddModelError("", "Venue already exists.");
                return BadRequest();
            }
            else
            {
                _venueRepo.AddVenue(venue);
                return CreatedAtAction("Get", new { id = venue.Id }, venue);

            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _venueRepo.DeleteVenue(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_venueRepo.GetVenueById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Venue venue)
        {
            if (id != venue.Id)
            {
                return BadRequest();
            }

            _venueRepo.UpdateVenue(venue);
            return Ok();
        }

    }
}