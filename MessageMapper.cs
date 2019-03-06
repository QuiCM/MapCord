using System;
using System.Linq;
using Discord.Descriptors;
using Discord.Descriptors.Channels;
using Discord.Descriptors.Guilds;
using Discord.Descriptors.Guilds.Members;

namespace MapCord
{
    internal class MessageMapper
    {
        internal void Map(MessageDescriptor message, GuildMapper guildMapper, PrivateChannelMapper privateChannelMapper)
        {
            if (message.guild_id.HasValue)
            {
                MapGuildMessage(message, guildMapper);
            }
            else
            {
                MapPrivateMessage(message, privateChannelMapper);
            }
        }


        private void MapGuildMessage(MessageDescriptor message, GuildMapper mapper)
        {
            if (!mapper.MappedGuilds.TryGetValue(message.guild_id.Value, out GuildDescriptor guild))
            {
                throw new MappingException("Guild ID contained in message not previously mapped.", message.guild_id.Value, message);
            }
            message.Guild = guild;

            ChannelDescriptor channel = guild.Channels.FirstOrDefault(c => c.Id == message.channel_id);
            message.Channel = channel ?? throw new MappingException("Channel ID contained in message not previously mapped", message.channel_id, message, guild);

            MemberDescriptor member = guild.Members.FirstOrDefault(m => m.User.Id == message.author.id);
            message.Sender = member ?? throw new MappingException("Author ID contained in message not previously mapped", message.author.id, message, guild);

            message.MentionedRoles = guild.Roles.Join(inner: message.mention_roles,
                outerKeySelector: rD => rD.Id,
                innerKeySelector: id => id,
                resultSelector: (rD, id) => rD).ToList();

            message.MentionedUsers = guild.Members.Join(inner: message.mentions,
                outerKeySelector: mD => mD.User.Id,
                innerKeySelector: uO => uO.id,
                resultSelector: (mD, uO) => mD).ToList();
        }

        private void MapPrivateMessage(MessageDescriptor message, PrivateChannelMapper privateChannelMapper)
        {

        }
    }
}