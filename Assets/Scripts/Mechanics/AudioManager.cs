using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip musicClip;
    public AudioClip fireClip;
    public AudioClip hitClip;
    public AudioClip collectClip;
    public AudioClip unlockClip;
    public AudioClip teleportClip;
    public AudioClip talkClip;

    void Start() {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayFire() {
        sfxSource.clip = fireClip;
        sfxSource.Play();
    }
    public void PlayHit() {
        sfxSource.clip = hitClip;
        sfxSource.Play();
    }
    public void PlayCollect() {
        sfxSource.clip = collectClip;
        sfxSource.Play();
    }
    public void PlayUnlock() {
        // sfxSource.clip = unlockClip;
        sfxSource.PlayOneShot(unlockClip);
    }
    public void PlayTeleport() {
        sfxSource.clip = teleportClip;
        sfxSource.Play();
    }
    public void PlayTalk() {
        sfxSource.clip = talkClip;
        sfxSource.Play();
    }



}
