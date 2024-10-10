using Application.Libraries.SAP.SL;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Utilies.Newtonsoft
{
    public class SapPropertyContractResolver : DefaultContractResolver
    {
        public static readonly SapPropertyContractResolver Instance = new();
        protected override string ResolvePropertyName(string propertyName)
        {
            return base.ResolvePropertyName(propertyName);
        }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.PropertyName.EndsWith("Property", StringComparison.OrdinalIgnoreCase))
            {
                int i = property.PropertyName.IndexOf("Property", StringComparison.OrdinalIgnoreCase);
                property.PropertyName = property.PropertyName.Substring(0, i);
            }

            if(property.PropertyName.Contains("DynamicProperties")) {
                property.Ignored = true;
            }
            return property;
        }
    }

    public class SapConverters : JsonConverter<TimeOfDay?>
    {
        //private readonly Type[] _types = new Type[] { typeof(Nullable<TimeOfDay>), typeof(string) };
        public override TimeOfDay? ReadJson(JsonReader reader, Type objectType, TimeOfDay? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is null) return default;
            string s = (string)reader.Value;
            var x = Convert(s);
            return x;
        }

        public override void WriteJson(JsonWriter writer, TimeOfDay? value, JsonSerializer serializer)
        {
            if (value is TimeOfDay c)
            {
                string converted = string.Format("{0}:{1}:{2}",
                    c.Hours.ToString("D2"),
                    c.Minutes.ToString("D2"),
                    c.Seconds.ToString("D2")
                );
                writer.WriteValue(converted);
            }
        }

        private TimeOfDay Convert(string value)
        {
            string[] arrVal = value.Split(':');

            int hours = int.Parse(arrVal[0]),
                minutes = int.Parse(arrVal[1]),
                seconds = int.Parse(arrVal[2]);

            var x = new TimeOfDay(hours, minutes, seconds, 0);
            return x;
        }

    }

    public class SapDateConverter : JsonConverter<DateTimeOffset?>
    {
        static string FORMAT = "yyyy-MM-dd";
        public override DateTimeOffset? ReadJson(JsonReader reader, Type objectType, DateTimeOffset? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is null) return default;
            string s = (string)reader.Value;
            var x = DateTimeOffset.ParseExact(s, FORMAT, CultureInfo.InvariantCulture);
            return x;
        }

        public override void WriteJson(JsonWriter writer, DateTimeOffset? value, JsonSerializer serializer)
        {
            if (value is DateTimeOffset c)
            {
                string converted = c.ToString(FORMAT);
                writer.WriteValue(converted);
            }
        }
    }
}
