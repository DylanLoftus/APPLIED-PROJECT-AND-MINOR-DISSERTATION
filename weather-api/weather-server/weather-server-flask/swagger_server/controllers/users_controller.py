import connexion
import six

from ..models.weather_history import WeatherHistory  # noqa: E501
from ..models.weather_history import DataPoint
from .. import util


def get_weather_history(area, dataset):  # noqa: E501
    """retrieves weather history

    Retrieves a premade dataset of weather history for a specified area  # noqa: E501

    :param area: location of weather data to use
    :type area: str
    :param dataset: dataset number to use
    :type dataset: int

    :rtype: WeatherHistory
    """
    test = WeatherHistory("Test desc", 1, [DataPoint("15-aug-2012 17:00", 17, 6)])
    return test
