using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string[] Spawns;
    public float SpawnTerm;
    public EnemyData enemyData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(doWork());
    }
        
    private IEnumerator doWork()
    {
        for (int i = 0; i < Spawns.Length; ++i)
        {
            var ed = enemyData.Data.Find(x => x.EnemyName == Spawns[i]);
            if (ed != null)
            {
                var e = Instantiate(ed.EnemyPrefab);
                e.transform.position = transform.position;
            }
            yield return new WaitForSeconds(SpawnTerm);
        }
        Destroy(gameObject);
    }
}
