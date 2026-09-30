using UnityEngine;
using UnityEngine.UI;
using Aetherium.UI;
using Aetherium.Core.Constants;

namespace Aetherium.EditorTools
{
    public static class UIHierarchyBuilder
    {
        public static void BuildHUDHierarchy(GameObject hudCanvas)
        {
            CanvasScaler scaler = hudCanvas.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            hudCanvas.AddComponent<GraphicRaycaster>();

            BuildPlayerHUD(hudCanvas.transform);
            BuildBossHUD(hudCanvas.transform);
            BuildLockOnReticle(hudCanvas.transform);
        }

        private static void BuildLockOnReticle(Transform parent)
        {
            GameObject reticle = new GameObject("LockOnReticle", typeof(RectTransform));
            reticle.transform.SetParent(parent, false);
            Image img = reticle.AddComponent<Image>();
            img.color = UIConstants.DeathblowSigilRed;
            img.enabled = false;
            reticle.GetComponent<RectTransform>().sizeDelta = new Vector2(32f, 32f);
            reticle.AddComponent<LockOnMarkerController>();
        }

        private static void BuildPlayerHUD(Transform parent)
        {
            GameObject hudGo = new GameObject("PlayerHUD", typeof(RectTransform));
            hudGo.transform.SetParent(parent, false);
            RectTransform rt = hudGo.GetComponent<RectTransform>();
            SetAnchor(rt, new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(40f, -40f), new Vector2(300f, 80f));

            Image bgHealth = CreateBar(hudGo.transform, "HealthBG", new Vector2(0f, 0f), new Vector2(260f, 16f), UIConstants.HealthBackground);
            Image ghostHealth = CreateBar(bgHealth.transform, "HealthGhost", Vector2.zero, Vector2.zero, UIConstants.DamageGhostYellow, true);
            Image fillHealth = CreateBar(bgHealth.transform, "HealthFill", Vector2.zero, Vector2.zero, UIConstants.HealthCrimson, true);

            Image bgStamina = CreateBar(hudGo.transform, "StaminaBG", new Vector2(0f, -24f), new Vector2(200f, 10f), UIConstants.StaminaBackground);
            Image fillStamina = CreateBar(bgStamina.transform, "StaminaFill", Vector2.zero, Vector2.zero, UIConstants.StaminaGreen, true);

            PlayerHUDController controller = hudGo.AddComponent<PlayerHUDController>();
            SetPrivateField(controller, "healthFillImage", fillHealth);
            SetPrivateField(controller, "healthGhostImage", ghostHealth);
            SetPrivateField(controller, "staminaFillImage", fillStamina);
        }

        private static void BuildBossHUD(Transform parent)
        {
            GameObject bossGo = new GameObject("BossHealthBar", typeof(RectTransform));
            bossGo.transform.SetParent(parent, false);
            RectTransform rt = bossGo.GetComponent<RectTransform>();
            SetAnchor(rt, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 70f), new Vector2(720f, 70f));

            Image bgHealth = CreateBar(bossGo.transform, "BossHealthBG", new Vector2(0f, 0f), new Vector2(700f, 20f), UIConstants.HealthBackground);
            Image ghostHealth = CreateBar(bgHealth.transform, "GhostFill", Vector2.zero, Vector2.zero, UIConstants.DamageGhostYellow, true);
            Image fillHealth = CreateBar(bgHealth.transform, "HealthFill", Vector2.zero, Vector2.zero, UIConstants.HealthCrimson, true);

            Image bgPosture = CreateBar(bossGo.transform, "PostureBG", new Vector2(0f, -16f), new Vector2(480f, 6f), UIConstants.PostureBackground);
            Image fillPosture = CreateBar(bgPosture.transform, "PostureFill", Vector2.zero, Vector2.zero, UIConstants.PostureOrange, true);

            GameObject groggyGo = new GameObject("GroggyDeathblowSigil", typeof(RectTransform));
            groggyGo.transform.SetParent(bossGo.transform, false);
            Image sigil = groggyGo.AddComponent<Image>();
            sigil.color = UIConstants.DeathblowSigilRed;
            groggyGo.GetComponent<RectTransform>().sizeDelta = new Vector2(28f, 28f);
            groggyGo.SetActive(false);

            BossHealthBar controller = bossGo.AddComponent<BossHealthBar>();
            SetPrivateField(controller, "healthFillImage", fillHealth);
            SetPrivateField(controller, "healthGhostImage", ghostHealth);
            SetPrivateField(controller, "postureFillImage", fillPosture);
            SetPrivateField(controller, "groggyIndicator", groggyGo);
        }

        private static Image CreateBar(Transform parent, string name, Vector2 pos, Vector2 size, Color color, bool stretch = false)
        {
            GameObject go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            RectTransform rt = go.GetComponent<RectTransform>();
            if (stretch)
            {
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.sizeDelta = Vector2.zero;
            }
            else
            {
                rt.anchoredPosition = pos;
                rt.sizeDelta = size;
            }

            Image img = go.AddComponent<Image>();
            img.color = color;
            img.type = Image.Type.Filled;
            img.fillMethod = Image.FillMethod.Horizontal;
            img.fillAmount = 1f;
            return img;
        }

        private static void SetAnchor(RectTransform rt, Vector2 min, Vector2 max, Vector2 pos, Vector2 size)
        {
            rt.anchorMin = min;
            rt.anchorMax = max;
            rt.anchoredPosition = pos;
            rt.sizeDelta = size;
        }

        private static void SetPrivateField(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field?.SetValue(target, value);
        }
    }
}
