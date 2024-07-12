using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Ability : ScriptableObject
{
    [SerializeField] public string ItemName = "�̸��� ���� �����ϴ�.";
    [SerializeField] public string Description = "������ �ʿ��մϴ�.";

    public abstract void GetAbility(ref ObjectSystem ballStat);
}
