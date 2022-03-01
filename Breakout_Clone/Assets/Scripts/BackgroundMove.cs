using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float randOffset;

    private void Start()
    {
        randOffset = Random.Range(0f, 5f);
    }

    private void Update()
    {
        float perlin = Mathf.PerlinNoise(transform.position.y / 5f + Time.time * 1f, transform.position.x / 5f + Time.time * 1f);
        transform.localScale = new Vector3(1f, 1f, perlin * 3.5f + 1f);
    }
}
