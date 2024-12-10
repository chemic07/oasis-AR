using UnityEngine;

public class ScaleWithCameraDistance : MonoBehaviour
{
    private Transform cameraTarget; // The target of the camera
    private Camera mainCamera;
    public float baseScale = 1f; // Adjust this value to control the base size of the object

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.GetComponent<CameraController>() != null)
        {
            cameraTarget = mainCamera.GetComponent<CameraController>().GetTarget();
        }
    }

    void Update()
    {
        if (mainCamera != null && cameraTarget != null)
        {
            // Calculate the distance between the camera and the camera target
            float distanceToTarget = Vector3.Distance(mainCamera.transform.position, cameraTarget.position);
            Debug.Log("Distance to camera's target: " + distanceToTarget);

            // Calculate the scale factor based on distance
            float scaleFactor = baseScale * distanceToTarget;

            // Apply the scale
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }
}
