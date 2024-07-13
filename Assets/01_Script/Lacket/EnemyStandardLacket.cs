using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandardLacket : ObjectSystem, HitModule
{
    [Header("LacketHP")]
    [SerializeField] public float _lacketMaxHP = 10;
    [SerializeField] public float _lacketCurHP = 10;

    private void Awake()
    {
        _lacketMaxHP = _originHP;
        _lacketCurHP = _originHP;
    }

    public void HitEvent(float dmg)
    {
        _lacketCurHP -= dmg;

        if (_lacketCurHP < 0)
        {
            EffectObject e = PoolManager.Instance.Pop("Explosion_1_FX") as EffectObject;
            e.transform.position = transform.position;
            Destroy(e, 2f);
            Destroy(gameObject);
        }
    }

    public void HitBall(BallSystem ball)
    {
        if (_lacketCurHP > 0)
        {
            if (ball.IsCanBind() && ball._ownerEnum != BallOwner.Enemy)
                _lacketCurHP -= ball.BallDamage();

            if(_lacketCurHP <0)
            {
                EffectObject e = PoolManager.Instance.Pop("Explosion_1_FX") as EffectObject;
                e.transform.position = transform.position;
                Destroy(e, 2f);
                Destroy(gameObject);
            }
            else
            {
                Vector3 dir = transform.forward;
                Quaternion quat = Quaternion.Euler(0, UnityEngine.Random.Range(-GetReflectValue(), GetReflectValue()), 0);
                dir.y = 0;
                dir = quat * dir;

                ball.Input(dir, BallOwner.Enemy);
            }
        }
    }
}
