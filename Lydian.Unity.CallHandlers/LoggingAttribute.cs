using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Lydian.Unity.CallHandlers
{
    /// <summary>
    /// Applies the LoggingHandler onto the specified method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Class)]
    public class LoggingAttribute : HandlerAttribute
    {
        private readonly String completionMessage;
        private readonly String startMessage;
        /// <summary>
        /// Creates a new instance of the Logging Call Handler Attribute.
        /// </summary>
        /// <param name="startMessage">The message to be published on entering the target method.</param>
        /// <param name="completionMessage">The message to be published on exiting the target method.</param>
        public LoggingAttribute(String startMessage = null, String completionMessage = null)
        {
            this.startMessage = startMessage;
            this.completionMessage = completionMessage;
        }

        /// <summary>
        /// Creates the handler.
        /// </summary>
        /// <param name="container">The container to use to create the handler.</param>
        /// <returns>The Logging call handler.</returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            var handler = container.CreateCallHandler<LoggingHandler>(Order);
            handler.StartMessage = startMessage;
            handler.CompletionMessage = completionMessage;
            return handler;
        }
    }
}
