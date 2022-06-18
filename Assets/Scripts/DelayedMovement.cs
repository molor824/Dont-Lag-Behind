using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DelayedMovement : MonoBehaviour
{
    public float Delay = 1;

    [SerializeField] Rigidbody2D _player;

    List<Vector2> _states;
    Rigidbody2D _rb;
    float _currentDelay;

    public void Reset()
    {
        var position = _player.position;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        transform.position = position;
        _currentDelay = Delay;

        if (_states == null) { _states = new(Mathf.CeilToInt(1 / Time.fixedDeltaTime)); }

        _states.Clear();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        Reset();
    }
    void FixedUpdate()
    {
        _states.Add(_player.velocity);

        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }

        var velocity = _states[0];

        _rb.velocity = velocity;

        _states.RemoveAt(0);
    }
}
