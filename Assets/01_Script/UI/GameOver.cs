using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI RunTimeTxt;
    void Start()
    {
        RunTimeTxt = GameObject.Find("RunTimeTxt").GetComponent<TextMeshProUGUI>();

        float time = PlayerPrefs.GetFloat("RunTime", 0);

        RunTimeTxt.text = ": " + string.Format("{0:D2}:{1:D2}", (int)time / 60, (int)time % 60);
    }

}
