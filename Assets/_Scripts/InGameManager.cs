using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InGameManager : MonoBehaviour {

	public MusicalGame OreGame;
	public static InGameManager instance;

	public PlayerController playerController;
	public Animator InterfaceAnimator;

	public ParticleSystem cleanParticle;
	public ParticleSystem waterParticle;
	public ParticleSystem miningChargeParticle;
	public ParticleSystem miningCharge2Particle;
	public ParticleSystem miningHitParticle;
	public ParticleSystem miningHitParticle2;

	public Animator machineAnimator;
	public Animator machineBushController;

	public GameObject machineCanvas;
	public GameObject miningCanvas;
	public GameObject quitCanvas;
	public Button resumeBtn;
	public Button quitBtn;
	public bool isPlanting;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else 
		{
			Destroy (gameObject);
		}
        if (PlayerPrefs.GetString("Game") == "continue")
        {
            //Invoke("LoadGame", 0.1f);
        }
        //InvokeRepeating("SaveGame", 30.0f, 30f);

    }

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (quitCanvas.activeSelf) 
			{
				HideQuitGameMenu ();
				return;
			}
			if (!machineCanvas.activeSelf && !miningCanvas.activeSelf && !isPlanting) 
			{
				ShowQuitGameMenu ();
			}
		}
	}

	public void ShowQuitGameMenu()
	{
		quitCanvas.SetActive (true);
		EventSystem.current.SetSelectedGameObject(resumeBtn.gameObject);
		playerController.isActive= false;

	}

	public void HideQuitGameMenu()
	{
		quitCanvas.SetActive (false);
		playerController.isActive= true;

	}

	public void QuitTheGame()
	{
		Application.Quit();
	}

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.ore = ResourcesManager.instance.rawOre;
        save.essence = ResourcesManager.instance.essence;
        save.bushSeed = ResourcesManager.instance.bushSeed;
        save.treeSeed = ResourcesManager.instance.treeSeed;
        save.flowerSeed = ResourcesManager.instance.flowerSeed;

		//désactiver temporairement le temps de mettre ca au propre.
//        save.plantList = PlantationManager.instance.savePlantation();

        return save;
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // 3

            ResourcesManager.instance.setRawOre(save.ore);
            ResourcesManager.instance.setEssence(save.essence);
            ResourcesManager.instance.setBushSeed(save.bushSeed);
            ResourcesManager.instance.setTreeSeed(save.treeSeed);
            ResourcesManager.instance.setFlowerSeed(save.flowerSeed);

            PlantationManager.instance.loadPlantation(save.plantList);
            
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}
