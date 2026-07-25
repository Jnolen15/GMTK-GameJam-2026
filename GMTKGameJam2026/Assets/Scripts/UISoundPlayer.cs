using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    // ============== Refrences / Variables ==============
    [SerializeField] private AudioSource _as;
    [SerializeField] private AudioClip _hoverSFX;
    [SerializeField] private AudioClip _clickSFX;
    [SerializeField] private Vector2 _pitchShift = new Vector2(0.9f, 1.1f);
    private float _soundBuffer = 0.1f;
    private float _soundBufferTimer;

    public delegate void PlaySoundEvent();
    public static PlaySoundEvent OnPlayUIHover;
    public static PlaySoundEvent OnPlayUIClick;

    // ============== Setup ==============
    #region Setup
    void Start()
    {
        OnPlayUIHover += PlayHoverSFX;
        OnPlayUIClick += PlayClickSFX;
    }

    private void OnDestroy()
    {
        OnPlayUIHover -= PlayHoverSFX;
        OnPlayUIClick -= PlayClickSFX;
    }
    #endregion

    // ============== Function ==============
    #region Function
    private void Update()
    {
        if (_soundBufferTimer > 0)
            _soundBufferTimer -= Time.deltaTime;
    }

    private void PlayHoverSFX()
    {
        PlaySFXRandomPitch(_hoverSFX);
    }

    private void PlayClickSFX()
    {
        PlaySFXRandomPitch(_clickSFX);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (_soundBufferTimer > 0) return;
        _soundBufferTimer = _soundBuffer;

        _as.PlayOneShot(clip);
    }

    public void PlaySFXRandomPitch(AudioClip clip)
    {
        if (_soundBufferTimer > 0) return;
        _soundBufferTimer = _soundBuffer;

        _as.pitch = Random.Range(_pitchShift.x, _pitchShift.y);
        _as.PlayOneShot(clip);
    }
    #endregion
}
