﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
namespace Core.Models
{
  public class ForecastResponse
  {
    //CREDITS : https://quicktype.io/csharp/
    //I leveraged the above website to generate the response classes
    //I had to rename some classes/properties to suit the meaning
    [JsonProperty("cod")]
    [JsonConverter(typeof(ParseStringConverter))]
    public long Cod { get; set; }

    [JsonProperty("message")]
    public long Message { get; set; }

    [JsonProperty("cnt")]
    public long Cnt { get; set; }

    [JsonProperty("list")]
    public List<Forecast> ForecastList { get; set; }

    [JsonProperty("city")]
    public City City { get; set; }
  }

  public class City
  {
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("coord")]
    public Coord Coord { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("timezone")]
    public long Timezone { get; set; }

    [JsonProperty("sunrise")]
    public long Sunrise { get; set; }

    [JsonProperty("sunset")]
    public long Sunset { get; set; }
  }

  public class Coord
  {
    [JsonProperty("lat")]
    public double Lat { get; set; }

    [JsonProperty("lon")]
    public double Lon { get; set; }
  }

  public class Forecast
  {
    [JsonProperty("dt")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime ForecastDateTime { get; set; }

    [JsonProperty("main")]
    public ForecastVariable ForecastVariable { get; set; }

    [JsonProperty("weather")]
    public List<Weather> Weather { get; set; }

    [JsonProperty("clouds")]
    public Clouds Clouds { get; set; }

    [JsonProperty("wind")]
    public Wind Wind { get; set; }

    [JsonProperty("sys")]
    public Sys Sys { get; set; }

    [JsonProperty("dt_txt")]
    public DateTimeOffset DtTxt { get; set; }

    [JsonProperty("rain", NullValueHandling = NullValueHandling.Ignore)]
    public Rain Rain { get; set; }

    [JsonProperty("snow", NullValueHandling = NullValueHandling.Ignore)]
    public Rain Snow { get; set; }
  }

  public class Clouds
  {
    [JsonProperty("all")]
    public long All { get; set; }
  }

  public class ForecastVariable
  {
    [JsonProperty("temp")]
    public double Temp { get; set; }

    [JsonProperty("feels_like")]
    public double FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public double TempMin { get; set; }

    [JsonProperty("temp_max")]
    public double TempMax { get; set; }

    [JsonProperty("pressure")]
    public long Pressure { get; set; }

    [JsonProperty("sea_level")]
    public long SeaLevel { get; set; }

    [JsonProperty("grnd_level")]
    public long GrndLevel { get; set; }

    [JsonProperty("humidity")]
    public long Humidity { get; set; }

    [JsonProperty("temp_kf")]
    public double TempKf { get; set; }
  }

  public class Rain
  {
    [JsonProperty("3h")]
    public double The3H { get; set; }
  }

  public class Sys
  {
    [JsonProperty("pod")]
    public Pod Pod { get; set; }
  }

  public class Weather
  {
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("main")]
    public MainEnum Main { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }
  }

  public class Wind
  {
    [JsonProperty("speed")]
    public double Speed { get; set; }

    [JsonProperty("deg")]
    public long Deg { get; set; }
  }

  public enum Pod { D, N };

  public enum MainEnum { Clear, Clouds, Rain, Snow };

  internal static class Converter
  {
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
      DateParseHandling = DateParseHandling.None,
      Converters =
            {
                PodConverter.Singleton,
                MainEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
    };
  }

  internal class ParseStringConverter : JsonConverter
  {
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null) return null;
      var value = serializer.Deserialize<string>(reader);
      long l;
      if (Int64.TryParse(value, out l))
      {
        return l;
      }
      throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
      if (untypedValue == null)
      {
        serializer.Serialize(writer, null);
        return;
      }
      var value = (long)untypedValue;
      serializer.Serialize(writer, value.ToString());
      return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
  }

  internal class PodConverter : JsonConverter
  {
    public override bool CanConvert(Type t) => t == typeof(Pod) || t == typeof(Pod?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null) return null;
      var value = serializer.Deserialize<string>(reader);
      switch (value)
      {
        case "d":
          return Pod.D;
        case "n":
          return Pod.N;
      }
      throw new Exception("Cannot unmarshal type Pod");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
      if (untypedValue == null)
      {
        serializer.Serialize(writer, null);
        return;
      }
      var value = (Pod)untypedValue;
      switch (value)
      {
        case Pod.D:
          serializer.Serialize(writer, "d");
          return;
        case Pod.N:
          serializer.Serialize(writer, "n");
          return;
      }
      throw new Exception("Cannot marshal type Pod");
    }

    public static readonly PodConverter Singleton = new PodConverter();
  }

  internal class MainEnumConverter : JsonConverter
  {
    public override bool CanConvert(Type t) => t == typeof(MainEnum) || t == typeof(MainEnum?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null) return null;
      var value = serializer.Deserialize<string>(reader);
      switch (value)
      {
        case "Clear":
          return MainEnum.Clear;
        case "Clouds":
          return MainEnum.Clouds;
        case "Rain":
          return MainEnum.Rain;
        case "Snow":
          return MainEnum.Snow;
      }
      throw new Exception("Cannot unmarshal type MainEnum");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
      if (untypedValue == null)
      {
        serializer.Serialize(writer, null);
        return;
      }
      var value = (MainEnum)untypedValue;
      switch (value)
      {
        case MainEnum.Clear:
          serializer.Serialize(writer, "Clear");
          return;
        case MainEnum.Clouds:
          serializer.Serialize(writer, "Clouds");
          return;
        case MainEnum.Rain:
          serializer.Serialize(writer, "Rain");
          return;
        case MainEnum.Snow:
          serializer.Serialize(writer, "Snow");
          return;
      }
      throw new Exception("Cannot marshal type MainEnum");
    }

    public static readonly MainEnumConverter Singleton = new MainEnumConverter();
  }
}
