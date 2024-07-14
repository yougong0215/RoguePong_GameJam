using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public GameObject pauseObj;

    private void Awake()
    {
        pauseObj = transform.Find("Pause").gameObject;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePause(!pauseObj.activeSelf);
        }
    }
    
    public void EnablePause(bool Enable)
    {
        pauseObj.SetActive(Enable);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
