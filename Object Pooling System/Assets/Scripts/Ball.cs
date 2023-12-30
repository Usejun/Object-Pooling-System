using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    const float DESTROYTIME = 1.0f;

    public Pool pool;

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
