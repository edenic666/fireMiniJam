using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;

    [SerializeField] private AudioSource sfxSource;

    public AudioClip walking;
    public AudioClip unlock;
    public AudioClip lightTorch;
    public AudioClip enterDoor;
    public AudioClip buttonInteract;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }
}