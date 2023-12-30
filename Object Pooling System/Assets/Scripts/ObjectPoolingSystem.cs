using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingSystem : Singleton<ObjectPoolingSystem>
{
    Dictionary<string, Pool> pools;

    public override void Awake()
    {
        base.Awake();

        pools = new Dictionary<string, Pool>();
    }

    public void AddPool(GameObject obj,
                        Func<GameObject> create,
                        Action<GameObject> get,
                        Action<GameObject> release,
                        Action<GameObject> delete,
                        int defualtCapacity,
                        int maxCount)                      
    {
        string name = obj.name;

        if (!pools.ContainsKey(name))
        {
            pools.Add(name, new Pool(create, get, release, delete, defualtCapacity, maxCount));
        }
    }

    public void RemovePool(GameObject obj)
    {
        string name = obj.name;

        if (pools.ContainsKey(name))
        {
            pools.Remove(name);
        }
    }

    public Pool this[GameObject obj]
    {
        get
        {
            if (pools.TryGetValue(obj.name, out Pool pool))
                return pool;

            throw new PoolException("No pools of this thing have been created.");
            
        }
    }
}

public class PoolException : Exception
{
    public PoolException() { }
    public PoolException(string message) : base(message) { }
    public PoolException(string message, Exception inner) : base(message, inner) { }
    protected PoolException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
