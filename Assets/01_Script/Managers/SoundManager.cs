using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource Source;
    public AudioMixer AudioMixer;

    private void Start()
    {
        DontDestroyOnLoad(this);
        if(Source == null)
        {
            Source = GetComponent<AudioSource>();
        }

    }

    public void PlayGlobal(AudioClip clipName, bool Loop = false, EAudioType audioType = EAudioType.SFX)
    {
        if(clipName != null)
        {
            
            if(audioType == EAudioType.BGM)
            {
                Source.clip = clipName;
                Source.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(audioType.ToString())[0];
                Source.loop = Loop;
                Source.Play();
            }
            else
            {
                GameObject obj = new GameObject();
                AudioSource so = obj.AddComponent < AudioSource>();
                so.playOnAwake = false;
                so.clip = clipName;
                so.Play();

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
