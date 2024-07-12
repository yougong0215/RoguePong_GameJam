using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Playables;
using UnityEngine;

public enum PlayerEnum
{
    Player = 0,
    Lacket = 1,
    Ball,
}


public class PlayerSystem : ObjectSystem, HitModule
{

    [Header("LacketAbility")]
    [SerializeField] List<Ability> _lacketAbility = new();
    [SerializeField] List<Ability> _lacketHitStatAbility = new();

    [Header("PlayerAbility")]
    [SerializeField] List<Ability> _statAbility = new();

    [SerializeField] float _lastMaxHP = 0;
    [SerializeField] float _currentHP =0;


    CharacterController _char;

    public bool _isCanMove = false;
    Vector3 forceDir = Vector3.zero;

    Vector3 hitPoint;

    public Vector3 MousePos => hitPoint;

    [Header("Lacket")]
    [SerializeField] PlayerLacketHit _lacket;


    private void Awake()
    {
        _char = GetComponent<CharacterController>();
    }

    private void Start()
    {
        RefreshStat();
    }

    public void AddAbility(PlayerEnum _enum, Ability _ability)
    {
        switch (_enum)
        {
            case PlayerEnum.Player:
                {
                    _statAbility.Add(_ability);
                }
                break;
            case PlayerEnum.Lacket:
                {
                    _lacketAbility.Add(_ability);
                }
                break;
            case PlayerEnum.Ball:
                {
                    _lacketHitStatAbility.Add(_ability);
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
            ability.GetAbility(ref obj1);
        }
        foreach (var ability in _lacketHitStatAbility)
        {
            ability.GetAbility(ref obj2);
        }
        _lacket.RefreshStat(obj1, obj2);

        

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
        GameObject.FindObjectOfType<BallSystem>().ResetCollision();
    }


    void Movement()
    {
        Vector3 vec = Vector3.zero;


        if (Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out float enter))
            {
                hitPoint = ray.GetPoint(enter);
                hitPoint.y = 0;
                //transform.LookAt(hitPoint);
                hitPoint = (hitPoint - transform.position ).normalized;

                forceDir = hitPoint * 128;
                StartCoroutine(WaiterHit());

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



        _char.Move(vec);
    }

    public void HitBall(BallSystem ball)
    {
        _currentHP -= ball.BallDamage();
    }

    public void HitEvent(float dmg, Action<PlayerSystem> act = null)
    {
        _currentHP -= dmg;
        act?.Invoke(this);
    }
}
