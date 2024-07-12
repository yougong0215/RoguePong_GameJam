using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AudioSet
{
    public float Master;
    public float SFX;
    public float BGM;
}

public class AudioSetting : MonoBehaviour
{
    public AudioSet Set;

}
