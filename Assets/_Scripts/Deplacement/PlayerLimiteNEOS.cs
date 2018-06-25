using UnityEngine;

public class PlayerLimiteNEOS : MonoBehaviour
{
    public LimiteFly limiteFly;

    private void Update()
    {
        Vector3 pos = transform.position;
        if (transform.position.x > limiteFly.LimiteEst)
        {
            pos.x = limiteFly.LimiteOuest;
        }
        else if (transform.position.x < limiteFly.LimiteOuest)
        {
            pos.x = limiteFly.LimiteEst;
        }
        if (transform.position.z > limiteFly.LimiteNord)
        {
            pos.z = limiteFly.LimiteSud;
        }
        else if (transform.position.z < limiteFly.LimiteSud)
        {
            pos.z = limiteFly.LimiteNord;
        }

        if (transform.position != pos)
        {
            transform.position = pos;
            CameraController.instance.moveCam();
        }
    }
}