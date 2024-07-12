using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class BallStat
{

    [Header("AddStat")]
    [SerializeField] float _addATK = 0f;
    [SerializeField] float _addSize = 0f;
    [SerializeField] float _addSpeed = 0f;
    [SerializeField] float _addDurationTime = 0f;

    [Header("MultiplyStat")]
    [SerializeField] float _multyATK = 1f;
    [SerializeField] float _multySize = 1f;
    [SerializeField] float _multySpeed = 1f;
    [SerializeField] float _multyDurationTime = 1f;

    public float ResultATK(float origin = 0)
    {
        return (origin + _addATK) * _multyATK;
    }
    public void ResetATK() { _addATK = 0; _multyATK = 1f; }
    public float ResultSize(float origin = 0)
    {
        return (origin + _addSize) * _multySize;
    }
    public void ResetSize() { _addSize = 0; _multySize = 1f; }
    public float ResultSpeed(float origin = 0)
    {
        return (origin + _addSpeed) * _multySpeed;
    }
    public void ResetSpeed() { _addSpeed = 0; _multySpeed = 1f; }
    public float ResultDuration(float origin = 0)
    {
        return (origin + _addDurationTime) * _multyDurationTime;
    }
    public void ResetDuration() { _addDurationTime = 0; _multyDurationTime = 1f; }

}


public class BallSystem : ObjectSystem
{

    [SerializeField]
    public float _randomRange = 0.3f;


    ColliderCast _cols;
    Action<Collider> Collision = null;
    Action<Collider> First = null;

    Vector3 dir;

    [Header("CurrentInfo")]
    [SerializeField] float _curATK = 1f;
    [SerializeField] float _curSize = 1f;
    [SerializeField] float _curSpeed = 1f;
    [SerializeField] float _curtime = 0;

    private void Start()
    {
        _cols = GetComponent<ColliderCast>();
        Input(new Vector3(UnityEngine.Random.Range(-1f,1f),0, UnityEngine.Random.Range(-1f, 1f)));
    }

    private void Update()
    {
        _curtime += Time.deltaTime / GetDurationValue();
        _curATK = Mathf.Lerp(GetATKValue(), _ballStat.ResultATK(_originATK), _curtime);
        _curSpeed = Mathf.Lerp(GetSpeedValue(), _ballStat.ResultSpeed(_originSpeed), _curtime);
        _curSize = Mathf.Lerp(GetSizeValue(), _ballStat.ResultSize(), _curtime);

        transform.position += _curSpeed * dir.normalized * Time.deltaTime;
        

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
        return Mathf.Abs(GetSpeedValue()) > _ballStat.ResultSpeed(_originSpeed);
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
        First = null;
    }



    
}
