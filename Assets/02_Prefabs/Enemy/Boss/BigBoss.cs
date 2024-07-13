using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : EnemyObject
{
    public override void HitBall(BallSystem ball)
    {
        if (ball.IsCanBind() && ball._ownerEnum != BallOwner.Enemy)
            _currentHP -= ball.BallDamage();

        Vector3 dir = ball.Dir;

        dir.z *= -1;
        ball.Input(dir, BallOwner.Enemy);
    }
}
