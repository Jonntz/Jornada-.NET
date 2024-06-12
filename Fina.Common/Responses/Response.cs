using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fina.Common.Responses
{
    public class Response<TData>
    {
        [JsonConstructor]
        public Response() => _code = Configuration.DefaultPageStatusCode;

        public Response(TData? data, int code = Configuration.DefaultPageStatusCode, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        private int _code = Configuration.DefaultPageStatusCode;

        [JsonIgnore]
        public bool IsSuccess => _code is > 200 and <= 299;

        public TData? Data { get; set; }
        public string? Message { get; set; }
    }
}