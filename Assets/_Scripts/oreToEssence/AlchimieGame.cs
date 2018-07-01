using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchimieGame : MonoBehaviour
{
    #region editor variables

    //A compléter : le type de graine donnée.
    public PlantObject plantGiven;

    public OreToEssenceUI interfaceMachine;
    public MachineUIManager machineUI;

    public float timeBonus;
    public float luckPercent;

    public AudioClip miniGameSuccess;
    public ParticleSystem BurstPtc;
    public AudioSource sourceSound;
    public AudioClip miniGameFail;
    public Animator animator;

    public ressourceEnum inputRessource;
    public ressourceEnum outputRessource;
    public BiomeEnum outputBiome;

    public int ressourceNeed;

    public List<Image> jaugeList;

    [HideInInspector]
    public bool willPlayIntroVideo = false;

    #endregion editor variables

    #region other variables

    protected int count;
    protected int bonus;
    protected int harvest;
    protected float time;
    protected bool lucky;
    protected int ressourceDispo;

    private bool wonBonus;
    private int previousBonusCount;
    bool isCreating;
    #endregion other variables

    #region monobehaviour methods

    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        machineUI.InitializeTheUI(plantGiven);
        interfaceMachine.InitializeTheGameUI(plantGiven);
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
    protected void Update()
    {
        if (Input.GetKeyDown(CustomInputManager.instance.actionKey) && !isCreating)
        {
            isCreating = true;
        }
        if(isCreating)
        {
        if (count < jaugeList.Count)
            {
                machineUI.BlinkActionBarArrows();
                jaugeList[count].enabled = true;
                jaugeList[count].gameObject.GetComponent<AudioSource>().Play();
                animator.Play("MachineSeedSpace");

                particleEffect(10);
                interfaceMachine.setScore(++count);
            }
            else if (count == jaugeList.Count)
            {
                if (Time.time - time <= timeBonus && Random.value <= luckPercent)
                {
                    bonus++;
                    playASOund(miniGameSuccess);
                    harvestRessouce(true);
                }
                else
                {
                    harvestRessouce(false);
                }
                harvest++;

                if (bonus > previousBonusCount)
                {
                    previousBonusCount = bonus;
                    wonBonus = true;
                }
                machineUI.ShowRewardImg(wonBonus);
                wonBonus = false;
                count++;
            }
            else if (count == jaugeList.Count + 1)
            {
                isCreating = false;
                resetJauge();
                count = 0;
                time = Time.time;
                machineUI.bigTimerZone.enabled = true;
                machineUI.mediumTimerZone.enabled = true;
                machineUI.smallTimerZone.enabled = true;
                machineUI.timerIcon.enabled = true;
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

        if (count < jaugeList.Count)

            if (Time.time - time > 1.5f)
            {
                if (machineUI.bigTimerZone.enabled)
                {
                    machineUI.bigTimerZone.enabled = false;
                }
                if (Time.time - time > 2f)
                {
                    if (machineUI.mediumTimerZone.enabled)
                    {
                        machineUI.mediumTimerZone.enabled = false;
                    }
                    if (Time.time - time > 2.5f)
                    {
                        if (machineUI.smallTimerZone.enabled)
                        {
                            machineUI.smallTimerZone.enabled = false;
                            machineUI.timerIcon.enabled = false;
                        }
                    }
                }
            }

        interfaceMachine.setChrono(Time.time - time);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enabled = false;
        }
    }

    private void OnDisable()
    {
        //harvestRessouce();
        launchAnimation("GameEnabled", false);

        machineUI.ShowHideActionArrows();

        interfaceMachine.unactivate();
    }

    #endregion monobehaviour methods

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
        if (ressourceDispo >= ressourceNeed)
        {
            enabled = true;
            interfaceMachine.activate(ressourceDispo, ressourceNeed, SimuleSynthetize());
        }
        else
        {
            enabled = false;
            playASOund(miniGameFail);
        }
        return enabled;
    }

    public void playASOund(AudioClip sound)
    {
        sourceSound.PlayOneShot(sound);
    }

    protected void particleEffect(int emission)
    {
        if (BurstPtc)
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
            Debug.Log("l'animator n'est pas encore défini : " + gameObject.name + ", OreToEssenceGame");
        }
    }

    public virtual void harvestRessouce()
    {
        if (harvest > 0)
        {
            ResourcesManager.instance.setRessourceQuantity(plantGiven, harvest + bonus);
            ResourcesManager.instance.setRessourceQuantity(inputRessource, -harvest * ressourceNeed);
        }
        resetJauge();
    }

    public virtual void harvestRessouce(bool bonus)
    {
        if (bonus)
        {
            ResourcesManager.instance.setRessourceQuantity(plantGiven, 2);
        }
        else
        {
            ResourcesManager.instance.setRessourceQuantity(plantGiven, 1);
        }

        ResourcesManager.instance.setRessourceQuantity(inputRessource, -ressourceNeed);
        resetJauge();
        if (willPlayIntroVideo)
        {
            GameEventsManager.instance.StartIntroCinematicSeedCreated();
            willPlayIntroVideo = false;
            enabled = false;
        }
    }

    public int SimuleSynthetize()
    {
        return ressourceDispo / ressourceNeed;
    }

    #endregion other methods
}