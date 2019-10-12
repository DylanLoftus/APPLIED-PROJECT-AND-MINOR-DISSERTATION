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

## Sample network protocol

As of right now, it's hard to know if sockets, or a HTTP/RESTful API, would be more appropriate for the simulation. It's also hard to guess what sort of information the game would need to receive for updates. However, here is an example network protocol which may be useful as a starting point (to be used over sockets):

*(packets sent as lines of text, with comma separated values, in the format **id:value,value,value,...\n** for this example)*

**Protocol**
Packet ID | Sent from | Format (datatypes) | Format (desc) | Description
----------|-----------|--------------------|---------------|------------
0 | Client | (no values) | (no values) | Simulation start
1 | Server | int,bool | indicator id,state | Set state of indicator (eg. a warning light, warning siren, on/off)
2 | Server | id,double | environment variable id,level | Update of environment variable (eg. temperature in degrees celsius, pressure in pascals)
3 | Client | (no values) | (no values) | Simulation end

**Indicator list**
Indicator ID | Type | Description
-------------|------|------------
0 | Warning light | Temperature too high
1 | Warning light | Pressure too high
2 | Siren | Evacuate immediately

**Environment variables**
Environment variable ID | Name | Unit
-------------|------|------------
0 | Temperature | Celsius
1 | Pressure | Pascals

**Example packets**

Packet | Explanation
-------|------------
0: | Simulation start
1:1,true | Turn on "pressure too high" warning light
1:2,true | Turn on evacuation siren
2:0,35.0 | Temperature is at 35 degrees
3: | Simulation end

A similar looking protocol could be made based on HTTP requests. If we were to use HTTP, [Swagger](https://swagger.io) may be very useful to develop a protocol, which can automatically generate docomentation and client/server stubs based on an API description.

[Here](https://wiki.vg/Protocol) is an example of a more complicated server/client protocol, for Minecraft.