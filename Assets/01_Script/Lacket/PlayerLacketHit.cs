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
    float _parringTime = 1f;
    float _parringContinusTime = 0.125f;

    Coroutine _co;


    public void Update()
    {

        if(_currentParringCooldown <= 0)
        {
            if(Input.GetMouseButtonDown(0))
                StartCoroutine(ParringTime(_parringContinusTime));
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
    }

    IEnumerator LacketHPReturn()
    {
        yield return new WaitForSeconds(_laketReviveTime);
        _originHP = _lacketMaxHP;
        _co = null;
    }

    public override void HitBall(BallSystem ball)
    {
        if(ball.IsCanBind() && _lacketMaxHP > 0)
        {
            if(_isParring == false)
                _lacketMaxHP -= ball.BallDamage();
            Debug.Log(ball.BallDamage());

        }

        if(_lacketMaxHP > 0)
        {
            Vector3 dir = transform.forward;
            dir.y = 0;

            ball.Input(dir, (cols) =>
            {
                if (cols.TryGetComponent<HitModule>(out HitModule ms))
                {
                    // °ü·Ã ±â¹Í
                    
                }
            });
        }
        else
        {
            if (_co == null)
                _co = StartCoroutine( LacketHPReturn());
        }

    }

    public void RefreshStat(ObjectSystem obj)
    {
        _abilityStat = obj._abilityStat;

        _lacketMaxHP = _originHP;

        // °ªº¯È¯ ¾ë ±âº»°ª ³Ö¾îÁà¾ßµÊ
        transform.localScale = new Vector3(GetSizeValue() * 4.5f, GetSizeValue() * 1.3f, GetSizeValue() * 0.4f);
    }
}
