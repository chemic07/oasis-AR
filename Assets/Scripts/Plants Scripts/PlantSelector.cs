using UnityEngine;
using UnityEngine.Video;

public class PlantSelector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject plantInfoCanvas;
    [SerializeField] private PlantInfoUI plantInfoUI;

    void Start()
    {
        plantInfoCanvas.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    ShowPlantInfo(plant.plantData);
                }
            }
        }
    }

    private void ShowPlantInfo(PlantData plantData)
    {
        plantInfoCanvas.gameObject.SetActive(true);
        plantInfoUI.UpdateUI(plantData);
    }
}
