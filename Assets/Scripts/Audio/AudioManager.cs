using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]

    public AudioSource loopSource;
    public AudioSource reelSource;
    public AudioSource clickSource;

    [Header("Audio Clips")]

    public AudioClip loopSound;
    public AudioClip reelSound;
    public AudioClip clickSound;

    void Start()
    {
        // Play intro loop sound when game starts
        loopSource.clip = loopSound;
        loopSource.loop = true;
        loopSource.Play();
    }

    // Called when ENTER button is pressed
    public void StopLoopSound()
    {
        loopSource.Stop();
    }

    // Play reel spinning sound
    public void StartReelSound()
    {
        reelSource.clip = reelSound;
        reelSource.loop = true;
        reelSource.Play();
    }

    // Stop reel spinning sound
    public void StopReelSound()
    {
        reelSource.Stop();
    }

    // Play button click sound
    public void PlayClickSound()
    {
        clickSource.PlayOneShot(clickSound);
    }
}