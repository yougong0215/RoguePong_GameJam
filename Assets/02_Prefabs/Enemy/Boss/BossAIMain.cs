using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIMain : AISetter
{
    public GameObject pos3;

    public override void UpdateInvoke()
    {
    }

    protected override void AISetting()
    {

        Sequence BoombShot = new();
        AttackNode BombATK = new AttackNode(transform, ShootBigShot);
        WaitNode BombWait = new WaitNode(3f, () => { BombATK.StartInvoke(); StopExamine(); });
        BoombShot.AddNode(BombWait);
        BoombShot.AddNode(BombATK);


        rootNode.AddNode(BoombShot);
        StartExamine();
    }


    public IEnumerator ShootBigShot()
    {
        BulletBase bs = PoolManager.Instance.Pop("BigBullet") as BulletBase;
        bs.transform.position = pos3.transform.position;
        yield return new WaitForSeconds(1.2f);

        Vector3 forward = pos3.transform.forward;
        bs.Shoot(-forward, self, 14, 100, true, () =>
        {
            StartCoroutine(SHootBigShotSide(bs));

        });

        yield return new WaitForSeconds(10f);
        StartExamine();
    }

    IEnumerator SHootBigShotSide(BulletBase bs)
    {
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 36; i++)
            {
                BulletBase v2 = PoolManager.Instance.Pop("TempBullet") as BulletBase;
                v2.transform.position = bs.transform.position;
                Vector3 dir = Quaternion.Euler(0, 10 * i, 0) * bs.transform.forward;
                dir.y = 0;

                v2.Shoot(dir, self);
                yield return null;
            }

        }
    }
}
