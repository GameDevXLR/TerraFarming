using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlantationSpotEnhanced : MonoBehaviour
{
    //refonte du plantation spot pour qu'il se réfère plus aux scriptable object et pour différencier "plantation spot" et "plantes"...
    //C'était pas bien clair tout ca :/
    //voir aussi pour un nouveau systeme de menu des graines hein.

    #region les variables

    //sert a la sauvegarde! Doit etre configurer et être different de tout autre ID pour pas que ca plane xD nul...
    public int persistentID;

    //Le biome ou je me trouve:
    public BiomeEnum spotBiome;

    //la plante qui est planté ici. Peut etre vide hein.
    public PlantObject plantSO;

    //tout ce qui est son:
    public AudioClip planterSnd;

    public AudioClip growUpSnd;
    public AudioSource plantAudioS;

    //l'outliner
    public cakeslice.Outline outliner;

    //les visuels en enfant de l'objet.
    public GameObject debrisObj;

    public GameObject lopinNoSeedObj;
    public GameObject lopinSeedObj;

    //le type de plante
    public PlantTypeEnum plantType;

    //l'état actuel du spot (planter / vide ? )
    public PlantStateEnum actualPlantState;

    //les visuels de la plante a ses différentes étapes : hérité du SO.
    private GameObject babyVisual;

    private GameObject teenageVisual;
    private GameObject grownupVisual;

    //effet de particule en enfant qui s'active quand jneed de l'eau mec.
    public GameObject needWaterParticules;

    //gère ma croissance...Gère surtout l'arrosage pour le moment hein..Ne nous voilons pas la face XD
    public PlantGrowthCycleManager plantGrowth;

    public WaterIconManager waterIcon;

    private AudioSource audioS;
    public AudioClip waterSound;
    public AudioClip expansionSound;
    public AudioClip growthSound;

    public ParticleSystem activateParticleS;
    public ParticleSystem expansionParticleS;
    public ParticleSystem wateringAroundParticleS;
    public ParticleSystem speedGrowthParticleS;

    //est ce que je donne des essences si on fait espace sur moi ?
    public bool giveEssence;

    //combien d'essences je donne par récolte??
    public int nbrOfGivenEssence;

    //je fais quoi comme son qd on me récolte?
    public AudioClip recolteSnd;

    //gestion des timers de pousse.
    public float timeToGrow;

    [HideInInspector] public float growthStartTime; // a augmenter pour speedé le dév d'une plante.
    private float timeSpentGrowing;
    private bool isGrowing;

    //Le génome de la plante.
    public Genome genome;

    //est ce que je suis pret a avoir un bébé XD
    public bool canMakeSeed;

    //est ce que ce spot est dans le rayon d'action du joueur ? Dépend de si il a des plantes adultes a proximité mettre false quand il faudra
    public bool canBeUsed = true;

    //la croissance a t-elle était accéléré dans cette phase de croissance?
    public bool growthBoosted;

    public Animator growthAnimator;

    [Header("Gestion du voisinnage")]
    private
    //un array des voisins peuplé par un spherecast. Limite le nombre max de détections, ne fait pas de "garbage".
    Collider[] hits = new Collider[12];

    //une liste de nos voisins(gameobjects).
    public List<PlantationSpotEnhanced> neighboursSpot = new List<PlantationSpotEnhanced>();

    //la portée de detection des voisins.
    public float sphereRadius;

    //les layers avec lesquelles le spherecast interagit.
    public LayerMask sphereCastLayer;

    //Pour la quête:
    [HideInInspector]
    public bool willShowSeedPlantedCinematic = false;

    #endregion les variables

    #region monoBehaviour Stuff

    private void Start()
    {
        if (!canBeUsed)
        {
            outliner.enabled = false;
        }
        audioS = GetComponent<AudioSource>();
        //fait un cast pour référencer tous les plantationSpot a proximité.
        FindYourNeighbours();
        if (!PlantationManager.instance.plantationList.Contains(this))
        {
            PlantationManager.instance.plantationList.Add(this);
        }
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
                growthBoosted = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isGrowing || other.tag == "Player" && giveEssence)
        {
            //si t'as pas de débris et que t'es pas a portée ,arrete.
            if (actualPlantState != PlantStateEnum.debris && !canBeUsed)
            {
                return;
            }
            ListenForAction();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(CustomInputManager.instance.actionKey)
            && other.tag == "Player"
            && !PlantationManager.instance.isSeedMenuOpen
            && InGameManager.instance.playerController.canDoAction)
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
                    InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Cleaning", layer: -1, fixedTime: 1);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopListeningForAction();
            InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Stop();
            InGameManager.instance.waterParticle.GetComponent<ParticleSystem>().Stop();
            if (PlantationManager.instance.isSeedMenuOpen) PlantationManager.instance.HidePlantTypeMenu();
        }
    }

    #endregion monoBehaviour Stuff

    #region gestion du voisinage

    //permet de spherecast tous les plantations spots alentour.
    public void FindYourNeighbours()
    {
        neighboursSpot.Clear();
        int j = Physics.OverlapSphereNonAlloc(transform.position, sphereRadius, hits, sphereCastLayer);
        for (int i = 0; i < j; i++)
        {
            if (hits[i].transform != transform)
            {
                neighboursSpot.Add(hits[i].transform.gameObject.GetComponent<PlantationSpotEnhanced>());
            }
        }
    }

    //permet de trouver un arbre avec qui s'accoupler xD lol
    public void FindLover()
    {
        timeToGrow *= 1.5f;
        for (int i = 0; i < neighboursSpot.Count; i++)
        {
            //si le voisin contient une plante adulte
            if (neighboursSpot[i].actualPlantState == PlantStateEnum.grownup)
            {
                //et que cet adulte est du meme type que moi (fleur buisson arbre)
                if (neighboursSpot[i].plantType == plantType && neighboursSpot[i].canMakeSeed)
                {
                    //alors on peut baiser
                    neighboursSpot[i].canMakeSeed = false;
                    canMakeSeed = false;
                    //ca fait un petit
                    GameObject go = GameObject.Instantiate(PlantCollection.instance.genericSeed);
                    go.transform.position = transform.position + new Vector3(1, 3, 1);
                    go.GetComponent<DroppedSeed>().daddy = plantSO;
                    go.GetComponent<DroppedSeed>().mummy = neighboursSpot[i].plantSO;
                    go.GetComponent<DroppedSeed>().plantType = plantType;
                    return;
                    //					switch (plantType)
                    //					{
                    //					case PlantTypeEnum.flower:
                    //						go.GetComponent<DroppedSeed> ().plantType = ressourceEnum.flower;
                    //						break;
                    //					case PlantTypeEnum.bush:
                    //						go.GetComponent<DroppedSeed> ().plantType = ressourceEnum.bush;
                    //						break;
                    //					case PlantTypeEnum.tree:
                    //						go.GetComponent<DroppedSeed> ().plantType = ressourceEnum.tree;
                    //						break;
                    //					default:
                    //						break;
                    //					}
                    //					go.GetComponent<DroppedSeed> ().biome1 = spotBiome;
                }
            }
        }
    }

    #endregion gestion du voisinage

    #region capacités speciales des plantes

    //rendre utilisable les spots a proximité : se produit quand on passe a l'age adulte
    public void ActivateTheSurroundingSpots()
    {
        audioS.PlayOneShot(expansionSound);
        expansionParticleS.Play();
        for (int i = 0; i < neighboursSpot.Count; i++)
        {
            //si t'es adulte et qu'un de tes voisins est pas "utilisable par le joueur" et que t'as la propriété "dome" ben rend le utilisable ^^
            if (!neighboursSpot[i].canBeUsed)
            {
                neighboursSpot[i].canBeUsed = true;
                neighboursSpot[i].outliner.enabled = true;
                neighboursSpot[i].activateParticleS.Play();
            }
        }
    }

    //arrose les plantes environnante (mais pas toi, et ne marche que si t'es arroser.
    public void WaterTheSurroundingArea()
    {
        audioS.PlayOneShot(waterSound);

        wateringAroundParticleS.Play();
        for (int i = 0; i < neighboursSpot.Count; i++)
        {
            if (!neighboursSpot[i].isGrowing && neighboursSpot[i].actualPlantState != PlantStateEnum.debris && neighboursSpot[i].actualPlantState != PlantStateEnum.lopin)
            {
                neighboursSpot[i].AutoWaterThePlant();
            }
        }
    }

    //accelere la croissance des plantes environnantes : ne peut arriver qu'une fois par cycle pour une valeur temporel fixe de 10sec...
    public void BoostSurroundingGrowth()
    {
        audioS.PlayOneShot(growthSound);

        speedGrowthParticleS.Play();
        for (int i = 0; i < neighboursSpot.Count; i++)
        {
            if (!neighboursSpot[i].growthBoosted && neighboursSpot[i].actualPlantState != PlantStateEnum.debris && neighboursSpot[i].actualPlantState != PlantStateEnum.lopin)
            {
                //on change le début de la phase de croissance xD ca accelere la croissance.
                //peut arriver qu'une fois par cycle de croissance a une plante donnée.
                neighboursSpot[i].growthStartTime -= 20;
                neighboursSpot[i].growthBoosted = true;
                neighboursSpot[i].speedGrowthParticleS.Play();
            }
        }
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

    public void AutoWaterThePlant()
    {
        growthStartTime = Time.time - timeSpentGrowing;
        isGrowing = true;
        needWaterParticules.SetActive(false);
        plantGrowth.StartCoroutine(plantGrowth.StartGrowing());
        waterIcon.gameObject.SetActive(false);
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
                InGameManager.instance.cleanParticle.transform.position = transform.position;
                InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Play();
                break;

            case PlantStateEnum.lopin:
                if (!canBeUsed)
                {
                    return;
                }
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
                if (genome.isDome)
                {
                    ActivateTheSurroundingSpots();
                }
                //			InGameManager.instance.playerController.GetComponent<Animator> ().PlayInFixedTime("Plant", layer:-1, fixedTime:2);
                //			plantAudioS.PlayOneShot (growUpSnd);
                break;

            case PlantStateEnum.grownup:
                //si t'es pas une fleur tu donnes des essences.
                if (plantType == PlantTypeEnum.tree)
                {
                    timeToGrow += Random.Range(-1.5f, 1.5f);
                    giveEssence = true;
                }
                if (canMakeSeed && isGrowing)
                {
                    if (Random.Range(0, 2) == 0)
                        FindLover();
                }
                else
                {
                    canMakeSeed = true;
                }
                if (genome.isWateringAround)
                {
                    WaterTheSurroundingArea();
                }
                if (genome.isGlowing)
                {
                    BoostSurroundingGrowth();
                }
                break;

            default:

                break;
        }
    }

    //	// choisir la graine OLD
    //	/// <summary>
    //	/// Selects the type of the plant.
    //	/// </summary>
    //	/// <param name="index">Index.</param>
    //	public void SelectPlantType(int index)
    //	{
    //		switch (spotBiome)
    //		{
    //		case BiomeEnum.plain:
    //			switch (index)
    //			{
    //			//t'es une fleur
    //			case 0:
    //				ResourcesManager.instance.ChangeFlowerSeed (-1);
    //				plantSO = PlantCollection.instance.airFlower;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				//faut mettre un calcul pertinent pour le temps d'animation juste la :
    //				growthAnimator.SetFloat ("growthspeed", 16f);
    //				plantType = PlantTypeEnum.flower;
    //
    //				break;
    //
    //				//t'es un bush
    //			case 1:
    //				ResourcesManager.instance.ChangeBushSeed (-1);
    //				plantSO = PlantCollection.instance.airBush;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				nbrOfGivenEssence = 1;
    //				growthAnimator.SetFloat ("growthspeed", 8.3f);
    //				plantType = PlantTypeEnum.bush;
    //				break;
    //
    //
    //				//t'es un arbre
    //			case 2:
    //				ResourcesManager.instance.ChangeTreeSeed (-1);
    //				plantSO = PlantCollection.instance.airTree;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				nbrOfGivenEssence = 3;
    //				growthAnimator.SetFloat ("growthspeed", 3.3f);
    //				plantType = PlantTypeEnum.tree;
    //
    //				break;
    //
    //			default:
    //				Debug.Log ("t'es une plante inconnu mec!");
    //				break;
    //			}
    //			break;
    //		case BiomeEnum.crater:
    //			switch (index)
    //			{
    //			//t'es une fleur
    //			case 0:
    //				ResourcesManager.instance.ChangeFlowerSeed (-1);
    //				plantSO = PlantCollection.instance.craterFlower;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				//faut mettre un calcul pertinent pour le temps d'animation juste la :
    //				growthAnimator.SetFloat ("growthspeed", 16f);
    //				plantType = PlantTypeEnum.flower;
    //
    //				break;
    //
    //				//t'es un bush
    //			case 1:
    //				ResourcesManager.instance.ChangeBushSeed (-1);
    //				plantSO = PlantCollection.instance.craterBush;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				nbrOfGivenEssence = 1;
    //				growthAnimator.SetFloat ("growthspeed", 8.3f);
    //				plantType = PlantTypeEnum.bush;
    //				break;
    //
    //
    //				//t'es un arbre
    //			case 2:
    //				ResourcesManager.instance.ChangeTreeSeed (-1);
    //				plantSO = PlantCollection.instance.craterTree;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				nbrOfGivenEssence = 3;
    //				growthAnimator.SetFloat ("growthspeed", 3.3f);
    //				plantType = PlantTypeEnum.tree;
    //
    //				break;
    //
    //			default:
    //				Debug.Log ("t'es une plante inconnu mec!");
    //				break;
    //			}
    //			break;
    //		case BiomeEnum.cave:
    //			switch (index)
    //			{
    //			//t'es une fleur
    //			case 0:
    //				ResourcesManager.instance.ChangeFlowerSeed (-1);
    //				plantSO = PlantCollection.instance.caveFlower;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				//faut mettre un calcul pertinent pour le temps d'animation juste la :
    //				growthAnimator.SetFloat ("growthspeed", 16f);
    //				plantType = PlantTypeEnum.flower;
    //
    //				break;
    //
    //				//t'es un bush
    //			case 1:
    //				ResourcesManager.instance.ChangeBushSeed (-1);
    //				plantSO = PlantCollection.instance.caveBush;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				nbrOfGivenEssence = 1;
    //				growthAnimator.SetFloat ("growthspeed", 8.3f);
    //				plantType = PlantTypeEnum.bush;
    //				break;
    //
    //
    //				//t'es un arbre
    //			case 2:
    //				ResourcesManager.instance.ChangeTreeSeed (-1);
    //				plantSO = PlantCollection.instance.caveTree;
    //
    //				timeToGrow = plantSO.desiredGrowthTime;
    //				nbrOfGivenEssence = 3;
    //				growthAnimator.SetFloat ("growthspeed", 3.3f);
    //				plantType = PlantTypeEnum.tree;
    //
    //				break;
    //
    //			default:
    //				Debug.Log ("t'es une plante inconnu mec!");
    //				break;
    //			}
    //			break;
    //
    //		default:
    //			break;
    //		}
    //
    //		actualPlantState = PlantStateEnum.seed;
    //		Genome ge = gameObject.AddComponent<Genome> ();
    //		genome = ge;
    //
    //		//fonctionne que si t'es un "pure race a un biome"...Faudra voir ce qu'on fait pour les hybrides. je pense ajouter un parametre ou 2.
    //		genome.Initialize (plantSO,spotBiome);
    //		SpawnThenHidePlants ();
    //		lopinSeedObj.SetActive(true);
    //		growthStartTime = Time.time;
    //		RecquireWater();
    //	}

    //nouveau systeme de plantage de graine basé sur un PlantObj.
    //remplace la fonction au dessus.
    //est appelé principalement pour le moment par la nouvelle interface de plantage de graines et ses boutons (en cours de création)
    public void PlantSeedHere(PlantObject newPlant)
    {
        ResourcesManager.instance.setRessourceQuantity(newPlant, -1);
        plantSO = newPlant;
        plantType = plantSO.plantType;
        timeToGrow = plantSO.desiredGrowthTime;
        actualPlantState = PlantStateEnum.seed;
        lopinSeedObj.SetActive(true);
        growthStartTime = Time.time;
        RecquireWater();
        InGameManager.instance.playerController.GetComponent<Animator>().PlayInFixedTime("Plant", layer: -1, fixedTime: 2);
        plantAudioS.PlayOneShot(planterSnd);
        InGameManager.instance.cleanParticle.GetComponent<ParticleSystem>().Play();
        //le systeme suivant est provisoire. Il faut revoir le génome pour faire quelque chose de plus pertinent.
        //le genome ne servira de toute facon qu'une fois qu'on aura la pépiniere (ou le labo)
        Genome ge = gameObject.AddComponent<Genome>();
        genome = ge;
        genome.Initialize(plantSO, spotBiome);
        //faire ca plus tard depuis le génome.
        SpawnThenHidePlants();

        //les propriétés ci dessous mérite un travail de réflexion pour être revue.
        nbrOfGivenEssence = 3;
        growthAnimator.SetFloat("growthspeed", 3.3f);
        HidePlantTypeMenu();
        if (willShowSeedPlantedCinematic)
        {
            GameEventsManager.instance.StartIntroCinematicSeedPlanted();
            willShowSeedPlantedCinematic = false;
        }
    }

    private void SpawnThenHidePlants()
    {
        GameObject GObaby = Instantiate(plantSO.babyModel);
        GObaby.transform.localScale = new Vector3(plantSO.scale, plantSO.scale, plantSO.scale) * Random.Range(.7f, 1.3f);
        GObaby.transform.parent = transform;
        GObaby.transform.localPosition = Vector3.zero;
        babyVisual = GObaby;

        GameObject GOteen = Instantiate(plantSO.teenageModel);
        GOteen.transform.localScale = new Vector3(plantSO.scale, plantSO.scale, plantSO.scale) * Random.Range(.7f, 1.3f);
        GOteen.transform.parent = transform;
        GOteen.transform.localPosition = Vector3.zero;
        teenageVisual = GOteen;

        GameObject GOgrown = Instantiate(plantSO.grownupModel);
        GOgrown.transform.localScale = new Vector3(plantSO.scale, plantSO.scale, plantSO.scale) * Random.Range(.7f, 1.3f);
        GOgrown.transform.parent = transform;
        GOgrown.transform.localPosition = Vector3.zero;
        grownupVisual = GOgrown;

        babyVisual.SetActive(false);
        teenageVisual.SetActive(false);
        grownupVisual.SetActive(false);
    }

    #endregion capacités speciales des plantes

    #region gestion des retours utilisateurs (interface)

    private void ListenForAction()
    {
        //faire les changements d'apparence
        CustomInputManager.instance.ShowHideActionButtonVisual(true);
        outliner.enabled = true;
        // stock le plantationSpot actuel
        PlantationManager.instance.plantationSpot = this;
    }

    private void StopListeningForAction()
    {
        //arreter les effets visuels
        CustomInputManager.instance.ShowHideActionButtonVisual(false);
        outliner.enabled = false;

        PlantationManager.instance.plantationSpot = null;
    }

    public void ShowPlantTypeMenu()
    {
        if (ResourcesManager.instance.haveSeed())
        {
            PlantationManager.instance.ShowPlantTypeMenu(this);
        }
    }

    public void HidePlantTypeMenu()
    {
        PlantationManager.instance.HidePlantTypeMenu();
    }

    #endregion gestion des retours utilisateurs (interface)

    #region Gestion du load/save

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

    #endregion Gestion du load/save
}