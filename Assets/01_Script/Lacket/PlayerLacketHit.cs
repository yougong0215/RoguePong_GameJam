using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLacketHit : HitModule
{
    [Header("LacketHP")]
    [SerializeField] public float _lacketMaxHP = 1000;
    [SerializeField] public float _laketReviveTime = 2f;


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
        Debug.Log("�и�!!");
        _currentParringCooldown = _parringTime;
        _isParring = true;
        yield return new WaitForSeconds(t);
        Debug.Log("���");
        _isParring = false;
        _parringCoroutine = null;
    }

    IEnumerator LacketHPReturn()
    {
        yield return new WaitForSeconds(_laketReviveTime);
        _originHP = _lacketMaxHP;
        _hpCoroutine = null;
    }

    public override void HitBall(BallSystem ball)
    {

        if(_originHP > 0)
        {
            if(ball.IsCanBind())
            {
                if(_isParring == false)
                {
                    _originHP -= ball.BallDamage();
                    Debug.Log(ball.BallDamage());
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
            StartCoroutine(WaiterHit());
            ball.Input(dir, (cols) =>
            {
                
                if (cols.TryGetComponent<HitModule>(out HitModule ms))
                {
                    // ���� ���
                    
                }
            });
        }
        else
        {
            if (_hpCoroutine == null)
                _hpCoroutine = StartCoroutine( LacketHPReturn());
        }

    }

    public void RefreshStat(ObjectSystem obj, ObjectSystem _ballStat)
    {
        _abilityStat = obj._abilityStat;

        _lacketMaxHP = _originHP;
        this._ballStat = _ballStat;
        // ����ȯ �� �⺻�� �־���ߵ�
        transform.localScale = new Vector3(GetSizeValue() * 4.5f, 1.3f, 0.4f);
        //GetComponent<BoxColliderCast>()._box.size = transform.localScale;
    }

    IEnumerator WaiterHit()
    {
    
        yield return new WaitForSeconds(0.4f);
        GameObject.FindObjectOfType<BallSystem>().ResetCollision();
    }
}
