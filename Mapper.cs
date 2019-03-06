using Discord.Descriptors.Channels;
using Discord.Descriptors.Guilds;
using Discord.Gateway;
using MapCord.Deserializers;

namespace MapCord
{
    /// <summary>
    /// Mapper provides custom JSON deserializers and methods to map Discord-Core objects together
    /// </summary>
    public class Mapper
    {
        private GuildMapper _guildMapper;
        private MessageMapper _messageMapper;
        private PrivateChannelMapper _privateChannelMapper;
        private Events _events;

        /// <summary>
        /// Create a new Mapper instance. This will add Mapper's serializers to the provided <see cref="Gateway"/>'s
        /// <see cref="GatewayEvents"/> instance.
        /// </summary>
        /// <param name="gateway"></param>
        public Mapper(Gateway gateway)
        {
            _events = new Events(this);
            _guildMapper = new GuildMapper();
            _messageMapper = new MessageMapper();

            gateway.Events.AddConverter(new ChannelDeserializer());
            gateway.Events.AddConverter(new UserDeserializer());
            gateway.Events.AddConverter(new MemberDeserializer());
            gateway.Events.AddConverter(new EmojiDeserializer());
            gateway.Events.AddConverter(new RoleDeserializer());
            gateway.Events.AddConverter(new PermissionDeserializer());
            gateway.Events.AddConverter(new MemberActivityDeserializer());
            gateway.Events.AddConverter(new MemberActivityTimestampDeserializer());
            gateway.Events.AddConverter(new MessageDeserializer());

            gateway.Events.AddEventCallback<GuildDescriptor>(Discord.Enums.EventType.GUILD_CREATE, _events.Guild_Create);
            gateway.Events.AddEventCallback<GuildDescriptor>(Discord.Enums.EventType.GUILD_UPDATE, _events.Guild_Update);
            gateway.Events.AddEventCallback<GuildDescriptor>(Discord.Enums.EventType.GUILD_DELETE, _events.Guild_Delete);

            gateway.Events.AddEventCallback<MessageDescriptor>(Discord.Enums.EventType.MESSAGE_CREATE, _events.Message_Create);
        }

        /// <summary>
        /// Provides post-deserialization mapping to a GuildDescriptor instance
        /// </summary>
        /// <param name="guild"></param>
        public void Map(GuildDescriptor guild)
        {
            _guildMapper.Map(guild);
        }

        public GuildDescriptor UpdateMap(GuildDescriptor guild)
        {
            return _guildMapper.UpdateGuild(guild);
        }

        public void DeleteMap(GuildDescriptor guild)
        {
            _guildMapper.DeleteMap(guild);
        }

        /// <summary>
        /// Provides post-deserialization mapping to a MessageDescriptor instance
        /// </summary>
        /// <param name="message"></param>
        public void Map(MessageDescriptor message)
        {
            _messageMapper.Map(message, _guildMapper, _privateChannelMapper);
        }

        public void Map(PrivateChannelDescriptor privateChannel)
        {
            _privateChannelMapper.Map(privateChannel);
        }

        public void UpdateMap(MessageDescriptor original, MessageDescriptor message)
        {

        }

        public void UpdateMap(PrivateChannelDescriptor privateChannel)
        {

        }
    }
}
