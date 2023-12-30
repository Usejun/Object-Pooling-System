using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private Transform position;

    private ObjectPoolingSystem poolingSystem;

    private void Start()
    {
        poolingSystem = ObjectPoolingSystem.Instance;

        poolingSystem.AddPool(obj,
            () =>
            {
                GameObject _obj = Instantiate(obj, position);
                _obj.GetComponent<Ball>().pool = poolingSystem[obj];
                return _obj;
            },
            (GameObject _obj) =>
            {
                _obj.SetActive(true);
                Ball ball = _obj.GetComponent<Ball>();
                ball.ChangeRandomColor();
                ball.DesrtroyBall();
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
            _obj.transform.position = Vector3.zero;
        }
    }
}
