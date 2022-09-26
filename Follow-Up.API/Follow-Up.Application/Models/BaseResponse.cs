using Follow_Up.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Follow_Up.Application.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse(
            ProcessStatusEnum processStatus = ProcessStatusEnum.Success,
            FriendlyMessage friendlyMessage = null,
            T payload = default)
        {
            ProcessStatus = processStatus;
            Payload = payload;
            FriendlyMessage = friendlyMessage;
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProcessStatusEnum ProcessStatus { get; set; }
        public FriendlyMessage FriendlyMessage { get; set; }
        public T Payload { get; set; }

    }

    public class FriendlyMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public FriendlyMessage(string Title, string Message)
        {
            this.Title = Title;
            this.Message = Message;
        }

        public FriendlyMessage(string Title)
        {
            this.Title = Title;
        }

        public FriendlyMessage()
        {

        }
    }
}
