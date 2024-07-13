using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallSkillEnum
{
    PlayerStat = 0,
    LacketStat = 1,
    BallStat =2,
    BallHit = 3,
    BallUpdate =4,
    Parring = 5,
}


public class PlayerSystem : ObjectSystem, HitModule
{

    [Header("LacketAbility")]
    [SerializeField] List<Ability> _lacketAbility = new();
    [SerializeField] List<Ability> _lacketHitStatAbility = new();

    [Header("PlayerAbility")]
    [SerializeField] List<Ability> _statAbility = new(); 
    [SerializeField] List<SkillAbility> _parringAbility = new();


    [Header("BallSkill")]
    [SerializeField] List<SkillAbility> _ballHitSkill = new();
    [SerializeField] List<SkillAbility> _ballColisionSkill = new();
    [SerializeField] List<SkillAbility> _ballUpdateSkill = new();


    [SerializeField] public float _dachCooTime = 2f;
    float _curDashTime = 0;
    [SerializeField] public float _lastMaxHP = 10;
    [SerializeField] public float _currentHP =10;


    CharacterController _char;

    public bool _isCanMove = false;
    Vector3 forceDir = Vector3.zero;

    Vector3 hitPoint;

    public Vector3 MousePos => hitPoint;

    [Header("Lacket")]
    [SerializeField] public PlayerLacketHit _lacket;

    Coroutine isSuperArmor = null;


    private void Awake()
    {
        _char = GetComponent<CharacterController>();
    }

    private void Start()
    {
        RefreshStat();
    }

    public void AddAbility(BallSkillEnum _enum, Ability _ability)
    {
        switch (_enum)
        {
            case BallSkillEnum.PlayerStat:
                {
                    _statAbility.Add(_ability);
                }
                break;
            case BallSkillEnum.LacketStat:
                {
                    _lacketAbility.Add(_ability);
                }
                break;
            case BallSkillEnum.BallStat:
                {
                    _lacketHitStatAbility.Add(_ability);
                }
                break;
            case BallSkillEnum.BallHit:
                {
                    _ballHitSkill.Add(_ability as SkillAbility);
                }
                break;
        }

        RefreshStat();
    }

    public void RefreshStat()
    {
        ObjectSystem obj = new();
        foreach (var ability in _statAbility)
        {
            if(null != ability)
                ability.GetAbility(ref obj);
        }

        _abilityStat = obj._abilityStat;

        if(_lastMaxHP != GetHPValue())
        {
            float value = GetHPValue() - _lastMaxHP;

            _lastMaxHP = GetHPValue();
            _currentHP += value;
        }

        // °ªº¯È¯

        transform.localScale = new Vector3(GetSizeValue(), GetSizeValue(), GetSizeValue());


        ObjectSystem obj1 = new();
        ObjectSystem obj2 = new();
        foreach (var ability in _lacketAbility)
        {
            if (null != ability)
                ability.GetAbility(ref obj1);
        }
        foreach (var ability in _lacketHitStatAbility)
        {
            if (null != ability)
                ability.GetAbility(ref obj2);
        }
        _lacket.RefreshStat(obj1, obj2, _ballHitSkill, _ballColisionSkill, _ballUpdateSkill, _parringAbility);

        

    }


    private void Update()
    {

        MouseInput();

        Movement();

    }

    void MouseInput()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out float enter))
            {
                hitPoint = ray.GetPoint(enter);
                hitPoint.y = transform.position.y;
                transform.LookAt(hitPoint);
            }
        }
    }

    IEnumerator WaiterHit()
    {

        yield return new WaitUntil(() => forceDir.sqrMagnitude < 0.15f);
        GameObject.FindObjectOfType<BallSystem>()?.ResetCollision();
    }


    void Movement()
    {
        Vector3 vec = Vector3.zero;
        _curDashTime += Time.deltaTime;

        if (Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.LeftShift) && _curDashTime > _dachCooTime)
        {
            _curDashTime = 0;
            //Vector3 mousePosition = Input.mousePosition;
            //
            //Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            //
            //Plane plane = new Plane(Vector3.up, Vector3.zero);
            //if (plane.Raycast(ray, out float enter))
            {
                //hitPoint = ray.GetPoint(enter);
                //hitPoint.y = 0;
                //Vector3 tls = transform.position;
                //tls.y = 0;
                ////transform.LookAt(hitPoint);
                //hitPoint = (hitPoint - tls ).normalized;
                //
                //forceDir = hitPoint * 128;
                //Debug.Log($"Force {forceDir}");
                //StartCoroutine(WaiterHit());
                if (isSuperArmor != null)
                    isSuperArmor = null;
                isSuperArmor = StartCoroutine(SuperMod(0.3f));
            }


        }

        if (forceDir.sqrMagnitude < 0.1f)
        {
            forceDir = Vector3.zero;
        }
        else
        {
            forceDir = Vector3.Lerp(forceDir, Vector3.zero, Time.deltaTime * 10);
        }



        if (_isCanMove || forceDir == Vector3.zero)
            vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * GetSpeedValue() * Time.deltaTime;
        else
            vec = forceDir * Time.deltaTime;

        if(transform.position.y != 1)
        { 
            _char.enabled = false;
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                _char.enabled = true;
        }


        _char.Move(vec);
    }

    public void HitBall(BallSystem ball)
    {
        if (isSuperArmor!=null)
            return;
        isSuperArmor = StartCoroutine(SuperMod());
        _currentHP -= 1;
        GameManager.Instance.HUDCanvas.UpdateHeartUI();
    }

    public void HitEvent(float dmg, Action<PlayerSystem> act = null)
    {
        if (isSuperArmor != null)
            return;
        isSuperArmor = StartCoroutine(SuperMod());
        //_currentHP -= dmg;
        _currentHP = Mathf.Clamp(_currentHP-dmg, 0, GetHPValue());
        GameManager.Instance.HUDCanvas.UpdateHeartUI();
        act?.Invoke(this);
    }


    public IEnumerator FrameCharacterConoff()
    {
        _char.enabled = false;
        yield return null;
        _char.enabled = true;

    }
    IEnumerator SuperMod(float t = 0.7f)
    {
        yield return new WaitForSeconds(0.7f);
        isSuperArmor = null;
    }
}
