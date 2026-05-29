using UnityEngine;

public class DialogueAudioMgr : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        ResolveAudioSource();
    }

    private void Reset()
    {
        ResolveAudioSource();
    }

    public void PlayDialogue(AudioClip clip)
    {
        ResolveAudioSource();

        if (audioSource == null)
        {
            return;
        }

        audioSource.Stop();

        if (clip == null)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();
    }

    private void ResolveAudioSource()
    {
        if (audioSource != null)
        {
            return;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>(true);
        }
    }
}
