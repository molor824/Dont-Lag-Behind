using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public class CloneManager : MonoBehaviour
{
    Trigger _trigger;
    DelayedMovement _clone;
    bool _lastTriggerState;

    void Start()
    {
        _clone = GameObject.FindObjectOfType<DelayedMovement>(true);
        _trigger = GetComponent<Trigger>();
    }
    void FixedUpdate()
    {
        var collided = _trigger.IsCollided;

        if (collided && !_lastTriggerState)
        {
            _clone.Reset();
            _clone.gameObject.SetActive(true);
        }
        else if (!collided && _lastTriggerState)
        {
            _clone.gameObject.SetActive(false);
        }

        _lastTriggerState = collided;
    }
}
