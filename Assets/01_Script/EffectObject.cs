using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : PoolAble
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        
    }

    public override void ResetPool()
    {
        base.ResetPool();
        if(_particleSystem == null )
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        _particleSystem.Play();
    }

    private void Update()
    {
        if(_particleSystem.isStopped)
        {
            PoolManager.Instance.Push(this);
        }
    }
}
