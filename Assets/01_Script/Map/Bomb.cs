using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, HitModule
{
    public void HitBall(BallSystem bss)
    {
        bss._abilityStat._addSpeed += 10f;
        bss.WallCollisionItem(GetComponent<Collider>());
        bss.RefreshTime();
        Destroy(gameObject);
    }
}
