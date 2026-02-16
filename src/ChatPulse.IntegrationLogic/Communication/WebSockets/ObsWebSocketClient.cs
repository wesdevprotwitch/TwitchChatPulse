using ChatPulse.BusinessLogic;
using ChatPulse.BusinessLogic.ObsMessages;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatPulse.IntegrationLogic.Communication.WebSockets
{
    public class ObsWebSocketClient : IDisposable
    {
        private readonly ClientWebSocket _ws;
        private readonly ObsWebSocketClientConfig _config;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };  

        public ObsWebSocketClient(IOptions<ObsWebSocketClientConfig> config)
        {
            _config = config.Value;

            // TODO: Add config for web socket?
            _ws = new ClientWebSocket();
        }

        public async Task ConnectAsync(CancellationToken token = default)
        {
            await _ws.ConnectAsync(_config.Uri, token);
            var response = await ReceiveMessageAsync<HelloMessage>(token);

            if (response.OperationCode == ObsOpCode.Hello)
            {
                var identifyMessage = new ObsMessage<IdentifyMessage>
                {
                    OperationCode = ObsOpCode.Identify,
                    Data = new IdentifyMessage
                    {
                        RpcVersion = 1,
                        Authentication = ComputeObsAuth.ComputeAuth(_config.Password, response.Data.Authentication),
                        EventSubscriptions = 0xFFFFFFFF // Subscribe to all events - for now :)
                    }
                };

                await SendAsync(identifyMessage, token);

                var identifiedResponse = await ReceiveMessageAsync<IdentifiedMessage>(token);

                if (identifiedResponse.OperationCode != ObsOpCode.Identified)
                    throw new WebSocketException("OBS authentication failed.");
            }
        }

        public async Task SendAsync(string message, CancellationToken token = default)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);
            await _ws.SendAsync(bytes, WebSocketMessageType.Text, true, token);
        }

        public async Task SendAsync<TMessage>(TMessage message, CancellationToken token = default)
        {
            var json = JsonSerializer.Serialize(message);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            await _ws.SendAsync(bytes, WebSocketMessageType.Text, true, token);
        }

        public async Task<string> ReceiveMessageAsync(CancellationToken token = default)
        {
            var buffer = new byte[4096];
            using var ms = new MemoryStream();

            while (true)
            {
                var result = await _ws.ReceiveAsync(buffer, token);

                if (result.MessageType == WebSocketMessageType.Close)
                    throw new WebSocketException("WebSocket closed by the server.");

                ms.Write(buffer, 0, result.Count);

                if (result.EndOfMessage)
                    break;
            }

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public async Task<ObsMessage<T>> ReceiveMessageAsync<T>(CancellationToken token = default)
        {
            var buffer = new byte[4096];
            using var ms = new MemoryStream();

            while (true)
            {
                var result = await _ws.ReceiveAsync(buffer, token);

                if (result.MessageType == WebSocketMessageType.Close)
                    throw new WebSocketException("WebSocket closed by the server.");

                ms.Write(buffer, 0, result.Count);

                if (result.EndOfMessage)
                    break;
            }

            var json = Encoding.UTF8.GetString(ms.ToArray());

            return JsonSerializer.Deserialize<ObsMessage<T>>(json, _jsonOptions) ?? throw new JsonException($"Failed to deserialize JSON message to type {typeof(T).Name}. JSON: {json}");
        }


        public async Task CloseConnection(CancellationToken token = default) => await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", token);

        public void Dispose()
        {
            // CloseAsync is not awaited here because we want to ensure that the WebSocket is closed even if the close operation takes a long time or fails.
            // The Dispose method should not throw exceptions, so we use GetAwaiter().GetResult() to synchronously wait for the close operation to complete without throwing exceptions.
            CloseConnection(CancellationToken.None).GetAwaiter().GetResult();
            _ws.Dispose();
        }
    }
}
