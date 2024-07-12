using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;


public class BallSystem : ObjectSystem
{

    [SerializeField]
    public float _randomRange = 0.3f;


    ColliderCast _cols;
    public ColliderCast ColiderCast => _cols;
    Action<Collider> Collision = null;
    Action<Collider> First = null;

    Vector3 dir;

    [Header("CurrentInfo")]
    [SerializeField] float _curATK = 1f;
    [SerializeField] float _curSize = 1f;
    [SerializeField] float _curSpeed = 1f;

    [Header("Time")]
    [SerializeField] float _curtime = 1f;

    private void Start()
    {
        _cols = GetComponent<ColliderCast>();
        Input(new Vector3(UnityEngine.Random.Range(-1f,1f),0, UnityEngine.Random.Range(-1f, 1f)));
    }

    private void Update()
    {
        _curtime += Time.deltaTime / GetDurationValue();
        _curATK = Mathf.Lerp(GetATKValue(), _originStat.ResultATK(_originATK), _curtime);
        _curSpeed = Mathf.Lerp(GetSpeedValue(), _originStat.ResultSpeed(_originSpeed), _curtime);
        _curSize = Mathf.Lerp(GetSizeValue(), _originStat.ResultSize(_originSize), _curtime);

        transform.position += _curSpeed * dir.normalized * Time.deltaTime;
        transform.localScale = Vector3.one * _curSize;

        if(_curtime > 1)
        {
            _abilityStat.ResetDuration();
        }
    }

    public void Input(Vector3 dir, Action<Collider> Collision = null, Action<Collider> First = null)
    {
        dir.y = 0;
        this.dir = dir.normalized;
        _curtime = 0;

        this.Collision = NormalRule;
        
        Collision += this.Collision;
        First += this.First;

        _cols.Now(transform, Collision, First);
    }

    public bool IsCanBind()
    {
        return Mathf.Abs(GetSpeedValue()) > _originStat.ResultSpeed(_originSpeed);
    }

    public float BallDamage()
    {
        return _curATK * _curSpeed;
    }

    public void NormalRule(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Wall"))
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
        if(col.TryGetComponent<HitModule>(out HitModule hs))
        {
            hs.HitBall(this);
        }

        //else if(col.gameObject.layer == LayerMask.NameToLayer("Lacket"))
        //{
        //    dir = col.gameObject.transform.forward;
        //    dir.y = 0;
        //    dir = dir.normalized;
        //
        //    _transSpeed = 12f;
        //}
        //else
        //{
        //    Debug.LogError($"Layer None : {col.gameObject.layer.ToString()}");
        //}


    }

    public void ResetCollision()
    {
        _cols.PlaeyrReset();
    }



    
}
