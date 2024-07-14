using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class HUD : MonoBehaviour
{
    private List<Image> hearts = new List<Image>();
    private List<Image> shileds = new List<Image>();

    private TextMeshProUGUI goldTxt;
    private TextMeshProUGUI shieldTxt;
    private TextMeshProUGUI swordTxt;
    private TextMeshProUGUI lastTxt;
    private TextMeshProUGUI currentStageTxt;
    private TextMeshProUGUI currentTimeTxt;

    private Image DashCool;
    private Image ParyingCool;


    void Awake()
    {
        hearts = transform.Find("Under/Contents/Vertical Layout/HP").GetComponentsInChildren<Image>().ToList();
        shileds = transform.Find("Under/Contents/Vertical Layout/Shield").GetComponentsInChildren<Image>().ToList();

        goldTxt = transform.Find("Under/Contents/Gold").GetComponentInChildren<TextMeshProUGUI>();
        shieldTxt = transform.Find("Under/Contents/Shield").GetComponentInChildren<TextMeshProUGUI>();
        swordTxt = transform.Find("Under/Contents/Sword").GetComponentInChildren<TextMeshProUGUI>();
        lastTxt = transform.Find("Under/Contents/Last").GetComponentInChildren<TextMeshProUGUI>();
        currentStageTxt = transform.Find("Under/Contents/CurrentStage").GetComponentInChildren<TextMeshProUGUI>();
        currentTimeTxt = transform.Find("Under/Contents/CurrentTime").GetComponentInChildren<TextMeshProUGUI>();

        DashCool = transform.Find("Right/DashCool/Loading").GetComponent<Image>();
        ParyingCool = transform.Find("Right/ParyingCool/Loading").GetComponent<Image>();

  


        UpdateHeartUI();
        UpdateShieldUI();
        UpdateGoldText(0);
        UpdateShieldText("Normal");
    }

    private void Update()
    {

    }

    public void UpdateHeartUI()
    {
        float currentHP = GameManager.Instance.Player._currentHP;
        float maxHP = GameManager.Instance.Player.GetHPValue();
        int i = 0;
        
        for (i = 0; i < hearts.Count; i++)
        {
            hearts[i].fillAmount = 0.0f;
        }

        for(i = 0; i< currentHP/2 - 1; i++)
        {
            hearts[i].fillAmount = 1.0f;
        }

        if (currentHP % 2 == 1)
        {
            hearts[i].fillAmount = 0.5f;
        }
        else hearts[i].fillAmount = 1.0f;
    }

    public void UpdateShieldUI()
    {
        float currentShieldHP = GameManager.Instance.Player._lacket._lacketCurHP;
        float maxShieldHP = GameManager.Instance.Player._lacket._lacketMaxHP;

        int i = 0;

        for (i = 0; i < shileds.Count; i++)
        {
            shileds[i].fillAmount = 0.0f;
        }

        for (i = 0; i < currentShieldHP / 2 - 1; i++)
        {
            shileds[i].fillAmount = 1.0f;
        }

        if (currentShieldHP % 2 == 1)
        {
            shileds[i].fillAmount = 0.5f;
        }
        else shileds[i].fillAmount = 1.0f;
    }

    public void UpdateGoldText(int newValue)
    {
        goldTxt.text = $": {newValue}";
    }

    public void UpdateShieldText(string newValue)
    {
        shieldTxt.text = newValue;
    }

    public void UpdateSwordText(string newValue)
    {
        swordTxt.text = newValue;
    }

    public void UpdateLastEnemyText(int newValue)
    {
        lastTxt.text = $": {newValue}";
    }

    public void UpdateCurrentStageText(int stage, int floor = 0)
    {
        currentStageTxt.text = $": {stage}";
    }

    public void UpdateCurrentTimeText(float newValue)
    {
        currentTimeTxt.text = ": " + string.Format("{0:D2}:{1:D2}", (int)newValue/60, (int)newValue%60);
    }

    public void UpdateDashCoolUI(float newValue)
    {
        DashCool.fillAmount = newValue;
    }

    public void UpdateParyingCoolUI(float newValue)
    {
        ParyingCool.fillAmount = newValue;
    }
}
