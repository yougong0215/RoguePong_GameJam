using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public enum EAudioType
{
    Master,
    SFX,
    BGM,
    None
}

public struct AudioSet
{
    public float MasterVolume;
    public float SFXVolume;
    public float BGMVolume;
}

public class AudioSetting : MonoBehaviour
{
    private AudioSet set;
    private AudioSet previousSet;

    private const float minVolume = -40.0f;
    private const float maxVolume = 0.0f;

    [SerializeField]
    private AudioMixer audioMixer;
    private readonly string fileName = "AudioSetting";

    //UI
    private Slider masterSlider;
    private Slider sfxSlider;
    private Slider bgmSlider;

    private bool notSaved;
    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        previousSet = set;
    }

    public void ApplyMixer(string parameterName, float value)
    {
        float inputValue = Mathf.Clamp(value, minVolume, maxVolume);
        if (inputValue == minVolume) inputValue = -80.0f; //-40부터 -80은 생략
        audioMixer.SetFloat(parameterName, inputValue);
    }

    public void Save()
    {
        JsonManager<AudioSet>.SaveJson(set, fileName);
        previousSet = set;

        notSaved = false;
    }

    public void Load()
    {
        if (JsonManager<AudioSet>.LoadJson(fileName, out set))
        {
            MasterVolume = set.MasterVolume;
            SFXVolume = set.SFXVolume;
            BGMVolume = set.BGMVolume;
        }
    }

    public float MasterVolume
    {
        get { return set.MasterVolume; }
        set
        {
            set.MasterVolume = value;

            if (masterSlider.value != value)
            {
                masterSlider.value = value;
            }

            ApplyMixer("Master", value);
            notSaved = true;
        }
    }

    public float SFXVolume
    {
        get { return set.SFXVolume; }
        set
        {
            set.SFXVolume = value;

            if (sfxSlider.value != value)
            {
                sfxSlider.value = value;
            }

            ApplyMixer("SFX", value);
            notSaved = true;
        }
    }

    public float BGMVolume
    {
        get { return set.BGMVolume; }
        set
        {
            set.BGMVolume = value;

            if (bgmSlider.value != value)
            {
                bgmSlider.value = value;
            }

            ApplyMixer("BGM", value);
            notSaved = true;
        }
    }


    public bool NotSaved => notSaved;
}
