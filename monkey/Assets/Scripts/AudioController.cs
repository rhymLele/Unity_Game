using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    AudioSource[] audioSources;
    public Toggle NhacNen, SFXToggle;
    public Scrollbar AmluongNNen, AmluongSFX;

    public AudioSource vfxAudioSource;
    public AudioClip jumpClip, gameoverClip, loxoClip, mubayClip, quaiClip, baloClip, bandanClip;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        if (audioSources.Length == 0) return;
        int MusicBackground = PlayerPrefs.GetInt("MusicBackground", 1);
        if (NhacNen != null) NhacNen.isOn = (MusicBackground == 1);

        if (NhacNen != null && NhacNen.isOn)
            audioSources[0].Play();
        else
            audioSources[0].Stop();
        float savedVolume = PlayerPrefs.GetFloat("Amluong", 1f);
        audioSources[0].volume = savedVolume;
        // Kiểm tra nếu Scrollbar được gán trong Inspector
        if (AmluongNNen != null)
        {
            AmluongNNen.value = savedVolume;
            AmluongNNen.onValueChanged.AddListener(delegate { Thietlapanluongnhacnen(); });
        }

        float savedSFXVolume = PlayerPrefs.GetFloat("AmluongSFX", 1f);
        vfxAudioSource.volume = savedSFXVolume;
        if (AmluongSFX != null)
        {
            AmluongSFX.value = savedSFXVolume;
            AmluongSFX.onValueChanged.AddListener(delegate { ThietlapanluongSFX(); });
        }

        int SFXEnabled = PlayerPrefs.GetInt("SFXEnabled", 1);
        if (SFXToggle != null) SFXToggle.isOn = (SFXEnabled == 1);
        if (SFXToggle != null)
            SFXToggle.onValueChanged.AddListener(delegate { OnOffSFX(); });

        if (NhacNen != null)
            NhacNen.onValueChanged.AddListener(delegate { OnOffMusicBackground(); });
    }

    public void Thietlapanluongnhacnen()
    {
        if (AmluongNNen != null)
        {
            audioSources[0].volume = AmluongNNen.value;
            PlayerPrefs.SetFloat("Amluong", AmluongNNen.value);
            PlayerPrefs.Save();
        }
    }

    public void ThietlapanluongSFX()
    {
        if (AmluongSFX != null)
        {
            vfxAudioSource.volume = AmluongSFX.value;
            PlayerPrefs.SetFloat("AmluongSFX", AmluongSFX.value);
            PlayerPrefs.Save();
        }
    }

    public void OnOffMusicBackground()
    {
        if (audioSources.Length == 0) return;

        if (NhacNen != null && NhacNen.isOn)
        {
            audioSources[0].Play();
            PlayerPrefs.SetInt("MusicBackground", 1);
        }
        else
        {
            audioSources[0].Pause();
            PlayerPrefs.SetInt("MusicBackground", 0);
        }
        PlayerPrefs.Save();
    }

    public void OnOffSFX()
    {
        if (SFXToggle != null)
        {
            bool isEnabled = SFXToggle.isOn;
            PlayerPrefs.SetInt("SFXEnabled", isEnabled ? 1 : 0);
            PlayerPrefs.Save();

            vfxAudioSource.mute = !isEnabled;
        }
    }

    void Update()
    {
        if (AmluongNNen != null && IsMouseOverScrollbar(AmluongNNen))
        {
            float scrollAmount = Input.mouseScrollDelta.y * 0.1f;
            AmluongNNen.value = Mathf.Clamp(AmluongNNen.value + scrollAmount, 0f, 1f);
        }
        else if (IsMouseOverScrollbar(AmluongSFX))
        {
            float scrollAmount = Input.mouseScrollDelta.y * 0.1f;
            AmluongSFX.value = Mathf.Clamp(AmluongSFX.value + scrollAmount, 0f, 1f);
        }
    }

    bool IsMouseOverScrollbar(Scrollbar scrollbar)
    {
        if (scrollbar == null) return false;

        Vector2 localMousePos = Input.mousePosition;
        RectTransform scrollbarRect = scrollbar.GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(scrollbarRect, localMousePos, Camera.main);
    }

    public void PlaySFX(AudioClip sfxclip)
    {
        if (sfxclip == null || vfxAudioSource.mute)
        {
            //Debug.LogError("SFX clip is null or SFX is disabled");
            return;
        }

        vfxAudioSource.PlayOneShot(sfxclip);
    }
}
