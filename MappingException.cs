using System;
using System.Collections.Generic;
using System.Text;

namespace MapCord
{
    public class MappingException : Exception
    {
        public MappingException(string message, params object[] data) : base(message)
        {
            Data.Add("Mapping data", data);
        }
    }
}
