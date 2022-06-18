using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    [SerializeField] float _speed = 2;
    [SerializeField] Vector2 _offset = new(10, 0);

    Vector2 _position;
    float _time;
    Controls _controls;
    Vector2 _targetPosition;
    Vector2 _lastPosition;

    public void OnEnter()
    {
        _targetPosition = _position + _offset;
        _lastPosition = transform.position;
        _time = 0;
    }
    public void OnExit()
    {
        _targetPosition = _position;
        _lastPosition = transform.position;
        _time = 0;
    }
    void Start()
    {
        _controls = new();
        _controls.Player.Enable();
        _position = transform.position;
        _lastPosition = _position;
        _targetPosition = _position;
    }
    void FixedUpdate()
    {
        _time = Mathf.Clamp01(_time + Time.deltaTime * _speed);

        transform.position = Vector3.Lerp(
            _lastPosition,
            _targetPosition,
            Ease.OutQuart(_time)
        );
    }
}
