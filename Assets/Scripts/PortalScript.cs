using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Transform playerPos;
    public Transform recieverInside;
    public Transform recieverOutside;

    private bool playerisOverlapping = false;

 
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerisOverlapping && InTunnelManager.instance.inTunnel == false)
        {
            Debug.Log("Used portal From Outside");
            Vector3 portalToplayer = playerPos.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToplayer);

            if (dotProduct < 0f)
            {
                float rotationdiff = -Quaternion.Angle(transform.rotation, recieverInside.rotation);

                rotationdiff += 180;
                playerPos.Rotate(Vector3.up, rotationdiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationdiff, 0f) * portalToplayer;
             
                playerPos.position = recieverInside.position + positionOffset;
                
                if (Vector3.Distance(playerPos.position, recieverInside.position) > 10f)
                {
                    Debug.Log("Player too far from receiver");
                    playerPos.position = recieverInside.position;
                }
                playerisOverlapping = false;
            }
        }

        if (playerisOverlapping && InTunnelManager.instance.inTunnel == true)
        {
            Debug.Log("Used portal From Inside");
            Vector3 portalToplayer = playerPos.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToplayer);

            if (dotProduct < 0f)
            {
                float rotationdiff = -Quaternion.Angle(transform.rotation, recieverOutside.rotation);

                rotationdiff += 180;
                playerPos.Rotate(Vector3.up, rotationdiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationdiff, 0f) * portalToplayer;

                playerPos.position = recieverOutside.position + positionOffset;

                if (Vector3.Distance(playerPos.position, recieverOutside.position) > 10f)
                {
                    Debug.Log("Player too far from receiver. Resetting position.");
                    playerPos.position = recieverOutside.position;
                }
                // inTunnel = true;
                playerisOverlapping = false;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerisOverlapping = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerisOverlapping = false;
        }
    }  
}
