using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Pool pool;

    public System.Action<Ball> move = null;

    public Vector3 location;

    private void Update()
    {
        move?.Invoke(this);
    }

    public void DesrtroyBall(float destoryDelay)
    {
        StartCoroutine(DelayToDestroy(destoryDelay));
    }

    public void ChangeRandomColor()
    {
        Renderer renderer = GetComponent<Renderer>();

        renderer.material.color = Random.ColorHSV();
    }

    private IEnumerator DelayToDestroy(float destoryDelay)
    {
        yield return new WaitForSeconds(destoryDelay);

        move = null;
        pool.Release(gameObject);
    }
}
