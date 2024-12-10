using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FadeByDistanceFromCenter : MonoBehaviour
{
    public float fadeDistance = 5.0f;  // Distance over which the object fades
    private Material material;
    private Vector3 center;

    void Start()
    {
        // Get the material of the object
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("Renderer not found on the object.");
            return;
        }

        // Calculate the center of the object in world space
        center = renderer.bounds.center;
    }

    void Update()
    {
        // Calculate distance from the camera to the center of the object
        float distance = Vector3.Distance(Camera.main.transform.position, center);

        // Calculate alpha based on distance
        float alpha = Mathf.Clamp01(1.0f - (distance / fadeDistance));

        // Set the alpha of the material
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }
}
