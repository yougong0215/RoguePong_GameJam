using System;
using System.Collections;
using UnityEngine;

public class WaitNode : ActionNode
{
    private float waitTime;
    private float startTime;
    bool isStart = false;
    Action isEnd;

    public WaitNode(float waitTime, Action isEnd)
    {
        this.waitTime = waitTime;
        this.isEnd = isEnd;
    }

    public override bool Execute()
    {
        Debug.Log($"{Time.time - startTime} > {waitTime}");
        StartWait();

        if (Time.time - startTime >= waitTime)
        {
            isStart = false;
            isEnd.Invoke();
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