import pandas as pd
import numpy as np
import json

# some maths to work out how many lines of data are needed for a certain length of simulation time
# how long the simulation should last for a single set of data (minutes)
sim_length_mins = 15
# how many seconds of simulated time make up one "real" hour from the dataset
seconds_per_hour = 5
# lines needed for one run of the simulation
sim_set_length = (sim_length_mins * 60) // seconds_per_hour
# number of simulation sets to extract from the raw data
num_sets = 15
# number of lines to skip from the start of the csv file
num_skip = 10000

# 15 simulated minutes = 7.5 "real" days

# path of raw weather data (csv)
input_file_path = "../data/raw/athenry-hourly.csv"

out_dir = "../data/json/athenry"

# remove lines above column headers and null data cells (" ") from csv file before running this script

# declare column data types
dtypes = {
    "date": np.str,
    "ind1": np.byte,
    "rain": np.float64,
    "ind2": np.byte,
    "temp": np.float64,
    "ind3": np.float64,
    "wetb": np.float64,
    "dewpt": np.float64,
    "vappr": np.float64,
    "rhum": np.float64,
    "msl": np.float64,
    "ind4": np.float64,
    "wdsp": np.float64,
    "ind5": np.float64,
    "wddir": np.float64
}

# load CSV into pandas dataframe
df = pd.read_csv(input_file_path, dtype=dtypes)
# convert date string to datetime
# df["date"] = pd.to_datetime(df.date, format="%d-%b-%Y %H:%M")

# create json data sets
print("\nConverting CSV from:\n\t{}\ninto JSON and dumping into:\n\t{}\n\n...\n".format(input_file_path, out_dir))
line = num_skip
for i in range(num_sets):
    weather_history = {
        "description": "Data set description here",
        "length": sim_set_length,
        "data": []
    }

    # populate data array
    for j in range(line, line + sim_set_length):
        row = df.iloc[j]
        weather_history["data"].append(
            {
                "timestamp": row["date"],
                "temperature": row["temp"],
                "windspeed": row["wdsp"]
            }
        )

    # dump json file
    out_path = "{}/{}.json".format(out_dir, str(i))
    with open(out_path, "w") as out_file:
        json.dump(weather_history, out_file)

    line += sim_set_length

print("Finished.")