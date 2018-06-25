using UnityEngine;

public class Genome : MonoBehaviour
{
    //l'expression de leur gene depend du lieu  ou elle pousse : plutot dome ou plutot eau etc...
    //le pourcentage de chance qu'un gene s'exprime plutot qu'un autre dépend du biome.
    //l'interet de la pépiniere est de pouvoir choisir ces aspects spécifiques.
    //il y aura qq abérations mais globalement l'aspect sera plus homogène;

    //liste toutes les propriétés d'une plante et est attaché a un objet "plante" tout au long de sa vie.
    //cela doit inclure: les biomes/le type de plante (fleur buisson arbre) / les statistiques personnalisable associé / les propriétés de l'objet.

    #region initialisation du genome

    //nouvelle version de l'initialisation :
    public void Initialize(PlantObject PO, BiomeEnum spotBiome)
    {
        me = PO;
        biomeIAmIn = spotBiome;

        if (me.biome1 == BiomeEnum.plain || me.biome2 == BiomeEnum.plain || me.biome3 == BiomeEnum.plain)
        {
            isDome = true;
            ConfigureBiomeInfluence(1, 0, 0);
        }
        if (me.biome1 == BiomeEnum.crater || me.biome2 == BiomeEnum.crater || me.biome3 == BiomeEnum.crater)
        {
            isWateringAround = true;
            ConfigureBiomeInfluence(0, 1, 0);
        }
        if (me.biome1 == BiomeEnum.cave || me.biome2 == BiomeEnum.cave || me.biome3 == BiomeEnum.cave)
        {
            isGlowing = true;
            ConfigureBiomeInfluence(0, 0, 1);
        }
    }

    //	//premiere version de la fonction, ne prend en compte que le sol ou il pop. C'est tout : pas ses vrais parents.
    //	public void Initialize(PlantTypeEnum type, BiomeEnum biome)
    //	{
    //		switch (biome)
    //		{
    //		case BiomeEnum.plain:
    //			isDome = true;
    //
    //			switch (type)
    //			{
    //			case PlantTypeEnum.flower:
    //				daddy = PlantCollection.instance.airFlower;
    //				mummy = PlantCollection.instance.airFlower;
    //				me = PlantCollection.instance.airFlower;
    //				break;
    //			case PlantTypeEnum.bush:
    //				daddy = PlantCollection.instance.airBush;
    //				mummy = PlantCollection.instance.airBush;
    //				me = PlantCollection.instance.airBush;
    //				break;
    //			case PlantTypeEnum.tree:
    //				daddy = PlantCollection.instance.airTree;
    //				mummy = PlantCollection.instance.airTree;
    //				me = PlantCollection.instance.airTree;
    //				break;
    //			default:
    //				break;
    //			}
    //			break;
    //		case BiomeEnum.crater:
    //			isWateringAround = true;
    //
    //			switch (type)
    //			{
    //			case PlantTypeEnum.flower:
    //				daddy = PlantCollection.instance.craterFlower;
    //				mummy = PlantCollection.instance.craterFlower;
    //				me = PlantCollection.instance.craterFlower;
    //				break;
    //			case PlantTypeEnum.bush:
    //				daddy = PlantCollection.instance.craterBush;
    //				mummy = PlantCollection.instance.craterBush;
    //				me = PlantCollection.instance.craterBush;
    //				break;
    //			case PlantTypeEnum.tree:
    //				daddy = PlantCollection.instance.craterTree;
    //				mummy = PlantCollection.instance.craterTree;
    //				me = PlantCollection.instance.craterTree;
    //				break;
    //			default:
    //				break;
    //			}
    //			break;
    //		case BiomeEnum.cave:
    //			isGlowing = true;
    //
    //			switch (type)
    //			{
    //			case PlantTypeEnum.flower:
    //				daddy = PlantCollection.instance.caveFlower;
    //				mummy = PlantCollection.instance.caveFlower;
    //				me = PlantCollection.instance.caveFlower;
    //				break;
    //			case PlantTypeEnum.bush:
    //				daddy = PlantCollection.instance.caveBush;
    //				mummy = PlantCollection.instance.caveBush;
    //				me = PlantCollection.instance.caveBush;
    //				break;
    //			case PlantTypeEnum.tree:
    //				daddy = PlantCollection.instance.caveTree;
    //				mummy = PlantCollection.instance.caveTree;
    //				me = PlantCollection.instance.caveTree;
    //				break;
    //			default:
    //				break;
    //			}
    //			break;
    //		default:
    //			break;
    //		}
    //
    //		biomeIAmIn = biome;
    //
    //	}

    #endregion initialisation du genome

    #region Mes parents

    public PlantObject daddy;
    public PlantObject mummy;

    public PlantObject me;

    #endregion Mes parents

    #region Biomes

    //je suis planté dans quoi ?
    private BiomeEnum biomeIAmIn;

    //de quel type est la plante? hérité
    //le tout doit faire 100. (100%)
    private int plainBiomeInfluence;

    private int craterBiomeInfluence;
    private int caveBiomeInfluence;
    public int mySpotInfluence = 90;

    public void ConfigureBiomeInfluence(int air, int water, int earth)
    {
        plainBiomeInfluence += air;
        craterBiomeInfluence += water;
        caveBiomeInfluence += earth;
    }

    #endregion Biomes

    #region VisualProperties

    private BiomeEnum visualShape; // la forme hérité
    private BiomeEnum plantColors; // le material hérité
    private BiomeEnum plantEffects; // l'effet de particule / animation hérité

    public void ConfigureVisualType(BiomeEnum visual, BiomeEnum color, BiomeEnum effect)
    {
        visualShape = visual;
        plantColors = color;
        plantEffects = effect;
    }

    #endregion VisualProperties

    #region SpecialProperties

    //propriétés spéciales de la plante.
    public bool isDome; // agrandi la portée du jardin

    public bool isGlowing; // éclaire, est jolie, augmente la vitesse de pousse alentour.
    public bool isWateringAround; //arrosage auto autour de soit : augmente le temps nécessaire entre 2 arrosages

    #endregion SpecialProperties

    #region CommonStatistics

    private float initialGrowthTime;
    private float initialScale;

    #endregion CommonStatistics

    #region utilitaires

    //déterminer mes genes de facon auto dans un premier temps:
    public void DefineMyGenes()
    {
        initialGrowthTime = me.desiredGrowthTime;
        initialScale = me.scale;
    }

    //une chance sur deux:
    private bool FlipCoin()
    {
        if (Random.Range(0, 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion utilitaires
}