using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MachineUIManager : MonoBehaviour
{
    public Image leftActionArrowImg;
    public Image rightActionArrowImg;
    public Sprite normalArrow;
    public Sprite highlightArrow;

    public Image timerIcon;
    public Sprite timerSprite;

    public Image bigTimerZone;
    public Image mediumTimerZone;
    public Image smallTimerZone;

    public Image rewardImg;
    public Sprite normalRewardImg;
    public Sprite bonusRewardImg;
    public Text rewardTxt;

    public void InitializeTheUI(PlantObject plant)
    {
        bigTimerZone.enabled = true;
        mediumTimerZone.enabled = true;
        smallTimerZone.enabled = true;
        ShowHideActionArrows();
        ChangeTimerIcon();
    }

    public void ChangeTimerIcon()
    {
        timerIcon.enabled = true;
        timerIcon.sprite = timerSprite;
    }

    public void ShowHideActionArrows()
    {
        leftActionArrowImg.enabled = !leftActionArrowImg.enabled;
        rightActionArrowImg.enabled = !rightActionArrowImg.enabled;
    }

    public void BlinkActionBarArrows()
    {
        StartCoroutine(BlinkArrow());
    }

    private IEnumerator BlinkArrow()
    {
        rightActionArrowImg.sprite = highlightArrow;
        leftActionArrowImg.sprite = highlightArrow;
        yield return new WaitForSecondsRealtime(0.1f);
        leftActionArrowImg.sprite = normalArrow;
        rightActionArrowImg.sprite = normalArrow;
    }

    public void ShowRewardImg(bool wonBonus)
    {
        if (wonBonus)
        {
            rewardImg.sprite = bonusRewardImg;
            rewardTxt.enabled = true;
        }
        else
        {
            rewardImg.sprite = normalRewardImg;
        }
        rewardImg.enabled = true;
        Invoke("HideReward", 1f);
    }

    private void HideReward()
    {
        rewardImg.enabled = false;
        rewardTxt.enabled = false;
    }
}