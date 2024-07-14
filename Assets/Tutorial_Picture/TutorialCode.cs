using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCode : MonoBehaviour
{
    public List<Sprite> spi = new();

    public Image img;
    public TextMeshProUGUI _tmp;

    int idx =0;

    public void Input(int t)
    {
        idx += t;

        if (idx < 0)
        {
            idx = 0;
        }
        else if(idx > 6)
        {
            idx = 6;
        }

        img.sprite = spi[idx];
        _tmp.text = $"( {idx+1} / 7 )";

    }

}
