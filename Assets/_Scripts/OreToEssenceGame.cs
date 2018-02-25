using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreToEssenceGame : MonoBehaviour {


    #region editor variables

    public OreToEssenceUI oreToEssenceUI;

    public float timeBonus;
    public List<Image> jaugeList;

    #endregion

    #region other variables

    int count;
    float time;
    #endregion



    #region mnobehaviour methods

    private void OnEnable()
    {
        count = 0;
        foreach(Image img in jaugeList)
        {
            img.enabled = false;
        }
        oreToEssenceUI.setChrono(0);
        oreToEssenceUI.setTimeBonus(timeBonus);
        time = Time.time;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(CustomInputManager.instance.actionKey))
        {
            if (count < jaugeList.Count)
            {
                jaugeList[count++].enabled = true;
                oreToEssenceUI.setScore(count);
            }
        }

        if(count < jaugeList.Count)
            oreToEssenceUI.setChrono(Time.time - time);
		
	}

#endregion
}
