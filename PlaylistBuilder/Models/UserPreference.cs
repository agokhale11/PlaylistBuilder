using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{ 

    // consists of the parameters a user can choose to build a playlist from
    public class UserPreference
    {
        public string ArtistName { get; set; }

        public string Tempo { get; set; }

        public string Danceable { get; set; }
    }
}
