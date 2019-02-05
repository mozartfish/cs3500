SPACEWARS

SUMMARY
A modern version of the classic 1977 game designed by Larry Rosenthal which is based on the classic 1962 Spacewar! game developed by
Steve Russell for the DEC PDP-1 at MIT. Spacewars is a combat space game where 1 or more players control different ships with the aim 
of eliminating the other players in the game. There is a star in the game that has a gravitational force that pulls in the different player
ships in an attempt to make them crash. Players receive points for the number of spaceships that they destroy. An extraContent mode allows
users to form two teams and play against each other. In addition to team mode, extraContent mode also contains projectile wrap around where projectiles
do not die upon hitting the edge of the screen, upon impact with one another projectiles die, and gravity pulls projectiles toward the star where upon impact
with the star, projectiles die.
Authors
Pranav Rajan and Thomas Ady

SOFTWARE PRACTICE

SUMMARY
This project uses MVC architecture and Client-Server engineering. The project was broken down into two phases. For phase 1, we built the client over
two weeks. The client uses network architecture, multi-threading and MVC architecture. For data structures we used a mixture of Sorted Dictionaries,
Dictionaries and IEnumerables for keeping track of the data in the world that the view accessses for painting the images on the screen. For the MVC architecture
we separated the concerns by having the following classes: SpaceWars View which drew all the game and the scoreboard, World Model which kept track of 
all the entities that were going to be drawn for the game, and the Controllers Class which contained the code for the GameController, Network Controller and
and SocketState. For phase 2, we built the game mechanics including adding physics to the world, added an extra feature mode where the server provides certain features
that are relayed to the client to enable, and constructed the logic for entities in the game. We also updated our networking code to include code for the server such as 
code for reading user input in the XML file that customizes game settings.


SERVER

STUFF WE WANT THE 3500 STAFF TO KNOW ABOUT THE PROJECT (CLIENT)
1) We decided to not display our name and server address in a menustrip like what the 3500 Staff Client had. 
Instead we decided to show the name and server address at the beginning when the user is attempting to connect to the server.
2) We added multi-threading for our help menu where the user can view the control instructions while playing the game
3) For XP we decided to have a green bar with a red bar. When a player is losing XP, the red bar begins to display and when the
red bar is fully displayed it means the player has died
4) We added an explosion sprite for when a ship is destroyed
5) We managed to correctly implement the scoreboard such that the players are ranked on the scoreboard according to their score from highest to lowest
6) When a connection is faulty, we display a message for when the user enters an incorrect server address
7) When a user attempts to open the game without the server running we have a display message indicating that the server  is not up
8) If a server disconnects, the initial connection screen appears again

STUFF WE WANT THE 3500 STAFF TO KNOW ABOUT THE PROJECT (SERVER)
1) We added projectile wrap around for the extra feature mode
2) In extra feature mode when projectiles crash with one another they are destroyed
3) In extra feature mode, we added gravity to the projectiles such that when they are fired the star's gravity pulls in the projectile where upon impact with 
the star the projectile is destroyed
4) In extra feature mode we also have a team mode where players are given a unique team ID that is associtated with their name. To determine the correct team
we use modulo by 2 to determine whether the player is odd or even and then assign the player to the correct team. For the scoreboard, the player's team is 
displayed along with their score in the game
5) In the XML file, we have a extra content node that determines whether a user plays in the extra mode or regular mode. If the extra content node is missing
or the value is anything that is not "enabled" then the extra feature mode is disabled and the user plays the game in regular mode.
6) When running our unit tests, the generate safe spawn test may not actually function properly when trying to spawn in the way of the sun, which is supposed to
trigger the method to rerun. This is rare, but if it happens try running the method again, the world should be at 99.84% coverage on a proper run 
(Code Coverage was achieved on CADE Lab Machine Lab 6-28 when running the server on debug mode)

CLIENT DOCUMENTATION
DAY 1 - November 2, 2018
Today we set up the required projects and began work on figuring out how to connect to the server.

DAY 2 - November 3, 2018
Today we set up the classes for the ships, projectiles and stars. We also began experimenting with how to include
the images provided by Kopta for use in the drawing panel. We also began work on the drawing panel by studying Kopta's
example and using that as starting code for the game.

DAY 3 - November 6, 2018
Today we figured out how to include the images and draw them on the drawing panel. Tom found the graphics methods to draw images from
png files and Pranav figured out how to access the files from the images folder in the Resources folder. There were some IO exceptions
that we had with the file path that Jin helped us fix. We still have to figure out how to handle the image size for the game since
the PNG images provided by Kopta are really big. We added add and remove methods for the images today in the world class. We also got
the drawing panel working.

DAY 4 - November 9, 2018
Today we completed most of our networking methods such as the send, receive callback, connected callback, get data, etc. We also worked
on error handling and other stuff and began talking with the TA's and planning how we are going to have event handlers for the game. We 
think our networking code is mostly complete but we won't know for sure until we connect to the server to play the game. We also began brainstorming ideas for
how to connect the view to receiving and implementing the data from the server.

