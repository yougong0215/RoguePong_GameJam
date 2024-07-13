using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBossAI : AISetter
{
    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;


    protected override void AISetting()
    {
        Sequence BoombShot = new();
        AttackNode BombATK = new AttackNode(transform, ShootBigShot);
        WaitNode BombWait = new WaitNode(3f, () => { BombATK.StartInvoke(); StopExamine(); });
        BoombShot.AddNode(BombWait);
        BoombShot.AddNode(BombATK);


        Sequence NormalAttackSeq = new();

        AttackNode ShootAway = new AttackNode(transform, TempAttackOne);
        WaitNode ShootAwayWait = new WaitNode(4f, () => { ShootAway.StartInvoke(); StopExamine(); });

        NormalAttackSeq.AddNode(ShootAwayWait);
        NormalAttackSeq.AddNode(ShootAway);

        rootNode.AddNode(BoombShot);
        rootNode.AddNode(NormalAttackSeq);

        StartExamine();
    }

    public IEnumerator ShootBigShot()
    {
        BulletBase bs = PoolManager.Instance.Pop("BigBullet") as BulletBase;
        bs.transform.position = pos3.transform.position;
        yield return new WaitForSeconds(1.2f);

        Vector3 forward = pos3.transform.forward;
        bs.Shoot(-forward, self, 0.2f, 100,true, () =>
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
                dir.y = 1;

                v2.Shoot(dir, self); 
                yield return null;
            }

        }
    }


    public IEnumerator TempAttackOne()
    {

        int[] to = { -30, -15, 0, 15, 30 };

        for (int i = 0; i < 12; i++)
        {

            
            for (int j = 0; j < 5; j++)
            {
                BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
                bs.transform.position = pos1.transform.position;

                Vector3 forward = pos1.transform.forward;
                Quaternion rotation = Quaternion.Euler(0, to[j], 0);
                Vector3 dir = rotation * forward;
                bs.Shoot(dir, self);

                yield return null;

            }

            for (int j = 0; j < 5; j++)
            {
                BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
                bs.transform.position = pos2.transform.position;

                Vector3 forward = pos2.transform.forward;
                Quaternion rotation = Quaternion.Euler(0, to[j], 0);
                Vector3 dir = rotation * forward;
                bs.Shoot(dir, self);

                yield return null;
            }


            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(5f);

        StartExamine();

    }
    public override void UpdateInvoke()
    {
        Vector3 v = GameManager.Instance.Player.transform.position;
        v.y = 1;
        pos1.transform.rotation = Quaternion.LookRotation(v - pos1.transform.position);
        pos2.transform.rotation = Quaternion.LookRotation(v - pos2.transform.position);
    }
}
