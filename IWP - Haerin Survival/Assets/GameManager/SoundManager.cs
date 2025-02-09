using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    [System.Serializable]
    public class Sound
    {
        public string name;  // Name of the sound
        public AudioClip clip;  // Audio file
        [Range(0f, 1f)] public float volume = 1f;  // Individual volume slider
    }

    public Sound[] sounds; // List of sounds with volumes
    private Dictionary<AudioClip, float> soundVolumes = new Dictionary<AudioClip, float>();

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;

            // Store sound volumes in dictionary for quick access
            foreach (Sound sound in sounds)
            {
                soundVolumes[sound.clip] = sound.volume;
            }
        }
        else
        {
            Destroy(gameObject); // This will destroy the previous instance if one exists
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        if (soundVolumes.TryGetValue(_sound, out float volume))
        {
            source.PlayOneShot(_sound, volume);
        }
        else
        {
            Debug.LogWarning("Sound not found in SoundManager! Playing at default volume.");
            source.PlayOneShot(_sound, 1f); // Default volume if not in the list
        }
    }
}
