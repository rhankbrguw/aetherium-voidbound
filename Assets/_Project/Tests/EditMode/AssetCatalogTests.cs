using NUnit.Framework;
using UnityEngine;
using Aetherium.Core.Data;

namespace Aetherium.Tests.EditMode
{
    [TestFixture]
    public class AssetCatalogTests
    {
        [Test]
        public void AudioEntry_ConstructsWithValidVolume()
        {
            AudioEntry entry = new AudioEntry
            {
                key = "SFX_Parry_Success",
                clip = null,
                volume = 0.85f
            };

            Assert.AreEqual("SFX_Parry_Success", entry.key);
            Assert.AreEqual(0.85f, entry.volume, 0.001f);
            Assert.IsNull(entry.clip);
        }

        [Test]
        public void VFXEntry_ConstructsWithValidLifetime()
        {
            VFXEntry entry = new VFXEntry
            {
                key = "VFX_Deflect_Sparks",
                prefab = null,
                defaultLifetime = 1.5f
            };

            Assert.AreEqual("VFX_Deflect_Sparks", entry.key);
            Assert.AreEqual(1.5f, entry.defaultLifetime, 0.001f);
            Assert.IsNull(entry.prefab);
        }
    }
}
