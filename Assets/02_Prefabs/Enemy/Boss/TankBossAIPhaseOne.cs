using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBossAIPhaseOne : AISetter
{
    public GameObject ShootPos;

    public Transform _sommonPos1;
    public string _sommonName1;
    public Transform _sommonPos2;
    public string _sommonName2;
    public Transform _sommonPos3;
    public string _sommonName3;

    protected override void AISetting()
    {

        Sequence SummonPatton = new();
        AttackNode _summonEnemy = new AttackNode(transform, SummonEnemy);
        WaitNode SommonWait = new WaitNode(3f, () => { _summonEnemy.StartInvoke(); });

        SummonPatton.AddNode(SommonWait);
        SummonPatton.AddNode(_summonEnemy);

        //Sequence BoombShot = new();
        //AttackNode BombATK = new AttackNode(transform, ShootBigShot);
        //WaitNode BombWait = new WaitNode(3f, () => { BombATK.StartInvoke(); StopExamine(); });
        //BoombShot.AddNode(BombWait);
        //BoombShot.AddNode(BombATK);


        Sequence NormalAttackSeq = new();

        AttackNode ShootAway = new AttackNode(transform, TempAttackOne);
        WaitNode ShootAwayWait = new WaitNode(10f, () => { ShootAway.StartInvoke(); StopExamine(); });

        NormalAttackSeq.AddNode(ShootAwayWait);
        NormalAttackSeq.AddNode(ShootAway);

        rootNode.AddNode(SummonPatton);
        rootNode.AddNode(NormalAttackSeq);

        StartExamine();
    }

    IEnumerator SummonEnemy()
    {
        yield return null;
        EnemyObject eo1 = PoolManager.Instance.Pop(_sommonName1) as EnemyObject;
        eo1.transform.position = _sommonPos1.position;
        yield return null;
        EnemyObject eo2 = PoolManager.Instance.Pop(_sommonName2) as EnemyObject;
        eo2.transform.position = _sommonPos2.position;
        yield return null;
        EnemyObject eo3 = PoolManager.Instance.Pop(_sommonName3) as EnemyObject;
        eo3.transform.position = _sommonPos3.position;
    }
    /*
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
    }*/


    public IEnumerator TempAttackOne()
    {

        int[] to = { -30, -15, 0, 15, 30 };

        for (int i = 0; i < 12; i++)
        {

            
            for (int j = 0; j < 5; j++)
            {
                BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
                bs.transform.position = ShootPos.transform.position;

                Vector3 forward = ShootPos.transform.forward;
                Quaternion rotation = Quaternion.Euler(0, to[j], 0);
                Vector3 dir = rotation * forward;
                bs.Shoot(dir, self);

                yield return null;

            }

            //for (int j = 0; j < 5; j++)
            //{
            //    BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
            //    bs.transform.position = pos2.transform.position;
            //
            //    Vector3 forward = pos2.transform.forward;
            //    Quaternion rotation = Quaternion.Euler(0, to[j], 0);
            //    Vector3 dir = rotation * forward;
            //    bs.Shoot(dir, self);
            //
            //    yield return null;
            //}


            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(5f);

        StartExamine();

    }
    public override void UpdateInvoke()
    {
        Vector3 v = GameManager.Instance.Player.transform.position;
        v.y = 1;
        ShootPos.transform.rotation = Quaternion.LookRotation(v - ShootPos.transform.position);
    }
}
