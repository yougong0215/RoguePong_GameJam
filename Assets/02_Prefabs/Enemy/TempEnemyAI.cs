using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempEnemyAI : AISetter
{
    protected override void AISetting()
    {

        Sequence NormalAttackSeq = new();

        AttackNode attackNode = new AttackNode(transform, TempAttackOne);
        WaitNode waitAttack = new WaitNode(2f, () => { attackNode.StartInvoke(); StopExamine(); });


        Sequence MoveSeq = new();

        MoveNode moveNode = new MoveNode(NavmeshAgent);
        WaitNode waitTime = new WaitNode(1f, () => { moveNode.Move(transform, new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * Random.Range(2f, 6f)); });


        NormalAttackSeq.AddNode(waitAttack);
        NormalAttackSeq.AddNode(attackNode);

        MoveSeq.AddNode(waitTime);
        MoveSeq.AddNode(moveNode);


        rootNode.AddNode(NormalAttackSeq);
        rootNode.AddNode(MoveSeq);

        StartExamine();
    }

    public IEnumerator TempAttackOne()
    {
            
        int[] to = { -30, 0, 30 };
        
        for (int i = 0; i < 12; i++)
        {


            for (int j = 0; j < 3; j++)
            {
                BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
                bs.transform.position = transform.position;

                Vector3 forward = transform.forward;
                Quaternion rotation = Quaternion.Euler(0, to[j], 0);
                Vector3 dir = rotation * forward;
                bs.Shoot(dir, self);

                yield return null;
                
            }

            yield return new WaitForSeconds(0.15f);
        }

        StartExamine();

    }

    public override void UpdateInvoke()
    {
        Vector3 v = GameManager.Instance.Player.transform.position;

        v.y = 0;
        transform.rotation = Quaternion.LookRotation(v);
    }
}
