using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lydian.Unity.CallHandlers.Logging
{
    /// <summary>
    /// A call handler to log the entrance and exits of individual methods. Listeners should subscribe to events published by the IMethodLogPublisher.
    /// </summary>
    public sealed class LoggingHandler : ICallHandler
    {
        private readonly static Type asyncStateMachineAttribute = Type.GetType("System.Runtime.CompilerServices.AsyncStateMachineAttribute");
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
            
            var asyncTask = TryGetAsyncReturnTask(input, result);
            
            if (asyncTask == null)
                OnComplete(input);
            else
                asyncTask.ContinueWith(t => OnComplete(input));

            return result;
        }

        private void OnComplete(IMethodInvocation input)
        {
            publisher.Trigger(new CallSiteEventArgs(input.Target, input.MethodBase, MethodEventType.Exit, CompletionMessage));
        }

        private static Task TryGetAsyncReturnTask(IMethodInvocation input, IMethodReturn result)
        {
            var taskResult = result.ReturnValue as Task;
            var isAsync = taskResult != null &&
                          input.MethodBase.GetCustomAttributes(asyncStateMachineAttribute, true).Any();
            return isAsync ? taskResult : null;
        }
    }
}