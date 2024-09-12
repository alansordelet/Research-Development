using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamDirection : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform portalPos;
    [SerializeField] private Transform otherPortalPos;
  
    void LateUpdate()
    {
        Vector3 playeroffset = playerCam.position - portalPos.position;
  
        transform.position = otherPortalPos.position + new Vector3(-playeroffset.x, playeroffset.y, -playeroffset.z);

        float angleDifference = Quaternion.Angle(portalPos.rotation, otherPortalPos.rotation);
        Quaternion portalRotDiff = Quaternion.AngleAxis(angleDifference, Vector3.up);
        Vector3 newRot = portalRotDiff * new Vector3(-playerCam.forward.x, playerCam.forward.y, -playerCam.forward.z);
        transform.rotation = Quaternion.LookRotation(newRot, Vector3.up);
    }
}
