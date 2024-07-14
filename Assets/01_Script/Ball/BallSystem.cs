using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum BallEnum
{
    Normal =0,
    Slime = 1,
    Bomb = 2,
    Balling =3,
    Metho =4,
    Lava = 5,
    Ice = 6,
    Fairy =7,
    Giant = 8,
    Dice = 10,
}

public enum BallOwner
{
    Player =0,
    Enemy = 1,
    Natural = 2,

}

public class BallSystem : ObjectSystem
{
    public bool _isStart = false;
    public bool _isReal = false;

    public Material _matGlow;


    [SerializeField]
    LayerMask _wallLayer;

    [SerializeField]
    public float _randomRange = 0.3f;


    Vector3 _endPoint;
    
    ColliderCast _cols;
    public ColliderCast ColiderCast => _cols;
    Action<Collider> Collision = null;
    Action<Collider> First = null;


    Action<BallSystem> _updateInvoke = null;
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

    [Header("BallType")]
    public BallEnum _ballEnum = BallEnum.Normal;

    public GameObject explodeEffect;
    public GameObject methoEffect;
    public GameObject lavaFloorTrail;
    public GameObject iceFloorTrail;

    public BallData ballData;

    private GameObject ballMesh;

    [Header("BallOwenr")]
    public BallOwner _ownerEnum = BallOwner.Natural;

    private void Awake()
    {
        _cols = GetComponent<ColliderCast>();
        if(_isStart )
        {
            _isReal = true;
            Input(new Vector3(UnityEngine.Random.Range(-1f,1f),0, UnityEngine.Random.Range(-1f, 1f)), BallOwner.Natural);
        }
    }

    private void Start()
    {
        SettingBallType( _ballEnum );
    }

    public void SettingBallType(BallEnum b)
    {
        _ballEnum = b;
        _originStat = new();

        BallAsset asset = ballData.BallAssets.Find(x=>x.BallType == b);

        _originStat._multySize = asset.MultySize;
        _originStat._multySpeed = asset.MultySpeed;
        _originStat._multyATK = asset.MultyATK;

        if(ballMesh != null )
        {
            Destroy( ballMesh );
            ballMesh = null;
        }
        ballMesh = Instantiate(asset.BallMesh, transform.position, Quaternion.identity, transform);

        GameManager.Instance.HUDCanvas.UpdateSwordText(b.ToString());

        /*
        switch (_ballEnum)
        {
            case BallEnum.Normal:
                {

                }
                break;
            case BallEnum.Slime:
                {
                    _originStat._multySpeed = 0.5f;
                    _originStat._multySize = 1.15f;
                }
                break;
            case BallEnum.Bomb:
                {
                    _originStat._multySpeed = 0.5f;
                    _originStat._multySize = 1.15f;
                }
                break;
            case BallEnum.Balling:
                {
                    _originStat._multySpeed = 0.3f;
                    _originStat._multyATK = 2;
                }
                break;
            case BallEnum.Metho:
                {
                    _originStat._multySpeed = 1.25f;
                    _originStat._multyATK = 0.5f;
                }
                break;
            case BallEnum.Lava:
                {
                    _originStat._multySpeed = 0.7f;
                    _originStat._multySize = 0.75f;
                    _originStat._multyATK = 0.3f;
                }
                break;
            case BallEnum.Ice:
                {
                    _originStat._multySpeed = 1.3f;
                    _originStat._multySize = 0.75f;
                    _originStat._multyATK = 1.25f;
                }
                break;
            case BallEnum.Fairy:
                {
                    _originStat._multySpeed = 1.7f;
                    _originStat._multySize = 0.3f;
                    _originStat._multyATK = 1.7f;
                }
                break;
            case BallEnum.Giant:
                {
                    _originStat._multySize = 1.5f;
                    _originStat._multyATK = 0.7f;
                    _originStat._multySpeed = 0.7f;
                }
                break;
            case BallEnum.Dice:
                break;
        }
        */
    }

    public override void ResetPool()
    {
        base.ResetPool();
        _cols.End();
    }

    float ballGimik = 0f;

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
        
