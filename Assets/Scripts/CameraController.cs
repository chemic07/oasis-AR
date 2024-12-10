using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // The object to rotate around
    public float rotationSpeed = 5f; // Speed of rotation
    public float zoomSpeed = 5f; // Speed of zooming
    public float maxZoomDistance = 10f; // Maximum zoom out distance
    public float minZoomDistance = 2f; // Minimum zoom in distance
    public float maxXRotation = 80f; // Maximum rotation around X-axis
    public float minXRotation = 10f; // Minimum rotation around X-axis

    // Sensitivity fields
    public float rotationSensitivity = 1f;
    public float movementSensitivity = 1f;

    private float currentDistance;
    private Vector3 lastMousePosition;

    public Transform GetTarget()
    {
        return target;
    }
    void Start()
    {
        currentDistance = Vector3.Distance(transform.position, target.position);
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Check if the pointer is over a UI element for both touch and mouse inputs
        if (EventSystem.current.IsPointerOverGameObject()) // for mouse
            return;

        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) // for touch
            return;

        // Rotation
        if (Input.GetMouseButton(0))
        {
            if (Input.touchCount == 1)
            {
                TouchRotateCamera();
            }
            else
            {
                RotateCamera();
            }
        }

        // Zooming
        if (Input.touchCount == 2)
        {
            ZoomCamera();
        }
        else
        {
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(zoomInput) > 0)
            {
                ZoomCamera(zoomInput);
            }
        }

        // Selecting
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SelectObject();
        }
    }


    void RotateCamera()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        float rotationX = mouseDelta.x * rotationSpeed * rotationSensitivity * Time.deltaTime;
        float rotationY = mouseDelta.y * rotationSpeed * rotationSensitivity * Time.deltaTime;


        transform.RotateAround(target.position, Vector3.up, rotationX);


        Quaternion newRotation = transform.rotation * Quaternion.Euler(-rotationY, 0, 0);
        Vector3 newPosition = target.position - (newRotation * Vector3.forward * currentDistance);


        float xRotation = newRotation.eulerAngles.x;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);


        transform.rotation = Quaternion.Euler(xRotation, newRotation.eulerAngles.y, 0);
        transform.position = newPosition;

        lastMousePosition = Input.mousePosition;
    }

    void TouchRotateCamera()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float rotationX = touchDeltaPosition.x * rotationSpeed * rotationSensitivity * Time.deltaTime;
            float rotationY = touchDeltaPosition.y * rotationSpeed * rotationSensitivity * Time.deltaTime;


            transform.RotateAround(target.position, Vector3.up, rotationX);


            Quaternion newRotation = transform.rotation * Quaternion.Euler(-rotationY, 0, 0);
            Vector3 newPosition = target.position - (newRotation * Vector3.forward * currentDistance);


            float xRotation = newRotation.eulerAngles.x;
            xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);


            transform.rotation = Quaternion.Euler(xRotation, newRotation.eulerAngles.y, 0);
            transform.position = newPosition;
        }
    }

    void ZoomCamera()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;
        float zoomFactor = difference * zoomSpeed * Time.deltaTime;

        currentDistance -= zoomFactor;
        currentDistance = Mathf.Clamp(currentDistance, minZoomDistance, maxZoomDistance); // Adjust the min and max zoom limits

        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * currentDistance;
    }

    void ZoomCamera(float zoomInput)
    {
        currentDistance -= zoomInput * zoomSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, minZoomDistance, maxZoomDistance);

        Vector3 direction = (transform.position - target.position).normalized;
        transform.position = target.position + direction * currentDistance;
    }

    void SelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Debug.Log("Selected: " + hit.transform.name);
        }
    }
}
