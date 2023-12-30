using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [SerializeField]
    private GameObject original;

    [SerializeField]
    private int defaultCapacity;

    [SerializeField]
    private int maxCapacity;

    public IObjectPool<GameObject> Pool { get; private set; }

    public override void Awake()
    {
        base.Awake();

        Pool = new ObjectPool<GameObject>(CreatePoolObject, GetPoolObject, ReleasePoolObject, DestroyPoolObject, true, defaultCapacity, maxCapacity);

        for (int i = 0; i < defaultCapacity; i++)
        {
            Ball ball = CreatePoolObject().GetComponent<Ball>();
            ball.pool.Release(ball.gameObject);
        }
    }

    private GameObject CreatePoolObject()
    {
        GameObject obj = Instantiate(original, transform);
        obj.GetComponent<Ball>().pool = Pool;
        return obj;
    }

    private void GetPoolObject(GameObject obj)
    {
        obj.SetActive(true);
        Ball ball = obj.GetComponent<Ball>();
        ball.ChangeRandomColor();
        ball.DesrtroyBall();
    }

    private void ReleasePoolObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
    }

    private void DestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
        print("오브젝트 삭제");
    }
}
