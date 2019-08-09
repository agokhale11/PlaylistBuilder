using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    //consists of authorization access tokens for authentication
    public class SpotifyToken
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public string scope { get; set; }

        public int expires_in { get; set; }

        public string refresh_token { get; set; }
    }

    //defines which user is currently using the access token
    public class User
    {
        public string id { get; set; }
    }
}

