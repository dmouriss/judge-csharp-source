using System;
using judge_c_sharp.Json;
using Newtonsoft.Json;

namespace judge_c_sharp.Feedback
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
