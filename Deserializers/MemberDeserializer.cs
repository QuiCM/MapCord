using Discord.Descriptors.Guilds.Members;
using Discord.Json.Objects.Guilds.Members;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MapCord.Deserializers
{
    public class MemberDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MemberObject);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return JToken.ReadFrom(reader).ToObject<MemberDescriptor>(serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
