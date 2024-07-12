using System.Collections;
using UnityEngine;

public class WaitNode : ActionNode
{
    private float waitTime;
    private float startTime;

    public WaitNode(float waitTime)
    {
        this.waitTime = waitTime;
    }

    public override bool Execute()
    {
        if (Time.time - startTime >= waitTime)
        {
            return true;
        }
        return false;
    }

    public void StartWait()
    {
        startTime = Time.time;
    }
}