using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelCamera : MonoBehaviour
{

    [SerializeField] private Transform playerCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float angleDifference = Quaternion.Angle(portalPos.rotation, otherPortalPos.rotation);
        //  Quaternion portalRotDiff = Quaternion.AngleAxis(angleDifference, Vector3.up);
        //Vector3 newRot = portalRotDiff * playerCam.forward;
        transform.rotation = playerCam.rotation;//Quaternion.LookRotation(playerCam.forward, Vector3.up);
    }
}
