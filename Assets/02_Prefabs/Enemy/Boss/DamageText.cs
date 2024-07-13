using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DamageText : PoolAble
{
    [SerializeField] TextMeshPro tmpPro;

    Canvas _cans;
    Vector3 plspos;

    private void Awake()
    {
        
        tmpPro = transform.GetComponentInChildren<TextMeshPro>();
        tmpPro.text = "";
    }

    public void Show(string text, Vector3 pos, Color col, float size =20)
    {

        transform.position = pos;
        tmpPro.text = text;
        tmpPro.fontSize = size;
        tmpPro.color= col;

        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.8f);
        PoolManager.Instance.Push(this);
    }
}