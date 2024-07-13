using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : ObjectSystem, HitModule
{
    [Header("Info")]
    [SerializeField] protected float _currentHP;

    public float GetCurrentHP => _currentHP;

    private void Start()
    {
        _currentHP = GetHPValue();
    }

    public virtual void HitBall(BallSystem ball)
    {
        if(ball.IsCanBind() && ball._ownerEnum != BallOwner.Enemy)
        {

            DamageText tm = PoolManager.Instance.Pop("DamageText") as DamageText;
            tm.Show($"{(int)(ball.BallDamage())}", transform.position + new Vector3(0, 0, 2), Color.white);
            _currentHP -= ball.BallDamage();    
        }

    }

    public void HitEvent(Action<EnemyObject> act)
    {
        act.Invoke(this);
    }
}
