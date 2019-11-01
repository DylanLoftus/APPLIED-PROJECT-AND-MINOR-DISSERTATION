
Here is a mockup of a simple RESTful API design for the project.

This API can be used to retrieve historical hourly weather data (temperature being most relevant) from different places. To record live data later in the project, it may be useful to also have PUT/POST resources.

General structure:

**GET**:
    /(historical|test|randomly generated)/(place)/(set number)

    Example: /historical/Athenry/2

    Returns (JSON):
    {
        "hours": 3,

        "data": [
            {
                "timestamp":"2pm, October 12th",
                "data": [
                    "temperature": 10,
                    "windspeed": 3
                ]
            }
        ],
        [
            {
                "timestamp":"3pm, October 12th",
                "data": [
                    "temperature": 8,
                    "windspeed": 5
                ]
            }
        ],
        [
            {
                "timestamp":"4pm, October 12th",
                "data": [
                    "temperature": 8,
                    "windspeed": 4
                ]
            }
        ],
    }

Doing it this way allows the client (the Unity simulation) to access a set of hourly weather data on an hour by hour basis, or all at once. The timestamp could be displayed to the user as time passes and weather changes.
