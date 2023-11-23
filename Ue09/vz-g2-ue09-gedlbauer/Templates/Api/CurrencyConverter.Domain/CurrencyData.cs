using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CurrencyConverter.Domain
{
  [DataContract]
  // [JsonObject(ItemRequired = Required.Always)]
  public class CurrencyData
  {
    [DataMember(IsRequired = true)]
    // [JsonProperty(Required = Required.Always)]
    public string Symbol { get; set; }

    [DataMember(IsRequired = true)]
    // [JsonProperty(Required = Required.Always)]
    public string Name { get; set; }

    [DataMember] // Default: IsRequired=false
    // [JsonProperty(Required = Required.AllowNull)]
    public string Country { get; set; }

    [DataMember(IsRequired = true)]
    // [JsonProperty(Required = Required.Always)]
    public double EuroRate { get; set; }
   
    public override string ToString()
    {
      return String.Format($"{Name} ({Symbol}): euroRate={EuroRate}; country={Country}");
    }
  }

  public class CurrencyData2
  {
    public string Symbol { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public double EuroRate { get; set; }

    public override string ToString()
    {
      return String.Format($"{Name} ({Symbol}): euroRate={EuroRate}; country={Country}");
    }
  }

}
