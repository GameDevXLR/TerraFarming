using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedSeed : MonoBehaviour 
{
	//le type de "ressources" plante que tu vas ajouter.
	public ressourceEnum plantType;
	//une graine n'a qu'un biome pour le moment : pas d'hybride encore.
	public BiomeEnum biome1;
	public BiomeEnum biome2;
	public BiomeEnum biome3;
	public Transform playerTransform;

	public bool moveToPlayer;

	public void GiveRewardForLooting()
	{
		ResourcesManager.instance.setRessourceQuantity(plantType, 1, biome1,biome2,biome3);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			playerTransform = other.transform;
			moveToPlayer = true;
		}
	}

	void FixedUpdate()
	{
		if (moveToPlayer) 
		{
			transform.position = Vector3.MoveTowards (transform.position, playerTransform.position, .3f);
			if (transform.position == playerTransform.position) 
			{
				moveToPlayer = false;
				GiveRewardForLooting ();
				
			}
		}
	}
}
