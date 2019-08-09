﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistBuilder.Models
{
    // consists of all the fields that identify and describe a 
    // Paging Object as described by Spotify API
    public class PagingObject<T>
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        public bool HasNextPage()
        {
            return Next != null;
        }

        public bool HasPreviousPage()
        {
            return Next != null;
        }
    }
}
