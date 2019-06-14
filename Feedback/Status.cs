using System;
using Newtonsoft.Json;
using judge_c_sharp.Json;

namespace judge_c_sharp.Feedback
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
