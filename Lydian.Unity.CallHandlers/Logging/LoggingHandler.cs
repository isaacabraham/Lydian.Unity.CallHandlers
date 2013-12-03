using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers.Logging
{
    /// <summary>
    /// A call handler to log the entrance and exits of individual methods. Listeners should subscribe to events published by the IMethodLogPublisher.
    /// </summary>
    public sealed class LoggingHandler : ICallHandler
    {
        private readonly MethodLogPublisher publisher;

        /// <summary>
        /// Order in which the handler will be executed.
        /// </summary>
        public Int32 Order { get; set; }
        /// <summary>
        /// An optional message to publish on entering the target method.
        /// </summary>
        public String StartMessage { get; set; }
        /// <summary>
        /// An optional message to publish on exiting the target method.
        /// </summary>
        public String CompletionMessage { get; set; }
        /// <summary>
        /// Creates a new instance of the Logging Handler.
        /// </summary>
        /// <param name="publisher">The publisher to use for exposing logging events.</param>
        public LoggingHandler(IMethodLogPublisher publisher)
        {
            this.publisher = (MethodLogPublisher)publisher;
        }

        /// <summary>
        /// Invokes the Logging Handler
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
        /// <returns>Return value from the target.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            publisher.Trigger(new CallSiteEventArgs(input.Target, input.MethodBase, MethodEventType.Entry, StartMessage));
            var result = getNext()(input, getNext);
            publisher.Trigger(new CallSiteEventArgs(input.Target, input.MethodBase, MethodEventType.Exit, CompletionMessage));

            return result;
        }
    }
}