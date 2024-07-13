using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

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

    void Start()
    {
        hearts = transform.Find("Contents/Vertical Layout/HP").GetComponentsInChildren<Image>().ToList();
        shileds = transform.Find("Contents/Vertical Layout/Shield").GetComponentsInChildren<Image>().ToList();

        goldTxt = transform.Find("Contents/Gold").GetComponentInChildren<TextMeshProUGUI>();
        shieldTxt = transform.Find("Contents/Shield").GetComponentInChildren<TextMeshProUGUI>();
        swordTxt = transform.Find("Contents/Sword").GetComponentInChildren<TextMeshProUGUI>();
        lastTxt = transform.Find("Contents/Last").GetComponentInChildren<TextMeshProUGUI>();
        currentStageTxt = transform.Find("Contents/CurrentStage").GetComponentInChildren<TextMeshProUGUI>();
        currentTimeTxt = transform.Find("Contents/CurrentTime").GetComponentInChildren<TextMeshProUGUI>();

        UpdateHeartUI();
        UpdateShieldUI();
        UpdateGoldText(0);
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
        goldTxt.text = ": " + newValue.ToString();
    }

    public void UpdateShiledText(string newValue)
    {
        shieldTxt.text = newValue;
    }

    public void UpdateSwordText(string newValue)
    {
        swordTxt.text = newValue;
    }

    public void UpdateLastEnemyText(int newValue)
    {
        lastTxt.text = ": " + newValue.ToString();
    }

    public void UpdateCurrentStageText(int newValue)
    {
        currentStageTxt.text = ": " + newValue.ToString();
    }

    public void UpdateCurrentTimeText(int newValue)
    {
        currentTimeTxt.text = ": " + string.Format("{0:D2}:{1:D2}", newValue/60, newValue%60); ;
    }
}
