using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HeadFlipper : MonoBehaviour
{
    SpriteRenderer _renderer;

    float _lastX;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        var x = transform.parent.position.x;

        if (_lastX != x)
        {
            _renderer.flipX = _lastX > x;
            _lastX = x;
        }
    }
}