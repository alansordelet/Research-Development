using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    public float panSpeed = 30.0f;
    public float maxLookAngle = 45.0f;
    public Transform player;
    private float currentRotation = 0.0f;
    private float currentLookAngle = 45f;
   
    public bool playerInLightRay = false;
    public Light thisLight;
    public float maxDistance = 20f;
    private Vector3 direction;
    void Update()
    {
        int layer = 1 << LayerMask.NameToLayer("Player");
        playerInLightRay = false;

        for (float angleY = -10f; angleY <= 10f; angleY += 1f)
        {
            for (float angleX = -10f; angleX <= 10f; angleX += 1f)
            {
                Quaternion rotation = Quaternion.AngleAxis(angleY, Vector3.up) * Quaternion.AngleAxis(angleX, Vector3.right);
                Vector3 coneDirection = rotation * direction;
                Ray ray = new Ray(thisLight.transform.position, coneDirection);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
                {
                    playerInLightRay = true;
                    break;
                }
            }
        }


        if (playerInLightRay)
        {
            Vector3 playerPosition = player.position;
            transform.LookAt(playerPosition);
        }
        else
        {
            currentRotation += panSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, currentRotation, 0);

            Quaternion newRotation = Quaternion.Euler(currentLookAngle, currentRotation, 0);
            transform.rotation = newRotation;
            direction = thisLight.transform.forward;
        }
    }


    private void OnDrawGizmos()
    {

        for (float angleX = -10f; angleX <= 10f; angleX += 1f)
        {
            for (float angleY = (-10f + 5); angleY <= (10f - 5); angleY += 1f)
            {
                Quaternion rotation = Quaternion.AngleAxis(angleX, Vector3.up) * Quaternion.AngleAxis(angleY, Vector3.right);
                Vector3 coneDirection = rotation * direction;
                Vector3 coneTip = thisLight.transform.position + coneDirection * maxDistance;
                Gizmos.DrawLine(thisLight.transform.position, coneTip);
            }
        }
    }
}
