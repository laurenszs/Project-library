using UnityEngine;

public class scans2 : MonoBehaviour
{
    public Material material;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture(MainTex, source);
        Graphics.Blit(source, destination, material);
    }
}