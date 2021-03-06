﻿using Discord.Descriptors.Guilds;
using Discord.Json.Objects.Guilds;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MapCord.Deserializers
{
    public class EmojiDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EmojiObject);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return JToken.ReadFrom(reader).ToObject<EmojiDescriptor>(serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
