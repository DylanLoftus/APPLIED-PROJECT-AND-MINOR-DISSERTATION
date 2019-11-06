import connexion
import six

from swagger_server.models.weather_history import WeatherHistory  # noqa: E501
from swagger_server import util


def get_weather_history(area, dataset):  # noqa: E501
    """retrieves weather history

    Retrieves a premade dataset of weather history for a specified area  # noqa: E501

    :param area: location of weather data to use
    :type area: str
    :param dataset: dataset number to use
    :type dataset: int

    :rtype: WeatherHistory
    """
    return 'do some magic!'
