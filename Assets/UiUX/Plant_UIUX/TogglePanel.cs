using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    private static TogglePanel currentlyVisiblePanel; // Track currently visible panel
    public GameObject[] objectsToToggle; // Array of objects to toggle visibility

    void Start()
    {
        // Deactivate all objects in objectsToToggle array when the scene starts
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }

        // Deactivate the panel itself initially
        gameObject.SetActive(false);
    }

    public void ToggleVisibility()
    {
        // Check if this panel is currently visible
        bool isVisible = gameObject.activeSelf;

        // Deactivate this panel if it's already visible
        if (isVisible)
        {
            gameObject.SetActive(false);
        }
        else // Activate this panel and deactivate any other visible panel
        {
            // Deactivate any other visible panel
            if (currentlyVisiblePanel != null && currentlyVisiblePanel != this)
            {
                currentlyVisiblePanel.HidePanel();
            }

            // Activate this panel
            gameObject.SetActive(true);
            currentlyVisiblePanel = this;
        }

        // Toggle visibility of objects in objectsToToggle array
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(!isVisible); // Toggle the visibility state based on the panel's previous state
        }
    }

    public void HidePanel()
    {
        // Deactivate this panel
        gameObject.SetActive(false);

        // Deactivate objects in objectsToToggle array
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }
    }
}

