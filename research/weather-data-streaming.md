## Streaming live weather data

One idea for our project involves streaming live "weather" data from an IoT device (e.g. a temperature sensor on a Raspberry Pi) straight into the Unity simulation. This could also tie in to some other ideas, such as recording weather data to the database on the weather server, which could be simulated inside the Unity simulation at another time.

Since the existing RESTful weather API may not be an appropriate, efficient, or practical method of streaming live data to the simlation, another method should be used. gRPC may be a good method for streaming this data, because:

* It is extremely fast
* It is language and platform independant (the server on the Pi can be written in Java or Python for example, which can seamlessly communicate with the C# based Unity engine)
* Client and server stubs can be generated automatically, taking care of any low level implementation details

Luckily, there appears to be a gRPC package for unity available from [here](https://packages.grpc.io). Hopefully this can be used without too many complications.