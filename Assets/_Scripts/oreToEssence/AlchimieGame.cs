using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchimieGame : MonoBehaviour {


    #region editor variables

    public OreToEssenceUI interfaceMachine;

    public float timeBonus;
    public float luckPercent;

    public AudioClip miniGameSuccess;
    public ParticleSystem BurstPtc;
    public AudioSource sourceSound;
    public AudioClip miniGameFail;
    public Animator animator;

    public ressourceEnum inputRessource;
    public ressourceEnum outputRessource;


    public int ressourceNeed;

    public List<Image> jaugeList;

    #endregion

    #region other variables

    protected int count;
    protected int bonus;
    protected int harvest;
    protected float time;
    protected bool lucky;
    protected int ressourceDispo;

    #endregion



    #region monobehaviour methods


    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        time = Time.time;
        count = 0;
        bonus = 0;
        harvest = 0;
        ressourceDispo = ResourcesManager.instance.GetRessourceQuantity(inputRessource);
        resetJauge();

        interfaceMachine.setChrono(0);
        interfaceMachine.setTimeBonus(timeBonus);
        launchAnimation("GameEnabled", true);

        particleEffect(10);
    }

    // Update is called once per frame
    protected void Update () {

        if (Input.GetKeyDown(CustomInputManager.instance.actionKey) )
        {
            if (count < jaugeList.Count)
            {
                jaugeList[count].enabled = true;
				jaugeList [count].gameObject.GetComponent<AudioSource> ().Play ();
				animator.Play ("MachineSeedSpace");

                particleEffect(10);
                interfaceMachine.setScore(++count);
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
					playASOund(miniGameSuccess);
                }
                harvest++;

                if ((harvest * ressourceNeed) + ressourceNeed <= ressourceDispo)
                {
                    interfaceMachine.synthetisationSucessFull(harvest + bonus, bonus);
                }
                else
                {
                    interfaceMachine.endSynthetisation(harvest + bonus, bonus);
                    enabled = false;
                }
            }
        }

        if(count < jaugeList.Count)
            interfaceMachine.setChrono(Time.time - time);
	}

    private void OnDisable()
    {
        harvestRessouce();
		launchAnimation("GameEnabled", false);
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

    public void activate(int need)
    {
        ressourceNeed = need;
        enabled = true;
    }

    public bool activate()
    {
        ressourceDispo = ResourcesManager.instance.GetRessourceQuantity(inputRessource);
        if(ressourceDispo >= ressourceNeed)
        {
            enabled = true;
            
        }
        else
        {
            playASOund(miniGameFail);
        }
        interfaceMachine.activate(ressourceDispo, ressourceNeed, SimuleSynthetize());
        return enabled;
    }

    public void playASOund(AudioClip sound)
    {
        sourceSound.PlayOneShot(miniGameSuccess);
    }

    protected void particleEffect(int emission)
    {
        if(BurstPtc)
            BurstPtc.GetComponent<ParticleSystem>().Emit(emission);
        else
        {
            Debug.Log("Particle pas encore défini : " + gameObject.name + ", OreToEssenceGame");
        }
    } 

    protected void launchAnimation(string animation, bool etat)
    {
        if (animator)
        {

            animator.SetBool(animation, etat);
        }
        else
        {
            Debug.Log("l'animator n'est pas encore défini : " + gameObject.name + ", OreToEssenceGame" );
        }
    }

    public virtual void harvestRessouce()
    {
        if(harvest > 0)
        {
            ResourcesManager.instance.setRessourceQuantity(outputRessource, harvest + bonus);
            ResourcesManager.instance.setRessourceQuantity(inputRessource, -harvest * ressourceNeed);
        }
    }

    public int SimuleSynthetize()
    {
        return ressourceDispo / ressourceNeed;
    }
    #endregion
}
