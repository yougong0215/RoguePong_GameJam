using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ColliderCast : MonoBehaviour
{

	//[Header("Collider Name")] [SerializeField]
	//private string _name;
//
	//public string Name => _name;
	
	[Header("Collision Layer")]
	[SerializeField] private LayerMask _layer;

    [SerializeField] protected Transform Owner;

	public LayerMask Layer => _layer;
	
	[Header("Already Get Object")][SerializeField] public Dictionary<Collider, bool> CheckDic = new();


	private bool _isRunning = false;
	
	
	private int _attackAbleCount = 0;

	protected Quaternion _quaternion;
	
	public abstract Collider[] ReturnColliders();
	
	public Action<Collider> CastAct;
	private Action<Collider> FirstAct;
	private Action _startCall = null;
	private bool isFirst = false;
	
	protected void Update()
	{
		//Debug.LogError("업데이트 돌긴함");
		if (_isRunning == false)
			return;
		
		if(_attackAbleCount != -1 && CheckDic.Count > _attackAbleCount)
			return;
		
		
		//Debug.LogError("업데이트 들어옴");
		// 생각해 봤는데 어차피 col있는 만큼만 돌아가기 때문에 큰 문제 없음
		foreach (var col in ReturnColliders())
		{


			if (CheckDic.ContainsKey(col))
			
                return;
			
			else
			{
				CheckDic.Clear();

				CheckDic.Add(col, true);

                CastAct?.Invoke(col);

                //Debug.LogError(isFirst + " Fuck Fuck");

                if (isFirst == false)
                {
                    FirstAct?.Invoke(col);
                    isFirst = true;
                }

                //Debug.LogError(col.name);
            }
			
			
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	public void Now(Transform _owner, Action<Collider> act = null, Action<Collider> act2=null, int attackAble = -1, float StartSec = -1, float EndSec = -1)
	{
		this.Owner = _owner;

		_isRunning = false;
		isFirst = false;

		_quaternion = Owner.rotation;
		// CheckDic = new();

		_attackAbleCount = attackAble;
		if(StartSec > 0)
		{
			StartCoroutine(StartSet(StartSec, act, act2));
		}
		else
		{
			//Debug.LogError("시작");
			_isRunning = true;
			if (act != null)
				CastAct = act;
			if (act2 != null)
				FirstAct = act2;
			
		}



		if(EndSec > 0)
		{
			StartCoroutine(EndSet(EndSec));
		}
		
	}

	public void End()
	{
		_isRunning = false;
		
		CheckDic.Clear();
		CastAct = null;

	}

	IEnumerator StartSet(float t, Action<Collider> act = null, Action<Collider> act2 = null)
	{
		yield return new WaitForSeconds(t);
		CheckDic.Clear();
		_isRunning = true;
		if (act != null)
			CastAct = act;
		if (act2 != null)
			FirstAct = act2;

	}

	IEnumerator EndSet(float t)
	{
		yield return new WaitForSeconds(t);
		End();
	}
	
}
