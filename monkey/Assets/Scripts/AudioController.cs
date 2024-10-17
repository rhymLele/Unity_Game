using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    AudioSource[] audioSources; // Đổi tên biến từ audio thành audioSources
    int MusicBackground;
    public Toggle NhacNen;
    public Scrollbar AmluongNNen;

    public AudioSource vfxAudioSource;
    public AudioClip jumpClip;
    public AudioClip gameoverClip;
    public AudioClip loxoClip;
    public AudioClip mubayClip;
    public AudioClip quaiClip;
    public AudioClip baloClip;
    public AudioClip bandanClip;

    // Start is called before the first frame update
    void Start()
    {
        MusicBackground = PlayerPrefs.GetInt("MusicBackground");
        audioSources = GetComponents<AudioSource>(); // Cập nhật tên biến ở đây

        if (MusicBackground == 1)
            audioSources[0].Play();
        else
            audioSources[0].Stop();

        audioSources[0].volume = PlayerPrefs.GetFloat("Amluong");
        AmluongNNen.value = audioSources[0].volume;
    }

    public void Thietlapanluongnhacnen()
    {
        audioSources[0].volume = AmluongNNen.value;
        PlayerPrefs.SetFloat("Amluong", audioSources[0].volume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (audioSources[0].isPlaying)
            {
                audioSources[0].Pause();
            }
            else
            {
                audioSources[0].Play();
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            AmluongNNen.value += 0.1f;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            AmluongNNen.value -= 0.1f;
        }
    }

    public void OnOffMusicBackground()
    {
        if (audioSources[0].isPlaying)
        {
            audioSources[0].Pause();
            PlayerPrefs.SetInt("MusicBackground", 0);
        }
        else
        {
            audioSources[0].Play();
            PlayerPrefs.SetInt("MusicBackground", 1);
        }
    }

    public void PlaySFX(AudioClip sfxclip)
    {
        if (sfxclip == null)
        {
            Debug.LogError("SFX clip is null");
            return;
        }

        Debug.Log($"Playing sound: {sfxclip.name}");
        vfxAudioSource.clip = sfxclip;
        if (vfxAudioSource.volume <= 0)
        {
            Debug.LogWarning("Volume is set to 0, sound will not be audible.");
        }
        vfxAudioSource.PlayOneShot(sfxclip);
    }
}
