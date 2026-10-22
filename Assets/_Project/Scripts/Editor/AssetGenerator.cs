using UnityEditor;
using UnityEngine;
using Aetherium.Combat.Data;
using Aetherium.Player.Data;

namespace Aetherium.Editor
{
    public static class AssetGenerator
    {
        private const string ROOT_PATH = "Assets/_Project/ScriptableObjects";

        [MenuItem("Tools/Aetherium/Generate Combat Assets")]
        public static void GenerateAllAssets()
        {
            EnsureDirectories();
            CreateAttackAssets();
            CreatePlayerStats();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void EnsureDirectories()
        {
            if (!AssetDatabase.IsValidFolder(ROOT_PATH + "/Attacks"))
            {
                AssetDatabase.CreateFolder(ROOT_PATH, "Attacks");
            }
            if (!AssetDatabase.IsValidFolder(ROOT_PATH + "/PlayerStats"))
            {
                AssetDatabase.CreateFolder(ROOT_PATH, "PlayerStats");
            }
        }

        private static void CreateAttackAssets()
        {
            CreateAttackData("LightAttack_1", 20f, 15f, 3f, 0.08f, 0.18f, 0.22f);
            CreateAttackData("LightAttack_2", 30f, 20f, 4f, 0.10f, 0.20f, 0.25f);
            CreateAttackData("LightAttack_3", 55f, 35f, 8f, 0.15f, 0.25f, 0.40f);
            CreateAttackData("HeavyAttack_Charged", 110f, 60f, 14f, 0.40f, 0.30f, 0.50f);
            CreateAttackData("Boss_TwinSlash", 35f, 25f, 6f, 0.25f, 0.22f, 0.35f);
            CreateAttackData("Boss_OverheadCleave", 75f, 50f, 12f, 0.50f, 0.25f, 0.60f);
            CreateAttackData("Boss_VoidShockwave", 90f, 70f, 15f, 0.60f, 0.35f, 0.70f);
        }

        private static void CreateAttackData(
            string name, float dmg, float posture, float knockback, float startup, float active, float recovery)
        {
            string path = $"{ROOT_PATH}/Attacks/{name}.asset";
            if (AssetDatabase.LoadAssetAtPath<AttackDataSO>(path) != null) return;

            AttackDataSO asset = ScriptableObject.CreateInstance<AttackDataSO>();
            SerializedObject serialized = new SerializedObject(asset);
            serialized.FindProperty("damage").floatValue = dmg;
            serialized.FindProperty("postureDamage").floatValue = posture;
            serialized.FindProperty("knockbackForce").floatValue = knockback;
            serialized.FindProperty("startupDuration").floatValue = startup;
            serialized.FindProperty("activeHitboxDuration").floatValue = active;
            serialized.FindProperty("recoveryDuration").floatValue = recovery;
            serialized.ApplyModifiedProperties();

            AssetDatabase.CreateAsset(asset, path);
        }

        private static void CreatePlayerStats()
        {
            string path = $"{ROOT_PATH}/PlayerStats/PlayerStats_Default.asset";
            if (AssetDatabase.LoadAssetAtPath<PlayerStatsSO>(path) != null) return;

            PlayerStatsSO stats = ScriptableObject.CreateInstance<PlayerStatsSO>();
            AssetDatabase.CreateAsset(stats, path);
        }
    }
}
