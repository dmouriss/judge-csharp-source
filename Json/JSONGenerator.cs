using System.Collections.Generic;
using Newtonsoft.Json;

namespace judge_csharp.Json
{
    public static class JSONGenerator
    {

        private static Dictionary<string, object> GetCommandDict(string command)
        {

            Dictionary<string, object> content = new Dictionary<string, object>
            {
                { "command", command}
            };

            return content;
        }
       
        public static string StartJudgement()
        {
            Dictionary<string, object> content = GetCommandDict("start-judgement");
            return JsonConvert.SerializeObject(content);
        }

        public static string CloseJudgement()
        {
            Dictionary<string, object> content = GetCommandDict("close-judgement");
            return JsonConvert.SerializeObject(content);
        }

        public static string StartTab(string name)
        {

            Dictionary<string, object> content = GetCommandDict("start-tab");
            content.Add("title", name);
            return JsonConvert.SerializeObject(content);
        }

        public static string CloseTab()
        {
            Dictionary<string, object> content = GetCommandDict("close-tab");
            return JsonConvert.SerializeObject(content);
        }

        public static string StartContext()
        {
            Dictionary<string, object> content = GetCommandDict("start-context");
            return JsonConvert.SerializeObject(content);
        }

        public static string CloseContext()
        {
            Dictionary<string, object> content = GetCommandDict("close-context");
            return JsonConvert.SerializeObject(content);
        }

        public static string StartTestcase(string description)
        {
            Dictionary<string, object> content = GetCommandDict("start-testcase");
            Dictionary<string, object> descriptionobject = new Dictionary<string, object>
            {
                { "format", "html" },
                { "description", "<span class=\"code\">" + description + "</span>" }
            };
            content.Add("description", descriptionobject);
            return JsonConvert.SerializeObject(content);
        }

        public static string CloseTestcase()
        {
            Dictionary<string, object> content = GetCommandDict("close-testcase");
            return JsonConvert.SerializeObject(content);
        }

        public static string CloseFailedTestcase()
        {
            Dictionary<string, object> content = GetCommandDict("close-testcase");
            content.Add("accepted", false);
            return JsonConvert.SerializeObject(content);
        }

        public static string StartTest(string expected)
        {
            Dictionary<string, object> content = GetCommandDict("start-test");
            content.Add("expected", expected);
            return JsonConvert.SerializeObject(content);
        }

        public static string CloseTest(Feedback.Status status, string generated)
        {
            Dictionary<string, object> content = GetCommandDict("close-test");

            Dictionary<string, object> statusEnum = new Dictionary<string, object>
            {
                { "enum", status }
            };

            content.Add("status", statusEnum);
            content.Add("generated", generated);
            return JsonConvert.SerializeObject(content);
        }

        public static string AppendMessage(Feedback.Format format, string description)
        {
            Dictionary<string, object> content = GetCommandDict("append-message");
            Dictionary<string, object> message = new Dictionary<string, object>
            {
                { "format", format },
                { "description", description }
            };

            content.Add("message", message);
            return JsonConvert.SerializeObject(content);
        }

        public static string EscalateStatus(Feedback.Status status)
        {
            Dictionary<string, object> content = GetCommandDict("escalate-status");
            Dictionary<string, object> statusEnum = new Dictionary<string, object>
            {
                { "enum", status }
            };

            content.Add("status", statusEnum);
            return JsonConvert.SerializeObject(content);
        }
    }
}
