using UnityEngine;

[CreateAssetMenu(fileName = "PlantSO", menuName = "MusicGameSO", order = 2)]
public class MusicGameScriptableObject : ScriptableObject
{
    [Header("Gestion de la musique")]
    public AudioClip backgroundMusic;

    [Tooltip("Le son jouer en cas d'erreur.")] public AudioClip errorKey;
    [Tooltip("Tous les accords que tu peux jouer.")] public AudioClip[] keys;

    [Tooltip("Temps entre 2 accords/un accord et un blanc...")]
    public float timeBetweenKeys;

    [Tooltip("La vitesse de défilement des touches dans le minigame.")]
    public float keySpeed;

    [Tooltip("Le code qui défini un blanc. Ne doit pas etre une possibilité de 'Keys' juste au dessus!!")]
    public int whiteKeyCode;

    [Tooltip("Ca c'est ta partition en gros! Tu place des index de 'keys' plus haut ou alors un blanc.")]
    public int[] keyTrack;
}