using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ChatPulse.BusinessLogic.ObsMessages
{
    public class EventMessage
    {
        public string EventType { get; set; } = string.Empty; public int EventIntent { get; set; }
        // May want to rethink this... for now lets comment this out...
        //public JsonElement EventData { get; set; }
    }
}
