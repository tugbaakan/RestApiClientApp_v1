using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiClientApp_v1.Models
{
    public class Country
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("iso_a2")]
        public string Code { get; set; }
        

    }
}
