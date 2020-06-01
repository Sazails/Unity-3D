using UnityEngine;

public class SoundBase : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip lineDestroyed;

    private void Start()
    {
        audioSource.playOnAwake = false;
        audioSource.clip = lineDestroyed;
    }

    // public void Play(AudioClip clip)
    // {
    //     audioSource.clip = clip;
    //     audioSource.Play();
    // }

    public void Play()
    {
        audioSource.Play();
    }
}
