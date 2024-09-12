using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class portalTextureSetup : MonoBehaviour
{
    public Camera VirtualCameraB;
    public Camera VirtualCameraC;
    public Camera VirtualCameraTunnel;
    public Camera VirtualCameraD;
    public Camera VirtualCameraE;
    public Material materialCam;
    public Material materialCamC;
    public Material materialCamTunnel;
    public Material materialCamD;
    public Material materialCamE;
    // Start is called before the first frame update
    void Start()
    {
        if (VirtualCameraB.targetTexture != null)
        {
            VirtualCameraB.targetTexture.Release();
        }
        VirtualCameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialCam.mainTexture = VirtualCameraB.targetTexture;

        if (VirtualCameraC.targetTexture != null)
        {
            VirtualCameraC.targetTexture.Release();
        }
        VirtualCameraC.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialCamC.mainTexture = VirtualCameraC.targetTexture;



        //if (VirtualCameraTunnel.targetTexture != null)
        //{
        //    VirtualCameraTunnel.targetTexture.Release();
        //}
        //VirtualCameraTunnel.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //materialCamTunnel.mainTexture = VirtualCameraTunnel.targetTexture;

        //if (VirtualCameraD.targetTexture != null)
        //{
        //    VirtualCameraD.targetTexture.Release();
        //}
        //VirtualCameraD.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //materialCamD.mainTexture = VirtualCameraD.targetTexture;

        //if (VirtualCameraE.targetTexture != null)
        //{
        //    VirtualCameraE.targetTexture.Release();
        //}
        //VirtualCameraE.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //materialCamE.mainTexture = VirtualCameraE.targetTexture;
    }

}
