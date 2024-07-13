using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Ability : ScriptableObject
{
    [SerializeField] public string ItemName = "이름이 아직 없습니다.";
    [SerializeField][TextArea] public string Description = "설명이 필요합니다.";
    [SerializeField] public Sprite IconImg = null;
    [SerializeField] public Color OutlineColor = Color.white;

    public abstract void GetAbility(ref ObjectSystem ballStat);
}
