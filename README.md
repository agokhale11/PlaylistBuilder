# PlaylistBuilder
Web application that uses the Spotify API to build users personalized playlists based on listening history

<h2>Overview</h2>

This application uses the ASP.NET Core web framework to construct an application that builds playlists for users based on their 
listening history and selected preferences. It interacts with the Spotify API to allow users to login and authenticate themselves. From there,
a variety of endpoints exist to allow for the collection of listening data and song traits. Those are used, along with preferences to construct
algorithms for creating playlists effectively.

<b>Types of playlists you can build</b>
- Top 50 most played songs from a user
- 50 most recently played songs from a user
- playlists based on danceability, energy, and tempo

In regards to the last bullet point, a user can put down a number from 1-5 for how energetic, how danceable, and how fast the tempo they want
their playlist to be. That information is combined with genre data from their listening history to create an ideal playlist.

<h2>How to use</h2>

To use this application, clone this repository to your local machine and run the application. Because this application is not hosted anywhere,
you may need to free up the ports 51257 or 44383 to listen on.


<h2>Support</h2>
Contact Aditya Gokhale (aditya.p.gokhale@vanderbilt.edu) with any questions about this project.
