using System.Collections;
using UnityEngine;
using Aetherium.Core.Events;

namespace Aetherium.AudioVFX
{
    public class HitStopManager : MonoBehaviour
    {
        private Coroutine hitStopCoroutine;

        private void OnEnable()
        {
            EventBus.Subscribe<CombatImpactEvent>(OnCombatImpact);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<CombatImpactEvent>(OnCombatImpact);
        }

        private void OnCombatImpact(CombatImpactEvent impact)
        {
            float duration = impact.IsParry ? 0.12f : (impact.IsCritical ? 0.08f : 0.04f);
            TriggerHitStop(duration);
        }

        public void TriggerHitStop(float duration)
        {
            if (hitStopCoroutine != null)
            {
                StopCoroutine(hitStopCoroutine);
            }
            hitStopCoroutine = StartCoroutine(HitStopRoutine(duration));
        }

        private IEnumerator HitStopRoutine(float duration)
        {
            Time.timeScale = 0.05f;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
            hitStopCoroutine = null;
        }
    }
}
