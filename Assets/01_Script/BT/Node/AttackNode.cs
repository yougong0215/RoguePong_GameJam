using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : ActionNode
{
    Transform _my;
    Func<bool> function;


    public AttackNode(Transform me, Func<bool> func = null)
    {
        _my = me;
        function = func;
    }


    public override bool Execute()
    {
        return function();
    }
}
