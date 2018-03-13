using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantationManager : MonoBehaviour {

    public List<PlantationSpot> plantationList;
    

    public static PlantationManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public List<PlanteSave> savePlantation()
    {
        List<PlanteSave> planteSave = new List<PlanteSave>();
        for (int i = 0; i < plantationList.Count; i++)
        {
            PlanteSave save = new PlanteSave { index = i, plantType = plantationList[i].plantType, plantState = plantationList[i].actualPlantState };
            planteSave.Add(save);
        }
        return planteSave;
    }


    public void loadPlantation(List<PlanteSave> planteSave)
    {
        foreach(PlanteSave save in planteSave)
        {
            
            plantationList[save.index].loadPlantState(save.plantState);
            if (save.plantType != PlantationSpot.PlantType.none)
            {

                plantationList[save.index].SelectPlantType(save.plantType);
                plantationList[save.index].RecquireWater();
            }

        }
    }
}
