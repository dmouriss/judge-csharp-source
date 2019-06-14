using System;
using System.Collections.Generic;

namespace judge_c_sharp.Feedback
{
    public class TestCase
    {
        private Message Description;
        private bool Accepted;
        private List<Message> Messages = new List<Message>();
        private List<Test> Tests = new List<Test>();

        public void SetDescription(Message description) => Description = description;

        public void SetAccepted(bool accepted) => Accepted = accepted;

        public void AddMessage(Message message) => Messages.Add(message);

        public void AddMessage(string message) => Messages.Add(Message.Plain(message));

        public void ClearMessages() => Messages.Clear();

        public void AddChild(Test child) => Tests.Add(child);

        public void PrependChild(Test child) => Tests.Insert(0, child);

        public Test LastChild() => Tests[Tests.Count - 1];

        public List<Test> Children() => Tests;
    }
}
