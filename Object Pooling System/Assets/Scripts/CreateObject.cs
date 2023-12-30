using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    ObjectPooling poolingSystem;

    private void Start()
    {
        poolingSystem = ObjectPooling.Instance;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject obj = poolingSystem.Pool.Get();
            obj.transform.position = Vector3.zero;
        }
    }
}
