using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScriptStaircase : MonoBehaviour
{
    public Transform playerPos;
    public Transform reciever;
    

    private bool playerisOverlapping = false;

 
    // Update is called once per frame
    void LateUpdate()
    {
        if (playerisOverlapping)
        {
            Vector3 portalToplayer = playerPos.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToplayer);

            if (dotProduct < 0f)
            {
                float rotationdiff = -Quaternion.Angle(transform.rotation, reciever.rotation);

              //  rotationdiff += 180;
               // playerPos.Rotate(Vector3.up, rotationdiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationdiff, 0f) * portalToplayer;
             
                playerPos.position = reciever.position + positionOffset;
                
                if (Vector3.Distance(playerPos.position, reciever.position) > 10f)
                {
                    Debug.Log("Player too far from receiver");
                    playerPos.position = reciever.position;
                }
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
