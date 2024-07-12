using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TouchBlock : ObjectSystem,HitModule
{
    public TextMeshPro countText;
    public int count;
    public Vector3 moveTo;
    private BoxColliderCast boxcast;
    private Vector3 origin;
    private bool canCollide = true;
    // Start is called before the first frame update
    void Start()
    {
        boxcast = GetComponent<BoxColliderCast>();
        origin = gameObject.transform.position;
        countText.text = count.ToString();
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
        if (boxcast.ReturnColliders().Length > 0)
        {
            TriggerEnter(boxcast.ReturnColliders()[0]);
        }
    }

    IEnumerator waitForSec(float sec)
    {
        canCollide = false;
        yield return new WaitForSeconds(sec);
        canCollide = true;
    }

    private void TriggerEnter(Collider other)
    {
        //if (other.TryGetComponent<BallSystem>(out BallSystem bss))
        //{
        //    if (canCollide)
        //    {
        //        if (count > 0)
        //            --count;
        //        if (count <= 0)
        //            StartCoroutine(moveObject());
        //        countText.text = count.ToString();
        //        StartCoroutine(waitForSec(0.5f));
        //    }
        //    //bss.WallCollisionItem(GetComponent<Collider>());
        //    //bss.RefreshTime();
        //}
    }

    public void HitBall(BallSystem ball)
    {
        if (canCollide)
        {
            if (count > 0)
                --count;
            if (count <= 0)
                StartCoroutine(moveObject());
            countText.text = count.ToString();
            StartCoroutine(waitForSec(0.5f));
        }
    }
}
