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
    private TextMeshProUGUI shiledTxt;
    private TextMeshProUGUI swordTxt;
    private TextMeshProUGUI lastTxt;
    private TextMeshProUGUI currentStageTxt;
    private TextMeshProUGUI currentTimeTxt;

    void Start()
    {
        hearts = transform.Find("Contents/Vertical Layout/HP").GetComponentsInChildren<Image>().ToList();
        shileds = transform.Find("Contents/Vertical Layout/Shiled").GetComponentsInChildren<Image>().ToList();

        goldTxt = transform.Find("Contents/Vertical Layout/Gold").GetComponentInChildren<TextMeshProUGUI>();
        shiledTxt = transform.Find("Contents/Vertical Layout/Shiled").GetComponentInChildren<TextMeshProUGUI>();
        swordTxt = transform.Find("Contents/Vertical Layout/Sword").GetComponentInChildren<TextMeshProUGUI>();
        lastTxt = transform.Find("Contents/Vertical Layout/Last").GetComponentInChildren<TextMeshProUGUI>();
        currentStageTxt = transform.Find("Contents/Vertical Layout/CurrentStage").GetComponentInChildren<TextMeshProUGUI>();
        currentTimeTxt = transform.Find("Contents/Vertical Layout/CurrentTime").GetComponentInChildren<TextMeshProUGUI>();
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
            if (i > maxHP - 1)
            {
                // �ִ� HP�� �Ѵ� ��Ʈ�� ��Ȱ��ȭ
                hearts[i].color = new Color(1, 1, 1, 0);
            }
            else
            {
                // �ִ� HP ������ ��Ʈ�� Ȱ��ȭ
                hearts[i].color = Color.white;

                // ���� HP�� ���� ��Ʈ ä���
                if (i < currentHP / 2)
                {
                    hearts[i].fillAmount = 1f; // ���� ä�� ��Ʈ
                }
                else if (i == currentHP / 2)
                {
                    if (currentHP % 2 == 0)
                    {
                        hearts[i].fillAmount = 1f; // ���� ä�� ��Ʈ
                    }
                    else
                    {
                        hearts[i].fillAmount = 0.5f; // �� ä�� ��Ʈ
                    }
                }
                else
                {
                    hearts[i].fillAmount = 0f; // �� ��Ʈ
                }
            }
        }
    }
}
