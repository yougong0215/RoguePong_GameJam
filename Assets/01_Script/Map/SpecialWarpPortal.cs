using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWarpPortal : WarpPortal
{
    protected override void Start()
    {
        colliderCast = GetComponent<BoxColliderCast>();
        GameManager.Instance.AssignSpeicalPortal(gameObject);
        Debug.LogError("����� ��Ż ��ϵ�");
    }
    protected override void ChangeMap()
    {
        if (GameManager.Instance.isCleared)
        {

        }
    }

}
