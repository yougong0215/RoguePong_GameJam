using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyBall : ObjectSystem, HitModule
{
    [SerializeField] float _damage;
    ColliderCast _cols;
    Vector3 dir;
    float _speed = 21;

    private void Awake()
    {
        _cols = GetComponent<ColliderCast>();
        Input(new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)), BallOwner.Natural);
    }

    public void Input(Vector3 dir, BallOwner ball)
    {
        this.dir = dir;
        this.dir.y = 0;

        _cols.Now(transform, (tl) => { NormalRule(tl); });
    }

    private void Update()
    {
        transform.position += _speed * dir.normalized * Time.deltaTime;
    }

    public float BallDamage()
    {
        return _damage;
    }
    public void NormalRule(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Vector3 closestPoint = col.ClosestPoint(transform.position);
            Vector3 positionDifference = (closestPoint - transform.position);

            positionDifference.y = 0;

            Vector3 overlapDirection = positionDifference.normalized;

            if (Mathf.Abs(overlapDirection.x) > 0.1f)
            {
                dir.x *= -1;
            }

            if (Mathf.Abs(overlapDirection.z) > 0.1f)
            {
                dir.z *= -1;
            }
        }
        else if (col.TryGetComponent<ObjectSystem>(out ObjectSystem obj))
        {
            if (obj is PlayerSystem)
            {
                (obj as PlayerSystem).HitEvent(1);
            }
            else if (obj is EnemyObject)
            {
                (obj as EnemyObject).HitEvent(BallDamage());
            }
            else if (obj is PlayerLacketHit)
            {
                dir = obj.gameObject.transform.forward;
                (obj as PlayerLacketHit).HitEvent(1);
            }
            else if (obj is EnemyStandardLacket)
            {
                dir = obj.gameObject.transform.forward;
                (obj as EnemyStandardLacket).HitEvent(BallDamage());
            }
        }
    }

    public void HitBall(BallSystem ball)
    {
        //PoolManager.Instance.Push(this);
    }
}
