using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;


public class BallSystem : ObjectSystem
{
    public bool _isStart = false;


    [SerializeField]
    LayerMask _wallLayer;

    [SerializeField]
    public float _randomRange = 0.3f;


    Vector3 _endPoint;
    
    ColliderCast _cols;
    public ColliderCast ColiderCast => _cols;
    Action<Collider> Collision = null;
    Action<Collider> First = null;

    Vector3 dir;
    public Vector3 Dir => dir;

    [Header("CurrentInfo")]
    [SerializeField] float _curATK = 1f;
    [SerializeField] float _curSize = 1f;
    [SerializeField] float _curSpeed = 1f;

    [Header("Time")]
    [SerializeField] float _curtime = 1f;

    Vector3 predictPos;
    RaycastHit hitPoint;

    Coroutine _latePos;

    private void Awake()
    {
        _cols = GetComponent<ColliderCast>();
        if(_isStart )
        Input(new Vector3(UnityEngine.Random.Range(-1f,1f),0, UnityEngine.Random.Range(-1f, 1f)));
    }

    private void Update()
    {
        _curtime += Time.deltaTime / GetDurationValue();
        _curATK = Mathf.Lerp(GetATKValue(), _originStat.ResultATK(_originATK), _curtime);
        _curSpeed = Mathf.Lerp(GetSpeedValue(), _originStat.ResultSpeed(_originSpeed), _curtime);
        _curSize = Mathf.Lerp(GetSizeValue(), _originStat.ResultSize(_originSize), _curtime);


        predictPos = transform.position + (_curSpeed * dir.normalized * Time.deltaTime * 2);

        if(IsWallBetweenPoints(transform.position, predictPos) == false)
        {
            transform.position += _curSpeed * dir.normalized * Time.deltaTime;
        }
        else
        {
            if(_latePos == null)
                _latePos = StartCoroutine(LateTime());
        }




        transform.localScale = Vector3.one * _curSize;


        if(_curtime > 1)
        {
            _abilityStat.ResetDuration();
        }
    }

    IEnumerator LateTime()
    {
        yield return new WaitForEndOfFrame();
        transform.position = hitPoint.point + -dir * GetSizeValue()/2;
        _latePos = null;

    }

    public bool IsWallBetweenPoints(Vector3 pointA, Vector3 pointB)
    {
        Vector3 direction = pointB - pointA;
        float distance = direction.magnitude;

        if (Physics.Raycast(pointA, direction, out hitPoint, distance, _wallLayer))
        {
            return true;
        }

        return false;
    }

    public void Stoped()
    {
        _cols.End();
    }

    public void Input(Vector3 dir, Action<Collider> Collision = null, Action<Collider> First = null, float timeLate = 0.0f)
    {

        StartCoroutine(StartLatetime( dir, timeLate));
    }

    IEnumerator StartLatetime(Vector3 dir, float t)
    {
        dir.y = 0;
        this.dir = dir.normalized;
        _curtime = 0;

        this.Collision = NormalRule;

        Collision += this.Collision;
        First += this.First;

        yield return new WaitForSeconds(t);

        _cols.Now(transform, Collision, First);
    }

    public void TimeLimit(float t)
    {
        StartCoroutine(DieTime(t));
    }

    IEnumerator DieTime(float t)
    {
        yield return new WaitForSeconds(t);
        PoolManager.Instance.Push(this);
    }

    public void SetttingDir(Vector3 _dir)
    {
        dir = _dir;
    }

    public void RefreshTime()
    {
        _curtime = 0;
    }

    public bool IsCanBind()
    {
        return Mathf.Abs(GetSpeedValue()) > _originStat.ResultSpeed(_originSpeed);
    }

    public float BallDamage()
    {
        return _curATK * _curSpeed;
    }

    public void WallCollisionItem(Collider col)
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
