﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiClientApp_v1.Models
{
    public class City
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
        public int Id { get; set; }

    }
}
