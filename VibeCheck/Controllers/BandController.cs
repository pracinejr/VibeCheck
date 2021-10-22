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
    public class BandController : ControllerBase
    {
        private readonly IBandRepository _bandRepo;

        public BandController(IBandRepository bandRepository)
        {
            _bandRepo = bandRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bandRepo.GetAllBands());
        }

        [HttpPost]

        public IActionResult Create(Band band)
        {
            List<Band> bands = _bandRepo.GetAllBands();
            if (bands.Any(b => b.Name == band.Name))
            {
                ModelState.AddModelError("", "Band already exists.");
                return BadRequest();
            }
            else
            {
                _bandRepo.AddBand(band);
                return CreatedAtAction("Get", new { id = band.Id }, band);

            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _bandRepo.DeleteBand(id);
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
            return Ok(_bandRepo.GetBandById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Band band)
        {
            if (id != band.Id)
            {
                return BadRequest();
            }

            _bandRepo.UpdateBand(band);
            return Ok();
        }

    }
}