using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ProjectEarthServerAPI.Util
{
	public class DateTimeConverter : JsonConverter<DateTime>
	{
		public override DateTime ReadJson(JsonReader reader, Type objectType, [AllowNull] DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			return (DateTime)reader.Value;
		}

		public override void WriteJson(JsonWriter writer, [AllowNull] DateTime value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
		}
	}
}
