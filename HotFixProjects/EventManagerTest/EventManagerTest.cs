using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EventCore;
using NSubstitute;

namespace EventManagerTest
{
    public class TestEvent : Event
    {

    }
    [TestClass]
    public class EventManagerTest
    {
        [TestMethod]
        public void TestAddListener()
        {
            var callBack = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            var callBack2 = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            EventManager manager = new EventManager();
            manager.AddListener<TestEvent>(callBack);
            manager.AddListener<TestEvent>(callBack2);
            var e = new TestEvent();
            //action
            manager.Raise(e);

            //expect
            callBack.Received(1).DynamicInvoke(new object[] { e });
            callBack2.Received(1).DynamicInvoke(new object[] { e });
        }

        [TestMethod]
        public void TestAddListenerDuplicateAndCallOnce()
        {
            var callBack = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            EventManager manager = new EventManager();
            manager.AddListener<TestEvent>(callBack);
            manager.AddListener<TestEvent>(callBack);
            var e = new TestEvent();
            //action
            manager.Raise(e);

            //expect
            callBack.Received(1).DynamicInvoke(new object[] { e });
        }

        [TestMethod]
        public void TestRemoveListener()
        {
            var callBack = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            EventManager manager = new EventManager();
            manager.AddListener<TestEvent>(callBack);
            manager.RemoveListener<TestEvent>(callBack);
            var e = new TestEvent();
            //action
            manager.Raise(e);

            //expect
            callBack.DidNotReceive().DynamicInvoke(new object[] { e });
        }

        [TestMethod]
        public void TestHandled()
        {
            var callBack = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            EventManager manager = new EventManager();
            manager.AddListener<TestEvent>(callBack);
            manager.AddListener<TestEvent>((p) => { p.Handled = true; });
            var e = new TestEvent();
            //action
            manager.Raise(e);

            //expect
            callBack.DidNotReceive().DynamicInvoke(new object[] { e });
        }

        [TestMethod]
        public void TestHandleToo()
        {
            var callBack = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            EventManager manager = new EventManager();
            manager.AddListener<TestEvent>((p) => { p.Handled = true; });
            manager.AddListener<TestEvent>(callBack, true);
            var e = new TestEvent();
            //action
            manager.Raise(e);

            //expect
            callBack.Received(1).DynamicInvoke(new object[] { e });
        }

        [TestMethod]
        public void TestHandlerCount()
        {
            //arrange
            var callBack = Substitute.For<EventManager.EventDelegate<TestEvent>>();
            EventManager manager = new EventManager();

            //action
            manager.AddListener<TestEvent>((p) => { p.Handled = true; });
            manager.AddListener<TestEvent>(callBack);
            manager.RemoveListener<TestEvent>(callBack);

            //expect
            Assert.AreEqual(1, manager.GetCount<TestEvent>());
        }
    }
}
