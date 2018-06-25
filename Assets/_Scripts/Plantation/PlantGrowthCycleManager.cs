using System.Collections;
using UnityEngine;

public class PlantGrowthCycleManager : MonoBehaviour
{
    //est activé sur le plant spot une fois que ya une plante de planté xD.

    private bool isWatered;
    private float wateredTime;

    private PlantationSpotEnhanced plantSpot;

    // Use this for initialization
    private void Start()
    {
        plantSpot = GetComponent<PlantationSpotEnhanced>();
    }

    public IEnumerator StartGrowing()
    {
        wateredTime = 150;
        isWatered = true;
        while (isWatered)
        {
            wateredTime--;
            yield return new WaitForSecondsRealtime(1f);
            if (wateredTime <= 0)
            {
                isWatered = false;
                plantSpot.RecquireWater();
                //dire a plantspot qu'il faut de l'eau!
            }
        }
    }
}