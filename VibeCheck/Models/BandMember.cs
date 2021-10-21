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

        [Required]
        public int UserId { get; set; }
    }
}
