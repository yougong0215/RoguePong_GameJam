using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNode : ActionNode
{
    NavMeshAgent _agent;
    Vector3 dir;
    public MoveNode(NavMeshAgent ag)
    {
        _agent = ag;
    }

    public void Move(Transform tls,Vector3 dir)
    {
        UnityEngine.AI.NavMesh.SamplePosition(tls.position + dir, out UnityEngine.AI.NavMeshHit hit, 1f, UnityEngine.AI.NavMesh.AllAreas);
        _agent.SetDestination(hit.position);
    }

    public override bool Execute()
    {
        return true;
    }
}
