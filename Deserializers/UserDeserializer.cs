using Discord.Descriptors;
using Discord.Json.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MapCord.Deserializers
{
    public class UserDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UserObject);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return JToken.ReadFrom(reader).ToObject<UserDescriptor>(serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
