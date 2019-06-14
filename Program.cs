using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using judge_c_sharp.EventHandling;
using NUnit.Engine;

namespace judge_c_sharp
{

    public class NUnitJson
    {

        readonly BlockingCollection<String> Eventqueue;
        readonly MyEventListener NunitListener;
        readonly string TestAssemblyPath;

        public NUnitJson(string testAssemblyPath)
        {
            Eventqueue = new BlockingCollection<string>(new ConcurrentQueue<string>());
            NunitListener = new MyEventListener(Eventqueue);
            TestAssemblyPath = testAssemblyPath;
        }

        public void Start()
        {
            /* run tests in new thread */
            new Thread(RunTests).Start();


            /* for some reason stdout gets changed while running NUnit tests */
            TextWriter outputChannel = Console.Out;
            new QueueListener(Eventqueue, new OutputHandler(outputChannel)).StartListening();
        }

        public void RunTests()
        {
            ITestEngine engine = TestEngineActivator.CreateInstance();
            var package = new TestPackage(TestAssemblyPath);
            ITestRunner runner = engine.GetRunner(package);
            runner.Run(NunitListener, TestFilter.Empty);
        }


        public static void Main(string[] args)
        {
            string testassemblyPath = args[0];
            new NUnitJson(testassemblyPath).Start();
        }
    }
}
