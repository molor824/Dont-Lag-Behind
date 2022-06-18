using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRigidbodies : MonoBehaviour
{
    [SerializeField] Rigidbody2D _connectedRigidbody;

    Vector2 _offset;

    void Start()
    {
        _offset = _connectedRigidbody.position - (Vector2)transform.position;
    }
    void FixedUpdate()
    {
        _connectedRigidbody.position = (Vector2)transform.position + _offset;
    }
}