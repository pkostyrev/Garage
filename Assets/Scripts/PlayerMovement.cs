using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed = 2.0f;
    [SerializeField] private float _fastMovementSpeed = 2.0f;
    [SerializeField] private float _mouseSensitivity = 100f;

    private float _xRotation = 0f;

    public float PlayerSpeed
    {
        get
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return _fastMovementSpeed;
            }
            else return _movementSpeed;
        }
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (mouseX != 0)
        {
            _characterController.transform.Rotate(Vector3.up * mouseX * _mouseSensitivity * Time.deltaTime);
        }

        if (mouseY != 0)
        {
            _xRotation -= (mouseY * _mouseSensitivity * Time.deltaTime);
            _xRotation = Mathf.Clamp(_xRotation, -70f, 70f);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        if (moveX != 0 || moveZ != 00)
        {
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            _characterController.Move(move * Time.deltaTime * PlayerSpeed);
        }
    }

}
