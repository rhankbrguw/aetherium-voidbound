using UnityEngine;
using UnityEngine.UI;
using Aetherium.Core.Events;
using Aetherium.Core.Constants;

namespace Aetherium.UI
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthFillImage;
        [SerializeField] private Image healthGhostImage;
        [SerializeField] private Image postureFillImage;
        [SerializeField] private GameObject groggyIndicator;

        private float targetHealthFill = 1.0f;
        private float ghostDelayTimer;
        private bool isGroggyActive;

        private void OnEnable()
        {
            EventBus.Subscribe<BossHealthChangedEvent>(OnBossHealthChanged);
            EventBus.Subscribe<BossPostureChangedEvent>(OnBossPostureChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<BossHealthChangedEvent>(OnBossHealthChanged);
            EventBus.Unsubscribe<BossPostureChangedEvent>(OnBossPostureChanged);
        }

        private void Update()
        {
            UpdateGhostHealth(Time.deltaTime);
            UpdateGroggyPulse();
        }

        private void OnBossHealthChanged(BossHealthChangedEvent evt)
        {
            if (evt.MaxHealth <= 0f) return;
            float newFill = Mathf.Clamp01(evt.CurrentHealth / evt.MaxHealth);

            if (newFill < targetHealthFill)
            {
                ghostDelayTimer = UIConstants.DAMAGE_GHOST_DELAY;
            }

            targetHealthFill = newFill;
            if (healthFillImage != null)
            {
                healthFillImage.fillAmount = targetHealthFill;
            }
        }

        private void UpdateGhostHealth(float deltaTime)
        {
            if (healthGhostImage == null) return;

            if (ghostDelayTimer > 0f)
            {
                ghostDelayTimer -= deltaTime;
                return;
            }

            if (healthGhostImage.fillAmount > targetHealthFill)
            {
                healthGhostImage.fillAmount = Mathf.MoveTowards(
                    healthGhostImage.fillAmount,
                    targetHealthFill,
                    UIConstants.DAMAGE_GHOST_LERP_SPEED * deltaTime
                );
            }
            else
            {
                healthGhostImage.fillAmount = targetHealthFill;
            }
        }

        private void OnBossPostureChanged(BossPostureChangedEvent evt)
        {
            if (postureFillImage != null && evt.MaxPosture > 0f)
            {
                postureFillImage.fillAmount = Mathf.Clamp01(evt.CurrentPosture / evt.MaxPosture);
                postureFillImage.color = evt.IsGroggy ? UIConstants.PostureGroggyRed : UIConstants.PostureOrange;
            }

            isGroggyActive = evt.IsGroggy;
            if (groggyIndicator != null)
            {
                groggyIndicator.SetActive(isGroggyActive);
            }
        }

        private void UpdateGroggyPulse()
        {
            if (!isGroggyActive || groggyIndicator == null) return;

            float pulse = 0.6f + 0.4f * Mathf.Sin(Time.time * UIConstants.GROGGY_PULSE_FREQUENCY);
            groggyIndicator.transform.localScale = Vector3.one * pulse;
        }
    }
}
