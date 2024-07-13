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

    public void HitBall(BallSystem ball)
    {
        if (_lacketCurHP > 0)
        {
            if (ball.IsCanBind() && ball._ownerEnum != BallOwner.Enemy)
                _lacketCurHP -= ball.BallDamage();

            if(_lacketCurHP <0)
            {
                Debug.LogError($"이팩트 추가하렴");
                // 대충 추하고 오브젝트 삭제도 하렴
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
