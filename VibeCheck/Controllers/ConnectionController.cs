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
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionRepository _connectionRepo;

        public ConnectionController(IConnectionRepository connectionRepository)
        {
            _connectionRepo = connectionRepository; 
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult Get(string firebaseUserId)
        {
            return Ok(_connectionRepo.GetUsersConnections(firebaseUserId));
        }

        [HttpPost]

        public IActionResult Create(Connection connection, string firebaseUserId)
        {
            List<Connection> connections = _connectionRepo.GetUsersConnections(firebaseUserId);
            if (connections.Any(b => b.AcquaintanceId == connection.AcquaintanceId))
            {
                ModelState.AddModelError("", "Connection already exists.");
                return BadRequest();
            }
            else
            {
                _connectionRepo.AddConnection(connection);
                return CreatedAtAction("Get", new { id = connection.Id }, connection);

            }

        }

        [HttpGet("GetById/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_connectionRepo.GetConnectionById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Connection connection)
        {
            if (id != connection.Id)
            {
                return BadRequest();
            }

            _connectionRepo.UpdateConnection(connection);
            return Ok();
        }
    }
}
