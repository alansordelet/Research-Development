using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistortion : MonoBehaviour
{
    public Camera distortedCamera;
    public float maxFOV = 120f; // Increased maximum field of view for more distortion
    public float distortionStrength = 0.5f; // Strength of shader distortion
    public Shader distortionShader; // Assign a distortion shader
    private float maxSpeed = 10f;
    private Material distortionMaterial;
    private float baseFOV;

    void Start()
    {
        if (distortedCamera == null)
        {
            distortedCamera = Camera.main;
        }
        baseFOV = distortedCamera.fieldOfView;

        // Initialize the distortion material
        if (distortionShader != null)
        {
            distortionMaterial = new Material(distortionShader);
        }
    }

    public void UpdateDistortion(float speed)
    {
        // Adjust FOV based on speed
        distortedCamera.fieldOfView = Mathf.Lerp(baseFOV, maxFOV, speed / maxSpeed);

        // Adjust shader distortion based on speed
        if (distortionMaterial != null)
        {
            float distortionAmount = Mathf.Clamp(speed / maxSpeed, 0, distortionStrength);
            distortionMaterial.SetFloat("_DistortionAmount", distortionAmount);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (distortionMaterial != null)
        {
            Graphics.Blit(src, dest, distortionMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
