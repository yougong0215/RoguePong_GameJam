using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    private AudioSetting audioSetting;
    // Start is called before the first frame update
    void Start()
    {
        audioSetting = GetComponentInChildren<AudioSetting>();
    }
    
    public void Open()
    {
        audioSetting.Load();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        audioSetting.Save();
        gameObject.SetActive(false);
    }
}
