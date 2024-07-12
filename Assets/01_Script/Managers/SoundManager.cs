using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource Source;
    public SoundData SoundData;

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

    public void PlayGlobal(string clipName, bool Loop = false)
    {
        SoundAsset asset = SoundData.soundAssets.Find(x=>x.name == clipName);
        if(asset != null)
        {
            Source.clip= asset.clip;
            Source.loop = Loop;
            Source.Play();
        }
        else
        {
            print($"{clipName} 사운드는 없어요!");
        }
    }

}
