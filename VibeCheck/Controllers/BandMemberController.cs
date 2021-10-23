using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VibeCheck.Models;
using VibeCheck.Repositories;

namespace VibeCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandMemberController : ControllerBase
    {
        private readonly IBandMemberRepository _bandMemberRepo;
        private readonly IUserRepository _userRepository;

        public BandMemberController(IBandMemberRepository bandMemberRepository, 
                                    IBandRepository bandRepository, 
                                    IUserRepository userRepository)
        {
            _bandMemberRepo = bandMemberRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bandMemberRepo.GetAllBandMembers());
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bandMembers = _bandMemberRepo.GetBandMemberByBandId(id);
            if (bandMembers != null)
            {
                NotFound();
            }
            return Ok(bandMembers);
        }

        [HttpPost]
        public IActionResult Post(BandMember bandMember)
        {

            _bandMemberRepo.AddBandMember(bandMember);
            return CreatedAtAction(nameof(Get), new { id = bandMember.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _bandMemberRepo.DeleteBandMember(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, BandMember band)
        {
            if (id != band.Id)
            {
                return BadRequest();
            }

            _bandMemberRepo.UpdateBandMember(band);
            return Ok();
        }

    }
}
