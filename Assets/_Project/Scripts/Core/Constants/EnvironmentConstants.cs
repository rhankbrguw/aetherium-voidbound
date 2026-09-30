using UnityEngine;

namespace Aetherium.Core.Constants
{
    public static class EnvironmentConstants
    {
        public const float ARENA_RADIUS = 18.0f;
        public const float ARENA_FLOOR_HEIGHT = 0.5f;
        public const int PILLAR_COUNT = 8;
        public const float PILLAR_HEIGHT = 9.0f;
        public const float PILLAR_RADIUS_OFFSET = 16.5f;
        public const float BRAZIER_LIGHT_RANGE = 12.0f;
        public const float BRAZIER_LIGHT_INTENSITY = 2.5f;

        public const float SUN_LIGHT_INTENSITY = 0.85f;
        public const float RIM_LIGHT_INTENSITY = 1.2f;

        public static readonly Color AstralVoidCyan = new Color(0.15f, 0.75f, 1.0f, 1.0f);
        public static readonly Color AstralVoidPurple = new Color(0.55f, 0.15f, 0.95f, 1.0f);
        public static readonly Color DarkSunGold = new Color(0.95f, 0.75f, 0.45f, 1.0f);
        public static readonly Color DeepShadowColor = new Color(0.04f, 0.03f, 0.08f, 1.0f);
    }
}
