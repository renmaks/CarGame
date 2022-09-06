using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Game.bG)
        {
            float offset = Mathf.Repeat(Time.time * speed, 6);
            transform.position = startPosition + new Vector3(0, -offset, 0);
        }
    }
}
