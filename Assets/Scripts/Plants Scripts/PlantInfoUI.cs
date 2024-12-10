using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI plantNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image plantImage;
    [SerializeField] private TextMeshProUGUI medicinalUsesText;

    public void UpdateUI(PlantData plantData)
    {
        plantNameText.text = plantData.PlantName;
        descriptionText.text = plantData.description;
        plantImage.sprite = plantData.plantSprite;
        medicinalUsesText.text = plantData.medicalUse;
    }

    public void CutButton()
    {

        this.gameObject.SetActive(false);
    }
}
