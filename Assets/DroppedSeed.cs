using UnityEngine;

public class DroppedSeed : MonoBehaviour
{
    //le type de "ressources" plante que tu vas ajouter.
    public PlantTypeEnum plantType;

    //une graine n'a qu'un biome pour le moment : pas d'hybride encore.
    public BiomeEnum biome1;

    public BiomeEnum biome2;
    public BiomeEnum biome3;
    public PlantObject daddy;
    public PlantObject mummy;

    public PlantObject me;
    public Transform playerTransform;

    public bool moveToPlayer;

    private void Start()
    {
        //si t'es pas la de base sans parent xD
        //		if (daddy) {
        FigureOutMe();
        //		}
    }

    private void FixedUpdate()
    {
        if (moveToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, .3f);
            if (transform.position == playerTransform.position)
            {
                moveToPlayer = false;
                GiveRewardForLooting();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
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
        if (!GameEventsManager.instance.hasShownDropSeedCinematic)
        {
            me = GameEventsManager.instance.firstSeedReceivedPO;
            return;
        }
        if (daddy.biome1 == BiomeEnum.plain || mummy.biome1 == BiomeEnum.plain)
        {
            biome1 = BiomeEnum.plain;
            if (daddy.biome1 != mummy.biome1)
            {
                if (Random.Range(0, 2) > 0)
                {
                    biome1 = mummy.biome1;
                }
            }
        }
        else if (daddy.biome1 == BiomeEnum.crater || mummy.biome1 == BiomeEnum.crater)
        {
            biome1 = BiomeEnum.crater;
        }
        else if (daddy.biome1 == BiomeEnum.cave || mummy.biome1 == BiomeEnum.cave)
        {
            biome1 = BiomeEnum.cave;
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
                    if (Random.Range(0, 2) > 0)
                    {
                        biome2 = BiomeEnum.cave;
                    }
                }
                //				//on peut return ya pu de possibilités pour toi la.
                //				break;
            }
        }
        if (biome1 != BiomeEnum.crater && biome1 != BiomeEnum.cave && daddy.biome1 == BiomeEnum.crater || daddy.biome2 == BiomeEnum.crater || mummy.biome1 == BiomeEnum.crater || mummy.biome2 == BiomeEnum.crater)
        {
            //tu as une chance sur 2 que ton biome2 soit crater.
            if (Random.Range(0, 2) > 0)
            {
                biome2 = BiomeEnum.crater;
            }
        }
        if (biome2 == BiomeEnum.none)
        {
            //si maman ou papa ont un gene "cave" alors...
            if (daddy.biome1 == BiomeEnum.cave || daddy.biome2 == BiomeEnum.cave || daddy.biome3 == BiomeEnum.cave || mummy.biome1 == BiomeEnum.cave || mummy.biome2 == BiomeEnum.cave || mummy.biome3 == BiomeEnum.cave)
            {
                //tu as une chance sur 2 que ton biome2 soit cave.
                if (Random.Range(0, 2) > 0)
                {
                    biome2 = BiomeEnum.cave;
                }
                //				//on peut return ya pu de possibilités pour toi la.
                //				break;
            }
        }
        else
        {
            if (biome2 == BiomeEnum.crater && daddy.biome1 == BiomeEnum.cave || daddy.biome2 == BiomeEnum.cave || daddy.biome3 == BiomeEnum.cave || mummy.biome1 == BiomeEnum.cave || mummy.biome2 == BiomeEnum.cave || mummy.biome3 == BiomeEnum.cave)
            {
                //tu as une chance sur 2 que ton biome3 soit cave.
                if (Random.Range(0, 2) > 0)
                {
                    biome3 = BiomeEnum.cave;
                }
            }
        }

        for (int i = 0; i < PlantCollection.instance.allPlantObjects.Length; i++)
        {
            if (PlantCollection.instance.allPlantObjects[i].plantType == plantType && PlantCollection.instance.allPlantObjects[i].biome1 == biome1 && PlantCollection.instance.allPlantObjects[i].biome2 == biome2 && PlantCollection.instance.allPlantObjects[i].biome3 == biome3)
            {
                me = PlantCollection.instance.allPlantObjects[i];

                return;
            }
        }
        if (me == null)
        {
            if (Random.Range(0, 2) > 0)
            {
                me = daddy;
            }
            else
            {
                me = mummy;
            }
        }
    }

    public void GiveRewardForLooting()
    {
        //si t'as un PO utilise le nouveau systeme, else l'ancien systeme.
        if (me != null)
        {
            if (!GameEventsManager.instance.hasShownDropSeedCinematic)
            {
                GameEventsManager.instance.StartIntroCineFirstSeed();
                GameEventsManager.instance.hasShownDropSeedCinematic = true;
            }
            ResourcesManager.instance.setRessourceQuantity(me, 1);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("je n'ai pas de type de graine précis a donné!!! a corriger. Pas fini tout ca!");
            //			ResourcesManager.instance.setRessourceQuantity (plantType, 1, biome1, biome2, biome3);
            Destroy(gameObject);
        }
    }
}