        _updateInvoke?.Invoke(this);


        switch (_ballEnum)
        {
            case BallEnum.Lava:
                {
                    ballGimik += Time.deltaTime;
                    if(ballGimik >= 1f)
                    {
                        ballGimik = 0;
                        var o = Instantiate(lavaFloorTrail);
                        o.transform.position = transform.position;
                        Destroy(o, 1f);
                    }
                }
                break;
            case BallEnum.Ice:
                {
                    ballGimik += Time.deltaTime;
                    if (ballGimik >= 1f)
                    {
                        ballGimik = 0;
                        var o = Instantiate(iceFloorTrail);
                        o.transform.position = transform.position;
                        Destroy(o, 1f);
                    }
                }
                break;
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
        transform.position = hitPoint.point + -dir * GetSizeValue();
        
        WallCollisionItem(hitPoint.collider);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

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

    public void Input(Vector3 dir, BallOwner ball, Action<Collider> Collision = null, Action<Collider> First = null, Action<BallSystem> _udpateAct=null, bool b = false, float timeLate = 0)
    {
        _ownerEnum = ball;

        Color color = Color.blue;
        if(ball == BallOwner.Enemy)
        {
            color = Color.red;
        }
        _matGlow.SetColor("_Color", color);

        Collision += NormalRule;
        First += this.First;
        _updateInvoke = _udpateAct;
        Debug.Log($"기다리기전 {b}");
        if (b == false)
        {
            this.dir = dir;
            _curtime = 0;


            _cols.Now(transform, Collision, First);
        }
        else
        {
            StartCoroutine(StartLatetime( dir, timeLate, Collision, First));
        }

    }

    IEnumerator StartLatetime(Vector3 dir, float t, Action<Collider> Collision = null, Action<Collider> First = null)
    {

        Debug.Log($"기다리기전 333");
        dir.y = 0;
        this.dir = dir.normalized;
        _curtime = 0;
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

    public AudioClip _clip;

    public void NormalRule(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Wall") && _latePos == null)
        {

            SoundManager.Instance.PlayGlobal(_clip.ToString()); ;
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
        else if(_ballEnum == BallEnum.Dice)
        {
            _abilityStat._multySize += UnityEngine.Random.Range(-1f, 1f);
            _abilityStat._multyATK += UnityEngine.Random.Range(-1f, 1f);
            _abilityStat._multySpeed += UnityEngine.Random.Range(-1f, 1f);
            _abilityStat._addReflect += UnityEngine.Random.Range(-360f, 360f);
            _abilityStat._addDurationTime += UnityEngine.Random.Range(-6f, 6f);

        }

        switch (_ballEnum)
        {
            case BallEnum.Slime:
                {
                    if(col.TryGetComponent<ObjectSystem>(out ObjectSystem os))
                    {
                        os.Debuff(StatEnum.SPEED, MathType.Multiply, 0.3f, 2f);
                    }
                }
                break;
            case BallEnum.Bomb:
                {
                    if (col.TryGetComponent<ObjectSystem>(out ObjectSystem os))
                    {
                        // 폭발 넣어라 알아서 / 냉
                        var eff = PoolManager.Instance.Pop("Explosion_1_FX");

                        eff.transform.position = gameObject.transform.position;
                        Destroy(eff, 2f);
                    }
                }
                break;
            case BallEnum.Metho:
                {
                    if (col.TryGetComponent<ObjectSystem>(out ObjectSystem os))
                    {
                        // 메테오 넣어라 알아서 ?? 이펙트 구현을 유초루가 안함 / 넣었는데 쓰라고
                        var eff = PoolManager.Instance.Pop("Explosion_3_FX");
                        eff.transform.position = gameObject.transform.position;
                        Destroy(eff, 2f);
                    }
                }
                break;
        }


        if (null != col.gameObject.GetComponent<HitModule>())
        {
            col?.gameObject?.GetComponent<HitModule>()?.HitBall(this);
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
        if(_cols != null)
            _cols.PlaeyrReset();
    }


    public void DieObj()
    {
        PoolManager.Instance.Push(this);
    }

}
