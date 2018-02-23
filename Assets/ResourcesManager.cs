using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesManager : MonoBehaviour {


	public static ResourcesManager instance;

	public int rawOre;
	public int essence;
	public int flowerSeed;
	public int bushSeed;
	public int treeSeed;

	public Text rawOreDisplay;
	public Text essenceDisplay;
	public Text flowerSeedDisplay;
	public Text bushSeedDisplay;
	public Text treeSeedDisplay;


	void Awake()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	public void ChangeRawOre(int qty)
	{
		rawOre += qty;
		rawOreDisplay.text = rawOre.ToString ();
	}

	public void ChangeEssence(int qty)
	{
		essence += qty;
		essenceDisplay.text = essence.ToString ();
	}

	public void ChangeFlowerSeed(int qty)
	{
		flowerSeed += qty;
		flowerSeedDisplay.text = flowerSeed.ToString ();

	}
	public void ChangeBushSeed(int qty)
	{
		bushSeed += qty;
		bushSeedDisplay.text = bushSeed.ToString ();

	}
	public void ChangeTreeSeed(int qty)
	{
		treeSeed += qty;
		treeSeedDisplay.text = treeSeed.ToString ();
	}
}
