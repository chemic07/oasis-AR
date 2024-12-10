using UnityEngine;

namespace cakeslice
{
    public class Toggle : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // Check if the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the click hits this object
                if (IsClickingObject(Input.mousePosition))
                {
                    // Toggle the outline's enabled state
                    ToggleOutline();
                }
            }
        }

        // Method to check if the mouse click hits this object or its children
        bool IsClickingObject(Vector2 clickPosition)
        {
            // Cast a ray from the click position
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit hit;

            // Check if the ray hits this object or any of its children
            bool isHit = Physics.Raycast(ray, out hit);
            Debug.Log("Raycast hit: " + isHit);
            if (isHit)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                Debug.Log("This object: " + gameObject.name);
            }

            return isHit && (hit.collider.gameObject == gameObject || hit.collider.transform.IsChildOf(transform));
        }

        // Method to toggle the outline's enabled state for this object and its children
        void ToggleOutline()
        {
            // Get all children of this object
            Transform[] children = GetComponentsInChildren<Transform>(true);

            // Toggle the outline's enabled state for each child
            foreach (Transform child in children)
            {
                Outline outline = child.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = !outline.enabled;
                }
            }
        }
    }
}
