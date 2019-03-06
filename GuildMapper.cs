using Discord.Descriptors.Channels;
using Discord.Descriptors.Guilds;
using Discord.Descriptors.Guilds.Members;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapCord
{
    internal class GuildMapper
    {
        internal IDictionary<ulong, GuildDescriptor> MappedGuilds { get; } = new Dictionary<ulong, GuildDescriptor>();

        public void Map(GuildDescriptor descriptor)
        {
            descriptor.AfkChannel =
                descriptor.afk_channel_id.HasValue
                ? descriptor.Channels.First(c => c.id == descriptor.afk_channel_id)
                : null;
            descriptor.EmbedChannel =
                descriptor.HasEmbedChannel
                ? descriptor.Channels.First(c => c.id == descriptor.embed_channel_id)
                : null;
            descriptor.SystemChannel =
                descriptor.HasSystemChannel
                ? descriptor.Channels.First(c => c.id == descriptor.system_channel_id)
                : null;

            foreach (MemberDescriptor member in descriptor.Members)
            {
                member.Guild = descriptor;
                member.guild_id = descriptor.Id;
                //member.Roles will be null before this.
                //descriptor.Roles contains the fully defined Role objects for the entire server
                //member.roles contains role IDs
                //The join query maps these two collections together to provide member.Roles with a fully defined set of Role objects
                //based on the role IDs assigned to the member
                member.Roles = descriptor.Roles.Join(member.roles,
                    roleD => roleD.Id,
                    roleId => roleId,
                    (d, id) => d);

                member.PresenceState = descriptor.Presences.FirstOrDefault(p => p.user.id == member.User.Id);
                member.VoiceState = descriptor.VoiceStates.FirstOrDefault(v => v.user_id == member.User.Id);
            }

            foreach (ChannelDescriptor channel in descriptor.Channels)
            {
                channel.guild_id = descriptor.Id;

                if (channel is ChannelCategoryDescriptor catChannel)
                {
                    catChannel.Guild = descriptor;
                }

                if (channel is TextChannelDescriptor txtChannel)
                {
                    txtChannel.Guild = descriptor;
                    txtChannel.ParentCategory = txtChannel.parent_id.HasValue
                        ? (ChannelCategoryDescriptor)descriptor.Channels.First(c => c.Id == txtChannel.parent_id.Value)
                        : null;
                }

                if (channel is VoiceChannelDescriptor voiChannel)
                {
                    voiChannel.Guild = descriptor;
                    voiChannel.ParentCategory = voiChannel.parent_id.HasValue
                         ? (ChannelCategoryDescriptor)descriptor.Channels.First(c => c.Id == voiChannel.parent_id.Value)
                         : null;
                }
            }

            descriptor.Owner = descriptor.Members.First(m => m.User.Id == descriptor.owner_id).User;


            MappedGuilds.TryAdd(descriptor.Id, descriptor);
        }

        public GuildDescriptor UpdateGuild(GuildDescriptor guild)
        {
            if (!MappedGuilds.TryGetValue(guild.Id, out GuildDescriptor original))
            {
                throw new MappingException("Attempted to update a guild that was not previously tracked", guild);
            }

            GuildMapperTypeInfo.Update(original, guild);

            original.AfkChannel =
                original.afk_channel_id.HasValue
                ? original.Channels.First(c => c.id == original.afk_channel_id)
                : null;
            original.EmbedChannel =
                original.HasEmbedChannel
                ? original.Channels.First(c => c.id == original.embed_channel_id)
                : null;
            original.SystemChannel =
                original.HasSystemChannel
                ? original.Channels.First(c => c.id == original.system_channel_id)
                : null;

            return original;
        }

        public void DeleteMap(GuildDescriptor guild)
        {
            MappedGuilds.Remove(guild.Id);
        }
    }
}