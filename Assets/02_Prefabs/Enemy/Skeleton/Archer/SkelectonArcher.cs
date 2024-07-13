using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelectonArcher : AISetter
{
    public override void UpdateInvoke()
    {
        Vector3 v = GameManager.Instance.Player.transform.position;

        v.y = 0;
        transform.rotation = Quaternion.LookRotation(v);
    }

    protected override void AISetting()
    {
        Sequence NormalAttackSeq = new();

        AttackNode attackNode = new AttackNode(transform, TempAttackOne);
        WaitNode waitAttack = new WaitNode(3f, () => { attackNode.StartInvoke(); StopExamine(); });


        Sequence MoveSeq = new();

        NavmeshAgent.updateRotation = false;
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


        BulletBase bs = PoolManager.Instance.Pop("TempBullet") as BulletBase;
        bs.transform.position = transform.position;

        Vector3 forward = transform.forward;
        //Quaternion rotation = Quaternion.Euler(0, to[j], 0);
        //Vector3 dir = rotation * forward;
        bs.Shoot(forward, self);

        yield return null;

        StartExamine();

    }

}
