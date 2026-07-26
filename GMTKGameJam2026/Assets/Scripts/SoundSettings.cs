using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void SetSFXVolume(float vol)
    {
        PlayerPrefs.SetFloat("MasterVol", vol);

        _audioMixer.SetFloat("MasterVol", Mathf.Log10(vol) * 20);
    }
}
