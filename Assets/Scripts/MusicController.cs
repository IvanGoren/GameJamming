using UnityEngine;

public class MusicController : MonoBehaviour 
{
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;
    public AudioClip megaWeaponMusic;

    public AudioSource audioSource;

    void Start() 
    {
        audioSource.clip = gameplayMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TriggerGameOver() 
    {
        audioSource.clip = gameOverMusic;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void SetMuted(bool isMuted)
    {
        audioSource.mute = isMuted;
    }
}
