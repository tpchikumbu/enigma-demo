using UnityEngine;

namespace DialoguePandemoniumSystem {

    public class SoundManagerPandemonium : MonoBehaviour
    {
        public static SoundManagerPandemonium instance { get; private set; }

        private AudioSource audioSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }

}
