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
        

        public IActionResult GetUser()
        {
            string URL = "" + baseUrl + "/v1/me";
            string access_token = Request.Cookies["access_token"];

            string json = Helpers.CreateGetRequest(URL, access_token, "");

            user = JsonConvert.DeserializeObject<User>(json);
            Response.Cookies.Append("user", user.id);
            return View("PlaylistBuilder");
        }

        public IActionResult PlaylistBuilder()
        {
            return View("PlaylistBuilder");
        }

        public IActionResult TopSongCreator()
        {
            string access_token = Request.Cookies["access_token"];
            string user = Request.Cookies["user"];
            string URL = "" + baseUrl + "/v1/me/top/tracks" + "?limit=50";

            string json = Helpers.CreateGetRequest(URL, access_token, user);

            PagingObject<TrackObject> songData = JsonConvert.DeserializeObject<PagingObject<TrackObject>>(json);

            PlaylistObject playlist = Helpers.CreatePlaylist("Top Tracks", access_token, user);
            Helpers.AddTracksToPlaylist(playlist.Id, access_token, songData);
            return View("Done");
        }

        public IActionResult RecentlyPlayed()
        {
            string access_token = Request.Cookies["access_token"];
            string user = Request.Cookies["user"];
            string URL = "" + baseUrl + "/v1/me/player/recently-played" + "?limit=50";

            string json = Helpers.CreateGetRequest(URL, access_token, user);

            return View("Done");
        }
    }
}


