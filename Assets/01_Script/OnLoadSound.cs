using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadSound : MonoBehaviour
{
    [SerializeField] public AudioClip _audio;
    private void OnEnable()
    {
        SoundManager.Instance.PlayGlobal(_audio, true, EAudioType.BGM);    
    }
}
