using UnityEngine;

namespace Dungeon.Logic.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        public AudioClip Clip => _audioSource.clip;

        private AudioSource _audioSource;

        protected virtual void Awake()
        {
            TryGetComponent(out _audioSource);
        }

        public void Play()
        {
            if (_audioSource.clip != null)
            {
                _audioSource.Play();
                return;
            }

            Debug.Log("There is no clip to play!");
        }

        public void PlayClip(AudioClip clip, bool loop = false)
        {
            SetClip(clip);
            _audioSource.loop = loop;
            _audioSource.Play();
        }

        public void SetClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

    }
}
