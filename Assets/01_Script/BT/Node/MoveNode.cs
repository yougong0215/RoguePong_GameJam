using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNode : ActionNode
{
    NavMeshAgent _agent;
    public MoveNode(NavMeshAgent ag)
    {
        _agent = ag;
    }

    public void Move(Transform tls,Vector3 dir)
    {
        

        UnityEngine.AI.NavMesh.SamplePosition(dir, out UnityEngine.AI.NavMeshHit hit, 5f, UnityEngine.AI.NavMesh.AllAreas);
        if ((hit.position - tls.transform.position).sqrMagnitude > Mathf.Pow(2, 2))
        {
            _agent.isStopped = false;
            _agent.updatePosition = true;
            _agent.SetDestination(hit.position);
        }
        else
        {
            _agent.isStopped = true;
            _agent.updatePosition = false;
            _agent.velocity = Vector3.zero;
            _agent.SetDestination(tls.position);
        }

    }

    public override bool Execute()
    {
        return true;
    }
}
