using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, HitModule
{
    public GameObject bombEffect;
    public void HitBall(BallSystem bss)
    {

        var effect = Instantiate(bombEffect);
        effect.transform.position = gameObject.transform.position;
        effect.transform.localScale = new Vector3(2, 2, 2);
        bss._abilityStat._addSpeed += 10f;
        bss.WallCollisionItem(GetComponent<Collider>());
        bss.RefreshTime();
        Destroy(effect, 4f);
        Destroy(gameObject, 0.1f);
    }
}
