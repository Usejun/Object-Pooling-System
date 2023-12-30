using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Ball : MonoBehaviour
{
    const float DESTROYTIME = 1.0f;

    public IObjectPool<GameObject> pool { get; set; }

    public void DesrtroyBall()
    {
        StartCoroutine(DelayToDestroy());
    }

    public void ChangeRandomColor()
    {
        Renderer renderer = GetComponent<Renderer>();

        renderer.material.color = Random.ColorHSV();
    }

    private IEnumerator DelayToDestroy()
    {
        yield return new WaitForSeconds(DESTROYTIME);

        pool.Release(gameObject);
    }
}
