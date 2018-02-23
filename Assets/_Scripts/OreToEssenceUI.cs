using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OreToEssenceUI : MonoBehaviour {

    public Text oreDisplay;
    public Text EssenceGot;
    public Canvas canvas;

    public bool isActive = false;

	public void activate () {
        oreDisplay.text = ResourcesManager.instance.rawOre.ToString();
        EssenceGot.text = ResourcesManager.instance.SimuleSynthtizeEssence().ToString();
        canvas.gameObject.SetActive(true);
        isActive = true;
	}
    public void unacticate()
    {
        canvas.gameObject.SetActive(false);
        isActive = false;
    }
	
}
