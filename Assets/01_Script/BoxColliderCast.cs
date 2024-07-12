using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxColliderCast : ColliderCast
{

	public BoxCollider _box;
	[Header("Info")]
	public bool _useScale = false;
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

    private void Start()
    {
        if(Owner == null)
		{
			Owner = gameObject.transform;
		}
    }

    // Update is called once per frame
    public override Collider[] ReturnColliders()
    {

		if(_useScale)
			return Physics.OverlapBox(transform.position, transform.localScale/2, Owner.rotation, Layer);
		else
        {
            return Physics.OverlapBox(transform.position, _box.size /2, Owner.rotation, Layer);

        }
    }

    private void OnDrawGizmos()
    {
        if (_box == null) return;

        Gizmos.color = Color.red;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, Owner.rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, _box.size);
    }

}
