using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCam : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private Transform portalPos;
    [SerializeField] private Transform otherPortalPos;
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private Camera camera3;
    [SerializeField] private Camera camera4;
    float portalLengthDifference = 0;
    // Update is called once per frame
    void LateUpdate()
    {

        camera1.transform.position = new Vector3(playerCam.position.x - 18f, playerCam.position.y, playerCam.position.z);
        camera2.transform.position = new Vector3(playerCam.position.x + 18f, playerCam.position.y, playerCam.position.z);
        camera3.transform.position = new Vector3(playerCam.position.x - 18f, playerCam.position.y, playerCam.position.z - 16f);
        camera4.transform.position = new Vector3(playerCam.position.x + 18f, playerCam.position.y, playerCam.position.z + 16f);

        Vector3 newRot = playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newRot, Vector3.up);
        camera2.transform.rotation = Quaternion.LookRotation(newRot, Vector3.up);
        camera3.transform.rotation = Quaternion.LookRotation(newRot, Vector3.up);
        camera4.transform.rotation = Quaternion.LookRotation(newRot, Vector3.up);
    }
}
