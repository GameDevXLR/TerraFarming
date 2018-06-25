using System.Collections.Generic;

[System.Serializable]
public struct PlanteSave
{
    public int index;
    public PlantTypeEnum plantType;
    public PlantStateEnum plantState;
}

[System.Serializable]
public class Save
{
    public int ore = 0;
    public int essence = 0;
    public int flowerSeed = 0;
    public int bushSeed = 0;
    public int treeSeed = 0;

    public List<PlanteSave> plantList;
}