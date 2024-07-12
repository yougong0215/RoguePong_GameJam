using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, HitModule
{
    private BoxColliderCast boxcast;
    // Start is called before the first frame update
    void Start()
    {
        boxcast = GetComponent<BoxColliderCast>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitBall(BallSystem bss)
    {
        bss._abilityStat._addSpeed += 10f;
        bss.WallCollisionItem(GetComponent<Collider>());
        bss.RefreshTime();
        Destroy(gameObject);
    }
}
