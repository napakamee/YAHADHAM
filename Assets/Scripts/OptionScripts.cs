using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionScripts : MonoBehaviour
{
    Animator animator;
    [SerializeField] Toggle _toggleMaster;
    [SerializeField] Toggle _toggleMusic;
    [SerializeField] Toggle _toggleSFX;
    [SerializeField] Slider MasterVolume;
    [SerializeField] Slider MusicVolume;
    [SerializeField] Slider SFXVolume;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("IsLoaded", true);
    }
    private void Start()
    {
        SoundManagerSingleton.Instance.Mixer.GetFloat("MasterVol", out float _masterVolume);
        MasterVolume.value = _masterVolume;
        SoundManagerSingleton.Instance.Mixer.GetFloat("MusicVol", out float _musicVolume);
        MusicVolume.value = _musicVolume;
        SoundManagerSingleton.Instance.Mixer.GetFloat("SFXVol", out float _sfxVolume);
        SFXVolume.value = _sfxVolume;
    }


    void Update()
    {
        if (SoundManagerSingleton.Instance.MasterEnabled)
        {
            SoundManagerSingleton.Instance.Mixer.SetFloat("MasterVol", MasterVolume.value);
        }
        if (SoundManagerSingleton.Instance.MusicEnabled)
        {
            SoundManagerSingleton.Instance.Mixer.SetFloat("MusicVol", MusicVolume.value);
        }
        if (SoundManagerSingleton.Instance.SFXEnabled)
        {
            SoundManagerSingleton.Instance.Mixer.SetFloat("SFXVol", SFXVolume.value);
        }

    }

    public void TransOut()
    {
        if (animator.GetBool("IsLoaded"))
        {
            animator.SetBool("IsLoaded", false);
        }
    }
    public void LoadMainmenu()
    {
        if (!animator.GetBool("IsLoaded"))
        {
            SceneManager.LoadScene("Mainmenu", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Options");
        }
    }

    public void OnToggleMaster()
    {
        SoundManagerSingleton.Instance.MasterEnabled = _toggleMaster.isOn;
        if (SoundManagerSingleton.Instance.MasterEnabled)
            SoundManagerSingleton.Instance.MasterVolume = SoundManagerSingleton.Instance.MasterVolumeDefault;
        else
            SoundManagerSingleton.Instance.MasterVolume = SoundManagerSingleton.MUTE_VOLUME;
    }
    public void OnToggleMusic()
    {
        SoundManagerSingleton.Instance.MusicEnabled = _toggleMusic.isOn;
        if (SoundManagerSingleton.Instance.MusicEnabled)
            SoundManagerSingleton.Instance.MusicVolume = SoundManagerSingleton.Instance.MusicVolumeDefault;
        else
            SoundManagerSingleton.Instance.MusicVolume = SoundManagerSingleton.MUTE_VOLUME;
    }
    public void OnToggleSFX()
    {
        SoundManagerSingleton.Instance.SFXEnabled = _toggleSFX.isOn;
        if (SoundManagerSingleton.Instance.SFXEnabled)
            SoundManagerSingleton.Instance.SFXVolume = SoundManagerSingleton.Instance.SFXVolumeDefault;
        else
            SoundManagerSingleton.Instance.SFXVolume = SoundManagerSingleton.MUTE_VOLUME;
    }
}
