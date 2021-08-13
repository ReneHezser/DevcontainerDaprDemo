// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using Newtonsoft.Json;

namespace DemoDataGenerator
{
    [JsonObject("ambient")]
    public class Ambient
    {
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}