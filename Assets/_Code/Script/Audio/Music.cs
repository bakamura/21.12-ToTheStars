using Naka;
using UnityEngine;

namespace Stars.Audio {
    public class Music : Singleton<Music> {

        [Header("Cache")]

        private AudioSource _musicSource;

        protected override void Awake() {
            _musicSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }

        public void Play(AudioClip music) {
            //_musicSource.Stop();
            _musicSource.clip = music;
            //_musicSource.Play();
        }

    }
}
