using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using System;

namespace ConsoleApplication1
{
    /// <summary>
    /// Illustrates how to hook into publication events.
    /// </summary>
    public class SampleSubscriber
    {
        public void Subscribe(IUnityContainer container)
        {
            var logPublisher = container.Resolve<IMethodLogPublisher>();
            logPublisher.OnLogMessage += (_, e) =>
            {
                switch (e.MethodEventType)
                {
                    case MethodEventType.Entry:
                        Console.WriteLine("Entered method {0}.", e.Method.Name);
                        break;
                    case MethodEventType.Exit:
                        Console.WriteLine("Exited method {0}.", e.Method.Name);
                        break;
                }
            };

            var timePublisher = container.Resolve<IMethodTimePublisher>();
            timePublisher.OnMethodCompleted += (_, e) => Console.WriteLine("Method {0} took {1}ms.", e.Method.Name, e.CallDuration.TotalMilliseconds);
        }
    }
}
