using Discord.Descriptors.Channels;
using Discord.Json.Objects.Channels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MapCord.Deserializers
{
    /// <summary>
    /// Deserializes a ChannelObject directly into a ChannelDescriptor
    /// </summary>
    public class ChannelDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ChannelDescriptor);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.ReadFrom(reader);
            ChannelObject obj = token.ToObject<ChannelObject>(serializer);

            switch (obj.type)
            {
                case Discord.Enums.ChannelType.GUILD_CATEGORY:
                    return token.ToObject<ChannelCategoryDescriptor>(serializer);

                case Discord.Enums.ChannelType.GUILD_TEXT:
                    return token.ToObject<TextChannelDescriptor>(serializer);

                case Discord.Enums.ChannelType.GUILD_VOICE:
                    return token.ToObject<VoiceChannelDescriptor>(serializer);

                case Discord.Enums.ChannelType.DM:
                case Discord.Enums.ChannelType.GROUP_DM:
                    return token.ToObject<PrivateChannelDescriptor>(serializer);

                default:
                    throw new InvalidCastException($"Unknown channel type '{obj.type}'.");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
