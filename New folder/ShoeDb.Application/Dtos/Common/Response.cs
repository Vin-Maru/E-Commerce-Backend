using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MessagePack;
using KeyAttribute = MessagePack.KeyAttribute;

namespace ShoeStore.Application.Dtos.Common
{
    [MessagePackObject(AllowPrivate = true)]
    public partial record Response<T>
    {
        [Key(0)]
        public bool Successful { get; init; }
        [Key(1)]
        public string? Message { get; init; }
        [Key(2)]
        public T? Data { get; init; }
        [Key(3)]
        public Exception? Exception { get; init; }
        [Key(4)]
        public Dictionary<string, object>? AdditionalData { get; init; }

        [JsonConstructor]
        private Response(bool successful, string? message, T? data, Exception? exception, Dictionary<string, object>? additionalData)
        {
            Successful = successful;
            Message = message ?? "Operation Successful";
            Data = data;
            Exception = exception;
            AdditionalData = additionalData ?? new Dictionary<string, object>();
        }

        public static Response<T> Success(string message, T value, Exception? exception = null, Dictionary<string, object>? additionalData = null)
        {
            return new Response<T>(true, message, value, exception, additionalData);
        }

        public static Response<T> Failure(string errorMessage, T? data = default, Exception? error = null, Dictionary<string, object>? additionalData = null)
        {
            return new Response<T>(false, errorMessage, data, error, additionalData);
        }
    }
}
