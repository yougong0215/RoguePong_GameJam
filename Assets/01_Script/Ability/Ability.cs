using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Ability : ScriptableObject
{
    [SerializeField] public string ItemName = "�̸��� ���� �����ϴ�.";
    [SerializeField][TextArea] public string Description = "������ �ʿ��մϴ�.";
    [SerializeField] public Sprite IconImg = null;
    [SerializeField] public Color OutlineColor = Color.white;

    public abstract void GetAbility(ref ObjectSystem ballStat);
}
