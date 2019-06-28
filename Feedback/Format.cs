using System;
using judge_csharp.Json;
using Newtonsoft.Json;

namespace judge_csharp.Feedback
{
    [JsonConverter(typeof(DodonaEnumConverter))]
    public enum Format
    {
        PLAIN,      /* Formatted as plain text */
        HTML,       /* HTML markup is not escaped */
        MARKDOWN,   /* Converted to HTML using Cramdown */
        CODE       /* Preserves whitespace and rendered in monospace */
    }
}
