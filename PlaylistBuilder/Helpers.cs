using Newtonsoft.Json;
using PlaylistBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistBuilder
{
    public class Helpers
    {
        private static string baseUrl = "https://api.spotify.com"; // Url to access spotify api base address

        // Takes in a webrequest and reads the response into a json formatted string for reading
        public static string ReadResponseToString(HttpWebRequest request)
        {
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return json;
        }

        // base method to add a set of tracks to an existing playlist
        // @param id - id of the playlist
        // @param access_token - token for authorization of the current user
        // @param songData - the list of the songs to be added to the playlist represented by "id"
        public static List<string> AddTracksToPlaylist(string id, string access_token, List<TrackObject> songData)
        {
            string addTracksEndpoint = "" + baseUrl + "/v1/playlists/" + id + "/tracks";

            //create POST web request and add headers
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(addTracksEndpoint);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization: Bearer " + access_token);

            List<string> songs = new List<string>(); // list of strings to be created in json format to form the post body
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                //create json formatted post body
                string body = ("{\"uris\":[");
                foreach (TrackObject track in songData)
                {
                    body = body + "\"" + track.Uri + "\",";
                    string artistNames = "";
                    List<ArtistObject> artists = track.Artists;

                    for(int i = 0; i < artists.Count; ++i)
                    {
                        if (i > 0)
                        {
                            artistNames = artistNames + ", " + artists[i].Name;
                        }
                        else
                        {
                            artistNames = artists[i].Name;
                        }
                    }

                    string song = track.Name + "-- " + artistNames; // string used to display all tracks added on view page
                    song = song.TrimEnd(',');
                    songs.Add(song);
                }
                body = body.TrimEnd(',') + "]}";                    // json string to send in request

                streamWriter.Write(body);
            }
            string json = Helpers.ReadResponseToString(request);   // must get back returned output or request is considered invalid 
            return songs;
        }

        // base method to create a new playlist
        // @param name - name of the playlist
        // @param access_token - token for authorization of the current user
        // @param user - the user's id
        public static PlaylistObject CreatePlaylist(string name, string access_token, string user)
        {
            string playlistEndpoint = "" + baseUrl + "/v1/users/" + user + "/playlists";

            // create post request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(playlistEndpoint);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization: Bearer " + access_token);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string body = ("{\"name\":\""+ name +"\"}");

                streamWriter.Write(body);
            }
            string json = Helpers.ReadResponseToString(request); //turn returned data to json formatted string
            PlaylistObject playlist = JsonConvert.DeserializeObject<PlaylistObject>(json); //turn json string to defined playlist object
            return playlist;
        }

        // base method to create a get request for the spotify api
        public static string CreateGetRequest(string URL, string access_token, string user)
        { 
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(URL);

            webRequest.Method = "GET";
            webRequest.Headers.Add("Authorization", "Bearer " + access_token);
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/json";
            string json =  Helpers.ReadResponseToString(webRequest);
            return json;
        }

        // method to ensure the user's parameters for custom playlists are valid
        // @param preference - preference object defining user's input parameters
        public static bool PreferenceValidation(UserPreference preference)
        {
            int tempo;
            float danceable;

            if(!int.TryParse(preference.Tempo, out tempo))
            {
                return false;
            }
            else if(!float.TryParse(preference.Danceable, out danceable) || danceable > 1.0 || danceable < 0.0)
            {
                return false;
            }
            return true;
        }
    }
}
