using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource Source;
    public SoundData SoundData;
    public AudioMixer AudioMixer;

    private void Start()
    {
        if(Source == null)
        {
            Source = GetComponent<AudioSource>();
        }

        if(SoundData == null)
        {
            Debug.LogError("SoundData 없어요");
        }
    }

    public void PlayGlobal(string clipName, bool Loop = false, EAudioType audioType = EAudioType.SFX)
    {
        SoundAsset asset = SoundData.soundAssets.Find(x=>x.name == clipName);
        if(asset != null)
        {
            Source.clip= asset.clip;
            Source.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(audioType.ToString())[0];
            Source.loop = Loop;
            Source.Play();
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
