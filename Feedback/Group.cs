using System;
using System.Collections.Generic;

namespace judge_c_sharp.Feedback
{
    public class Group<T>
    {

        protected List<Message> Messages = new List<Message>();
        protected List<T> Groups = new List<T>();

        public void AddMessage(Message message) => Messages.Add(message);

        public void AddMessage(string message) => Messages.Add(Message.Plain(message));

        public void ClearMessages() => Messages.Clear();

        public void AddChild(T child) => Groups.Add(child);

        public void PrependChild(T child) => Groups.Insert(0, child);

        public T LastChild() => Groups[Groups.Count - 1];

        public List<T> Children() => Groups;

    }
}
