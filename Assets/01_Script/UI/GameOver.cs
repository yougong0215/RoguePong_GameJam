using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI RunTimeTxt;
    public GameObject OverScreen;
    void Awake()
    {
        OverScreen = transform.Find("Over").gameObject;
        RunTimeTxt = OverScreen.transform.Find("RunTimeTxt").GetComponent<TextMeshProUGUI>();

    }

    public void Over()
    {
        OverScreen.SetActive(true);

        float time = PlayerPrefs.GetFloat("RunTime", 0);

        RunTimeTxt.text = "RUN TIME\n: " + string.Format("{0:D2}:{1:D2}", (int)time / 60, (int)time % 60);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene(0);
    }
}
