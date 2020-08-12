using System.Text.Json;
using EmbeddedJsonSerializer = System.Text.Json.JsonSerializer;

namespace TicketFlow.Common.Serializers
{
    internal class JsonSerializer : IJsonSerializer
    {
        public string Serialize<T>(T entity, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return EmbeddedJsonSerializer.Serialize(entity, jsonSerializerOptions);
        }

        public T Deserialize<T>(string json, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return EmbeddedJsonSerializer.Deserialize<T>(json);
        }
    }
}