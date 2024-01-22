using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Queue<GameObject> pool;

    private Func<GameObject> create;
    private Action<GameObject> get;
    private Action<GameObject> release;
    private Action<GameObject> delete;
    private int maxCount;

    public Pool(Func<GameObject> create, 
                Action<GameObject> get,
                Action<GameObject> release,
                Action<GameObject> delete,
                int defualtCapacity,
                int maxCount)
    {
        pool = new Queue<GameObject>(defualtCapacity);

        this.create = create;
        this.get = get;
        this.release = release;
        this.delete = delete;
        this.maxCount = maxCount;
    }

    public GameObject Get()
    {
        if (!pool.TryDequeue(out GameObject result))
        {
            result = create();
        }

        get(result);

        return result;

    }

    public void Release(GameObject obj)
    {
        if (pool.Count < maxCount)
        {
            pool.Enqueue(obj); 
            release(obj);
        }
        else
        {
            delete(obj);
        }
    }
}
