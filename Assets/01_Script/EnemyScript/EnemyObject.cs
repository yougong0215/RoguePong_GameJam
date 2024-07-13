using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : ObjectSystem, HitModule
{
    [Header("Info")]
    [SerializeField] protected float _currentHP;
    [SerializeField] protected int _dropGold;

    [SerializeField] GameObject pos;

    public float GetCurrentHP => _currentHP;

    private void Start()
    {
        _currentHP = GetHPValue();
        if(pos == null) { pos = this.gameObject; }
    }

    public virtual void HitBall(BallSystem ball)
    {
        if(ball.IsCanBind() && ball._ownerEnum != BallOwner.Enemy)
        {

            DamageText tm = PoolManager.Instance.Pop("DamageText") as DamageText;
            tm.Show($"{(int)(ball.BallDamage())}", pos.transform.position, Color.white);
            _currentHP -= ball.BallDamage();    
        }

        if(_currentHP <= 0)
        {
            GameManager.Instance.AddDeath();
            GameManager.Instance.Gold += _dropGold;
            Destroy(this.gameObject);
            //PoolManager.Instance.Pop("Explosion 1 FX")
        }

        Vector3 effectDir = ball.transform.forward * -1;

        var eff = PoolManager.Instance.Pop("Explosion_2_FX");
        eff.transform.position = ball.transform.position;
        eff.transform.eulerAngles = effectDir;
    }

    public void HitEvent(float dmg, Action<EnemyObject> act=null)
    {
        _currentHP -= dmg;
        act?.Invoke(this);
    }
}
