using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlantSO", menuName = "Plant", order = 1)]
public class PlantObject : ScriptableObject 
{
	public string plantName = "NoName";
	public GameObject babyModel;
	public GameObject teenageModel;
	public GameObject grownupModel;
	public float scale;

}
