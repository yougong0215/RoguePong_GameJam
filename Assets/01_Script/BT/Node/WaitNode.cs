using System.Collections;
using UnityEngine;

public class WaitNode : ActionNode
{
    private float waitTime;
    private float startTime;
    bool isStart = false;

    public WaitNode(float waitTime)
    {
        this.waitTime = waitTime;
    }

    public override bool Execute()
    {
        StartWait();

        if (Time.time - startTime >= waitTime)
        {
            isStart = false;
            return true;
        }
        return false;
    }

    public void StartWait()
    {
        if(isStart == false)
        { 
            isStart = true;
            startTime = Time.time;
        }
    }
}