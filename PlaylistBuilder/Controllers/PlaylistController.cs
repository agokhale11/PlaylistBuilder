using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlaylistBuilder.Models;
using PlaylistBuilder;
using Newtonsoft.Json;

namespace PlaylistBuilder.Controllers
{
    public class PlaylistController : Controller
    {
        private User user = new User();
        private string baseUrl = "https://api.spotify.com";

        // gets the user's id based on the access token
        public IActionResult GetUser()
        {
            string URL = "" + baseUrl + "/v1/me";
            string access_token = Request.Cookies["access_token"];

            string json = Helpers.CreateGetRequest(URL, access_token);

            user = JsonConvert.DeserializeObject<User>(json);
            Response.Cookies.Append("user", user.id);
            return View("PlaylistBuilder");
        }

        public IActionResult PlaylistBuilder()
        {
            return View("PlaylistBuilder");
        }

        //generates a playlist of a user's top songs
        public IActionResult TopSongCreator()
        {
            string access_token = Request.Cookies["access_token"];
            string user = Request.Cookies["user"];
            string URL = "" + baseUrl + "/v1/me/top/tracks" + "?limit=50";

            string json = Helpers.CreateGetRequest(URL, access_token); //create a get request and get the returned song data

            PagingObject<TrackObject> songData = JsonConvert.DeserializeObject<PagingObject<TrackObject>>(json); //deserialize json into song data
            List<TrackObject> data = songData.Items;

            PlaylistObject playlist = Helpers.CreatePlaylist("Top Tracks", access_token, user);
            ViewBag.songs = Helpers.AddTracksToPlaylist(playlist.Id, access_token, data);
            return View("Done");
        }

        public IActionResult CustomPlaylist()
        {
            return View();
        }

        // Create a custom playlist from the user's specfied preferences
        // @param seed - the user's preferences for artists, tempo, and danceability
        public IActionResult Custom(UserPreference seed)
        {
            //validate the preference fields
            if (!Helpers.PreferenceValidation(seed))
            {
                ViewBag.Message = "Please enter valid fields.";
                return View("CustomPlaylist");
            }

            string access_token = Request.Cookies["access_token"];
            string user = Request.Cookies["user"];
            string findIdUrl = "" + baseUrl + "/v1/search";
            string artistName = "";
            string type = "&type=artist";

            if (ModelState.IsValid)
            {
                artistName = seed.ArtistName.Replace(" ", "%20"); //encoded URI
            }

            findIdUrl = findIdUrl + "?q=" + artistName + type;
            string json = Helpers.CreateGetRequest(findIdUrl, access_token); //search spotify for artists matching the given name
            SearchQuery query = JsonConvert.DeserializeObject<SearchQuery>(json);
            List<ArtistObject> artists = query.artists.Items;

            if (!artists.Any())
            {
                ViewBag.Message = "No artists with that name could be found"; //validation
                return View("CustomPlaylist");
            }

            //gets all main recommended songs
            string URL = "" + baseUrl + "/v1/recommendations";
            string seeds = "?seed_artists=" + artists[0].Id + "&limit=50" + "&target_tempo=" + seed.Tempo + "&target_danceability=" + seed.Danceable;  
            string jsonRecommended = Helpers.CreateGetRequest(URL+ seeds, access_token);

            RecommendationObject recommendation = JsonConvert.DeserializeObject<RecommendationObject>(jsonRecommended);
            List<TrackObject> tracks = recommendation.Tracks.Cast<TrackObject>().ToList();

            PlaylistObject playlist = Helpers.CreatePlaylist("Recommended", access_token, user);
            ViewBag.songs = Helpers.AddTracksToPlaylist(playlist.Id, access_token, tracks);
            return View("Done");
        }

        //creates a playlist of the user's most recently played songs
        public IActionResult RecentlyPlayed()
        {
            string access_token = Request.Cookies["access_token"];
            string user = Request.Cookies["user"];
            string URL = "" + baseUrl + "/v1/me/player/recently-played" + "?limit=50";

            string json = Helpers.CreateGetRequest(URL, access_token);
            CursorPagingObject<PlayHistoryObject> songData = JsonConvert.DeserializeObject<CursorPagingObject<PlayHistoryObject>>(json);
            List<PlayHistoryObject> data = songData.Items;
            List<TrackObject> songs = new List<TrackObject>();

            for(int i = 0; i < data.Count; ++i)
            {
                songs.Add(data[i].Track);
            }

            PlaylistObject playlist = Helpers.CreatePlaylist("Recently Played", access_token, user);
            ViewBag.songs = Helpers.AddTracksToPlaylist(playlist.Id, access_token, songs);

            return View("Done");
        }
    }
}


