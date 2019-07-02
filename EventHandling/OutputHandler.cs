using System;
using judge_csharp.Feedback;
using System.Xml;
using System.IO;
using judge_csharp.Json;
using System.Linq;
using System.Collections.Generic;

namespace judge_csharp.EventHandling
{
    public class OutputHandler
    {
        private readonly TextWriter OutputChannel;
        private readonly Dictionary<string, Status> ResultMap;

        public OutputHandler(TextWriter outputChannel)
        {
            OutputChannel = outputChannel;
            ResultMap = new Dictionary<string, Status>();
            ResultMap.Add("Passed", Status.CORRECT);
            ResultMap.Add("Failed", Status.WRONG);
        }

        public bool Handle(string report)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(report);

            XmlNode node = doc.FirstChild;
            string nodetype = node.Name;

            bool proceed = true;

            switch (nodetype)
            {
                case "start-run":
                    OutputChannel.WriteLine(JSONGenerator.StartJudgement());
                    break;
                case "start-suite":
                    if (node.Attributes["type"].Value == "TestFixture")
                    {
                        string tabName = node.Attributes["name"].Value;
                        OutputChannel.WriteLine(JSONGenerator.StartTab(tabName));
                    }
                    break;
                case "start-test":
                    OutputChannel.WriteLine(JSONGenerator.StartContext());

                    string testcaseDescription = node.Attributes["name"].Value;

                    if ( node.SelectNodes("stdin").Count == 0)
                        OutputChannel.WriteLine(JSONGenerator.StartTestcase(testcaseDescription));
                    else
                        OutputChannel.WriteLine(JSONGenerator.StartTestcase(node.SelectSingleNode("stdin").ChildNodes[0].Value.Trim()));
                    break;
                case "test-case":
                    if (node.SelectNodes("assertions").Count == 0) /* runtime error */
                    {
                        string messagetext = node.SelectSingleNode("failure/message").ChildNodes[0].Value;
                        OutputChannel.WriteLine(JSONGenerator.AppendMessage(Format.PLAIN, messagetext));
                        OutputChannel.WriteLine(JSONGenerator.EscalateStatus(Status.RUNTIME_ERROR));
                        OutputChannel.WriteLine(JSONGenerator.CloseFailedTestcase());
                    }
                    else
                    {
                        foreach (XmlNode assertion in node.SelectSingleNode("assertions"))
                        {
                            string testresult = assertion.Attributes["result"].Value;
                            string expected = "";
                            string generated = "";
                            string message = "";
                            if (assertion.SelectNodes("actual").Count != 0 && assertion.SelectNodes("expected").Count != 0 )
                            {
                                expected = assertion.SelectSingleNode("expected").ChildNodes[0].Value.Trim();
                                generated = assertion.SelectSingleNode("actual").ChildNodes[0].Value.Trim();
                            }
                            if (assertion.SelectNodes("message").Count != 0 )
                                message = assertion.SelectSingleNode("message").ChildNodes[0].Value.Trim();

                            if (generated != "" || expected != "" || message != "" )
                            {
                                OutputChannel.WriteLine(JSONGenerator.StartTest(expected));
                                if (message.Length != 0)
                                    OutputChannel.WriteLine(JSONGenerator.AppendMessage(Format.PLAIN, message));
                                OutputChannel.WriteLine(JSONGenerator.CloseTest(ResultMap[testresult], generated));
                            }
                        }
                        OutputChannel.WriteLine(JSONGenerator.CloseTestcase());
                    }
                    OutputChannel.WriteLine(JSONGenerator.CloseContext());
                    break;
                case "test-suite":
                    if (node.Attributes["type"].Value == "TestFixture")
                        OutputChannel.WriteLine(JSONGenerator.CloseTab());
                    break;
                case "test-run":
                    OutputChannel.WriteLine(JSONGenerator.CloseJudgement());
                    proceed = false;
                    break;
            }
            return proceed;
        }
    }
}
