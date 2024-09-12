using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
public class PriestVisibilityCheck : MonoBehaviour
{
    //public static PriestVisibilityCheck instance { get; private set; }
    public Transform priestTransform;
    public bool priestIsInCameraView;
    public float coneAngle = 20f;
    public float coneLength = 30f;
    public Animator priestAnimator;
    public bool playerInView = false;
    public float timerChase = 0f;
    int minus = 0;
    public float addAngleX = 40f;
    public float addAngleY = 5f;
    public float Separation = 5f;
    //public static PriestVisibilityCheck GetInstance()
    //{
    //    return instance;
    //}

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //        Destroy(gameObject);
    //}
    int layer = 0;
    void Update()
    {
       // playerInView = false;
       // CheckForPlayer();
        PriestChasing();
    }

    void CheckForPlayer()
    {
       

        layer = 1 << LayerMask.NameToLayer("Player");
        Vector3 direction = transform.forward;
        float maxConeLength = coneLength; 

        for (float angleX = (-coneAngle - addAngleX); angleX <= (coneAngle + addAngleX); angleX += Separation)
        {
            for (float angleY = (-coneAngle + addAngleY); angleY <= (coneAngle - addAngleY); angleY += Separation)
            {
                Quaternion rotation = Quaternion.AngleAxis(angleY, Vector3.up) * Quaternion.AngleAxis(angleX, Vector3.right);
                Vector3 coneDirection = rotation * direction;

                Ray ray = new Ray(transform.position, coneDirection);

                if (Physics.Raycast(ray, out RaycastHit hit, coneLength, layer))
                {
                    playerInView = true;
                }
            }
        }
    }

    private void PriestChasing()
    {
        if (playerInView)
        {
            timerChase += Time.deltaTime;
            priestAnimator.SetBool("isSprinting", true);         
        }
        if (!playerInView)
            timerChase = 0;

        //if (timerChase > 2 && )
        //{
        //    timerChase = 0;
        //}
    }

    private void OnDrawGizmos()
    {
        layer = 1 << LayerMask.NameToLayer("Player");
        Vector3 direction = transform.forward;
        for (float angleX = (-coneAngle - addAngleX); angleX <= (coneAngle + addAngleX); angleX += Separation)
        {
            for (float angleY = (-coneAngle + addAngleY); angleY <= (coneAngle - addAngleY); angleY += Separation)
            {
                Quaternion rotation = Quaternion.AngleAxis(angleX, Vector3.up) * Quaternion.AngleAxis(angleY, Vector3.right);
                Vector3 coneDirection = rotation * direction;
                Vector3 coneTip = transform.position + coneDirection * coneLength;
                Ray ray = new Ray(transform.position, coneDirection);

                if (Physics.Raycast(ray, out RaycastHit hit, coneLength, layer))
                {
                    playerInView = true;
                    Gizmos.color = Color.green; 
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawLine(transform.position, coneTip);
            }
        }
    }
}
