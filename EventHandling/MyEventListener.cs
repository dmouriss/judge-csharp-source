using System;
using System.Collections.Concurrent;
using NUnit.Engine;
using NUnit.Engine.Extensibility;


namespace judge_c_sharp.EventHandling
{
    [Extension(EngineVersion = "3.4")]
    public class MyEventListener : ITestEventListener
    {

        private readonly BlockingCollection<String> Eventqueue;

        public MyEventListener(BlockingCollection<String> eventqueue)
        {
            Eventqueue = eventqueue;
        }

        public void OnTestEvent(string report)
        {
            Eventqueue.Add(report);
        }
    }
}
