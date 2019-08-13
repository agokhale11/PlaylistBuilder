# PlaylistBuilder
Web application that uses the Spotify API to build users personalized playlists based on listening history

Currently hosted at: https://playlistbuilder.azurewebsites.net/

<h2>Overview</h2>

This application uses the ASP.NET Core web framework to construct an application that builds playlists for users based on their 
listening history and selected preferences. It interacts with the Spotify API to allow users to login and authenticate themselves. From there,
a variety of endpoints exist to allow for the collection of listening data and song traits. Those are used, along with preferences to construct
algorithms for creating playlists effectively.

<b>Types of playlists you can build</b>
- Top 50 most played songs from a user
- 50 most recently played songs from a user
- Playlists based on danceability, artists, and tempo

In regards to the last bullet point, a user can put down a number from 1-5 for how energetic, how danceable, and how fast the tempo they want
their playlist to be. That information is combined with genre data from their listening history to create an ideal playlist.

<h2>Walkthrough</h2>

![Alt text](PlaylistBuilder/images/Home.png?raw=true "First")



<h2>Support</h2>
Contact Aditya Gokhale (aditya.p.gokhale@vanderbilt.edu) with any questions about this project.
