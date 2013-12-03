using System;
using System.Reflection;

namespace Lydian.Unity.CallHandlers.Logging
{
    /// <summary>
    /// Contains details of a method call.
    /// </summary>
    public class CallSiteEventArgs : EventArgs
    {
        /// <summary>
        /// The target of the method call.
        /// </summary>
        public Object Target { get; private set; }
        /// <summary>
        /// The method that is called.
        /// </summary>
        public MethodBase Method { get; private set; }
        /// <summary>
        /// An optional message with details about this stage of the method call.
        /// </summary>
        public String Message { get; private set; }
        /// <summary>
        /// Represents the stage at which this event is occurring.
        /// </summary>
        public MethodEventType MethodEventType { get; set; }
        internal CallSiteEventArgs(Object target, MethodBase method, MethodEventType eventType, String message = null)
        {
            Target = target;
            Method = method;
            MethodEventType = eventType;
            Message = message;
        }
    }

    /// <summary>
    /// The different stages that a log message can be fired.
    /// </summary>
    public enum MethodEventType
    {
        /// <summary>
        /// Fired on entry to the method.
        /// </summary>
        Entry,
        /// <summary>
        /// Fired on exiting the method.
        /// </summary>
        Exit
    }
}
