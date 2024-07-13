using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TouchBlock : ObjectSystem,HitModule
{
    public TextMeshPro countText;
    public GameObject cube;
    public int count;
    public Vector3 moveTo;
    private Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = gameObject.transform.position;
        countText.text = count.ToString();
        cube.transform.localScale = transform.parent.gameObject.GetComponent<BoxCollider>().size;
    }

    public IEnumerator moveObject()
    {
        float moveTime = 5f;
        float curMoveTime = 0f;
        while (Vector3.Distance(transform.localPosition, moveTo) > 0)
        {
            curMoveTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(origin, moveTo, curMoveTime / moveTime);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitBall(BallSystem ball)
    {
        if (count > 0)
            --count;
        if (count <= 0)
            StartCoroutine(moveObject());
        countText.text = count.ToString();
    }
}
