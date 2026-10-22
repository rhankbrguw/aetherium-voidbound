using System;
using System.Collections.Generic;
using UnityEngine;
using Aetherium.Core.Constants;

namespace Aetherium.Core.Data
{
    [Serializable]
    public struct AudioEntry
    {
        public string key;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
    }

    [CreateAssetMenu(fileName = "AudioCatalog", menuName = "Aetherium/Data/Audio Catalog")]
    public class AudioCatalogSO : ScriptableObject
    {
        [SerializeField] private List<AudioEntry> entries = new List<AudioEntry>();

        public AudioClip GetClip(string key, out float volume)
        {
            volume = 1.0f;
            if (string.IsNullOrEmpty(key)) return null;

            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].key == key)
                {
                    volume = entries[i].volume;
                    return entries[i].clip;
                }
            }
            return null;
        }

        public void AddEntry(string key, AudioClip clip, float volume = 1.0f)
        {
            entries.Add(new AudioEntry { key = key, clip = clip, volume = volume });
        }
    }
}
