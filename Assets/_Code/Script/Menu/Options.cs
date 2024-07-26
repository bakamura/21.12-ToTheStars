using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour {

    [Header("References")]

    [SerializeField] private AudioMixer _mixer;

    public void MusicToggle(bool isOn) {
        _mixer.SetFloat("MusicVol", isOn ? 0f : -80f);
    }

    public void SfxToggle(bool isOn) {
        _mixer.SetFloat("SfxVol", isOn ? 0f : -80f);
    }

}
