using UnityEngine;
using Aetherium.Combat.Hitbox;
using Aetherium.Combat.Data;
using Aetherium.Core.Events;
using Aetherium.Core.Constants;

namespace Aetherium.Player.Controller
{
    public class AnimationEventRelay : MonoBehaviour
    {
        [SerializeField] private HitboxController hitboxController;
        [SerializeField] private Hurtbox hurtbox;
        [SerializeField] private AttackDataSO[] attackDataSlots;

        public void SetHitboxActive(int attackIndex)
        {
            if (hitboxController == null)
            {
                return;
            }

            AttackDataSO attack = (attackDataSlots != null && attackIndex >= 0 && attackIndex < attackDataSlots.Length)
                ? attackDataSlots[attackIndex]
                : null;

            if (attack != null)
            {
                hitboxController.EnableHitbox(attack);
            }
        }

        public void SetHitboxInactive()
        {
            hitboxController?.DisableHitbox();
        }

        public void SetInvulnerable(int isInvulnerableFlag)
        {
            if (hurtbox != null)
            {
                hurtbox.IsInvulnerable = isInvulnerableFlag != 0;
            }
        }

        public void PlayFootstep()
        {
            EventBus.Raise(new SoundEffectEvent(
                AudioConstants.SFX_FOOTSTEP_STONE,
                transform.position,
                AudioConstants.VOL_FOOTSTEP
            ));
        }

        public void PlayWeaponSwing()
        {
            EventBus.Raise(new SoundEffectEvent(
                AudioConstants.SFX_SWORD_SWING,
                transform.position,
                AudioConstants.VOL_SWORD_SWING
            ));
        }
    }
}
