using System.Text.Json.Serialization;

namespace ChatPulse.BusinessLogic
{
    public class ObsMessage<Payload>
    {
        // <summary>
        // The "op" field in the OBS WebSocket protocol, which indicates the type of message being sent or received.
        // </summary>
        [JsonPropertyName("op")]
        public ObsOpCode OperationCode { get; set; }

        // <summary>
        // The "d" field in the OBS WebSocket protocol, which contains the payload of the message.
        // The type of this field can vary depending on the OpCode and the specific event or request.
        // </summary>
        [JsonPropertyName("d")]
        public Payload Data { get; set; } = default!;
    }
}
