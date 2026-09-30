using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Aetherium.Player.Controller;
using Aetherium.Enemy.Boss;
using Aetherium.Combat.Hitbox;
using Aetherium.Core.Constants;
using Aetherium.AudioVFX;
using Aetherium.CameraSystem;
using Aetherium.Editor;

namespace Aetherium.EditorTools
{
    public static class MasterSceneBuilder
    {
        [MenuItem("Tools/Aetherium/Build Master Scene (Astral Colosseum)")]
        public static void BuildMasterScene()
        {
            if (!Directory.Exists(SceneConstants.SCENE_DIR)) Directory.CreateDirectory(SceneConstants.SCENE_DIR);
            EnsureTagExists(TagConstants.PLAYER_TAG);
            EnsureTagExists(TagConstants.ENEMY_TAG);

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            ArenaEnvironmentBuilder.BuildArena();
            GameObject player = CreateEntity("Player", new Vector3(0f, 1.0f, -6f), 1.8f, 0.45f, true);
            GameObject boss = CreateEntity("VoidboundBoss", new Vector3(0f, 1.6f, 6f), 3.2f, 1.0f, false);
            CreateCameraRig(player.transform);
            CreateUIAndManagers();

            EditorSceneManager.SaveScene(scene, SceneConstants.ASTRAL_COLOSSEUM_SCENE);
            RegisterBuildScene(SceneConstants.ASTRAL_COLOSSEUM_SCENE);
            Debug.Log(StringConstants.MSG_SCENE_GENERATED);
        }

        private static void EnsureTagExists(string tag)
        {
            SerializedObject tm = new SerializedObject(AssetDatabase.LoadMainAssetAtPath("ProjectSettings/TagManager.asset"));
            SerializedProperty tags = tm.FindProperty("tags");
            for (int i = 0; i < tags.arraySize; i++)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue.Equals(tag)) return;
            }
            tags.InsertArrayElementAtIndex(tags.arraySize);
            tags.GetArrayElementAtIndex(tags.arraySize - 1).stringValue = tag;
            tm.ApplyModifiedProperties();
        }

        private static GameObject CreateEntity(string name, Vector3 pos, float height, float radius, bool isPlayer)
        {
            GameObject entity = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            entity.name = name;
            entity.transform.position = pos;
            entity.tag = isPlayer ? TagConstants.PLAYER_TAG : TagConstants.ENEMY_TAG;
            if (!isPlayer) entity.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            entity.transform.localScale = new Vector3(radius * 2f, height * 0.5f, radius * 2f);

            Collider defaultCol = entity.GetComponent<Collider>();
            if (defaultCol != null) Object.DestroyImmediate(defaultCol);
            SetupEntityVisual(entity, isPlayer);

            CharacterController cc = entity.AddComponent<CharacterController>();
            cc.height = height;
            cc.radius = radius;
            cc.center = new Vector3(0f, height * 0.5f, 0f);

            entity.AddComponent<Hurtbox>();
            AttachEntityComponents(entity, isPlayer);
            return entity;
        }

        private static void SetupEntityVisual(GameObject entity, bool isPlayer)
        {
            Renderer rend = entity.GetComponent<Renderer>();
            if (rend == null) return;
            Color tint = isPlayer ? new Color(0.15f, 0.55f, 0.95f) : new Color(0.85f, 0.12f, 0.15f);
            rend.sharedMaterial = MaterialUtility.CreateMaterial(tint, smoothness: 0.5f, metallic: 0.3f);
        }

        private static void AttachEntityComponents(GameObject entity, bool isPlayer)
        {
            if (isPlayer)
            {
                entity.AddComponent<PlayerController>();
                entity.AddComponent<AnimationEventRelay>();
            }
            else
            {
                entity.AddComponent<BossController>();
                entity.AddComponent<BossPhaseManager>();
            }

            GameObject weapon = GameObject.CreatePrimitive(PrimitiveType.Cube);
            weapon.name = isPlayer ? "Player_Blade" : "Boss_Greatsword";
            weapon.transform.SetParent(entity.transform);
            weapon.transform.localPosition = isPlayer ? new Vector3(0.5f, 0.5f, 0.6f) : new Vector3(0.8f, 0.8f, 1.0f);
            weapon.transform.localScale = isPlayer ? new Vector3(0.12f, 0.12f, 1.2f) : new Vector3(0.3f, 0.3f, 2.5f);

            Collider col = weapon.GetComponent<Collider>();
            if (col != null) Object.DestroyImmediate(col);
            Renderer rend = weapon.GetComponent<Renderer>();
            if (rend != null)
            {
                Color c = isPlayer ? new Color(0.85f, 0.85f, 0.95f) : new Color(0.25f, 0.05f, 0.08f);
                rend.sharedMaterial = MaterialUtility.CreateMaterial(c, 0.8f, 0.7f);
            }
            weapon.AddComponent<HitboxController>();
        }

        private static void CreateCameraRig(Transform target)
        {
            GameObject camObj = new GameObject("Main Camera");
            camObj.tag = "MainCamera";
            UnityEngine.Camera cam = camObj.AddComponent<UnityEngine.Camera>();
            cam.fieldOfView = 60f;
            cam.farClipPlane = 500f;
            camObj.AddComponent<AudioListener>();
            camObj.AddComponent<LockOnTargetController>();
            camObj.AddComponent<ThirdPersonCameraController>().SetTarget(target);
            camObj.transform.position = target.position + new Vector3(0f, 3.5f, -6f);
            camObj.transform.LookAt(target.position + Vector3.up * 1.5f);
        }

        private static void CreateUIAndManagers()
        {
            GameObject hud = new GameObject("HUDCanvas", typeof(RectTransform));
            hud.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            UIHierarchyBuilder.BuildHUDHierarchy(hud);

            GameObject mgr = new GameObject("Managers");
            mgr.AddComponent<HitStopManager>();
            mgr.AddComponent<VFXManager>();
            mgr.AddComponent<SoundManager>();
        }

        private static void RegisterBuildScene(string path)
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            for (int i = 0; i < scenes.Length; i++) if (scenes[i].path == path) return;
            EditorBuildSettingsScene[] updated = new EditorBuildSettingsScene[scenes.Length + 1];
            scenes.CopyTo(updated, 0);
            updated[scenes.Length] = new EditorBuildSettingsScene(path, true);
            EditorBuildSettings.scenes = updated;
        }
    }
}
