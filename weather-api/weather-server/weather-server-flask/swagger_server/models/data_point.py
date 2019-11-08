# coding: utf-8

from __future__ import absolute_import
from datetime import date, datetime  # noqa: F401

from typing import List, Dict  # noqa: F401

from ..models.base_model_ import Model
from .. import util


class DataPoint(Model):
    """NOTE: This class is auto generated by the swagger code generator program.

    Do not edit the class manually.
    """
    def __init__(self, timestamp: str=None, temperature: float=None, windspeed: float=None):  # noqa: E501
        """DataPoint - a model defined in Swagger

        :param timestamp: The timestamp of this DataPoint.  # noqa: E501
        :type timestamp: str
        :param temperature: The temperature of this DataPoint.  # noqa: E501
        :type temperature: float
        :param windspeed: The windspeed of this DataPoint.  # noqa: E501
        :type windspeed: float
        """
        self.swagger_types = {
            'timestamp': str,
            'temperature': float,
            'windspeed': float
        }

        self.attribute_map = {
            'timestamp': 'timestamp',
            'temperature': 'temperature',
            'windspeed': 'windspeed'
        }
        self._timestamp = timestamp
        self._temperature = temperature
        self._windspeed = windspeed

    @classmethod
    def from_dict(cls, dikt) -> 'DataPoint':
        """Returns the dict as a model

        :param dikt: A dict.
        :type: dict
        :return: The DataPoint of this DataPoint.  # noqa: E501
        :rtype: DataPoint
        """
        return util.deserialize_model(dikt, cls)

    @property
    def timestamp(self) -> str:
        """Gets the timestamp of this DataPoint.


        :return: The timestamp of this DataPoint.
        :rtype: str
        """
        return self._timestamp

    @timestamp.setter
    def timestamp(self, timestamp: str):
        """Sets the timestamp of this DataPoint.


        :param timestamp: The timestamp of this DataPoint.
        :type timestamp: str
        """

        self._timestamp = timestamp

    @property
    def temperature(self) -> float:
        """Gets the temperature of this DataPoint.


        :return: The temperature of this DataPoint.
        :rtype: float
        """
        return self._temperature

    @temperature.setter
    def temperature(self, temperature: float):
        """Sets the temperature of this DataPoint.


        :param temperature: The temperature of this DataPoint.
        :type temperature: float
        """
        if temperature is None:
            raise ValueError("Invalid value for `temperature`, must not be `None`")  # noqa: E501

        self._temperature = temperature

    @property
    def windspeed(self) -> float:
        """Gets the windspeed of this DataPoint.


        :return: The windspeed of this DataPoint.
        :rtype: float
        """
        return self._windspeed

    @windspeed.setter
    def windspeed(self, windspeed: float):
        """Sets the windspeed of this DataPoint.


        :param windspeed: The windspeed of this DataPoint.
        :type windspeed: float
        """

        self._windspeed = windspeed
