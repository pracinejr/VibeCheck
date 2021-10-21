using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VibeCheck.Models
{
    public class Connection
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int VenueId { get; set; }

        public int MutualFriendId { get; set; }

        [Required]
        public int AcquaintanceId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string Notes { get; set; }
    }
}
