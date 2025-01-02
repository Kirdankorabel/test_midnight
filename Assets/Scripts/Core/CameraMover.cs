using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Position Settings")]
    [SerializeField] private Vector3 minPosition = new Vector3(-10f, 5f, -10f);
    [SerializeField] private Vector3 maxPosition = new Vector3(10f, 15f, 10f);

    [Header("Rotation Settings")]
    [SerializeField] private Vector3 minRotation = new Vector3(10f, -45f, 0f);
    [SerializeField] private Vector3 maxRotation = new Vector3(80f, 45f, 0f);

    [Header("Zoom Settings")]
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private float zoomSpeed = 10f;

    [Header("Speed Settings")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float dragSpeed = 5f;

    private Vector3 currentRotation;
    private float currentZoom;
    private Vector3 lastMousePosition;

    void Start()
    {
        currentRotation = transform.eulerAngles;
        currentZoom = transform.position.y;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HandleMovement();
            HandleRotation();
            HandleZoom();
            HandleDrag();
        }
    }

    private void HandleMovement()
    {
        float moveForward = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float moveSide = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        Vector3 moveDirection = transform.forward * moveForward + transform.right * moveSide;

        Vector3 newPosition = transform.position + moveDirection;

        newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);
        newPosition.z = Mathf.Clamp(newPosition.z, minPosition.z, maxPosition.z);

        transform.position = newPosition;
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationY = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationX = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            currentRotation.y += rotationY;
            currentRotation.x += rotationX;

            currentRotation.x = Mathf.Clamp(currentRotation.x, minRotation.x, maxRotation.x);
            currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation.y, maxRotation.y);

            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Vector3 position = transform.position;
        position.y = currentZoom;
        transform.position = position;
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButton(0)) 
        {
            if (lastMousePosition != Vector3.zero)
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;
                Vector3 move = new Vector3(-delta.x, 0, -delta.y) * dragSpeed * Time.deltaTime;

                move = transform.right * move.x + transform.forward * move.z;

                Vector3 newPosition = transform.position + move;

                newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);
                newPosition.z = Mathf.Clamp(newPosition.z, minPosition.z, maxPosition.z);

                transform.position = newPosition;
            }

            lastMousePosition = Input.mousePosition;
        }
        else
        {
            lastMousePosition = Vector3.zero;
        }
    }
}
