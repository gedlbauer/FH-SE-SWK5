using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Api.Dtos
{
    public class CustomerCreationDto
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public Guid Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int ZipCode { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string City { get; set; }

        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Rating Rating { get; set; }
    }
}
