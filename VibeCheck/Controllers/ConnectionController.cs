using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
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
        private readonly IUserRepository _userRepo;

        public ConnectionController(IConnectionRepository connectionRepository, IUserRepository userRepository)
        {
            _connectionRepo = connectionRepository;
            _userRepo = userRepository;
        }

        [HttpGet("GetMyConnections")]
        public IActionResult Get()
        {
            var currentUser = GetCurrentUser();

            var connections = _connectionRepo.GetUsersConnections(currentUser.Id);

            return Ok(connections);
        }

        [HttpPost]

        public IActionResult Create(Connection connection)
        {
            var currentUser = GetCurrentUser();
            List<Connection> connections = _connectionRepo.GetUsersConnections(currentUser.Id);
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
        public IActionResult GetByConnectionId(int id)
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

        private User GetCurrentUser()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (firebaseUserId != null)
            {
                return _userRepo.GetByFirebaseUserId(firebaseUserId);
            }
            else
            {
                return null;
            }
        }
    }
}
