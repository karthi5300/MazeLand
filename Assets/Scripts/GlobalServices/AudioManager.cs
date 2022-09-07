using UnityEngine;

namespace GlobalServices
{
    //AudioManager class to play music/sound effects
    public class AudioManager : MonoSingletonGeneric<AudioManager>
    {
        // Audio players components.
        [SerializeField] private AudioSource EffectsSource;
        [SerializeField] private AudioSource MusicSource;

        // Random pitch adjustment range.
        [SerializeField] private float LowPitchRange = .95f;
        [SerializeField] private float HighPitchRange = 1.05f;

        public void Play(AudioClip clip)
        {
            EffectsSource.clip = clip;
            EffectsSource.Play();
        }

        // Play a single clip through the music source.
        public void PlayMusic(AudioClip clip)
        {
            MusicSource.clip = clip;
            MusicSource.Play();
        }

        // Play a random clip from an array, and randomize the pitch slightly.
        public void RandomSoundEffect(params AudioClip[] clips)
        {
            int randomIndex = Random.Range(0, clips.Length);
            float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

            EffectsSource.pitch = randomPitch;
            EffectsSource.clip = clips[randomIndex];
            EffectsSource.Play();
        }
    }
}