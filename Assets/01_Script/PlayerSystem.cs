using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{

    [SerializeField]
    public float _speed = 4;

    CharacterController _char;

    public bool _isCanMove = false;
    Vector3 forceDir = Vector3.zero;

    Vector3 hitPoint;

    public Vector3 MousePos => hitPoint;


    private void Awake()
    {
        _char = GetComponent<CharacterController>();
    }


    private void Update()
    {

        MouseInput();

        Movement();

    }

    void MouseInput()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            Plane plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out float enter))
            {
                hitPoint = ray.GetPoint(enter);
                hitPoint.y = transform.position.y;
                transform.LookAt(hitPoint);
            }
        }
    }

    void Movement()
    {
        Vector3 vec = Vector3.zero;


        if (Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 ad = MousePos;

            ad.z = ad.y;
            ad.y = 0;

            forceDir = ad.normalized * 128;
        }

        if (forceDir.sqrMagnitude < 0.1f)
        {
            forceDir = Vector3.zero;
        }
        else
        {
            forceDir = Vector3.Lerp(forceDir, Vector3.zero, Time.deltaTime * 10);
        }



        if (_isCanMove || forceDir == Vector3.zero)
            vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * _speed * Time.deltaTime;
        else
            vec = forceDir * Time.deltaTime;



        _char.Move(vec);
    }
}
