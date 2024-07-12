using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : ActionNode
{
    Transform _my;
    Func<IEnumerator> function;

    bool isStart =false;

    Coroutine _co = null;


    public AttackNode(Transform me, Func<IEnumerator> func = null)
    {
        _my = me;
        function = func;
    }


    public override bool Execute()
    {
        if(isStart)
        {
            _co =_my.GetComponent<MonoBehaviour>().StartCoroutine(function());
            isStart = false;
        }

        return _co == null;
    }

    public void StartInvoke()
    {
        isStart = true;
    }
}
