using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public int SpawnCount;
    [SerializeField]
    public string Spawns;
    [SerializeField]
    public float SpawnTerm;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(doWork());
    }

    private IEnumerator doWork()
    {
        for (int i = 0; i > SpawnCount; ++i)
        {
            //Spawns Enemy??
            yield return new WaitForSeconds(SpawnTerm);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
