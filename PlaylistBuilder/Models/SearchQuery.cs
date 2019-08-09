using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    // consists of the artists and tracks that a user can use to build a playlist from
    public class SearchQuery
    {
        [JsonProperty("artists")]
        public PagingObject<ArtistObject> artists { get; set; }

        [JsonProperty("tracks")]
        public PagingObject<TrackObject> tracks { get; set; }
    }
}
