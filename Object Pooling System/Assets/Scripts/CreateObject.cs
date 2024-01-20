using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private Transform location;

    [SerializeField]
    private Transform parent;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float delay;

    private ObjectPoolingSystem poolingSystem;
    private int action;

    private void Start()
    {
        poolingSystem = ObjectPoolingSystem.Instance;

        poolingSystem.AddPool(obj,
            () =>
            {
                GameObject _obj = Instantiate(obj, parent);
                _obj.GetComponent<Ball>().pool = poolingSystem[obj];
                return _obj;
            },
            (GameObject _obj) =>
            {
                _obj.SetActive(true);
                Ball ball = _obj.GetComponent<Ball>();
                ball.ChangeRandomColor();
                ball.DesrtroyBall(delay);
            },
            (GameObject _obj) => _obj.SetActive(false),
            (GameObject _obj) => Destroy(_obj),
            defualtCapacity: 10,
            maxCount: 20);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject _obj = poolingSystem[obj].Get();
            Ball ball = _obj.GetComponent<Ball>();
            _obj.transform.position = new(0, 0, 0);

            switch (action)
            {
                case 0:
                    _obj.AddComponent<Rigidbody>();
                    ball.move = b => { };
                    break;
                case 1:
                    transform.Rotate(new Vector3(0, speed, 0));
                    ball.location = location.position;
                    ball.move = b =>
                    {                        
                        b.transform.Translate(speed * Time.deltaTime * b.location);
                    };
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
            action = 0;
        else if (Input.GetKeyDown(KeyCode.F2))
            action = 1;

    }
}
