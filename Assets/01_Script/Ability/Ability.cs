using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Ability : ScriptableObject
{
    public abstract void GetAbility(ref ObjectSystem ballStat);
}
