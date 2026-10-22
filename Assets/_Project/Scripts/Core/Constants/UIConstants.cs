using UnityEngine;

namespace Aetherium.Core.Constants
{
    public static class UIConstants
    {
        public const float DAMAGE_GHOST_DELAY = 0.4f;
        public const float DAMAGE_GHOST_LERP_SPEED = 4.0f;
        public const float GROGGY_PULSE_FREQUENCY = 6.0f;

        public static readonly Color HealthCrimson = new Color(0.62f, 0.11f, 0.11f, 1.0f);
        public static readonly Color DamageGhostYellow = new Color(0.90f, 0.66f, 0.14f, 0.85f);
        public static readonly Color HealthBackground = new Color(0.11f, 0.07f, 0.07f, 0.95f);

        public static readonly Color StaminaGreen = new Color(0.18f, 0.50f, 0.28f, 1.0f);
        public static readonly Color StaminaExhausted = new Color(0.35f, 0.35f, 0.35f, 0.8f);
        public static readonly Color StaminaBackground = new Color(0.07f, 0.12f, 0.08f, 0.95f);

        public static readonly Color PostureOrange = new Color(0.85f, 0.42f, 0.08f, 1.0f);
        public static readonly Color PostureGroggyRed = new Color(1.0f, 0.15f, 0.15f, 1.0f);
        public static readonly Color PostureBackground = new Color(0.12f, 0.09f, 0.05f, 0.85f);

        public static readonly Color FrameBorderBronze = new Color(0.29f, 0.24f, 0.19f, 1.0f);
        public static readonly Color FrameDarkSlate = new Color(0.06f, 0.06f, 0.08f, 0.98f);
        public static readonly Color DeathblowSigilRed = new Color(0.95f, 0.1f, 0.1f, 0.95f);
    }
}
