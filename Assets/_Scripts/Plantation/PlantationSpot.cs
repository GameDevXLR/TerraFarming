using UnityEngine;
using UnityEngine.UI;

public class PlantationSpot : MonoBehaviour
{
    //sert a la sauvegarde! Doit etre configurer et être different de tout autre ID pour pas que ca plane xD nul...
    public int persistentID;

    public PlantObject plantSO;

    public AudioClip planterSnd;
    public AudioClip growUpSnd;
    public AudioSource plantAudioS;

    public cakeslice.Outline outliner;
    public GameObject debrisObj;
    public GameObject lopinNoSeedObj;
    public GameObject lopinSeedObj;
    public GameObject bush1Obj;
    public GameObject bush2Obj;
    public GameObject bush3Obj;
    public GameObject flower1Obj;
    public GameObject flower2Obj;
    public GameObject flower3Obj;
    public GameObject tree1Obj;
    public GameObject tree2Obj;
    public GameObject tree3Obj;

    public PlantTypeEnum plantType;
    public PlantStateEnum actualPlantState;

    private GameObject babyVisual;
    private GameObject teenageVisual;
    private GameObject grownupVisual;

    public GameObject plantTypeCanvas;
    private bool isPlantTypeMenuOpened;
    public int currentPlantTypeIndex = 1;

    public Image plantTypeSelectorImg;
    public Sprite flowerSelectorIcon;
    public Sprite bushSelectorIcon;
    public Sprite treeSelectorIcon;
    public Sprite leaveMenuIcon;

    public GameObject needWaterParticules;

    public PlantGrowthCycleManager plantGrowth;

    public bool giveEssence;
    public int nbrOfGivenEssence;
    public AudioClip recolteSnd;

    public float timeToGrow;
    private float growthStartTime;
    private float timeSpentGrowing;
    private bool isGrowing;
    public Animator growthAnimator;

    public WaterIconManager waterIcon;

    private void Start()
    {
        outliner.enabled = false;

        //		//juste pour voir si les SO marchent bien... : oui.
        //		if (plantSO) {
        //			GameObject GO = Instantiate (plantSO.babyModel);
        //			GO.transform.localScale = new Vector3 (plantSO.scale, plantSO.scale, plantSO.scale);
        //			GO.transform.parent = transform;
        //			GO.transform.localPosition = Vector3.zero;
        //		}
    }

    private void Update()
    {
        if (isGrowing)
        {
            if (Time.time > growthStartTime + timeToGrow)
            {
                //faire evoluer la plante
                ChangePlantState();
                growthStartTime = Time.time;
            }
        }

        if (isPlantTypeMenuOpened)
        {
            if (Input.GetKeyDown(CustomInputManager.instance.actionKey))
            {
                switch (currentPlantTypeIndex)
                {
                    case 1:
                        if (ResourcesManager.instance.bushSeed <= 0)
                        {
                            return;
                        }
                        break;

                    case 0:
                        if (ResourcesManager.instance.flowerSeed <= 0)
                        {
                            return;
                        }
                        break;

                    case 2:
                        if (ResourcesManager.instance.treeSeed <= 0)
                        {
                            return;
                        }
                        break;

                    case 3:
                        HidePlantTypeMenu();
                        return;

                    default:
                        break;
                }
                SelectPlantType(currentPlantTypeIndex);
                InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Plant", layer: -1, fixedTime: 2);
                plantAudioS.PlayOneShot(planterSnd);
                InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Play();
                HidePlantTypeMenu();
            }
            //faire défiler les graines:
            if (Input.GetKeyDown(CustomInputManager.instance.leftKey) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentPlantTypeIndex == 0)
                {
                    currentPlantTypeIndex = 4;
                }
                ChangePlantTypeIndex(true);
            }
            if (Input.GetKeyDown(CustomInputManager.instance.rightKey) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentPlantTypeIndex == 3)
                {
                    currentPlantTypeIndex = -1;
                }
                ChangePlantTypeIndex(false);
            }

