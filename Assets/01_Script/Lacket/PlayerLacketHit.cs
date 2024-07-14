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
    float _parringContinusTime = 0.4f;

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
            GameManager.Instance.HUDCanvas.UpdateParyingCoolUI(1 -(_currentParringCooldown / _parringTime)); 
        }
    }

    IEnumerator ParringTime(float t)
    {
        _currentParringCooldown = _parringTime;
        SoundManager.Instance.PlayGlobal("SFX0747");
        _isParring = true;

        yield return new WaitForSeconds(t);

        Debug.Log("���");
        _isParring = false;
        _parringCoroutine = null;
    }

    IEnumerator LacketHPReturn()
    {
        transform.localPosition = new Vector3(0, -5, 1);
        yield return new WaitForSeconds(_laketReviveTime);
        _lacketCurHP = _lacketMaxHP;
        transform.localPosition = new Vector3(0, 0, 1);
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
                    }
                    else
                    {
                        _lacketCurHP -= 1;
                    }
                    SoundManager.Instance.PlayGlobal("Cartoon_car_hit");

                    //Debug.Log(ball.BallDamage());
                    CameraManager.Instance.Shake(12, 0.12f);
                    ScreenEffectManager.Instance.SetChromaticAberration(1, 0.12f);
                    //_currentParringCooldown = 0f;
                }
                else
                {
                    //// 1. �� ��ġ ��������
                    //Vector3 myPosition = transform.position;
                    //
                    //// 2. ��� EnemyObject ��������
                    //List<EnemyObject> enemies = FindObjectsByType<EnemyObject>(FindObjectsSortMode.None).OrderBy(enemy => Vector3.Distance(myPosition, enemy.transform.position)).ToList();
                    //
                    //dir = (enemies[0].transform.position - transform.position).normalized;
                    //dir.y = 0;
                    //Debug.Log(dir);
                    ball.Debuff(StatEnum.SPEED, MathType.Plus, -30, 1.8f);
                    Action<BallSystem> parring = null;

                    foreach (var item in _parringSkillAbility)
                    {
                        item.SettingAction(ref parring, _ballStat);
                    }

                    parring?.Invoke(ball);

                    GameManager.Instance.Player.HitEvent(-2);
                    var eff = PoolManager.Instance.Pop("Mack_FX");
                    eff.transform.position = transform.position;
                    eff.transform.forward = transform.forward;

                    _currentParringCooldown = 0f;
                    ScreenEffectManager.Instance.SetChromaticAberration(6, 0.12f);
                    CameraManager.Instance.Shake(7, 0.12f);
                    SoundManager.Instance.PlayGlobal("SFX2580");
                    _lacketCurHP += 2;

                    if (_lacketCurHP > _lacketMaxHP)
                    {
                        _lacketCurHP = _lacketMaxHP;
                    }
                }
            }
            




            //ball._abilityStat = 

            Action<BallSystem> bss = null;

            foreach(var item in _lacketHitskillAbility)
            {
                if(item != null)
                item.SettingAction(ref bss, _ballStat);
            }

            bss?.Invoke(ball);

            Action<BallSystem> bss1 = null;

            foreach (var item in _ballHitSkillAbility)
            {
                if (item != null)
                    item.SettingAction(ref bss1, _ballStat);
            }

            Action<BallSystem> updateBall = null;

            foreach (var item in _ballUpdateSkillAbility)
            {
                if (item != null)
                    item.SettingAction(ref updateBall, _ballStat);
            }

            StartCoroutine(WaiterHit());
            ball.Input(dir, BallOwner.Player, (tls) =>
            {
                bss1?.Invoke(ball);
            }, default, updateBall);
        }


        if (_lacketCurHP < 0)
        {
            if (_hpCoroutine == null)
                _hpCoroutine = StartCoroutine(LacketHPReturn());
        }
        GameManager.Instance.HUDCanvas.UpdateShieldUI();
    }

    public void HitEvent(float dmg, GameObject hit)
    {
        if(_lacketCurHP > 0)
        {
            if (hit.TryGetComponent<BulletBase>(out BulletBase bs))
            {
                PoolManager.Instance.Push(bs);
            }
        }


        if (_isParring == false)
        {
            _lacketCurHP -= dmg;
            GameManager.Instance.HUDCanvas.UpdateShieldUI();
        }

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
        // ����ȯ �� �⺻�� �־���ߵ�
        transform.localScale = new Vector3(GetSizeValue(),1f, 1);
        //GetComponent<BoxColliderCast>()._box.size = transform.localScale;
    }

    IEnumerator WaiterHit()
    {
    
        yield return new WaitForSeconds(0.3f);
        GameObject.FindObjectOfType<BallSystem>()?.ResetCollision();
    }
}
