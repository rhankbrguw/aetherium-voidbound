using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Aetherium.Core.Constants;

namespace Aetherium.Editor
{
    public static class ArenaEnvironmentBuilder
    {
        [MenuItem("Tools/Aetherium/Build Dark Souls Arena")]
        public static void BuildArena()
        {
            GameObject arenaRoot = new GameObject("--- ASTRAL COLISEUM ---");
            CreateArenaFloor(arenaRoot.transform);
            CreateGothicPillars(arenaRoot.transform);
            CreateAtmosphericLighting(arenaRoot.transform);
            CreatePostProcessingVolume(arenaRoot.transform);
            Selection.activeGameObject = arenaRoot;
        }

        private static void CreateArenaFloor(Transform parent)
        {
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            floor.name = "Arena_Floor_Obsidian";
            floor.transform.SetParent(parent);
            floor.transform.position = new Vector3(0f, -0.25f, 0f);
            floor.transform.localScale = new Vector3(
                EnvironmentConstants.ARENA_RADIUS * 2f, 
                EnvironmentConstants.ARENA_FLOOR_HEIGHT, 
                EnvironmentConstants.ARENA_RADIUS * 2f
            );

            Collider defaultCol = floor.GetComponent<Collider>();
            if (defaultCol != null) Object.DestroyImmediate(defaultCol);
            MeshCollider meshCol = floor.AddComponent<MeshCollider>();
            meshCol.sharedMesh = floor.GetComponent<MeshFilter>().sharedMesh;

            Renderer rend = floor.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.sharedMaterial = MaterialUtility.CreateMaterial(
                    new Color(0.08f, 0.08f, 0.12f, 1f), 
                    smoothness: 0.6f, 
                    metallic: 0.25f
                );
            }
        }

        private static void CreateGothicPillars(Transform parent)
        {
            Transform pillarsRoot = new GameObject("Gothic_Pillars_Root").transform;
            pillarsRoot.SetParent(parent);

            float angleStep = 360f / EnvironmentConstants.PILLAR_COUNT;
            for (int i = 0; i < EnvironmentConstants.PILLAR_COUNT; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                Vector3 pos = new Vector3(
                    Mathf.Cos(angle) * EnvironmentConstants.PILLAR_RADIUS_OFFSET, 
                    EnvironmentConstants.PILLAR_HEIGHT * 0.5f, 
                    Mathf.Sin(angle) * EnvironmentConstants.PILLAR_RADIUS_OFFSET
                );

                GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                pillar.name = $"Pillar_{i + 1}";
                pillar.transform.SetParent(pillarsRoot);
                pillar.transform.position = pos;
                pillar.transform.localScale = new Vector3(2.5f, EnvironmentConstants.PILLAR_HEIGHT, 2.5f);

                Renderer rend = pillar.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.sharedMaterial = MaterialUtility.CreateMaterial(
                        new Color(0.18f, 0.18f, 0.22f, 1f), 
                        smoothness: 0.15f
                    );
                }

                CreateBrazier(pillar.transform, pos + Vector3.up * (EnvironmentConstants.PILLAR_HEIGHT * 0.5f + 0.5f));
            }
        }

        private static void CreateBrazier(Transform parent, Vector3 position)
        {
            GameObject brazier = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            brazier.name = "Astral_Brazier_Flame";
            brazier.transform.SetParent(parent);
            brazier.transform.position = position;
            brazier.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            Collider col = brazier.GetComponent<Collider>();
            if (col != null) Object.DestroyImmediate(col);

            Renderer rend = brazier.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.sharedMaterial = MaterialUtility.CreateMaterial(EnvironmentConstants.AstralVoidCyan, 0.8f);
            }

            Light pointLight = brazier.AddComponent<Light>();
            pointLight.type = LightType.Point;
            pointLight.color = EnvironmentConstants.AstralVoidCyan;
            pointLight.intensity = EnvironmentConstants.BRAZIER_LIGHT_INTENSITY;
            pointLight.range = EnvironmentConstants.BRAZIER_LIGHT_RANGE;
        }

        private static void CreateAtmosphericLighting(Transform parent)
        {
            GameObject sun = new GameObject("DarkSun_DirectionalLight");
            sun.transform.SetParent(parent);
            sun.transform.rotation = Quaternion.Euler(30f, -45f, 0f);

            Light sunLight = sun.AddComponent<Light>();
            sunLight.type = LightType.Directional;
            sunLight.color = EnvironmentConstants.DarkSunGold;
            sunLight.intensity = EnvironmentConstants.SUN_LIGHT_INTENSITY;
            sunLight.shadows = LightShadows.Soft;
        }

        private static void CreatePostProcessingVolume(Transform parent)
        {
            GameObject volumeObj = new GameObject("Global_PostProcess_Volume");
            volumeObj.transform.SetParent(parent);

            Volume volume = volumeObj.AddComponent<Volume>();
            volume.isGlobal = true;

            VolumeProfile profile = ScriptableObject.CreateInstance<VolumeProfile>();
            profile.name = "DarkSouls_PostProcess_Profile";

            Bloom bloom = profile.Add<Bloom>(true);
            bloom.intensity.value = 1.25f;
            bloom.threshold.value = 0.85f;

            Vignette vignette = profile.Add<Vignette>(true);
            vignette.intensity.value = 0.35f;
            vignette.smoothness.value = 0.45f;

            Tonemapping tonemapping = profile.Add<Tonemapping>(true);
            tonemapping.mode.value = TonemappingMode.ACES;

            volume.profile = profile;
        }
    }
}
