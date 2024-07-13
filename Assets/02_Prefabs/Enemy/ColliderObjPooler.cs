using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderObjPooler : PoolAble
{
    [SerializeField] ColliderCast _cols;
    public ColliderCast CollCac
    {
        get
        {
            if(_cols == null)
                _cols = GetComponent<ColliderCast>();
            return _cols;
        }
    }
}
