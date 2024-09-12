using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float directionX, directionY;
    private float rotationX;
    [SerializeField] private Camera mainCam;
    //[SerializeField] private CameraDistortion cameraDistortion; // Reference to the CameraDistortion script

    private Vector3 currentVelocity;
    private float acceleration = 100f; 
    private float maxSpeed = 6f; 

    public float jumpForce = 8f;
 

    void Update()
    {
        directionX = Input.GetAxisRaw("Mouse X");
        directionY = Input.GetAxisRaw("Mouse Y");

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        // Calculate target movement direction
        Vector3 targetMovement = horizontalMovement * transform.right + verticalMovement * transform.forward;

        // Accelerate towards the target movement
        currentVelocity = Vector3.MoveTowards(currentVelocity, targetMovement * maxSpeed, acceleration * Time.deltaTime);

        // Apply the movement
        transform.Translate(targetMovement * Time.deltaTime * maxSpeed, Space.World);

        // Camera rotation logic remains the same
        rotationX -= directionY * 10f;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        mainCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(0f, directionX * 10f, 0f);


       
      
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        //cameraDistortion.UpdateDistortion(currentVelocity.magnitude);
    }
}
