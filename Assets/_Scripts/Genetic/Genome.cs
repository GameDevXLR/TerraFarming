using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour 
{

	//liste toutes les propriétés d'une plante et est attaché a un objet "plante" tout au long de sa vie.
	//cela doit inclure: les biomes/le type de plante (fleur buisson arbre) / les statistiques personnalisable associé / les propriétés de l'objet.

	#region Biomes

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
	bool isDome; // agrandi la portée du jardin
	bool isGlowing; // éclaire, est jolie, augmente la vitesse de pousse alentour.
	bool isWateringAround; //arrosage auto autour de soit : augmente le temps nécessaire entre 2 arrosages

	#endregion

	#region CommonStatistics

	float initialGrowthTime;
	float initialScale;

	#endregion
}
