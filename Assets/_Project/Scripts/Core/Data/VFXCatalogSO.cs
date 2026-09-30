using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aetherium.Core.Data
{
    [Serializable]
    public struct VFXEntry
    {
        public string key;
        public GameObject prefab;
        public float defaultLifetime;
    }

    [CreateAssetMenu(fileName = "VFXCatalog", menuName = "Aetherium/Data/VFX Catalog")]
    public class VFXCatalogSO : ScriptableObject
    {
        [SerializeField] private List<VFXEntry> entries = new List<VFXEntry>();

        public GameObject GetPrefab(string key, out float lifetime)
        {
            lifetime = 2.0f;
            if (string.IsNullOrEmpty(key)) return null;

            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].key == key)
                {
                    lifetime = entries[i].defaultLifetime;
                    return entries[i].prefab;
                }
            }
            return null;
        }

        public void AddEntry(string key, GameObject prefab, float lifetime = 2.0f)
        {
            entries.Add(new VFXEntry { key = key, prefab = prefab, defaultLifetime = lifetime });
        }
    }
}
