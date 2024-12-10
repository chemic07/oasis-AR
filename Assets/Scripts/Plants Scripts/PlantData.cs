using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlantData", menuName = "Virtual Garden/Plant Data")]
public class PlantData : ScriptableObject
{
    public string PlantName;
    public string description;
    public Sprite plantSprite;
    public string medicalUse;
}
