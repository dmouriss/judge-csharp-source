using System;
using System.Collections.Concurrent;

namespace judge_c_sharp.EventHandling
{
    public class QueueListener
    {
        readonly OutputHandler Handler;
        readonly BlockingCollection<String> Eventqueue;

        public QueueListener(BlockingCollection<String> eventqueue, OutputHandler handler)
        {
            Handler = handler;
            Eventqueue = eventqueue;
        }

        public void StartListening()
        {
            bool proceed = true;
            while (proceed)
            {
                String item = Eventqueue.Take();
                proceed = Handler.Handle(item);
            }
        }
    }
}
