using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PlanteSave
{
    public int index;
    public PlantationSpot.PlantType plantType;
    public PlantationSpot.PlantState plantState;
}

[System.Serializable]
public class Save  {
    public int ore = 0;
    public int essence = 0;
    public int flowerSeed = 0;
    public int bushSeed = 0;
    public int treeSeed = 0;

    public List<PlanteSave> plantList;
}
