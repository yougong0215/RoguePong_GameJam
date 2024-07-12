using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class AISetter : MonoBehaviour
{

    EnemyObject _obj;
    protected EnemyObject self
    {
        get
        {
            if(_obj==null)
            {
                _obj = GetComponent<EnemyObject>();
            }
            return _obj;
        }

    }

    NavMeshAgent _nav;
    protected NavMeshAgent NavmeshAgent
    {
        get
        {
            if (_nav == null)
            { _nav = GetComponent<NavMeshAgent>(); }
            return _nav;
        }
    }
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

    public void StartExamine()
    {
        _isRunning = true;
    }

    public void StopExamine()
    {
        _isRunning=false;
    }


    void Update()
    {
        if(_isRunning)
        {
            rootNode.Execute();
        }
        UpdateInvoke();
    }

    public abstract void UpdateInvoke();
}