DAY 5 - November 10, 2018
Today we worked on most of the networking and and the game controller. We managed to get our networking correct such that we could communicate with the server 
and most of the networking problems that we had earlier. Currently we are having problems with drawing the images and getting everything in sync. At this point
we think we have covered most of the requirements for the client.

DAY 6 - November 11, 2018
Today we managed to include explosions. We attempted to test the client but ended up with many heisenbugs including spawning multiple versions of our player
We attempted to debug but there were still a lot of problems which we could not find. We got our scoreboard working correctly and behaving the way kopta suggested.

DAY 7 - November 13, 2018
Today we debugged why our painting was going wrong for our drawing panel and why when we pressed the spacebar we we connected multiple times.
With the help of Danny, we learned that the reason why we were having multiple connections and being unable to use our control keys was that 
buttons are also linked to keys such as spacebar and the enter key. Using Danny's advice we fixed our code and were able to get our client working.
We also fixed our painting problem that we had with our data structures that was caused by using an IEnumerable instead of a Dictionary. We attempted to
add other features such as the name of the player following the ship.

DAY 8 - November 15, 2018
Today we corrected the last couple of bugs that we had such as null dataProcessed(), the scoreboard issue that we had where the score was not updating
correctly if we had had >=10 AI and our multi-threading issues etc. We also looked for other bugs that may occur and potential heisenbugs that may have happened
that we diden't see earlier. Other than that there wasn't much stuff that we could find to fix. At this point we think our client behaves correctly according to
the specs specified by the 3500 Staff. Kopta also gave us really good advice about how to handle errors that may occur with closing the client.

TO DO
NONE!!!!!!!

SERVER DOCUMENTATION
DAY 1 - November 18, 2018
Today we made sure that our PS7 worked correctly in release mode. Afterwards we started laying out the foundation classes and methods
that we would need for the Server part of the project. We also got the networking code going.

DAY 2 - November 20, 2018
Today we continued working on the code and began looking at how to add the event loop to the program. With Jin's advice, we created a timer
to time when we should refresh and paint the screen. We also managed to draw a ship and star on the panel by sending data.

DAY 3 - November 27, 2018
Today we added in some methods that allow us to keep track of the commands for each user. We also began working on adding the physics into the game and during our session
today we added the flight mechanics for the ship and the gravity mechanics for the star.

DAY 4 - November 30, 2018
Today we added figured out how to add the star and get a ship turning. However the thrust is broken and we can only turn left(Robert Lafferty Bug).(UPDATE) We fixed the Robert Lafferty bug. 
We also have to figure out what our special feature and add some of the game mechanics in. In addition, the ship is also not moving closer to the sun.

DAY 5 - December 1, 2018
Today we worked on updating how we ships collided with the sun and with projectiles. We solved these problems and had no issues with lag, etc
We also added in respawning and adding a random number generator for the ships to be respawned at random places on the screen and not off the screen.
We have some issues with the 3500 Staff Client where there is a delay in the explosion animation and who has control when  multiple clients are open 
(ie. when we open our client and the 3500 Staff Client and try to play the game at the same time) (UPDATE) We managed to fix these problems when we saw
that we had a bug in our velocity calculations for when a ship dies and another bug located where our client disconnected.

DAY 6 - December 2, 2018
Today we added in the special features for our game. The features we added included projectile wrap around, team mode, projectile collision with other projectiles
and projectile gravity. We still have to do debugging and final polish. So far we do not not think that we have any errors with the 3500 Staff Client, 
however we have not tried working with other people's clients and servers so we do not know for sure if our server works correctly. We also don't know if we
need to add more extra features to our game. The 3500 Staff Client behaves as we expect it to behave when we implement extra content mode for our server.

DAY 7 - December 4, 2018
Today we continued debugging. We achieved 99.84% code coverage with the missing 0.16 due to being unable to hit the curly braces for the set private property for the world
Minor bugs were fixed that caused exceptions to be thrown with the send callback and generating random spawns. Polish was added for adding a couple print statements
to the console to show when the server was up and when a client has connected. We also tested our server by individually connecting to a CADE Lab Machine (Lab6 - 28)
to play the game against one another.Pranav and Tom connected on their laptops and added Robert Lafferty to the CADE LAB Machine(Lab 6-28) and played against one another.
We also hosted a game where we had a couple of people in the CADE lab connect their clients to our server and there were no issues. We didn't experience lag when we 
played until we added about 50-100 AI to the game. Upon further analysis of why lag was occuring when we added 100 AI to the game, we found that the CADE Lab Machine CPU 
was doing about 60% work while our server did about 10% work which resulted in the lag. After testing our client and server together, we tested our server agains the 3500 Staff
Client and produced the same results that occured with our client.

TO DO
NONE!!!!!!!!!!!!!!


