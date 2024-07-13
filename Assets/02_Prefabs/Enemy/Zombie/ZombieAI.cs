using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : AISetter
{
    [SerializeField] ColliderCast _cols;
    public override void UpdateInvoke()
    {
        Vector3 v = (GameManager.Instance.Player.transform.position - transform.position);

        v.y = 0;
        
        transform.rotation = Quaternion.LookRotation(v.normalized);
    }

    protected override void AISetting()
    {

        _cols.Now(transform, (pl) => { if (pl.TryGetComponent<PlayerSystem>(out PlayerSystem ps)) { ps.HitEvent(1); } });
        
        
        Sequence MoveSeq = new();

        MoveNode moveNode = new MoveNode(NavmeshAgent);
        WaitNode waitTime = new WaitNode(0.1f, () => 
        {
            NavmeshAgent.speed = self.GetSpeedValue();
            moveNode.Move(transform, GameManager.Instance.Player.transform.position );
            _cols.ClearDic();
        });

        MoveSeq.AddNode(waitTime);
        MoveSeq.AddNode(moveNode);

        rootNode.AddNode(MoveSeq);

        StartExamine();

    }

}
