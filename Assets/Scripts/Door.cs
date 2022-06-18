using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Linker), typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] Trigger _closeTrigger;
    [SerializeField] Vector2 _openPosition = new(0, 2);
    [SerializeField] float _time = 0.5f;

    Linker _linker;
    Collider2D _collider;
    float _currentTime;
    Vector3 _defaultPosition;
    bool _triggerActivated;

    void Start()
    {
        _linker = GetComponent<Linker>();
        _collider = GetComponent<Collider2D>();

        _defaultPosition = transform.localPosition;
    }
    void FixedUpdate()
    {
        if (_closeTrigger != null && !_triggerActivated)
        {
            _triggerActivated = _closeTrigger.IsCollided;
        }

        var active = !_triggerActivated && _linker.Active;
        var realOpenPosition = _defaultPosition + (Vector3)_openPosition;
        var speed = Time.deltaTime / _time;
        var direction = active ? 1 : -1;

        _collider.isTrigger = active;

        _currentTime = Mathf.Clamp01(_currentTime + direction * speed);
        transform.localPosition = Vector3.Lerp(
            _defaultPosition, realOpenPosition, Ease.InOutQuad(_currentTime)
        );
    }
}
