using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLacketHit : ObjectSystem, HitModule
{
    [Header("LacketHP")]
    [SerializeField] public float _lacketMaxHP = 1000;
    [SerializeField] public float _laketReviveTime = 2f;

    [Header("Ability")]
    [SerializeField] public List<SkillAbility> _skillAbility = new List<SkillAbility>();


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
        Debug.Log("ÆÐ¸µ!!");
        _currentParringCooldown = _parringTime;
        _isParring = true;
        yield return new WaitForSeconds(t);
        Debug.Log("Ãë¼Ò");
        _isParring = false;
        _parringCoroutine = null;
    }

    IEnumerator LacketHPReturn()
    {
        yield return new WaitForSeconds(_laketReviveTime);
        _originHP = _lacketMaxHP;
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

            foreach(var item in _skillAbility)
            {
                item.SettingAction(ref bss);
            }

            bss?.Invoke(ball);

            StartCoroutine(WaiterHit());
            ball.Input(dir, (cols) =>
            {
                
                if (cols.TryGetComponent<EnemyObject>(out EnemyObject ms))
                {
                    // °ü·Ã ±â¹Í
                    
                }
            });
        }
        

    }

    public void RefreshStat(ObjectSystem obj, ObjectSystem _ballStat, List<SkillAbility> Hiting)
    {
        _abilityStat = obj._abilityStat;

        _lacketMaxHP = _originHP;
        this._ballStat = _ballStat;


        _skillAbility = Hiting;

        // °ªº¯È¯ ¾ë ±âº»°ª ³Ö¾îÁà¾ßµÊ
        transform.localScale = new Vector3(GetSizeValue(),1f, 1);
        //GetComponent<BoxColliderCast>()._box.size = transform.localScale;
    }

    IEnumerator WaiterHit()
    {
    
        yield return new WaitForSeconds(0.4f);
        GameObject.FindObjectOfType<BallSystem>().ResetCollision();
    }
}
