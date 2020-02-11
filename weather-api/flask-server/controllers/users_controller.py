import sys
sys.path.append("..")

import connexion
import six

from models.weather_history import WeatherHistory
from models.weather_history_listing import WeatherHistoryListing
from models.data_point import DataPoint
import util
import pymongo
import gridfs
from flask import abort, jsonify

mongo = None

def get_weather_history(area, dataset):  # noqa: E501
    """retrieves weather history

    Retrieves a premade dataset of weather history for a specified area  # noqa: E501

    :param area: location of weather data to use
    :type area: str
    :param dataset: dataset number to use
    :type dataset: int

    :rtype: WeatherHistory
    """
    global mongo

    try:
        if mongo is None:
            mongo = pymongo.MongoClient("mongodb://Ronan:4thyearproject2019@ds243518.mlab.com:43518/weather")

        # user Ronan is read-only for security
        db = mongo["weather"]
        coll = db.get_collection(area)
        doc = list(coll.find({}))[dataset]
        del doc["_id"]
        # this line seems redundant, you can just return the doc dict instead
        response = WeatherHistory(**doc)

        return response
    except:
        abort(404)

def get_data_point(area, dataset, datapoint):  # noqa: E501
    """retrieves a specific DataPoint from a WeatherHistory set

    Retrieves a specific DataPoint from a WeatherHistory set  # noqa: E501

    :param area: location of weather data to use
    :type area: str
    :param dataset: dataset number to use
    :type dataset: int
    :param datapoint: index of specific DataPoint to get from the WeatherHistory
    :type datapoint: int

    :rtype: DataPoint
    """
    try:
        return get_weather_history(area, dataset).data[datapoint]
    except:
        abort(404)

def get_historical():  # noqa: E501
    """retrieves a list of historical weather history endpoints

    Retrieves a list of historical weather history endpoints  # noqa: E501


    :rtype: List[WeatherHistoryListing]
    """
    global mongo

    if mongo is None:
        mongo = pymongo.MongoClient("mongodb://Ronan:4thyearproject2019@ds243518.mlab.com:43518/weather")

    # user Ronan is read-only for security
    db = mongo["weather"]
    listings = []

    for coll_name in db.list_collection_names():
        if coll_name == "system.indexes":
            continue

        coll = db.get_collection(coll_name)
        dataset = 0
        for doc in list(coll.find({})):
            print("DOC: ", doc)
            link = "https://ronanstuff.strangled.net:53269/Ronan-H/weather-api/1.0.0/historical/{}/{}"\
                   .format(coll_name, dataset)

            # deserialaztion error here if WeatherHistoryListing object is used instead of dict, not sure why
            listings.append({
                "area": coll_name,
                "dataset": dataset,
                "start_time": doc["data"][0]["timestamp"],
                "length": doc["length"],
                "link": link
            })
            dataset += 1

    return listings
