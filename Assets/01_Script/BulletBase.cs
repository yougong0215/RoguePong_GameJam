using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : ObjectSystem, HitModule
{

    [Header("IsDestroy")]
    [SerializeField] bool isDestroy = false;

    Vector3 _dir;

    float _myHP;
    Action _dieEvent;

    public override void ResetPool()
    {
        _myHP = _originHP;
    }

    public void HitBall(BallSystem ball)
    {
        _myHP -= ball.BallDamage();
        Debug.Log($"Ball HP {_myHP}");
        if(_myHP <=0 || isDestroy)
        {
            _dieEvent?.Invoke();
            PoolManager.Instance.Push(this);
        }
    }

    public void Shoot(Vector3 dir, ObjectSystem obj, float Speed =10, float HP = 10, bool isAutoDie = true, Action t = null,Action DieEvent = null)
    {
        _dir = dir * Speed;
        _dieEvent = DieEvent;
        _myHP = HP;

        if(TryGetComponent<ColliderCast>(out ColliderCast cols))
        {
            cols.Now(transform, (module) => 
            {
                if(module.TryGetComponent<PlayerSystem>(out PlayerSystem ps))
                {
                    ps.HitEvent(1);
                }

                t?.Invoke();
            });
        }

        if(isAutoDie)
        {
            StartCoroutine(DieObject());
        }
    }
   


    void Update()
    {
        transform.position += _dir * Time.deltaTime;
    }

    public virtual IEnumerator DieObject()
    {
        yield return new WaitForSeconds(5f);
        PoolManager.Instance.Push(this);
    }
}
