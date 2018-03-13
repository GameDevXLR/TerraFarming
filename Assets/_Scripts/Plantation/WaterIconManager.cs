using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIconManager : MonoBehaviour {

    public void activate(PlantationSpot.PlantType type, PlantationSpot.PlantState state)
    {
        if(type == PlantationSpot.PlantType.bush && (state == PlantationSpot.PlantState.teenage || state == PlantationSpot.PlantState.grownup))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.80f);
        }
        else if(type == PlantationSpot.PlantType.tree)
        {
            if(state == PlantationSpot.PlantState.baby || state == PlantationSpot.PlantState.teenage)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1.1f);
            }
            else if (state == PlantationSpot.PlantState.grownup)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 5.2f);
            }
        }
        gameObject.SetActive(true);
    }
}
