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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHeartUI()
    {
        int currentHP = (int)GameManager.Instance.Player._currentHP;
        int maxHP = (int)GameManager.Instance.Player.GetHPValue();

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i >= maxHP)
            {
                hearts[i].color = new Color(1, 1, 1, 0);
                hearts[i].fillAmount = 0f;
            }
            else
            {
                hearts[i].color = Color.white;

                if (i < currentHP / 2)
                {
                    hearts[i].fillAmount = 1f;
                }
                else if (i == currentHP / 2)
                {
                    hearts[i].fillAmount = (currentHP % 2 == 0) ? 1f : 0.5f;
                }
                else
                {
                    hearts[i].fillAmount = 0f;
                }
            }

            Debug.Log($"Heart {i}: color = {hearts[i].color}, fillAmount = {hearts[i].fillAmount}");
        }
    }
}
