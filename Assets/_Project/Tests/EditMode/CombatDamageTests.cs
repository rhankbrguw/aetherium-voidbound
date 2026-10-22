using NUnit.Framework;
using UnityEngine;
using Aetherium.Combat.Data;

namespace Aetherium.Tests.EditMode
{
    public class CombatDamageTests
    {
        [Test]
        public void DamagePayload_ConstructsCorrectly()
        {
            Vector3 hitDirection = Vector3.forward;
            Vector3 hitPoint = new Vector3(0f, 1f, 2f);
            DamagePayload payload = new DamagePayload(25f, 15f, 5f, hitDirection, hitPoint, null, false);

            Assert.AreEqual(25f, payload.DamageAmount);
            Assert.AreEqual(15f, payload.PostureDamage);
            Assert.AreEqual(5f, payload.KnockbackForce);
            Assert.AreEqual(hitDirection, payload.HitDirection);
            Assert.AreEqual(hitPoint, payload.HitPoint);
            Assert.IsFalse(payload.IsUnblockable);
        }

        [Test]
        public void DamagePayload_UnblockableFlag_IsRespected()
        {
            DamagePayload payload = new DamagePayload(50f, 30f, 10f, Vector3.back, Vector3.zero, null, true);

            Assert.IsTrue(payload.IsUnblockable);
            Assert.AreEqual(50f, payload.DamageAmount);
        }
    }
}
