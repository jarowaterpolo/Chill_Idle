using UnityEngine;

public class CRT_WebFix : MonoBehaviour
{
    public CustomRenderTexture[] myCustomTextures;

    void Start()
    {
        foreach (var texture in myCustomTextures) 
        { 
            texture.Initialize();
            texture.Update();
        }
    }
}
