using System.Text.Json;

namespace TicketFlow.Common.Serializers
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T entity, JsonSerializerOptions jsonSerializerOptions = null);

        T Deserialize<T>(string json, JsonSerializerOptions jsonSerializerOptions = null);
    }
}