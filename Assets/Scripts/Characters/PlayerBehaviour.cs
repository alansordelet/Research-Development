using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using Cinemachine;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] CinemachineVirtualCamera MainCamera;
    [SerializeField] GameObject Priest;
    [SerializeField] GameObject Flashlight;
    [SerializeField] CharacterController Ccontroller;
    public float speed = 10f;
    private float rotationX;
    private Vector2 dirLook;
    public bool priestIsInCameraView;

    private Rigidbody hitRigidbody;

    private Vector3 targetPosition;
    float baseDistance;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
        MainCamera.transform.rotation = Quaternion.identity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Ccontroller = GetComponent<CharacterController>();  
        CamRotation();
        //InteractionWithObjects();
    }

    private void CamRotation()
    {
        dirLook.x = Input.GetAxisRaw("Mouse X");
        dirLook.y = Input.GetAxisRaw("Mouse Y");

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculez la direction du mouvement en fonction de la caméra.
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Appliquez la vélocité au Character Controller.
        Ccontroller.SimpleMove(moveDirection * speed);

        rotationX -= dirLook.y * 10f;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        MainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        transform.Rotate(0f, dirLook.x * 10f, 0f);
    }

    private void InteractionWithObjects()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(new Ray(MainCamera.transform.position, MainCamera.transform.forward), out RaycastHit hit, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Player"))))
                {
                    hitRigidbody = hit.rigidbody;
                    if (hitRigidbody != null)
                    {
                        hitRigidbody.useGravity = false;
                        baseDistance = Vector3.Distance(MainCamera.transform.position, hitRigidbody.transform.position);
                    }
                }
            }
            if (hitRigidbody != null)
            {
                targetPosition = MainCamera.transform.forward * baseDistance + MainCamera.transform.position;
                Vector3 objectToTarget = targetPosition - hitRigidbody.transform.position;
                Vector3 objectToTargetNormalized = objectToTarget.normalized;
                hitRigidbody.velocity = objectToTarget * 10f;
                baseDistance = Mathf.Clamp(baseDistance, 1f, 100f);
                if (Input.GetMouseButtonDown(2))
                {
                    hitRigidbody.AddForce(MainCamera.transform.forward * 25f, ForceMode.Impulse);
                    hitRigidbody.useGravity = true;
                    hitRigidbody = null;
                }

            }
        }
    }


}
