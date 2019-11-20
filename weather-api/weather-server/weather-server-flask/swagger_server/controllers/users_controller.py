import sys
sys.path.append("..")

import connexion
import six

from models.weather_history import WeatherHistory  # noqa: E501
from models.weather_history import DataPoint
import util
import pymongo
import gridfs
from flask import abort


def get_weather_history(area, dataset):  # noqa: E501
    """retrieves weather history

    Retrieves a premade dataset of weather history for a specified area  # noqa: E501

    :param area: location of weather data to use
    :type area: str
    :param dataset: dataset number to use
    :type dataset: int

    :rtype: WeatherHistory
    """
    try:
        # test = WeatherHistory("Test desc", 1, [DataPoint("15-aug-2012 17:00", 17, 6)])

        # user Ronan is read-only for security
        mongo = pymongo.MongoClient("mongodb://Ronan:4thyearproject2019@ds243518.mlab.com:43518/weather")
        db = mongo["weather"]
        coll = db.get_collection(area)
        doc = list(coll.find({}))[dataset]
        del doc["_id"]
        # this line seems redundant, you can just return the doc dict instead
        response = WeatherHistory(**doc)

        return response
    except:
        abort(404)
