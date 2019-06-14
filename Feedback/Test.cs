using System;
using System.Collections.Generic;

namespace judge_c_sharp.Feedback
{
    public class Test
    {

        private Message Description;
        private bool Accepted;
        private string Expected;
        private string Generated;
        private List<Message> Messages;

        public Test(Message description, bool accepted, string expected, string generated)
        {
            Description = description;
            Accepted = accepted;
            Expected = expected;
            Generated = generated;
            Messages = new List<Message>();
        }

        public void AddMessage(string message) => Messages.Add(Message.Plain(message));

        public void ClearMessages() => Messages.Clear();
    }
}
