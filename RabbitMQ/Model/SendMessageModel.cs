using System.Text.Json.Serialization;

namespace RabbitMQ.Model
{
    public class SendMessageModel<T>
    {
        [JsonIgnore]
        public string Exchange { get; set; } = string.Empty;
        [JsonIgnore]
        public string RoutingKey { get; set; } = string.Empty;
        [JsonIgnore]
        public bool Mandatory { get; set; } = false;
        
        public T? Body { get; set; }
        
        [JsonIgnore]
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    };
}
