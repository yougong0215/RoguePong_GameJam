using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletListShootBase : PoolAble
{

    Vector3 _dir;

    public List<BulletBase> _bullet = new();
    public List<Vector3> _pos = new();

    private void Awake()
    {
        _bullet = GetComponentsInChildren<BulletBase>().ToList();

        foreach (BulletBase b in _bullet)
        {
            _pos.Add(b.transform.localPosition);
        }
    }

    public override void ResetPool()
    {
        foreach(BulletBase b in _bullet)
        {
            PoolManager.Instance.Push(b);
        }
        _bullet.Clear();
        foreach(Vector3 v in _pos)
        {
            BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
            bs.transform.localPosition = v;
            bs.transform.parent = transform;
            _bullet.Add(bs);

        }
    }


    public void Shoot(Vector3 dir, ObjectSystem obj, float Speed = 1, float HP = 10, Action t = null, Action DieEvent = null)
    {
        _dir = dir * Speed;

        foreach (BulletBase b in _bullet)
        {
            b.Shoot(dir,obj,0,HP,false,t,DieEvent);
        }
    }

    void Update()
    {
        transform.position += _dir * Time.deltaTime;
        transform.Rotate(0, 360 * Time.deltaTime, 0);
    }
}
