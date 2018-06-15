using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUICanvas : MonoBehaviour 
{
	[Header("Les variables lié a la collection:")]
	public Transform collectionPanel;
	public Image showNotOwned;
	public Image showCave;
	public Image showPlain;
	public Image showCrater;

	public Color visibleBtnColor;
	public Color hidenBtnColor;

	public void ChangeCaveUIColor()
	{
		if (showCave.color == hidenBtnColor) 
		{
			showCave.color = visibleBtnColor;
		} else {
			showCave.color = hidenBtnColor;

		}
	}
	public void ChangePlainUIColor()
	{
		if (showPlain.color == hidenBtnColor) 
		{
			showPlain.color = visibleBtnColor;
		} else {
			showPlain.color = hidenBtnColor;

		}
	}
	public void ChangeCraterUIColor()
	{
		if (showCrater.color == hidenBtnColor) 
		{
			showCrater.color = visibleBtnColor;
		} else {
			showCrater.color = hidenBtnColor;

		}
	}
	public void ChangeNotOwnedUIColor()
	{
		if (showNotOwned.color == hidenBtnColor) 
		{
			showNotOwned.color = visibleBtnColor;
		} else {
			showNotOwned.color = hidenBtnColor;

		}
	}
}
