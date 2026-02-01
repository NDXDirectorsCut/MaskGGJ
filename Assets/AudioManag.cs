using UnityEngine;

public class AudioManag : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip background;
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
