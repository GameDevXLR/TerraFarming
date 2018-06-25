using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    //pas le temps pour ca pour le moment : a gérer plus tard.

    public static TerrainManager instance;
    public GameObject[] terrainAreaPrefabs; //les différents prefabs de terrain qu'on peut faire spawn.

    [Header("A compléter manuellement, les distances entre chaque terrain.")]
    public Vector3 northDistance;

    public Vector3 SouthDistance;
    public Vector3 northWestDistance;
    public Vector3 northEastDistance;
    public Vector3 southWestDistance;
    public Vector3 southEastDistance;

    [Tooltip("Combien de cases de terrain tu veux ti pd ? ")]
    public int numberOfTerrainToSpawn = 6;

    private int terrainExpansionFactor = 1; // en gros au début c'est fois 1 la distance, puis fois2 etc ... Donc ca c'est le facteur de distance.
    private bool isSpawningATerrain; // est ce qu'on est déja en train de faire spawn  un terrain, si non : fait en spawn un nouveau jusqu'a atteindre numberOfTerrainToSpawn : a faire en update donc jpense. avec un callb pour ce bool.

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //de la merde :
    public void BuildTheTerrain()
    {
        for (int i = 0; i < numberOfTerrainToSpawn; i++)
        {
        }
    }

    //une ptite crotte:
    private void SpawnSpecificTerrain(Vector3 terrainPos)
    {
    }
}