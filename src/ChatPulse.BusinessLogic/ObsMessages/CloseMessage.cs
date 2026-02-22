using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace ChatPulse.BusinessLogic.ObsMessages
{
    public class CloseMessage
    {
        public string CloseStatus { get; set; } = string.Empty;
        public string CloseStatusDescription { get; set; } = string.Empty;

        public ObsMessage<CloseMessage> MapObsResponse(WebSocketReceiveResult result)
        { 
            return new ObsMessage<CloseMessage>
            {
                OperationCode = ObsOpCode.Hello,
                Data = new CloseMessage
                {
                    CloseStatus = result.CloseStatus.ToString() ?? string.Empty,
                    CloseStatusDescription = result.CloseStatusDescription ?? string.Empty
                }
            };
        }
    }
}
