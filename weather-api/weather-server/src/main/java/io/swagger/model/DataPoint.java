package io.swagger.model;

import java.util.Objects;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonCreator;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import java.math.BigDecimal;
import org.springframework.validation.annotation.Validated;
import javax.validation.Valid;
import javax.validation.constraints.*;

/**
 * DataPoint
 */
@Validated
@javax.annotation.Generated(value = "io.swagger.codegen.v3.generators.java.SpringCodegen", date = "2019-11-02T10:07:25.240Z[GMT]")
public class DataPoint   {
  @JsonProperty("temperature")
  private BigDecimal temperature = null;

  @JsonProperty("windspeed")
  private BigDecimal windspeed = null;

  public DataPoint temperature(BigDecimal temperature) {
    this.temperature = temperature;
    return this;
  }

  /**
   * Get temperature
   * @return temperature
  **/
  @ApiModelProperty(example = "10", required = true, value = "")
      @NotNull

    @Valid
    public BigDecimal getTemperature() {
    return temperature;
  }

  public void setTemperature(BigDecimal temperature) {
    this.temperature = temperature;
  }

  public DataPoint windspeed(BigDecimal windspeed) {
    this.windspeed = windspeed;
    return this;
  }

  /**
   * Get windspeed
   * @return windspeed
  **/
  @ApiModelProperty(example = "3", value = "")
  
    @Valid
    public BigDecimal getWindspeed() {
    return windspeed;
  }

  public void setWindspeed(BigDecimal windspeed) {
    this.windspeed = windspeed;
  }


  @Override
  public boolean equals(java.lang.Object o) {
    if (this == o) {
      return true;
    }
    if (o == null || getClass() != o.getClass()) {
      return false;
    }
    DataPoint dataPoint = (DataPoint) o;
    return Objects.equals(this.temperature, dataPoint.temperature) &&
        Objects.equals(this.windspeed, dataPoint.windspeed);
  }

  @Override
  public int hashCode() {
    return Objects.hash(temperature, windspeed);
  }

  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder();
    sb.append("class DataPoint {\n");
    
    sb.append("    temperature: ").append(toIndentedString(temperature)).append("\n");
    sb.append("    windspeed: ").append(toIndentedString(windspeed)).append("\n");
    sb.append("}");
    return sb.toString();
  }

  /**
   * Convert the given object to string with each line indented by 4 spaces
   * (except the first line).
   */
  private String toIndentedString(java.lang.Object o) {
    if (o == null) {
      return "null";
    }
    return o.toString().replace("\n", "\n    ");
  }
}
