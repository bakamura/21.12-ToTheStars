using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [Header("References")]

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _sfxToggle;

    private void Awake() {
        float volume;
        if(_mixer.GetFloat("MusicVol", out volume)) _musicToggle.SetIsOnWithoutNotify(volume > -80f);
        if(_mixer.GetFloat("SfxVol", out volume)) _sfxToggle.SetIsOnWithoutNotify(volume > -80f);
    }

    public void MusicToggle(bool isOn) {
        _mixer.SetFloat("MusicVol", isOn ? 0f : -80f);
    }

    public void SfxToggle(bool isOn) {
        _mixer.SetFloat("SfxVol", isOn ? 0f : -80f);
    }

}
