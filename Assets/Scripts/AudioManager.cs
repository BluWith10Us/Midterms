using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip playerDeath;
    public AudioClip[] poofSounds = new AudioClip[3];
    AudioSource audioSource;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayPlayerDeath()
    {
        audioSource.PlayOneShot(playerDeath, 0.5f);
    }
    
    public void PlayPoofSounds()
    {
        int i = Random.Range(0, 3);
        audioSource.PlayOneShot(poofSounds[i]);
    }
}
