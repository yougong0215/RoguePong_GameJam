using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxColliderCast : ColliderCast
{
	
	private BoxCollider _box;
	private void Awake()
	{
		try
		{
			_box = GetComponent<BoxCollider>();
			_box.isTrigger = true;
		}
		catch
		{
			Debug.LogError($"Is it not Proper Collider! : BoxCollider => {gameObject.name}");
		}

		if (transform.localScale != Vector3.one)
		{
			Debug.LogWarning($"Object : {gameObject.name} is Not Scale Vector.oen(1,1,1)");
            
		}
	}

	// Update is called once per frame
    public override Collider[] ReturnColliders()
    {
	    return Physics.OverlapBox(transform.position, _box.size, _quaternion, Layer);
    }

}
