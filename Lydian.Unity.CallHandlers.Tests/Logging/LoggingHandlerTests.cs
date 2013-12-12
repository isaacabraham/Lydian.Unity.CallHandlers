using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lydian.Unity.CallHandlers.Tests.Logging
{
    [TestClass]
    public class LoggingHandlerTests
    {
        private UnityContainer container;
        private IMethodLogPublisher publisher;
        
        [TestInitialize]
        public void Setup()
        {
            container = new UnityContainer();
            container.AddNewExtension<Interception>();
            CallHandlerInitialiser.RegisterCallHandlerDependencies(container);
            HandlerHelpers.RegisterTypeWithCallHandler<LoggingHandler, SampleLoggingClass>(container);
            HandlerHelpers.RegisterTypeWithCallHandler<LoggingHandler, AsyncClass>(container, false);
            publisher = container.Resolve<IMethodLogPublisher>();
        }

        [TestMethod]
        public void MethodCalled_Always_BroadcastsOnStart()
        {
            var sample = container.Resolve<SampleLoggingClass>();
            CallSiteEventArgs args = null;
            publisher.OnLogMessage += (o, e) =>
            {
                if (e.MethodEventType == MethodEventType.Entry)
                    args = e;
            };

            // Act
            sample.Foo();
            
            // Assert
            Assert.AreEqual("Foo", args.Method.Name);
            Assert.AreSame(sample, args.Target);
            Assert.AreEqual(MethodEventType.Entry, args.MethodEventType);
        }

        [TestMethod]
        public void MethodCalled_Always_BroadcastsOnStartOnce()
        {
            var sample = container.Resolve<SampleLoggingClass>();
            var args = new List<CallSiteEventArgs>();
            publisher.OnLogMessage += (o, e) =>
            {
                if (e.MethodEventType == MethodEventType.Entry)
                    args.Add(e);
            };

            // Act
            sample.Foo();

            // Assert
            Assert.AreEqual(1, args.Count);
        }

        [TestMethod]
        public void MethodCalled_Always_BroadcastsOnComplete()
        {
            var sample = container.Resolve<SampleLoggingClass>();
            CallSiteEventArgs args = null;
            publisher.OnLogMessage += (o, e) =>
            {
                if (e.MethodEventType == MethodEventType.Exit)
                    args = e;
            };
            // Act
            sample.Foo();

            // Assert
            Assert.AreEqual("Foo", args.Method.Name);
            Assert.AreSame(sample, args.Target);
            Assert.AreEqual(MethodEventType.Exit, args.MethodEventType);
        }

        [TestMethod]
        public void MethodCalled_WithMessages_BroadcastsWithMessage()
        {
            var sample = container.Resolve<SampleLoggingClass>();
            var args = new Dictionary<MethodEventType, string>();
            publisher.OnLogMessage += (o, e) => { if (e.Message != null) args[e.MethodEventType] = e.Message; };

            // Act
            sample.Bar();

            // Assert
            Assert.AreEqual("Test", args[MethodEventType.Entry]);
            Assert.AreEqual("End", args[MethodEventType.Exit]);
        }

        [TestMethod]
        public async Task AsyncMethodCalled_TaskHasNotCompleted_DoesNotFireExitEvent()
        {
            var sample = container.Resolve<AsyncClass>();
            var exitReceived = false;
            publisher.OnLogMessage += (o, e) => { if (e.MethodEventType == MethodEventType.Exit) exitReceived = true; };

            // Act
            sample.Foo();

            // Wait 100 ms for the Continuation in the Handler to complete.
            await Task.Delay(1000);

            // Assert
            Assert.IsFalse(exitReceived);
        }

        [TestMethod]
        public async Task AsyncMethodCalled_TaskHasCompleted_FiresExitEvent()
        {
            var sample = container.Resolve<AsyncClass>();
            sample.Done.TrySetResult(true);
            var exitReceived = false;
            publisher.OnLogMessage += (o, e) => { if (e.MethodEventType == MethodEventType.Exit) exitReceived = true; };

            // Act
            sample.Foo();

            // Wait 100 ms for the Continuation in the Handler to complete.
            await Task.Delay(100);

            // Assert
            Assert.IsTrue(exitReceived);
        }

        [TestMethod]
        public async Task MethodReturningTaskCalled_TaskHasNotCompleted_FiresExitEvent()
        {
            var sample = container.Resolve<AsyncClass>();
            var exitReceived = false;
            publisher.OnLogMessage += (o, e) => { if (e.MethodEventType == MethodEventType.Exit) exitReceived = true; };

            // Act
            sample.Bar();

            // Wait 100 ms for the Continuation in the Handler to complete.
            await Task.Delay(100);

            // Assert
            Assert.IsTrue(exitReceived);
        }

        public class SampleLoggingClass
        {
            public virtual void Foo() { }
            [Logging("Test", "End")]
            public virtual void Bar() { }
        }

        public class AsyncClass
        {
            public TaskCompletionSource<bool> Done { get; set; }

            public AsyncClass()
            {
                Done = new TaskCompletionSource<bool>();
            }

            [Logging]
            public async virtual Task Foo()
            {
                await Done.Task;
            }

            [Logging]
            public virtual Task Bar()
            {
                return new TaskCompletionSource<bool>().Task;
            }
        }        
    }
}
