using UnityEngine;

public class OreGameDetectionArea : MonoBehaviour
{
    //	public bool isActive;
    //	public AudioSource oreAudioS;
    //	public AudioClip pointPlusSnd;
    //	public Image areaImg;
    //
    //	void OnTriggerStay2D(Collider2D other)
    //	{
    //		if (!isActive || InGameManager.instance.OreGame.hasClic)
    //		{
    //			InGameManager.instance.playerController.GetComponent<Animator> ().SetBool("MiningHit", false);
    //			return;
    //		}
    //		if(Input.GetKey(CustomInputManager.instance.actionKey))
    //			{
    //			GiveAPoint ();
    //			InGameManager.instance.playerController.GetComponent<Animator> ().SetBool("MiningHit", true);
    //			InGameManager.instance.miningHitParticle.GetComponent <ParticleSystem> ().Play ();
    //			}
    //
    //	}
    //
    ////	void OnTriggerEnter2D(Collider2D other)
    ////	{
    ////		if (!isActive)
    ////		{
    ////			return;
    ////		}
    ////		CustomInputManager.instance.ShowHideActionButtonVisual (true);
    ////	}
    ////	void OnTriggerExit2D(Collider2D other)
    ////	{
    ////		if (!isActive)
    ////		{
    ////			return;
    ////		}
    ////		CustomInputManager.instance.ShowHideActionButtonVisual (false);
    ////
    ////	}
    //
    //	void GiveAPoint()
    //	{
    //
    //		isActive = false;
    //		oreAudioS.PlayOneShot (pointPlusSnd);
    //		areaImg.CrossFadeAlpha (0, 1f, true);
    //		CustomInputManager.instance.ShowHideActionButtonVisual (false);
    //		InGameManager.instance.OreGame.currentScore++;
    //		InGameManager.instance.OreGame.PlayerPressedKey ();
    //		InGameManager.instance.OreGame.totalSessionScore++;
    //
    //        ResourcesManager.instance.ChangeRawOre(1);
    //        InGameManager.instance.OreGame.playerScoreTxt.text = InGameManager.instance.OreGame.totalSessionScore.ToString ();
    //
    //	}
}