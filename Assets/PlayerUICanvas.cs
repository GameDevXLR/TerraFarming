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
    public Image showTree;
    public Image showBush;
    public Image showFlower;

    public Color visibleBtnColor;
    public Color hidenBtnColor;

    public static PlayerUICanvas instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeCaveUIColor()
    {
        if (showCave.color == hidenBtnColor)
        {
            showCave.color = visibleBtnColor;
        }
        else
        {
            showCave.color = hidenBtnColor;
        }
    }

    public void ChangePlainUIColor()
    {
        if (showPlain.color == hidenBtnColor)
        {
            showPlain.color = visibleBtnColor;
        }
        else
        {
            showPlain.color = hidenBtnColor;
        }
    }

    public void ChangeCraterUIColor()
    {
        if (showCrater.color == hidenBtnColor)
        {
            showCrater.color = visibleBtnColor;
        }
        else
        {
            showCrater.color = hidenBtnColor;
        }
    }

    public void ChangeNotOwnedUIColor()
    {
        if (showNotOwned.color == hidenBtnColor)
        {
            showNotOwned.color = visibleBtnColor;
        }
        else
        {
            showNotOwned.color = hidenBtnColor;
        }
    }

    public void ResetPlantsUIColor()
    {
        showFlower.color = visibleBtnColor;
        showBush.color = hidenBtnColor;
        showTree.color = hidenBtnColor;
    }

    public void ChangeFlowerUIColor()
    {
        if (showFlower.color == hidenBtnColor)
        {
            showFlower.color = visibleBtnColor;
            showBush.color = hidenBtnColor;
            showTree.color = hidenBtnColor;
        }
        else
        {
            //			showFlower.color = hidenBtnColor;
        }
    }

    public void ChangeBushUIColor()
    {
        if (showBush.color == hidenBtnColor)
        {
            showBush.color = visibleBtnColor;
            showFlower.color = hidenBtnColor;
            showTree.color = hidenBtnColor;
        }
        else
        {
            //			showBush.color = hidenBtnColor;
        }
    }

    public void ChangeTreeUIColor()
    {
        if (showTree.color == hidenBtnColor)
        {
            showTree.color = visibleBtnColor;
            showFlower.color = hidenBtnColor;
            showBush.color = hidenBtnColor;
        }
        else
        {
            //			showTree.color = hidenBtnColor;
        }
    }
}