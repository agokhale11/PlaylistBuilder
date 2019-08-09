using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlaylistBuilder.Models;

namespace PlaylistBuilder.Controllers
{
    public class HomeController : Controller
    {
        private string clientID = "2406cc3719e748219629ee9cb4d950d6";
        private string clientSecret = "f8cda72cd393456f8b72dca1e0340555";
        private string loginRedirectUri = "https://spotify-playlist-builder-0.herokuapp.com/home/callback"; // page that spotify redirects to after logging in
        private string scopes = "user-top-read user-read-recently-played playlist-modify-public " +
            "playlist-read-collaborative playlist-read-private playlist-modify-private"; //permissions given to app from user
        private SpotifyToken token = new SpotifyToken();
        

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult InvalidLogin()
        {
            return View();
        }

        // gets the access tokens and authorization needed to access user data
        public IActionResult Callback()
        {
            string code = Request.Query["code"]; //gets the authorization code from header
            
            if (code != null)
            {
                string tokenURL = "https://accounts.spotify.com/api/token";

                //request to get the access token
                var encode_clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientID, clientSecret)));

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(tokenURL);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Accept = "application/json";
                webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);

                var request = "grant_type=authorization_code&code=" + code + "&redirect_uri=" + loginRedirectUri;
                byte[] req_bytes = Encoding.ASCII.GetBytes(request);
                webRequest.ContentLength = req_bytes.Length;

                Stream strm = webRequest.GetRequestStream();
                strm.Write(req_bytes, 0, req_bytes.Length);
                strm.Close();

                string json = Helpers.ReadResponseToString(webRequest);
                token = JsonConvert.DeserializeObject<SpotifyToken>(json);

                //add tokens to cookies
                Response.Cookies.Append("access_token", token.access_token);
                Response.Cookies.Append("scope", token.scope);
                Response.Cookies.Append("refresh_token", token.refresh_token);

                return View("Callback");
            }
            else
            {
                return View("Home");
            }
        }

        //redirects to spotify's login page
        public IActionResult GetLogin()
        {
            string spotifyURL = "https://accounts.spotify.com/authorize";
            string args = "?client_id=" + clientID + "&response_type=code&redirect_uri=" + loginRedirectUri + "&scope=" + scopes;
            return Redirect(spotifyURL + args);
        }       
    }
}
