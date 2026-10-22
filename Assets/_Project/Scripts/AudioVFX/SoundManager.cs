using UnityEngine;
using Aetherium.Core.Events;

namespace Aetherium.AudioVFX
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip parryClangClip;
        [SerializeField] private AudioClip swordHitClip;
        [SerializeField] private AudioClip postureBreakClip;

        private void Awake()
        {
            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void OnEnable()
        {
            EventBus.Subscribe<CombatImpactEvent>(HandleCombatImpact);
            EventBus.Subscribe<BossPostureChangedEvent>(HandleBossPosture);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<CombatImpactEvent>(HandleCombatImpact);
            EventBus.Unsubscribe<BossPostureChangedEvent>(HandleBossPosture);
        }

        private void HandleCombatImpact(CombatImpactEvent impact)
        {
            if (impact.IsParry)
            {
                PlayClip(parryClangClip, 1.0f);
            }
            else
            {
                PlayClip(swordHitClip, 0.8f);
            }
        }

        private void HandleBossPosture(BossPostureChangedEvent evt)
        {
            if (evt.IsGroggy)
            {
                PlayClip(postureBreakClip, 1.2f);
            }
        }

        private void PlayClip(AudioClip clip, float volume)
        {
            if (clip == null || sfxSource == null)
            {
                return;
            }

            sfxSource.PlayOneShot(clip, volume);
        }
    }
}
