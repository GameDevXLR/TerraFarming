using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreToEssenceGame : MonoBehaviour {


    #region editor variables

    public OreToEssenceUI oreToEssenceUI;

    public float timeBonus;
    public float luckPercent;
    public List<Image> jaugeList;
	public AudioClip miniGameSuccess;
	public ParticleSystem BurstPtc;

    #endregion

    #region other variables

    int count;
    int bonus;
    int harvest;
    float time;
    bool lucky;
    int oreDispo;
    int oreNeed;
   
    #endregion



    #region monobehaviour methods

    private void OnEnable()
    {
        time = Time.time;
        count = 0;
        bonus = 0;
        harvest = 0;
        oreDispo = ResourcesManager.instance.rawOre;
        resetJauge();

        oreToEssenceUI.setChrono(0);
        oreToEssenceUI.setTimeBonus(timeBonus);
		InGameManager.instance.machineAnimator.GetComponent<Animator> ().SetBool ("GameEnabled", true);
		BurstPtc.GetComponent<ParticleSystem> ().Emit(0);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(CustomInputManager.instance.actionKey) )
        {
            if (count < jaugeList.Count)
            {
                jaugeList[count].enabled = true;
				jaugeList [count].gameObject.GetComponent<AudioSource> ().Play ();
				BurstPtc.GetComponent<ParticleSystem> ().Emit(10);
                oreToEssenceUI.setScore(++count);
            }
            else if (count == jaugeList.Count)
            {
                
                resetJauge();
                count = 0;
                time = Time.time;

            }

            if(count == jaugeList.Count)
            {
                if (Time.time - time <= timeBonus && Random.value <= luckPercent)
                {
                    bonus++;
					GetComponent<AudioSource> ().PlayOneShot (miniGameSuccess);
                }
                harvest++;

                if ((harvest * oreNeed) + oreNeed <= oreDispo)
                {
                    oreToEssenceUI.synthetisationSucessFull(harvest + bonus, bonus);
                }
                else
                {
                    oreToEssenceUI.endSynthetisation(harvest + bonus, bonus);
                    enabled = false;
                }

            }

        }

        if(count < jaugeList.Count)
            oreToEssenceUI.setChrono(Time.time - time);
        
		
	}

    private void OnDisable()
    {
        ResourcesManager.instance.ChangeEssence(harvest + bonus);
        ResourcesManager.instance.ChangeRawOre(-harvest * oreNeed);
		InGameManager.instance.machineAnimator.GetComponent<Animator> ().SetBool ("GameEnabled", false);


    }

    #endregion
#region other methods

    public void resetJauge()
    {

        foreach (Image img in jaugeList)
        {
            img.enabled = false;
        }
    }
    
    public void activate(int oreNeed)
    {
        this.oreNeed = oreNeed;
        enabled = true;
    }
#endregion
}
