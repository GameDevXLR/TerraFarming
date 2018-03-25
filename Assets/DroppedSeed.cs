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
	public PlantObject daddy;
	public PlantObject mummy;

	public PlantObject me;
	public Transform playerTransform;

	public bool moveToPlayer;

	void Start()
	{
		//si t'es pas la de base sans parent xD
		if (daddy) {
			FigureOutMe ();
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

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			playerTransform = other.transform;
			moveToPlayer = true;
		}
	}

	//détermine mes biomes / la graine que je serais une fois looté quoi ^^ : pas top...Refonte en cours.
	public void FigureOutMe()
	{
		if (daddy.biome1 == BiomeEnum.plain || mummy.biome1 == BiomeEnum.plain) 
		{
			biome1 = BiomeEnum.plain;
			if (daddy.biome1 != mummy.biome1) 
			{
				if (Random.Range (0, 2) > 0) 
				{
					biome1 = mummy.biome1;
				}
			}
		}

		//si ton premier biome est pas "plaine" alors tu peux pas avoir de biome2 sauf dans un cas:
		if (biome1 != BiomeEnum.plain) 
		{
			if (biome1 == BiomeEnum.crater) 
			{
				//si maman ou papa ont un gene "cave" alors...
				if (daddy.biome1 == BiomeEnum.cave || daddy.biome2 == BiomeEnum.cave || daddy.biome3 == BiomeEnum.cave || mummy.biome1 == BiomeEnum.cave || mummy.biome2 == BiomeEnum.cave || mummy.biome3 == BiomeEnum.cave) 
				{
					//tu as une chance sur 2 que ton biome2 soit cave.
					if (Random.Range (0, 2) > 0) 
					{
						biome2 = BiomeEnum.cave;
					}
				}
				//on peut return ya pu de possibilités pour toi la.
				return;
			}
		}
		if (daddy.biome1 == BiomeEnum.crater || daddy.biome2 == BiomeEnum.crater || mummy.biome1 == BiomeEnum.crater || mummy.biome2 == BiomeEnum.crater) 
		{
			//tu as une chance sur 2 que ton biome2 soit crater.
			if (Random.Range (0, 2) > 0) 
			{
				biome2 = BiomeEnum.crater;
			}
		}
		if (biome2 != BiomeEnum.crater) {
			//si maman ou papa ont un gene "cave" alors...
			if (daddy.biome1 == BiomeEnum.cave || daddy.biome2 == BiomeEnum.cave || daddy.biome3 == BiomeEnum.cave || mummy.biome1 == BiomeEnum.cave || mummy.biome2 == BiomeEnum.cave || mummy.biome3 == BiomeEnum.cave) {
				//tu as une chance sur 2 que ton biome2 soit cave.
				if (Random.Range (0, 2) > 0) {
					biome2 = BiomeEnum.cave;
				}
				//on peut return ya pu de possibilités pour toi la.
				return;
			}
		} else 
		{
			if (daddy.biome1 == BiomeEnum.cave || daddy.biome2 == BiomeEnum.cave || daddy.biome3 == BiomeEnum.cave || mummy.biome1 == BiomeEnum.cave || mummy.biome2 == BiomeEnum.cave || mummy.biome3 == BiomeEnum.cave) {
				//tu as une chance sur 2 que ton biome3 soit cave.
				if (Random.Range (0, 2) > 0) {
					biome3 = BiomeEnum.cave;
				}
		}


		}
	}

	public void GiveRewardForLooting()
	{
		//si t'as un PO utilise le nouveau systeme, else l'ancien systeme.
		if (me != null) {
			ResourcesManager.instance.setRessourceQuantity (me, 1);
			Destroy (gameObject);

		} else {
			Debug.Log ("je n'ai pas de type de graine précis a donné!!! a corriger. Pas fini tout ca!");
//			ResourcesManager.instance.setRessourceQuantity (plantType, 1, biome1, biome2, biome3);
			Destroy (gameObject);
		}
	}
}
