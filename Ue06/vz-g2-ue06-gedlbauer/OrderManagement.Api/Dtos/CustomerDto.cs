using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Api.Dtos
{
    //[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ZipCode { get; set; }

        //[JsonProperty(PropertyName = "location")]
        public string City { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Rating Rating { get; set; }

        //[JsonIgnore]
        public decimal TotalRevenue { get; set; }
    }
}