            //annuler
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HidePlantTypeMenu();
            }
        }
    }

    public void ChangePlantTypeIndex(bool scrollLeft)
    {
        if (scrollLeft)
        {
            currentPlantTypeIndex--;
        }
        else
        {
            currentPlantTypeIndex++;
        }
        ActualizePlantTypeUI();
    }

    public void ActualizePlantTypeUI()
    {
        switch (currentPlantTypeIndex)
        {
            case 1:
                plantTypeSelectorImg.sprite = bushSelectorIcon;
                break;

            case 0:
                plantTypeSelectorImg.sprite = flowerSelectorIcon;
                break;

            case 2:
                plantTypeSelectorImg.sprite = treeSelectorIcon;
                break;

            case 3:
                plantTypeSelectorImg.sprite = leaveMenuIcon;
                break;

            default:
                Debug.Log("planage sur l'icone la!");
                break;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(CustomInputManager.instance.actionKey) && other.tag == "Player" && !isPlantTypeMenuOpened)
        {
            //si t'es pas encore une plante, fait ton taff normalement...
            if (plantType == PlantTypeEnum.none)
            {
                ChangePlantState();
            }
            else
            {
                if (giveEssence)
                {
                    InGameManager.instance.playerController.anim.SetBool(AnimeParameters.iscleaning.ToString(), true);
                    //InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Cleaning", layer: -1, fixedTime: 1);
                    giveEssence = false;
                    plantAudioS.PlayOneShot(growUpSnd);
                    InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Play();

                    ResourcesManager.instance.ChangeEssence(nbrOfGivenEssence);
                    return;
                }
                if (!isGrowing)
                {
                    WaterThePlant();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isGrowing || other.tag == "Player" && giveEssence)
        {
            ListenForAction();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopListeningForAction();
            InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Stop();
            InGameManager.instance.waterParticle.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void ListenForAction()
    {
        //faire les changements d'apparence
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        outliner.enabled = true;
    }

    private void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        outliner.enabled = false;
    }

    public void ShowPlantTypeMenu()
    {
        if (ResourcesManager.instance.haveSeed())
        {
            plantTypeCanvas.SetActive(true);
            isPlantTypeMenuOpened = true;
            InGameManager.instance.isPlanting = true;
            InGameManager.instance.playerController.disableMovement();
        }
    }

    public void HidePlantTypeMenu()
    {
        plantTypeCanvas.SetActive(false);
        isPlantTypeMenuOpened = false;
        InGameManager.instance.isPlanting = false;

        InGameManager.instance.playerController.enableMovement();
    }

    //faire pousser/choisir la plante etc...
    public void ChangePlantState()
    {
        switch (actualPlantState)
        {
            case PlantStateEnum.debris:
                actualPlantState = PlantStateEnum.lopin;
                ResourcesManager.instance.ChangeRawOre(Random.Range(1, 6));
                debrisObj.SetActive(false);
                //			lopinNoSeedObj.SetActive (true);
                InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Cleaning", layer: -1, fixedTime: 1);
                plantAudioS.PlayOneShot(planterSnd);
                InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Play();
                break;

            case PlantStateEnum.lopin:
                Invoke("ShowPlantTypeMenu", 0.1f);
                //			l'animation est à la sortie de menu graine line94
                break;

            case PlantStateEnum.seed:
                actualPlantState = PlantStateEnum.baby;
                //			lopinSeedObj.SetActive (false);
                babyVisual.SetActive(true);
                growthAnimator.SetBool("baby", true);

                //			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
                //			plantAudioS.PlayOneShot (growUpSnd);
                break;

            case PlantStateEnum.baby:
                actualPlantState = PlantStateEnum.teenage;
                babyVisual.SetActive(false);
                teenageVisual.SetActive(true);
                growthAnimator.SetBool("teenage", true);

                //			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
                //			plantAudioS.PlayOneShot (growUpSnd);
                break;

            case PlantStateEnum.teenage:
                actualPlantState = PlantStateEnum.grownup;
                teenageVisual.SetActive(false);
                grownupVisual.SetActive(true);
                growthAnimator.SetBool("grownup", true);
                growthAnimator.SetFloat("growthspeed", 100f);

                //			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
                //			plantAudioS.PlayOneShot (growUpSnd);
                break;

            case PlantStateEnum.grownup:
                if (plantType != PlantTypeEnum.flower)
                {
                    giveEssence = true;
                }
                break;

            default:

                break;
        }
    }

    //faire pousser/choisir la plante etc...
    public void loadPlantState(PlantStateEnum state)
    {
        actualPlantState = state;
        switch (actualPlantState)
        {
            case PlantStateEnum.debris:
                break;

            case PlantStateEnum.lopin:
                debrisObj.SetActive(false);
                break;

            case PlantStateEnum.seed:
                debrisObj.SetActive(false);
                babyVisual.SetActive(false);
                teenageVisual.SetActive(false);
                grownupVisual.SetActive(false);
                growthAnimator.SetBool("teenage", false);
                growthAnimator.SetBool("baby", false);
                growthAnimator.SetBool("grownup", false);
                break;

            case PlantStateEnum.baby:
                debrisObj.SetActive(false);
                babyVisual.SetActive(true);
                teenageVisual.SetActive(false);
                grownupVisual.SetActive(false);
                growthAnimator.SetBool("teenage", false);
                growthAnimator.SetBool("baby", true);
                growthAnimator.SetBool("grownup", false);
                break;

            case PlantStateEnum.teenage:
                debrisObj.SetActive(false);
                babyVisual.SetActive(false);
                teenageVisual.SetActive(true);
                grownupVisual.SetActive(false);
                growthAnimator.SetBool("teenage", true);
                growthAnimator.SetBool("baby", false);
                growthAnimator.SetBool("grownup", false);
                break;

            case PlantStateEnum.grownup:
                debrisObj.SetActive(false);
                babyVisual.SetActive(false);
                teenageVisual.SetActive(false);
                grownupVisual.SetActive(true);
                growthAnimator.SetBool("baby", false);
                growthAnimator.SetBool("grownup", true);
                growthAnimator.SetBool("teenage", false);

                break;

            default:

                break;
        }
    }

    // choisir la graine
    /// <summary>
    /// Selects the type of the plant.
    /// </summary>
    /// <param name="index">Index.</param>
    public void SelectPlantType(int index)
    {
        switch (index)
        {
            //t'es un bush
            case 1:
                ResourcesManager.instance.ChangeBushSeed(-1);
                timeToGrow = 120f;
                nbrOfGivenEssence = 1;
                growthAnimator.SetFloat("growthspeed", 8.3f);
                babyVisual = bush1Obj;
                teenageVisual = bush2Obj;
                grownupVisual = bush3Obj;
                plantType = PlantTypeEnum.bush;
                break;

            //t'es une fleur
            case 0:
                ResourcesManager.instance.ChangeFlowerSeed(-1);
                timeToGrow = 60f;
                growthAnimator.SetFloat("growthspeed", 16f);

                babyVisual = flower1Obj;
                teenageVisual = flower2Obj;
                grownupVisual = flower3Obj;
                plantType = PlantTypeEnum.flower;

                break;

            //t'es un arbre
            case 2:
                ResourcesManager.instance.ChangeTreeSeed(-1);
                timeToGrow = 300f;
                nbrOfGivenEssence = 3;

                growthAnimator.SetFloat("growthspeed", 3.3f);

                babyVisual = tree1Obj;
                teenageVisual = tree2Obj;
                grownupVisual = tree3Obj;
                plantType = PlantTypeEnum.tree;

                break;

            default:
                Debug.Log("t'es une plante inconnu mec!");
                break;
        }

        actualPlantState = PlantStateEnum.seed;
        //		lopinNoSeedObj.SetActive (false);
        lopinSeedObj.SetActive(true);
        growthStartTime = Time.time;
        RecquireWater();
    }// choisir la graine

     /// <summary>
     /// Selects the type of the plant.
     /// </summary>
     /// <param name="index">Index.</param>
    public void SelectPlantType(PlantTypeEnum index)
    {
        switch (index)
        {
            //t'es un bush
            case PlantTypeEnum.bush:
                timeToGrow = 120f;
                growthAnimator.SetFloat("growthspeed", 8.3f);
                babyVisual = bush1Obj;
                teenageVisual = bush2Obj;
                grownupVisual = bush3Obj;
                plantType = PlantTypeEnum.bush;
                break;

            //t'es une fleur
            case PlantTypeEnum.flower:
                timeToGrow = 60f;
                growthAnimator.SetFloat("growthspeed", 16f);

                babyVisual = flower1Obj;
                teenageVisual = flower2Obj;
                grownupVisual = flower3Obj;
                plantType = PlantTypeEnum.flower;

                break;

            //t'es un arbre
            case PlantTypeEnum.tree:
                timeToGrow = 300f;
                growthAnimator.SetFloat("growthspeed", 3.3f);

                babyVisual = tree1Obj;
                teenageVisual = tree2Obj;
                grownupVisual = tree3Obj;
                plantType = PlantTypeEnum.tree;

                break;

            default:
                Debug.Log("t'es une plante inconnu mec!");
                break;
        }

        actualPlantState = PlantStateEnum.seed;
        //		lopinNoSeedObj.SetActive (false);
        lopinSeedObj.SetActive(true);
        growthStartTime = Time.time;
        RecquireWater();
    }

    public void RecquireWater()
    {
        timeSpentGrowing = Time.time - growthStartTime;
        isGrowing = false;
        needWaterParticules.SetActive(true);
        waterIcon.activate(plantType, actualPlantState);
    }

    public void WaterThePlant()
    {
        growthStartTime = Time.time - timeSpentGrowing;
        isGrowing = true;
        needWaterParticules.SetActive(false);
        plantGrowth.StartCoroutine(plantGrowth.StartGrowing());

        //jouer ici les sons et anim lié au fait d'arroser:
        InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Water", layer: -1, fixedTime: 2);
        plantAudioS.PlayOneShot(growUpSnd);
        InGameManager.instance.waterParticle.GetComponent<ParticleSystem>().Play();
        waterIcon.gameObject.SetActive(false);
    }
}