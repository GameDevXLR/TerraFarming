using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour 
{

	//liste toutes les propriétés d'une plante et est attaché a un objet "plante" tout au long de sa vie.
	//cela doit inclure: les biomes/le type de plante (fleur buisson arbre) / les statistiques personnalisable associé / les propriétés de l'objet.
	#region initialisation du genome
	//premiere version de la fonction, ne prend en compte que le sol ou il pop. C'est tout : pas ses vrais parents.
	public void Initialize(PlantTypeEnum type, BiomeEnum biome)
	{
		switch (biome) 
		{
		case BiomeEnum.plain:
			isDome = true;

			switch (type) 
			{
			case PlantTypeEnum.flower:
				daddy = PlantCollection.instance.airFlower;
				mummy = PlantCollection.instance.airFlower;
				me = PlantCollection.instance.airFlower;
				break;
			case PlantTypeEnum.bush:
				daddy = PlantCollection.instance.airBush;
				mummy = PlantCollection.instance.airBush;
				me = PlantCollection.instance.airBush;
				break;
			case PlantTypeEnum.tree:
				daddy = PlantCollection.instance.airTree;
				mummy = PlantCollection.instance.airTree;
				me = PlantCollection.instance.airTree;
				break;
			default:
				break;
			}
			break;
		case BiomeEnum.crater:
			isWateringAround = true;

			switch (type) 
			{
			case PlantTypeEnum.flower:
				daddy = PlantCollection.instance.craterFlower;
				mummy = PlantCollection.instance.craterFlower;
				me = PlantCollection.instance.craterFlower;
				break;
			case PlantTypeEnum.bush:
				daddy = PlantCollection.instance.craterBush;
				mummy = PlantCollection.instance.craterBush;
				me = PlantCollection.instance.craterBush;
				break;
			case PlantTypeEnum.tree:
				daddy = PlantCollection.instance.craterTree;
				mummy = PlantCollection.instance.craterTree;
				me = PlantCollection.instance.craterTree;
				break;
			default:
				break;
			}
			break;
		case BiomeEnum.cave:
			isGlowing = true;

			switch (type) 
			{
			case PlantTypeEnum.flower:
				daddy = PlantCollection.instance.caveFlower;
				mummy = PlantCollection.instance.caveFlower;
				me = PlantCollection.instance.caveFlower;
				break;
			case PlantTypeEnum.bush:
				daddy = PlantCollection.instance.caveBush;
				mummy = PlantCollection.instance.caveBush;
				me = PlantCollection.instance.caveBush;
				break;
			case PlantTypeEnum.tree:
				daddy = PlantCollection.instance.caveTree;
				mummy = PlantCollection.instance.caveTree;
				me = PlantCollection.instance.caveTree;
				break;
			default:
				break;
			}
			break;
		default:
			break;
		}

		biomeIAmIn = biome;

	}

	#endregion

	#region Mes parents

	public PlantObject daddy;
	public PlantObject mummy;

	public PlantObject me;

	#endregion

	#region Biomes
	//je suis planté dans quoi ? 
	BiomeEnum biomeIAmIn;
	//de quel type est la plante? hérité
	//le tout doit faire 100. (100%)
	int plainBiomeInfluence;
	int craterBiomeInfluence;
	int caveBiomeInfluence;

	public void ConfigureBiomeInfluence(int air, int water, int earth)
	{
		plainBiomeInfluence = air;
		craterBiomeInfluence = water;
		caveBiomeInfluence = earth;
	}
	#endregion

	#region VisualProperties

	BiomeEnum visualShape; // la forme hérité
	BiomeEnum plantColors; // le material hérité
	BiomeEnum plantEffects; // l'effet de particule / animation hérité


	public void ConfigureVisualType(BiomeEnum visual, BiomeEnum color, BiomeEnum effect)
	{
		visualShape = visual;
		plantColors = color;
		plantEffects = effect;
	}
	#endregion

	#region SpecialProperties

		//propriétés spéciales de la plante.
	public bool isDome; // agrandi la portée du jardin
	public bool isGlowing; // éclaire, est jolie, augmente la vitesse de pousse alentour.
	public bool isWateringAround; //arrosage auto autour de soit : augmente le temps nécessaire entre 2 arrosages

	#endregion

	#region CommonStatistics

	float initialGrowthTime;
	float initialScale;

	#endregion

	#region utilitaires

	//déterminer mes genes de facon auto dans un premier temps:
	public void DefineMyGenes()
	{
		initialGrowthTime = me.desiredGrowthTime;
		initialScale = me.scale;

	}

	//une chance sur deux:
	bool FlipCoin()
	{
		if (Random.Range (0, 2) == 0) 
		{
			return true;
		} else 
		{
			return false;
		}
	}
	#endregion
}
