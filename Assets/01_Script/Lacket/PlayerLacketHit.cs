using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLacketHit : ObjectSystem, HitModule
{
    [Header("LacketHP")]
    [SerializeField] public float _lacketMaxHP = 10;
    [SerializeField] public float _lacketCurHP = 10;
    [SerializeField] public float _laketReviveTime = 2f;

    [Header("Ability")]
    [SerializeField] public List<SkillAbility> _lacketHitskillAbility = new List<SkillAbility>();
    [SerializeField] public List<SkillAbility> _ballHitSkillAbility = new List<SkillAbility>();
    [SerializeField] public List<SkillAbility> _ballUpdateSkillAbility = new List<SkillAbility>();


    bool _isParring =false;
    float _currentParringCooldown = 0;
    float _parringTime = 3f;
    float _parringContinusTime = 0.05f;

    Coroutine _hpCoroutine;
    Coroutine _parringCoroutine;

    ObjectSystem _ballStat = new();


    public void Update()
    {

        if(_currentParringCooldown <= 0)
        {
            if(Input.GetMouseButtonDown(0))
                _parringCoroutine = StartCoroutine(ParringTime(_parringContinusTime));
        }
        else
        {
            _currentParringCooldown -= Time.deltaTime;
        }
    }

    IEnumerator ParringTime(float t)
    {
        Debug.Log("패링!!");
        _currentParringCooldown = _parringTime;
        _isParring = true;
        yield return new WaitForSeconds(t);
        Debug.Log("취소");
        _isParring = false;
        _parringCoroutine = null;
    }

    IEnumerator LacketHPReturn()
    {
        yield return new WaitForSeconds(_laketReviveTime);
        _lacketCurHP = _lacketMaxHP;
        _hpCoroutine = null;
    }

    public  void HitBall(BallSystem ball)
    {

        if(_originHP > 0)
        {
            if(ball.IsCanBind())
            {
                if(_isParring == false)
                {
                    _originHP -= ball.BallDamage();
                    //Debug.Log(ball.BallDamage());

                    if(_originHP < 0)
                    {
                        if (_hpCoroutine == null)
                            _hpCoroutine = StartCoroutine(LacketHPReturn());
                    }

                }
                else
                {
                    _currentParringCooldown = 0f; 
                }
            }

            Vector3 dir = transform.forward;
            dir.y = 0;

            //ball._abilityStat = 
            ball._abilityStat = _ballStat._abilityStat;

            Action<BallSystem> bss = null;

            foreach(var item in _lacketHitskillAbility)
            {
                item.SettingAction(ref bss, _ballStat);
            }

            bss?.Invoke(ball);

            Action<BallSystem> bss1 = null;

            foreach (var item in _lacketHitskillAbility)
            {
                item.SettingAction(ref bss1, _ballStat);
            }

            Action<BallSystem> updateBall = null;

            foreach (var item in _ballUpdateSkillAbility)
            {
                item.SettingAction(ref updateBall, _ballStat);
            }

            StartCoroutine(WaiterHit());
            ball.Input(dir, (tls) =>
            {
                bss1?.Invoke(ball);
            }, default, updateBall);
        }
        

    }

    public void RefreshStat(ObjectSystem obj, ObjectSystem _ballStat, List<SkillAbility> Hiting, List<SkillAbility> ball, List<SkillAbility> update)
    {
        _abilityStat = obj._abilityStat;

        _lacketCurHP = _lacketMaxHP;
        this._ballStat = _ballStat;


        _lacketHitskillAbility = Hiting;
        _ballHitSkillAbility = ball;
        _ballUpdateSkillAbility = update;
        // 값변환 얜 기본값 넣어줘야됨
        transform.localScale = new Vector3(GetSizeValue(),1f, 1);
        //GetComponent<BoxColliderCast>()._box.size = transform.localScale;
    }

    IEnumerator WaiterHit()
    {
    
        yield return new WaitForSeconds(0.7f);
        GameObject.FindObjectOfType<BallSystem>().ResetCollision();
    }
}
