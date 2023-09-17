using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource audioSource=>GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool dest = false,float p1 = 0.85f,float p2 = 1.2f)
    {
        audioSource.pitch=Random.Range(p1,p2);
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlaySound(int index)
    {
        audioSource.pitch = Random.Range(0.85f, 1.2f);
        audioSource.PlayOneShot(sounds[index], 0.3f);
    }
}
