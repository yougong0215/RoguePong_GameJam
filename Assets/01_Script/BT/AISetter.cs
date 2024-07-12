using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AISetter : MonoBehaviour
{
    protected Selector rootNode = null;

    public bool _isRunning = false;

    private void Awake()
    {
        _isRunning = false;
        rootNode = new Selector();
    }

    private void Start()
    {
        AISetting();
    }

    protected abstract void AISetting();


    void Update()
    {
        if(_isRunning)
        {
            rootNode.Execute();
        }
    }
}
