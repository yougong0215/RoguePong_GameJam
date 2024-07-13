using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : ObjectSystem, HitModule
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _originSpeed * Time.deltaTime, 0);
    }

    IEnumerator Shoot()
    {
        for (int j = 0; j < 3; j++)
        {
            BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
            bs.transform.position = transform.position;

            Vector3 forward = transform.forward;
            Quaternion rotation = Quaternion.Euler(transform.forward.x, transform.forward.y, transform.forward.z);
            Vector3 dir = rotation * forward;
            bs.Shoot(dir, this);

            yield return new WaitForSeconds(0.15f);
        }
    }

    public void HitBall(BallSystem bss)
    {
        StartCoroutine(Shoot());
    }
}
