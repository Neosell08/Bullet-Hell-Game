using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScript : MonoBehaviour
{
    public Material fullscreenMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        
        fullscreenMaterial.SetTexture("_MainTex", src);

        src.Create();
        
        Graphics.Blit(null, null, fullscreenMaterial);
    }
}
