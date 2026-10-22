using UnityEngine;

namespace Aetherium.Core.Constants
{
    public static class GameConstants
    {
        public const float DEFAULT_GRAVITY = -19.62f;
        public const float GROUND_CHECK_DISTANCE = 0.2f;
        public const float SLOPE_LIMIT_ANGLE = 45f;
        
        public const float DEFAULT_ROTATION_SMOOTH_TIME = 0.12f;
        public const float DEFAULT_TARGET_MATCH_WEIGHT = 0.8f;
        
        public const float PARRY_WINDOW_DURATION = 0.15f;
        public const float DODGE_IFRAME_START = 0.033f;
        public const float DODGE_IFRAME_END = 0.233f;
        
        public const float POSTURE_RECOVERY_DELAY = 3.0f;
        public const float POSTURE_RECOVERY_RATE = 15.0f;
        public const float GROGGY_STATE_DURATION = 4.0f;

        public const int NON_ALLOC_COLLIDER_BUFFER_SIZE = 16;
        public const int NON_ALLOC_RAYCAST_BUFFER_SIZE = 8;
    }
}
