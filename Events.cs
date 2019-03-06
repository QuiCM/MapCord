using Discord.Descriptors;
using Discord.Descriptors.Channels;
using Discord.Descriptors.Guilds;

namespace MapCord
{
    class Events
    {
        internal Mapper mapper;

        internal Events(Mapper mapper)
        {
            this.mapper = mapper;
        }

        internal void Guild_Create(string json, DispatchGatewayEvent<GuildDescriptor> e)
        {
            mapper.Map(e.Payload);
        }

        internal void Guild_Update(string json, DispatchGatewayEvent<GuildDescriptor> e)
        {
            e.Payload = mapper.UpdateMap(e.Payload);
        }

        internal void Guild_Delete(string json, DispatchGatewayEvent<GuildDescriptor> e)
        {
            mapper.DeleteMap(e.Payload);
        }

        internal void Message_Create(string json, DispatchGatewayEvent<MessageDescriptor> e)
        {
            mapper.Map(e.Payload);
        }
    }
}
