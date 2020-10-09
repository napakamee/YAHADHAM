using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerSingleton : Singleton<SoundManagerSingleton>
{
    public const float MUTE_VOLUME = -80;
    [SerializeField]
    protected AudioMixer mixer;
    public AudioMixer Mixer { get { return mixer; } set { mixer = value; } }
    //public AudioSource BGMSource { get; set; }

    public bool MasterEnabled { get; set; } = true;
    public bool MusicEnabled { get; set; } = true;
    public bool SFXEnabled { get; set; } = true;

    #region Master Volume

    public float MasterVolumeDefault { get; set; }

    protected float masterVolume;
    public float MasterVolume
    {
        get
        {
            return this.masterVolume;
        }
        set
        {
            this.masterVolume = value;
            SoundManagerSingleton.Instance.Mixer.SetFloat("MasterVol", this.masterVolume);
        }
    }
    #endregion

    #region Music Volume

    public float MusicVolumeDefault { get; set; }

    protected float musicVolume;
    public float MusicVolume
    {
        get
        {
            return this.musicVolume;
        }
        set
        {
            this.musicVolume = value;
            SoundManagerSingleton.Instance.Mixer.SetFloat("MusicVol", this.musicVolume);
        }
    }
    #endregion

    #region SFX Volume
    protected float sfxVolume;
    public float SFXVolume
    {
        get
        {
            return this.sfxVolume;
        }
        set
        {
            this.sfxVolume = value;
            SoundManagerSingleton.Instance.Mixer.SetFloat("SFXVol", this.sfxVolume);
        }
    }
    public float SFXVolumeDefault { get; set; }
    #endregion

    private void Awake()
    {
        if (GameObject.Find("SoundManagerNoDestroy") != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.name = "SoundManagerNoDestroy";
        }
        //this.BGMSource = this.GetComponent<AudioSource>();

        float masterVolume;
        if (mixer.GetFloat("MasterVol", out masterVolume))
            SoundManagerSingleton.Instance.MasterVolumeDefault = masterVolume;
            
        float musicVolume;
        if (mixer.GetFloat("MusicVol", out musicVolume))
            SoundManagerSingleton.Instance.MusicVolumeDefault = musicVolume;
            
        float SFXVolume;
        if (mixer.GetFloat("SFXVol", out SFXVolume))
            SoundManagerSingleton.Instance.SFXVolumeDefault = SFXVolume;
    }
}
