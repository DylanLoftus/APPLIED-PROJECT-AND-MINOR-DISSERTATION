# coding: utf-8

from __future__ import absolute_import

from flask import json
from six import BytesIO

from swagger_server.models.weather_history import WeatherHistory  # noqa: E501
from swagger_server.test import BaseTestCase


class TestUsersController(BaseTestCase):
    """UsersController integration test stubs"""

    def test_get_weather_history(self):
        """Test case for get_weather_history

        retrieves weather history
        """
        response = self.client.open(
            '/Ronan-H/weather-api/1.0.0/historical/{area}/{dataset}'.format(area='area_example', dataset=56),
            method='GET')
        self.assert200(response,
                       'Response body is : ' + response.data.decode('utf-8'))


if __name__ == '__main__':
    import unittest
    unittest.main()
