using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLacketHit : HitModule
{
    [Header("LacketHP")]
    [SerializeField] float _lacketHP = 1000;
    [SerializeField] float _laketReviveTime = 2f;


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
        Debug.Log("패링!!");
        _currentParringCooldown = _parringTime;
        _isParring = true;
        yield return new WaitForSeconds(t);
        Debug.Log("취소");
        _isParring = false;        
    }

    IEnumerator LacketHPReturn()
    {
        yield return new WaitForSeconds(_laketReviveTime);
        _lacketHP = 1000;
        _co = null;
    }

    public override void HitBall(BallSystem ball)
    {
        if(ball.IsCanBind() && _lacketHP > 0)
        {
            if(_isParring == false)
                _lacketHP -= ball.BallDamage();
            Debug.Log(ball.BallDamage());

        }

        if(_lacketHP > 0)
        {
            Vector3 dir = transform.forward;
            dir.y = 0;

            ball.Input(dir, (cols) =>
            {
                if (cols.TryGetComponent<HitModule>(out HitModule ms))
                {
                    // 관련 기믹
                    
                }
            });
        }
        else
        {
            if (_co == null)
                _co = StartCoroutine( LacketHPReturn());
        }

    }
}
