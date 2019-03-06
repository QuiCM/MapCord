using Discord.Descriptors.Channels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MapCord
{
    public static class MessageMapperTypeInfo
    {
        private static Type _type;
        public static List<FieldInfo> UpdateableFields;

        static MessageMapperTypeInfo()
        {   
            _type = typeof(MessageDescriptor);
            UpdateableFields = new List<FieldInfo>
            {
                    
            };
        }
    }
}
