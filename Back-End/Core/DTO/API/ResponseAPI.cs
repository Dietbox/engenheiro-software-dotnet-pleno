using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.DTO.API
{
    public class ResponseAPI
    {

        public ResponseAPI(bool error, object result)
        {
            Error = error;
            Result = result;
        }

        public ResponseAPI(bool error, string message, object result = null)
        {
            Error = error;
            Messages = new string[] { message };
            Result = result;
        }

        public ResponseAPI(bool error, string[] messages, object result = null)
        {
            Error = error;
            Messages = messages;
            Result = result;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public bool Error { get; set; } = false;


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Messages { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Result { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public DateTime Date { get; } = DateTime.Now;

    }
}
