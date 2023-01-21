using System;
using Newtonsoft.Json;
using judge_csharp.Json;

namespace judge_csharp.Feedback
{
    [JsonConverter(typeof(DodonaEnumConverter))]
    public enum Status
    {
        CORRECT,
        WRONG,
        TIME_LIMIT_EXCEEDED,
        MEMORY_LIMIT_EXCEEDED,
        RUNTIME_ERROR,
        INTERNAL_ERROR
    }
}
