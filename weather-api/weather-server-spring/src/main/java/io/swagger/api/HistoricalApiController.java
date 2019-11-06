package io.swagger.api;

import io.swagger.model.WeatherHistory;
import com.fasterxml.jackson.databind.ObjectMapper;
import io.swagger.annotations.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RequestPart;
import org.springframework.web.multipart.MultipartFile;

import javax.validation.constraints.*;
import javax.validation.Valid;
import javax.servlet.http.HttpServletRequest;
import java.io.IOException;
import java.util.List;
import java.util.Map;
@javax.annotation.Generated(value = "io.swagger.codegen.v3.generators.java.SpringCodegen", date = "2019-11-02T10:07:25.240Z[GMT]")
@Controller
public class HistoricalApiController implements HistoricalApi {

    private static final Logger log = LoggerFactory.getLogger(HistoricalApiController.class);

    private final ObjectMapper objectMapper;

    private final HttpServletRequest request;

    @org.springframework.beans.factory.annotation.Autowired
    public HistoricalApiController(ObjectMapper objectMapper, HttpServletRequest request) {
        this.objectMapper = objectMapper;
        this.request = request;
    }

    public ResponseEntity<WeatherHistory> getWeatherHistory(@ApiParam(value = "location of weather data to use",required=true) @PathVariable("area") String area,@ApiParam(value = "dataset number to use",required=true) @PathVariable("dataset") Integer dataset) {
        String accept = request.getHeader("Accept");
        if (accept != null && accept.contains("application/json")) {
            try {
                return new ResponseEntity<WeatherHistory>(objectMapper.readValue("{\n  \"data\" : [ {\n    \"temperature\" : 10,\n    \"windspeed\" : 3\n  }, {\n    \"temperature\" : 10,\n    \"windspeed\" : 3\n  } ],\n  \"length\" : 0,\n  \"description\" : \"description\"\n}", WeatherHistory.class), HttpStatus.NOT_IMPLEMENTED);
            } catch (IOException e) {
                log.error("Couldn't serialize response for content type application/json", e);
                return new ResponseEntity<WeatherHistory>(HttpStatus.INTERNAL_SERVER_ERROR);
            }
        }

        return new ResponseEntity<WeatherHistory>(HttpStatus.NOT_IMPLEMENTED);
    }

}
