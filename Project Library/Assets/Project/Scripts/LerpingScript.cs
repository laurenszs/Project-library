using System.Collections;
using UnityEngine;

public class LerpingScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 endPos = new Vector3(5, 0, 0);

    [SerializeField] private float platformRaiseDuration = 1f;

    private int maxRight = 5, maxLeft = -5;

    private Coroutine movement = null;

    // Update is called once per frame
    private void Update()
    {
        if (movement == null)
        {
            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && transform.position.x < maxRight)
            {
                StopAllCoroutines();
                movement = StartCoroutine(RaisePlatform(platformRaiseDuration, maxRight));
            }
            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && transform.position.x > maxLeft)
            {
                StopAllCoroutines();
                movement = StartCoroutine(RaisePlatform(platformRaiseDuration, maxLeft));
            }
        }
    }

    private IEnumerator RaisePlatform(float duration, int pos)
    {
        float timer = 0f;
        float factor;
        Vector3 startPos = transform.position;
        endPos = new Vector3(transform.position.x + pos, transform.position.y, transform.position.z);
        while (timer < duration)
        {
            factor = timer / duration;
            transform.position = Vector3.Lerp(startPos, endPos, factor);
            timer += Mathf.Min(Time.deltaTime, duration - timer);
            yield return null;
        }
        transform.position = endPos;
        movement = null;
        Debug.Log(pos);
    }
}