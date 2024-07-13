using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SoundAsset
{
    public string name;
    public AudioClip clip;
}

[CreateAssetMenu(fileName = "SoundAssets")]
public class SoundData : ScriptableObject
{
    public List<SoundAsset> soundAssets;
}
