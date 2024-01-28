using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonEffect : MonoBehaviour
{
    public Sprite[] texture_pool;
    public AnimationCurve size_curve;
    public float duration;
    private float tracker;

    public void Awake()
    {
        transform.localScale = Vector3.one * size_curve.Evaluate(0);
        GetComponent<SpriteRenderer>().sprite = texture_pool[Random.Range(0,texture_pool.Length)];
    }

    private void Update()
    {
        transform.LookAt(-Camera.main.transform.position);
        transform.localScale = Vector3.one * size_curve.Evaluate(tracker/duration);
        tracker += Time.deltaTime;
        if (tracker > duration)
            Destroy(gameObject);
    }
}
