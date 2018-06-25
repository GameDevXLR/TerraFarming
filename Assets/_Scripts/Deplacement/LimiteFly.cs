using UnityEngine;

public class LimiteFly : MonoBehaviour
{
    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        InGameManager.instance.playerController.InFlyingZone = false;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        InGameManager.instance.playerController.InFlyingZone = true;
    //    }
    //}

    private float limiteNord;
    private float limiteSud;
    private float limiteEst;
    private float limiteOuest;

    public GameObject WardNO;
    public GameObject WardSE;

    public float LimiteOuest
    {
        get
        {
            return WardNO.transform.position.x;
        }
    }

    public float LimiteEst
    {
        get
        {
            return WardSE.transform.position.x;
        }
    }

    public float LimiteSud
    {
        get
        {
            return WardSE.transform.position.z;
        }
    }

    public float LimiteNord
    {
        get
        {
            return WardNO.transform.position.z;
        }
    }
}