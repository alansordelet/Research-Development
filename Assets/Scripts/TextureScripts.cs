using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScripts : MonoBehaviour
{
    public Camera CameraA;
    public Camera CameraB;
    public Camera CameraC;
    public Camera CameraD;
    public Material materialCamA;
    public Material materialCamB;
    public Material materialCamC;
    public Material materialCamD;

    void Start()
    {
        if (CameraA.targetTexture != null)
        {
            CameraA.targetTexture.Release();
        }
        CameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialCamA.mainTexture = CameraA.targetTexture;

        if (CameraB.targetTexture != null)
        {
            CameraB.targetTexture.Release();
        }
        CameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialCamB.mainTexture = CameraB.targetTexture;

        if (CameraC.targetTexture != null)
        {
            CameraC.targetTexture.Release();
        }
        CameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialCamC.mainTexture = CameraC.targetTexture;

        if (CameraD.targetTexture != null)
        {
            CameraD.targetTexture.Release();
        }
        CameraD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialCamD.mainTexture = CameraD.targetTexture;
    }
}
