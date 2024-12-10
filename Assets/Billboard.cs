using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    public float baseScale = 1f; // Adjust this value to control the base size of the text label

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Rotate the object to always face the camera
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y + 180, 0);
        }
    }
}
