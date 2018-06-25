using UnityEngine;

[CreateAssetMenu(fileName = "PlantSO", menuName = "Plant", order = 1)]
public class PlantObject : ScriptableObject
{
    [Header("Ma chère DA. Configure ici ta plante. Met lui les visuels associés. Dit moi c'est pour quel biome, si c'est une fleur, un arbre? Un hybride?")]
    public string plantName = "NoName";

    [Tooltip("C'est quoi comme genre de plante?")] public PlantTypeEnum plantType;
    [Tooltip("C'est quoi son/ses biome(s)?")] public BiomeEnum biome1;
    [Tooltip("C'est quoi son/ses biome(s)?")] public BiomeEnum biome2;
    [Tooltip("C'est quoi son/ses biome(s)?")] public BiomeEnum biome3;
    public GameObject babyModel;
    public GameObject teenageModel;
    public GameObject grownupModel;
    [Tooltip("L'icone a afficher dans l'interface qui doit être associé a cette plante.")] public Sprite plantIcon;
    [Tooltip("Echelle du modèle pour que ca colle bien")] public float scale;
    [Tooltip("Temps de croissance désirée par phase(bébé/ado/adulte).")] public float desiredGrowthTime;
    [Tooltip("Temps max de croissance pour ce type de plante.")] public float maxGrowthTime;
    [Tooltip("Temps min de croissance!En dessous ce serait de l'abus!")] public float minGrowthTime;
}