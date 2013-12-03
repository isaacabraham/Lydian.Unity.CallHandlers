using System;
using System.Reflection;

namespace Lydian.Unity.CallHandlers.Logging
{
    /// <summary>
    /// Represents the timed results of a completed method call.
    /// </summary>
    public class TimedCallEventArgs : EventArgs
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
        /// The length of time that the method call took.
        /// </summary>
        public TimeSpan CallDuration { get; private set; }

        /// <summary>
        /// Initializes a new instance of the TimedCallEventArgs class.
        /// </summary>
        internal TimedCallEventArgs(Object target, MethodBase method, TimeSpan callDuration)
        {
            Target = target;
            Method = method;
            CallDuration = callDuration;
        }
    }
}
