using UnityEngine;
using Naka;

namespace Stars.Audio {
    public class SfxPlayer : Singleton<SfxPlayer> {

        [Header("Cache")]

        private AudioSource _sfxSource;

        protected override void Awake() {
            _sfxSource = GetComponent<AudioSource>();

            DontDestroyOnLoad(gameObject);
        }

        public void Play(AudioClip sfx) {
            _sfxSource.PlayOneShot(sfx);
        }

    }
}
