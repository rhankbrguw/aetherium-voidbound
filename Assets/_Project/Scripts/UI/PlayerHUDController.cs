using UnityEngine;
using UnityEngine.UI;
using Aetherium.Core.Events;
using Aetherium.Core.Constants;

namespace Aetherium.UI
{
    public class PlayerHUDController : MonoBehaviour
    {
        [SerializeField] private Image healthFillImage;
        [SerializeField] private Image healthGhostImage;
        [SerializeField] private Image staminaFillImage;

        private float targetHealthFill = 1.0f;
        private float ghostDelayTimer;

        private void OnEnable()
        {
            EventBus.Subscribe<PlayerHealthChangedEvent>(OnHealthChanged);
            EventBus.Subscribe<PlayerStaminaChangedEvent>(OnStaminaChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<PlayerHealthChangedEvent>(OnHealthChanged);
            EventBus.Unsubscribe<PlayerStaminaChangedEvent>(OnStaminaChanged);
        }

        private void Update()
        {
            UpdateGhostHealthBar(Time.deltaTime);
        }

        private void OnHealthChanged(PlayerHealthChangedEvent evt)
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

        private void UpdateGhostHealthBar(float deltaTime)
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

        private void OnStaminaChanged(PlayerStaminaChangedEvent evt)
        {
            if (staminaFillImage == null || evt.MaxStamina <= 0f) return;
            staminaFillImage.fillAmount = Mathf.Clamp01(evt.CurrentStamina / evt.MaxStamina);
        }
    }
}
