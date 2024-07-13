using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public Ability ablity;
    public AbilityEnums _skillEnum;

    public Image IconImg;
    public TextMeshProUGUI NameTxt;
    public TextMeshProUGUI DescTxt;
    public Image OutlineImg;

    // Start is called before the first frame update
    void Start()
    {
        OutlineImg = transform.Find("BG/Outline").GetComponent<Image>();
        IconImg = transform.Find("Contents/Icon/IconImg").GetComponent<Image>();
        NameTxt = transform.Find("Contents/Name/NameTxt").GetComponent<TextMeshProUGUI>();
        DescTxt = transform.Find("Contents/Desc/DescTxt").GetComponent<TextMeshProUGUI>();
    }

    public void InitCard(Ability newAblity, AbilityEnums abenum) 
    {
        ablity = newAblity;
        _skillEnum = abenum;
        OutlineImg.color = ablity.OutlineColor;
        IconImg.sprite = ablity.IconImg;
        NameTxt.text = ablity.ItemName;
        DescTxt.text = ablity.Description;
    }
}
