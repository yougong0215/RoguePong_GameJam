using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource Source;
    public SoundData _sound;
    public AudioMixer AudioMixer;

    private void Start()
    {
        DontDestroyOnLoad(this);
        if(Source == null)
        {
            Source = GetComponent<AudioSource>();
        }

    }

    public void PlayGlobal(string clipName, bool Loop = false, EAudioType audioType = EAudioType.SFX)
    {

        AudioClip asset = _sound.soundAssets.Find(x => x.ToString() == clipName);
        if (asset != null)
        {
            
            if(audioType == EAudioType.BGM)
            {
                Source.clip = asset;
                Source.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(audioType.ToString())[0];
                Source.loop = Loop;
                Source.Play();
            }
            else
            {
                GameObject obj = new GameObject();
                AudioSource so = obj.AddComponent < AudioSource>();
                so.playOnAwake = false;
                so.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(audioType.ToString())[0];
                so.clip = asset;
                so.Play();
                StartCoroutine(CO(so));
            }

        }
        else
        {
            print($"{clipName} 사운드는 없어요!");
        }
    }

    IEnumerator CO(AudioSource s)
    {
        yield return new WaitUntil(()=> s.isPlaying == false);
        Destroy(s.gameObject);

    }

}
