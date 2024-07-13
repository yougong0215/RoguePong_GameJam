using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour
{

    private EnemyObject enemyObj;
    private Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        enemyObj = transform.parent.GetComponent<EnemyObject>();
        hpSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        hpSlider.value = enemyObj.GetCurrentHP / enemyObj.GetHPValue();
    }
}
