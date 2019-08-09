using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    // consists of the tracks retrieved from a spotify recommendation
    public class RecommendationObject
    {
        [JsonProperty("tracks")]
        public TrackObject[] Tracks { get; set; }
    }
}
