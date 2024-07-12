using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : ObjectSystem, HitModule
{

    [Header("IsDestroy")]
    [SerializeField] bool isDestroy = false;

    Vector3 _dir;

    float _myHP;

    public override void ResetPool()
    {
        _myHP = _originHP;
    }

    public void HitBall(BallSystem ball)
    {
        _myHP -= ball.BallDamage();

        if(_myHP <=0 && isDestroy)
        {
            PoolManager.Instance.Push(this);
        }
    }

    public void Shoot(Vector3 dir, ObjectSystem obj)
    {
        _dir = dir;
        obj._abilityStat = _abilityStat;
        if(TryGetComponent<ColliderCast>(out ColliderCast cols))
        {
            cols.Now(transform, (module) => 
            {
                if(module.TryGetComponent<PlayerSystem>(out PlayerSystem ps))
                {
                    ps.HitEvent(GetATKValue());
                }
            });
        }
    }


    void Update()
    {
        transform.position += _dir * Time.deltaTime * 12;
    }

    public virtual IEnumerator DieObject()
    {
        yield return new WaitForSeconds(5f);
        PoolManager.Instance.Push(this);
    }
}
