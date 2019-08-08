using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    public class UserPreference
    {
        [Required]
        public string ArtistName { get; set; }

        public int Tempo { get; set; }

        public float Danceable { get; set; }
    }
}
