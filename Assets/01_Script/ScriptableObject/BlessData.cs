using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class BlessData : ScriptableObject
{
    public float Probability;
    public float Value;
    public abstract void BlessDoing(float val);


}