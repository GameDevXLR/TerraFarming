using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OreToEssenceUI : MonoBehaviour {

    [Header("Text display")]
    [SerializeField]
    public Text oreDisplay;
    public Text oreNeddDisplay;
    public Text EssenceGot;
    public Canvas canvas;
    public Text chrono;
    public Text timeBonus;
    public Text score;
    [SerializeField]
    public GameObject jaugePanel;
    public GameObject alertPanel;
    public GameObject gamePanel;

    public bool isActive = false;

	public void activate (int rawOre, int oreNeed, int essenceGot) {
        oreDisplay.text = rawOre.ToString();
        EssenceGot.text = essenceGot.ToString();
        canvas.gameObject.SetActive(true);
        isActive = true;
	}
    public void unactivate()
    {
        canvas.gameObject.SetActive(false);
        isActive = false;
    }

    public void setScore(int score)
    {
        this.score.text = score.ToString();
    }

    public void setChrono(int timer)
    {
        chrono.text = timer.ToString();
    }

    public void setChrono(float timer)
    {
        chrono.text = timer.ToString("F2");
    }

    public void setTimeBonus(float timer)
    {
        timeBonus.text ="/ " + timer.ToString("F2");
    }

}
