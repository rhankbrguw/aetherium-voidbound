using UnityEngine;

namespace Aetherium.Core.Constants
{
    public static class AppConfig
    {
        public const string APP_NAME = "Aetherium: Voidbound";
        public const string APP_VERSION = "1.0.0";
        public const int TARGET_FRAME_RATE = 60;
        public const int FIXED_TIMESTEP_HERTZ = 60;

        public static bool IsProduction => !Debug.isDebugBuild;
        public static bool IsDevelopment => Debug.isDebugBuild;

        public static void ApplyEngineSettings()
        {
            Application.targetFrameRate = TARGET_FRAME_RATE;
            Time.fixedDeltaTime = 1.0f / FIXED_TIMESTEP_HERTZ;
            QualitySettings.vSyncCount = 0;
        }
    }
}
