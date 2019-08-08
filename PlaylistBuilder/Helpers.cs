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
        private static string baseUrl = "https://api.spotify.com";

        public static string ReadResponseToString(HttpWebRequest request)
        {
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return json;
        }

        public static List<string> AddTracksToPlaylist(string id, string access_token, List<TrackObject> songData)
        {
            string addTracksEndpoint = "" + baseUrl + "/v1/playlists/" + id + "/tracks";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(addTracksEndpoint);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization: Bearer " + access_token);

            List<string> songs = new List<string>();
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
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

                    string song = track.Name + "-- " + artistNames;
                    song = song.TrimEnd(',');
                    songs.Add(song);
                }
                body = body.TrimEnd(',') + "]}";

                streamWriter.Write(body);
            }

            string json = Helpers.ReadResponseToString(request);
            return songs;
        }

        public static PlaylistObject CreatePlaylist(string name, string access_token, string user)
        {
            string playlistEndpoint = "" + baseUrl + "/v1/users/" + user + "/playlists";
            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(playlistEndpoint);

            newRequest.Method = "POST";
            newRequest.ContentType = "application/json";
            newRequest.Headers.Add("Authorization: Bearer " + access_token);


            using (var streamWriter = new StreamWriter(newRequest.GetRequestStream()))
            {
                string body = ("{\"name\":\""+ name +"\"}");

                streamWriter.Write(body);
            }

            string newJson = Helpers.ReadResponseToString(newRequest);

            PlaylistObject playlist = JsonConvert.DeserializeObject<PlaylistObject>(newJson);
            return playlist;
        }

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
