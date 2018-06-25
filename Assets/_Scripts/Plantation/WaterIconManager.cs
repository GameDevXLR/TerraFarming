using UnityEngine;

public class WaterIconManager : MonoBehaviour
{
    public void activate(PlantTypeEnum type, PlantStateEnum state)
    {
        if (type == PlantTypeEnum.bush && (state == PlantStateEnum.teenage || state == PlantStateEnum.grownup))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.80f);
        }
        else if (type == PlantTypeEnum.tree)
        {
            if (state == PlantStateEnum.baby || state == PlantStateEnum.teenage)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1.1f);
            }
            else if (state == PlantStateEnum.grownup)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 5.2f);
            }
        }
        gameObject.SetActive(true);
    }
}