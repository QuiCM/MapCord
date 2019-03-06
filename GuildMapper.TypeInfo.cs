using Discord.Descriptors.Guilds;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MapCord
{
    /// <summary>
    /// Contains Type information used to update a specific subset of a GuildDescriptor with information that comes from a GuildUpdate event
    /// </summary>
    public static class GuildMapperTypeInfo
    {
        private static Type _type;
        public static List<FieldInfo> UpdateableFields;

        static GuildMapperTypeInfo()
        {
            _type = typeof(GuildDescriptor);
            UpdateableFields = new List<FieldInfo>
            {
                _type.GetField("widget_enabled"),
                _type.GetField("widget_channel_id"),
                _type.GetField("verification_level"),
                _type.GetField("system_channel_id"),
                _type.GetField("splash"),
                _type.GetField("Roles"),
                _type.GetField("region"),
                _type.GetField("owner_id"),
                _type.GetField("name"),
                _type.GetField("mfa_level"),
                _type.GetField("icon"),
                _type.GetField("features"),
                _type.GetField("explicit_content_filter"),
                _type.GetField("Emojis"),
                _type.GetField("embed_enabled"),
                _type.GetField("embed_channel_id"),
                _type.GetField("default_message_notifications"),
                _type.GetField("application_id"),
                _type.GetField("afk_timeout"),
                _type.GetField("afk_channel_id"),
            };
        }

        /// <summary>
        /// Updates the fields on a GuildDescriptor with those that are contained in a GuildUpdate event's GuildDescriptor
        /// </summary>
        /// <param name="original"></param>
        /// <param name="updated"></param>
        public static void Update(GuildDescriptor original, GuildDescriptor updated)
        {
            foreach (FieldInfo info in UpdateableFields)
            {
                info.SetValue(original, info.GetValue(updated));
            }
        }
    }
}
