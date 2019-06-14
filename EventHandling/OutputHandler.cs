using System;
using judge_c_sharp.Feedback;
using System.Xml;
using System.IO;
using judge_c_sharp.Json;
using System.Linq;

namespace judge_c_sharp.EventHandling
{
    public class OutputHandler
    {
        private readonly TextWriter OutputChannel;

        public OutputHandler(TextWriter outputChannel)
        {
            OutputChannel = outputChannel;
        }

        private Tuple<string, string, string> ProcessTestResult(string testresultmessage)
        {
            string expected = "";
            string generated = "";
            string message = "";

            string[] lines = testresultmessage.Split('\n');
            if (lines[lines.Length - 2].Trim().StartsWith("Expected:", StringComparison.Ordinal) && lines[lines.Length - 1].Trim().StartsWith("But was:", StringComparison.Ordinal))
            {
                expected = lines[lines.Length - 2].Trim().Substring("Expected:".Length).Trim();
                generated = lines[lines.Length - 1].Trim().Substring("Expected:".Length).Trim();
                message = string.Join("\n", lines.Take(lines.Length - 2));
            } else
            {
                message = testresultmessage.Trim();
            }
            return new Tuple<string, string, string>(expected, generated, message);
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
                    string testcaseDescription = node.Attributes["name"].Value;
                    OutputChannel.WriteLine(JSONGenerator.StartContext());
                    OutputChannel.WriteLine(JSONGenerator.StartTestcase(testcaseDescription));
                    break;
                case "test-case":
                    string testresult = node.Attributes["result"].Value;
                    if (testresult == "Passed") /* correct test */
                    {
                        OutputChannel.WriteLine(JSONGenerator.StartTest(""));
                        OutputChannel.WriteLine(JSONGenerator.CloseTest(Status.CORRECT, ""));
                    }
                    else if (node.SelectNodes("assertions").Count == 0) /* runtime error */
                    {
                        string messagetext = node.SelectSingleNode("failure/message").ChildNodes[0].Value;
                        OutputChannel.WriteLine(JSONGenerator.StartTest(""));
                        OutputChannel.WriteLine(JSONGenerator.AppendMessage(Format.PLAIN, messagetext));
                        OutputChannel.WriteLine(JSONGenerator.CloseTest(Status.RUNTIME_ERROR, ""));
                    }
                    else /* wrong answer */
                    {
                        foreach (XmlNode assertion in node.SelectSingleNode("assertions").SelectNodes("assertion[@result='Failed']"))
                        {
                            string testresultmessage = assertion.SelectSingleNode("message").ChildNodes[0].Value.Trim();
                            Tuple<string, string, string> processedTestresult = ProcessTestResult(testresultmessage);
                            string expected = processedTestresult.Item1;
                            string generated = processedTestresult.Item2;
                            string message = processedTestresult.Item3;

                            OutputChannel.WriteLine(JSONGenerator.StartTest(expected));
                            if (message.Length != 0)
                                OutputChannel.WriteLine(JSONGenerator.AppendMessage(Format.PLAIN, message));
                            OutputChannel.WriteLine(JSONGenerator.CloseTest(Status.WRONG, generated));
                        }
                    }
                    OutputChannel.WriteLine(JSONGenerator.CloseTestcase());
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
