## Streaming live sensor data

One idea for our project involves streaming live sensor data from an IoT device (e.g. a temperature sensor on a Raspberry Pi) straight into the Unity simulation. This could also tie in to some other ideas, such as recording weather data to the database on the weather server, which could be simulated inside the Unity simulation at another time.

Since the existing RESTful weather API may not be an appropriate, efficient, or practical method of streaming live data to the simlation, another method should be used. gRPC may be a good method for streaming this data, because:

* It is extremely fast
* It is language and platform independant (the server on the Pi can be written in Java or Python for example, which can seamlessly communicate with the C# based Unity engine)
* Client and server stubs can be generated automatically, taking care of any low level implementation details

Luckily, there appears to be a gRPC package for unity available from [here](https://packages.grpc.io). Hopefully this can be used without too many complications.

### The protocol

gRPC services rely on the use of a service definition, which defines how clients can communicate with the service. Here is an initial first draft for the service definition for the streaming of sensor data:

```protobuf
service SensorService {
    rpc StreamData(StreamParams) returns (stream SensorData);
    rpc StopStream(Nothing) returns (Nothing);
}

message StreamParams {
    // rate at which new sensor data is sent, in seconds
    double sensorUpdateRate = 1;
}

message SensorData {
    string timestamp = 1;
    double temperature = 2;
    // perhaps more types of sensor data here if needed
}

// since rpc methods must include paramaters and a return value...
message Nothing {}

```

It may be necessary to change this later on to fit the needs of the project. Ideally, the service should be implemented in a way that allows any number of clients to connect at once and receive sensor data, and allow clients to connect/disconnect seamlessly from the service.