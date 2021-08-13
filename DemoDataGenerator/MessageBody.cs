// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
using Newtonsoft.Json;

namespace DemoDataGenerator
{
    public class MessageBody
    {
        [JsonProperty("InstanceId")]
        public int InstanceId { get; set; }

        [JsonProperty("machine")]
        public Machine Machine { get; set; }

        [JsonProperty("ambient")]
        public Ambient Ambient { get; set; }
        
        [JsonProperty("timeCreated")]
        public string TimeCreated { get; set; }
    }
}