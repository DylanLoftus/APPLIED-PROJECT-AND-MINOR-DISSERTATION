# coding: utf-8

from __future__ import absolute_import

import sys
sys.path.append("..")

from datetime import date, datetime  # noqa: F401

from typing import List, Dict  # noqa: F401

from swagger_server.models.base_model_ import Model
from swagger_server.models.data_point import DataPoint  # noqa: F401,E501
from swagger_server import util


class WeatherHistory(Model):
    """NOTE: This class is auto generated by the swagger code generator program.

    Do not edit the class manually.
    """
    def __init__(self, description: str=None, length: int=None, data: List[DataPoint]=None):  # noqa: E501
        """WeatherHistory - a model defined in Swagger

        :param description: The description of this WeatherHistory.  # noqa: E501
        :type description: str
        :param length: The length of this WeatherHistory.  # noqa: E501
        :type length: int
        :param data: The data of this WeatherHistory.  # noqa: E501
        :type data: List[DataPoint]
        """
        self.swagger_types = {
            'description': str,
            'length': int,
            'data': List[DataPoint]
        }

        self.attribute_map = {
            'description': 'description',
            'length': 'length',
            'data': 'data'
        }
        self._description = description
        self._length = length
        self._data = data

    @classmethod
    def from_dict(cls, dikt) -> 'WeatherHistory':
        """Returns the dict as a model

        :param dikt: A dict.
        :type: dict
        :return: The WeatherHistory of this WeatherHistory.  # noqa: E501
        :rtype: WeatherHistory
        """
        return util.deserialize_model(dikt, cls)

    @property
    def description(self) -> str:
        """Gets the description of this WeatherHistory.


        :return: The description of this WeatherHistory.
        :rtype: str
        """
        return self._description

    @description.setter
    def description(self, description: str):
        """Sets the description of this WeatherHistory.


        :param description: The description of this WeatherHistory.
        :type description: str
        """
        if description is None:
            raise ValueError("Invalid value for `description`, must not be `None`")  # noqa: E501

        self._description = description

    @property
    def length(self) -> int:
        """Gets the length of this WeatherHistory.


        :return: The length of this WeatherHistory.
        :rtype: int
        """
        return self._length

    @length.setter
    def length(self, length: int):
        """Sets the length of this WeatherHistory.


        :param length: The length of this WeatherHistory.
        :type length: int
        """
        if length is None:
            raise ValueError("Invalid value for `length`, must not be `None`")  # noqa: E501

        self._length = length

    @property
    def data(self) -> List[DataPoint]:
        """Gets the data of this WeatherHistory.


        :return: The data of this WeatherHistory.
        :rtype: List[DataPoint]
        """
        return self._data

    @data.setter
    def data(self, data: List[DataPoint]):
        """Sets the data of this WeatherHistory.


        :param data: The data of this WeatherHistory.
        :type data: List[DataPoint]
        """
        if data is None:
            raise ValueError("Invalid value for `data`, must not be `None`")  # noqa: E501

        self._data = data
