﻿using System;
using Newtonsoft.Json;

namespace Castle.Infrastructure.Json
{
    internal class EmptyStringToFalseConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if ((string)value == string.Empty)
            {
                writer.WriteValue(false);
            }
            else
            {
                writer.WriteValue(value);   
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException("Only for serialization");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
