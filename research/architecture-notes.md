## Architecture notes/ideas

#### How should Unity recieve game updates (eg. data from sensors, real or fake)
* Network sockets?
* HTTP API? REST?
    * UnityWebRequest (deprecated)?

#### Where should game logic be done?
* Within Unity itself?
    * Unity recieves information from the backend, and sends nothing back
* On the backend?
    * Unity is just an "interface" for the game, which actually runs on the backend
    * Unity receives new data from the backend, Unity sends player actions back to backend, backend computes new game state, etc..

#### More questions
* What should the game's control panel look like?
* What types of controls should be available to the player?
* What will be provided for the game's creation?
    * Models? Data format/schema/protocol for game updates with sensor info etc?
* How should the simulation go/progress?