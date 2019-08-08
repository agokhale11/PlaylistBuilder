using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    public class SearchQuery
    {
        [JsonProperty("artists")]
        public PagingObject<ArtistObject> artists { get; set; }

        [JsonProperty("tracks")]
        public PagingObject<TrackObject> tracks { get; set; }
    }
}
