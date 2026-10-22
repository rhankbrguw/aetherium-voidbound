using UnityEngine;
using UnityEngine.Rendering;

namespace Aetherium.Editor
{
    public static class MaterialUtility
    {
        public static Shader GetCompatibleShader()
        {
            if (GraphicsSettings.currentRenderPipeline != null)
            {
                Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");
                if (urpShader != null) return urpShader;
            }

            Shader standard = Shader.Find("Standard");
            if (standard != null) return standard;

            return Shader.Find("Diffuse") ?? Shader.Find("Unlit/Color");
        }

        public static Material CreateMaterial(Color color, float smoothness = 0.5f, float metallic = 0.0f)
        {
            Shader shader = GetCompatibleShader();
            Material mat = new Material(shader);
            mat.color = color;
            if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color);
            if (mat.HasProperty("_Color")) mat.SetColor("_Color", color);
            if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", smoothness);
            if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", smoothness);
            if (mat.HasProperty("_Metallic")) mat.SetFloat("_Metallic", metallic);
            return mat;
        }
    }
}
