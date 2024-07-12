using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : ObjectSystem, HitModule
{
    [Header("Info")]
    [SerializeField] float _currentHP;
    private void Start()
    {
        _currentHP = GetHPValue();
    }

    public void HitBall(BallSystem ball)
    {
        _currentHP -= ball.BallDamage();
    }

    public void HitEvent(Action<EnemyObject> act)
    {
        act.Invoke(this);
    }
}
