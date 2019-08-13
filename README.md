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

The homepage prompts the user to login to their spotify account (choosing "No thanks" reprompts the user with an explanation for the use of the data, as the application needs access to the account to proceed).
![Alt text](PlaylistBuilder/images/Home.png?raw=true)

The user is taken to the Spotify login page where they can enter their credentials.
![Alt text](PlaylistBuilder/images/Spotify.png?raw=true)

The user has the option to select which type of playlist to create (this walkthrough will focus on the custom type).

The user can then enter the parameters that should determine the content of the playlist (input is validated).
![Alt text](PlaylistBuilder/images/Custom.png?raw=true)

The next page displays which songs make up the playlist.
![Alt text](PlaylistBuilder/images/Done.png?raw=true)

The user can then navigate to their spotify account and see that all songs have been added to a playlist called "Recommended."
![Alt text](PlaylistBuilder/images/Playlist.png?raw=true)

More playlists can be built by selecting the "Build another playlist" button at the bottom of the screen. When the user is finished,
he or she can choose to logout, which revokes the apps access to their spotify account, and redirects them to the homepage.
![Alt text](PlaylistBuilder/images/Logout.png?raw=true)



<h2>Support</h2>
Contact Aditya Gokhale (aditya.p.gokhale@vanderbilt.edu) with any questions about this project.
