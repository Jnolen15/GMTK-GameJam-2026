using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    // ============== Refrences / Variables ==============
    [SerializeField] private AudioSource _as;
    [SerializeField] private List<AudioClip> _clipList = new List<AudioClip>();
    [SerializeField] private Vector2 _pitchShift = new Vector2(1, 1);

    // ============== Function ==============
    public void PlaySFX(AudioClip clip)
    {
        _as.PlayOneShot(clip);
    }

    public void PlaySFX()
    {
        _as.PlayOneShot(_clipList[Random.Range(0, _clipList.Count)]);
    }

    public void PlaySFXRandomPitch(AudioClip clip)
    {
        _as.pitch = Random.Range(_pitchShift.x, _pitchShift.y);
        _as.PlayOneShot(clip);
    }

    public void PlaySFXRandomPitch()
    {
        _as.pitch = Random.Range(_pitchShift.x, _pitchShift.y);
        _as.PlayOneShot(_clipList[Random.Range(0, _clipList.Count)]);
    }

    public void PlaySFXLooping(AudioClip clip)
    {
        _as.clip = clip;
        _as.loop = true;
        _as.Play();
    }

    public void SetPitch(float pitch)
    {
        Mathf.Clamp(pitch, 0.5f, 1.5f);
        _as.pitch = pitch;
    }

    public void ChangeSourceSpatialBlend(float spatialBlend)
    {
        _as.spatialBlend = spatialBlend;
    }

    public void ChangeVolumeByPercent(float percent)
    {
        _as.volume = (_as.volume * percent);
    }

    public bool GetIsPlaying()
    {
        return _as.isPlaying;
    }
}
