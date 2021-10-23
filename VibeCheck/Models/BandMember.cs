using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VibeCheck.Models
{
    public class BandMember
    {
        public int Id { get; set; }

        [Required]
        public int BandId { get; set; }
        public Band Band { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
