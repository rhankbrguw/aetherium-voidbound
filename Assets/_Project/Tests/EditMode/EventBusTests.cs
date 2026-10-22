using NUnit.Framework;
using Aetherium.Core.Events;
using UnityEngine;

namespace Aetherium.Tests.EditMode
{
    public class EventBusTests
    {
        private struct TestPayloadEvent
        {
            public readonly int Value;
            public TestPayloadEvent(int value) => Value = value;
        }

        [SetUp]
        public void Setup()
        {
            EventBus.Clear();
        }

        [Test]
        public void EventBus_SubscribeAndRaise_DeliversPayload()
        {
            int receivedValue = 0;
            void Handler(TestPayloadEvent evt) => receivedValue = evt.Value;

            EventBus.Subscribe<TestPayloadEvent>(Handler);
            EventBus.Raise(new TestPayloadEvent(42));
            EventBus.Unsubscribe<TestPayloadEvent>(Handler);

            Assert.AreEqual(42, receivedValue);
        }

        [Test]
        public void EventBus_Unsubscribe_StopsDelivery()
        {
            int callCount = 0;
            void Handler(TestPayloadEvent evt) => callCount++;

            EventBus.Subscribe<TestPayloadEvent>(Handler);
            EventBus.Raise(new TestPayloadEvent(1));
            EventBus.Unsubscribe<TestPayloadEvent>(Handler);
            EventBus.Raise(new TestPayloadEvent(2));

            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void EventBus_CombatImpactEvent_DeliversAccurateValues()
        {
            CombatImpactEvent received = default;
            void Handler(CombatImpactEvent evt) => received = evt;

            EventBus.Subscribe<CombatImpactEvent>(Handler);
            EventBus.Raise(new CombatImpactEvent(Vector3.up, Vector3.forward, 50f, true, false));
            EventBus.Unsubscribe<CombatImpactEvent>(Handler);

            Assert.AreEqual(Vector3.up, received.HitPosition);
            Assert.AreEqual(50f, received.DamageAmount);
            Assert.IsTrue(received.IsCritical);
            Assert.IsFalse(received.IsParry);
        }
    }
}
