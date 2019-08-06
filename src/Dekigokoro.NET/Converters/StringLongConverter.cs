using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dekigokoro.NET
{
    internal class StringLongConverter : JsonConverter<long>
    {
        public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return long.Parse(reader.ReadAsString());
        }

        public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
