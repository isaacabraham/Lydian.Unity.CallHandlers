using Lydian.Unity.CallHandlers.Logging;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            HandlerHelpers.RegisterTypeWithCallHandler<LoggingHandler, AsyncVirtualClass>(container, false);
            container.RegisterType<IAsyncClass, IAsyncClassImplementation>(new InterceptionBehavior<PolicyInjectionBehavior>(), new Interceptor<InterfaceInterceptor>());
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
        public async Task VirtualClassWithAsyncMethodCalled_TaskHasNotCompleted_DoesNotFireExitEvent()
        {
            // Act
            var firedEvent = await DoAsyncCall<AsyncVirtualClass>(x => x.AwaitingMethod(), false);

            // Assert
            Assert.IsFalse(firedEvent);
        }

        [TestMethod]
        public async Task VirtualClassWithAsyncMethodCalled_TaskHasCompleted_FiresExitEvent()
        {
            // Act
            var firedEvent = await DoAsyncCall<AsyncVirtualClass>(x => x.AwaitingMethod(), true);

            // Assert
            Assert.IsTrue(firedEvent);
        }

        [TestMethod]
        public async Task VirtualClassWithMethodReturningTask_TaskHasNotCompleted_FiresExitEvent()
        {
            // Act
            var firedEvent = await DoAsyncCall<AsyncVirtualClass>(x => x.TaskGeneratingMethod(), false);

            // Assert
            Assert.IsTrue(firedEvent);
        }

        [TestMethod]
        public async Task InterfaceWithAsyncMethod_TaskHasNotCompleted_DoesNotFireExitEvent()
        {
            // Act
            var firedEvent = await DoAsyncCall<IAsyncClass>(x => x.AwaitingMethod(), false);

            // Assert
            Assert.IsFalse(firedEvent);
        }

        [TestMethod]
        public async Task InterfaceWithAsyncMethod_TaskHasCompleted_FiresExitEvent()
        {
            // Act
            var firedEvent = await DoAsyncCall<IAsyncClass>(x => x.AwaitingMethod(), true);

            // Assert
            Assert.IsTrue(firedEvent);
        }

        [TestMethod]
        public async Task InterfaceWithAsyncTaskReturningMethod_TaskHasNotCompleted_FiresExitEvent()
        {
            // Act
            var firedEvent = await DoAsyncCall<IAsyncClass>(x => x.TaskGeneratingMethod(), true);

            // Assert
            Assert.IsTrue(firedEvent);
        }

        private async Task<bool> DoAsyncCall<TTypeToResolve>(Action<TTypeToResolve> action, bool shouldExitEventFire) where TTypeToResolve : IAsyncClass
        {
            var sample = container.Resolve<TTypeToResolve>();
            var exitReceived = false;
            publisher.OnLogMessage += (o, e) => { if (e.MethodEventType == MethodEventType.Exit) exitReceived = true; };
            if (shouldExitEventFire)
                sample.Done.SetResult(true);

            // Act
            action(sample);

            // Wait 100 ms for the Continuation in the Handler to complete.
            await Task.Delay(250);

            // Assert
            return exitReceived;
        }
        public class SampleLoggingClass
        {
            public virtual void Foo() { }
            [Logging("Test", "End")]
            public virtual void Bar() { }
        }

        public interface IAsyncClass
        {
            Task TaskGeneratingMethod();
            TaskCompletionSource<bool> Done { get; set; }
            Task AwaitingMethod();
        }
        public class IAsyncClassImplementation : IAsyncClass
        {
            public IAsyncClassImplementation()
            {
                Done = new TaskCompletionSource<bool>();
            }

            [Logging]
            public Task TaskGeneratingMethod()
            {
                return new TaskCompletionSource<bool>().Task;
            }
            public TaskCompletionSource<bool> Done { get; set; }
            [Logging]
            public async Task AwaitingMethod()
            {
                await Done.Task;
            }
        }

        public class AsyncVirtualClass : IAsyncClass
        {
            public TaskCompletionSource<bool> Done { get; set; }

            public AsyncVirtualClass()
            {
                Done = new TaskCompletionSource<bool>();
            }

            [Logging]
            public virtual async Task AwaitingMethod()
            {
                await Done.Task;
            }

            [Logging]
            public virtual Task TaskGeneratingMethod()
            {
                return new TaskCompletionSource<bool>().Task;
            }
        }        
    }
}
