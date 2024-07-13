using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] public List<SkillAbility> _parringSkillAbility = new List<SkillAbility>();


    bool _isParring =false;
    float _currentParringCooldown = 0;
    float _parringTime = 3f;
    float _parringContinusTime = 0.2f;

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

        float ct = 0.0f;
        while(ct <= t)
        {   
            ct += Time.deltaTime;
            GameManager.Instance.HUDCanvas.UpdateParyingCoolUI(ct / t);
            yield return null;
        }
        GameManager.Instance.HUDCanvas.UpdateParyingCoolUI(1.0f);

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

            ball._abilityStat = _ballStat._abilityStat;

            Vector3 dir = transform.forward;
            Quaternion quat = Quaternion.Euler(0, UnityEngine.Random.Range(-ball.GetReflectValue(), ball.GetReflectValue()), 0);
            dir.y = 0;
            dir = quat * dir;

            if (ball.IsCanBind())
            {
                if (_isParring == false)
                {
                    if(ball._ownerEnum == BallOwner.Player)
                    {
                        _lacketCurHP -= 1;
                    }
                    else
                    {
                        _lacketCurHP -= 2;
                    }

                    //Debug.Log(ball.BallDamage());
                    CameraManager.Instance.Shake(12, 0.12f);
                    ScreenEffectManager.Instance.SetChromaticAberration(1, 0.12f);
                    //_currentParringCooldown = 0f;
                }
                else
                {
                    //// 1. 내 위치 가져오기
                    //Vector3 myPosition = transform.position;
                    //
                    //// 2. 모든 EnemyObject 가져오기
                    //List<EnemyObject> enemies = FindObjectsByType<EnemyObject>(FindObjectsSortMode.None).OrderBy(enemy => Vector3.Distance(myPosition, enemy.transform.position)).ToList();
                    //
                    //dir = (enemies[0].transform.position - transform.position).normalized;
                    //dir.y = 0;
                    //Debug.Log(dir);
                    //ball.Debuff(StatEnum.SPEED, MathType.Plus, -30, 0.9f);
                    Action<BallSystem> parring = null;

                    foreach (var item in _parringSkillAbility)
                    {
                        item.SettingAction(ref parring, _ballStat);
                    }

                    parring?.Invoke(ball);

                    

                    _currentParringCooldown = 0f;
                    ScreenEffectManager.Instance.SetChromaticAberration(6, 0.12f);
                    CameraManager.Instance.Shake(7, 0.12f);
                }
            }
            




            //ball._abilityStat = 

            Action<BallSystem> bss = null;

            foreach(var item in _lacketHitskillAbility)
            {
                item.SettingAction(ref bss, _ballStat);
            }

            bss?.Invoke(ball);

            Action<BallSystem> bss1 = null;

            foreach (var item in _ballHitSkillAbility)
            {
                item.SettingAction(ref bss1, _ballStat);
            }

            Action<BallSystem> updateBall = null;

            foreach (var item in _ballUpdateSkillAbility)
            {
                item.SettingAction(ref updateBall, _ballStat);
            }

            StartCoroutine(WaiterHit());
            ball.Input(dir, BallOwner.Player, (tls) =>
            {
                bss1?.Invoke(ball);
            }, default, updateBall);
        }


        if (_originHP < 0)
        {
            if (_hpCoroutine == null)
                _hpCoroutine = StartCoroutine(LacketHPReturn());
        }
        GameManager.Instance.HUDCanvas.UpdateShieldUI();
    }

    public void HitEvent(float dmg)
    {
        _lacketCurHP -= dmg;
        GameManager.Instance.HUDCanvas.UpdateShieldUI();
    }

    public void RefreshStat(ObjectSystem obj, ObjectSystem _ballStat, List<SkillAbility> Hiting, List<SkillAbility> ball, List<SkillAbility> update, List<SkillAbility> parring)
    {
        _abilityStat = obj._abilityStat;

        _lacketCurHP = _lacketMaxHP;
        this._ballStat = _ballStat;


        _lacketHitskillAbility = Hiting;
        _ballHitSkillAbility = ball;
        _ballUpdateSkillAbility = update;
        _parringSkillAbility = parring;
        // 값변환 얜 기본값 넣어줘야됨
        transform.localScale = new Vector3(GetSizeValue(),1f, 1);
        //GetComponent<BoxColliderCast>()._box.size = transform.localScale;
    }

    IEnumerator WaiterHit()
    {
    
        yield return new WaitForSeconds(0.3f);
        GameObject.FindObjectOfType<BallSystem>().ResetCollision();
    }
}
