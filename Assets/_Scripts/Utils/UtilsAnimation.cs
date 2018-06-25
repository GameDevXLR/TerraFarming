using UnityEngine;

public class UtilsAnimation
{
    public static void SwitchAnime(Animator anim, AnimeParameters anime, bool activate)
    {
        anim.SetBool(anime.ToString(), activate);
    }
}