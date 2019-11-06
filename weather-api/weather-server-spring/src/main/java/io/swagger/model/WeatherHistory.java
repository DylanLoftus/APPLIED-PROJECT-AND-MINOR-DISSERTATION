package io.swagger.model;

import java.util.Objects;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonCreator;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import io.swagger.model.DataPoint;
import java.util.ArrayList;
import java.util.List;
import org.springframework.validation.annotation.Validated;
import javax.validation.Valid;
import javax.validation.constraints.*;

/**
 * WeatherHistory
 */
@Validated
@javax.annotation.Generated(value = "io.swagger.codegen.v3.generators.java.SpringCodegen", date = "2019-11-02T10:07:25.240Z[GMT]")
public class WeatherHistory   {
  @JsonProperty("description")
  private String description = null;

  @JsonProperty("length")
  private Integer length = null;

  @JsonProperty("data")
  @Valid
  private List<DataPoint> data = new ArrayList<DataPoint>();

  public WeatherHistory description(String description) {
    this.description = description;
    return this;
  }

  /**
   * Get description
   * @return description
  **/
  @ApiModelProperty(required = true, value = "")
      @NotNull

    public String getDescription() {
    return description;
  }

  public void setDescription(String description) {
    this.description = description;
  }

  public WeatherHistory length(Integer length) {
    this.length = length;
    return this;
  }

  /**
   * Get length
   * @return length
  **/
  @ApiModelProperty(required = true, value = "")
      @NotNull

    public Integer getLength() {
    return length;
  }

  public void setLength(Integer length) {
    this.length = length;
  }

  public WeatherHistory data(List<DataPoint> data) {
    this.data = data;
    return this;
  }

  public WeatherHistory addDataItem(DataPoint dataItem) {
    this.data.add(dataItem);
    return this;
  }

  /**
   * Get data
   * @return data
  **/
  @ApiModelProperty(required = true, value = "")
      @NotNull
    @Valid
    public List<DataPoint> getData() {
    return data;
  }

  public void setData(List<DataPoint> data) {
    this.data = data;
  }


  @Override
  public boolean equals(java.lang.Object o) {
    if (this == o) {
      return true;
    }
    if (o == null || getClass() != o.getClass()) {
      return false;
    }
    WeatherHistory weatherHistory = (WeatherHistory) o;
    return Objects.equals(this.description, weatherHistory.description) &&
        Objects.equals(this.length, weatherHistory.length) &&
        Objects.equals(this.data, weatherHistory.data);
  }

  @Override
  public int hashCode() {
    return Objects.hash(description, length, data);
  }

  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder();
    sb.append("class WeatherHistory {\n");
    
    sb.append("    description: ").append(toIndentedString(description)).append("\n");
    sb.append("    length: ").append(toIndentedString(length)).append("\n");
    sb.append("    data: ").append(toIndentedString(data)).append("\n");
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
