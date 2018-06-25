using PhaethonGames.TerraFarming.OreToEssence;
using UnityEngine;
using UnityEngine.UI;

public class OreToEssenceUI : MonoBehaviour
{
    public TextDisplay textDisplay;
    public PanelList panelList;
    public AlertMessages messages;
    public Text plantName;
    public Text plantBiome;
    public Text plantBiomeDesc;

    public string plainBiomeDescription;
    public string craterBiomeDescription;
    public string caveBiomeDescription;

    public Canvas canvas;

    public bool isActive = false;

    public void InitializeTheGameUI(PlantObject createdPlant)
    {
        plantName.text = createdPlant.plantName;
        plantBiome.text = createdPlant.biome1.ToString();
        switch (createdPlant.biome1)
        {
            case BiomeEnum.plain:
                plantBiomeDesc.text = plainBiomeDescription;
                break;

            case BiomeEnum.crater:
                plantBiomeDesc.text = craterBiomeDescription;

                break;

            case BiomeEnum.cave:
                plantBiomeDesc.text = caveBiomeDescription;

                break;

            default:
                break;
        }
    }

    public void activate(int rawOre, int oreNeed, int essenceGot)
    {
        textDisplay.oreDisplay.text = rawOre.ToString();
        textDisplay.oreNeddDisplay.text = oreNeed.ToString();
        textDisplay.EssenceGot.text = essenceGot.ToString();
        canvas.gameObject.SetActive(true);
        isActive = true;
        if (rawOre >= oreNeed)
        {
            panelList.jaugePanel.SetActive(true);
        }
        else
        {
            panelList.jaugePanel.SetActive(false);
            textDisplay.alertDisplay.text = messages.warning;
            panelList.alertPanel.SetActive(true);
        }
    }

    public void unactivate()
    {
        canvas.gameObject.SetActive(false);
        isActive = false;
        panelList.jaugePanel.SetActive(false);
        panelList.alertPanel.SetActive(false);
    }

    public void setScore(int score)
    {
        textDisplay.score.text = score.ToString();
    }

    public void setChrono(int timer)
    {
        textDisplay.chrono.text = timer.ToString();
    }

    public void setChrono(float timer)
    {
        float tmpTime;
        tmpTime = 2.5f - timer;
        if (tmpTime < 0)
        {
            tmpTime = 0;
        }
        textDisplay.chrono.text = tmpTime.ToString("F2");
    }

    public void setTimeBonus(float timer)
    {
        textDisplay.timeBonus.text = "/ " + timer.ToString("F2");
    }

    public void synthetisationSucessFull(int nbrSynthSucessfull, int bonus = 0)
    {
        textDisplay.alertDisplay.text = messages.success + " : " + nbrSynthSucessfull.ToString() + " (" + bonus + " bonus)";
        panelList.alertPanel.SetActive(true);
    }

    public void endSynthetisation(int nbrSynthSucessfull, int bonus = 0)
    {
        textDisplay.alertDisplay.text = messages.end + " : " + nbrSynthSucessfull.ToString() + " (" + bonus + " bonus)";
        panelList.alertPanel.SetActive(true);
    }
}