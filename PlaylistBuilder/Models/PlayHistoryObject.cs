using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    public class PlayHistoryObject
    {
        [JsonProperty("track")]
        public TrackObject Track { get; set; }

        [JsonProperty("played_at")]
        public DateTime PlayedAt { get; set; }
    }
}
