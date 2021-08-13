// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using Newtonsoft.Json;

namespace DemoDataGenerator
{
    [JsonObject("machine")]
    public class Machine
    {
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        [JsonProperty("pressure")]
        public double Pressure { get; set; }
    }
}