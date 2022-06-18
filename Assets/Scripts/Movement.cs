using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    // multiplies every speed, acceleration etc.
    [HideInInspector] public float SpeedMultiplier = 1;
    [HideInInspector] public Vector2 Direction;
    [HideInInspector] public bool Run;

    [SerializeField] float _acceleration = 100;
    [SerializeField] float _deacceleration = 75;
    [SerializeField] float _runSpeed = 10;
    [SerializeField] float _walkSpeed = 5;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        var velocity = _rb.velocity;
        var speed = Run ? _runSpeed : _walkSpeed;
        var acceleration = _acceleration;

        // normalizing direction when its zero would cause problems
        // and on non zero direction, we should accelerate so leave acceleration variable untouched
        if (Direction != Vector2.zero) { Direction.Normalize(); }
        // if direction is zero then we should deaccelerate
        else { acceleration = _deacceleration; }

        velocity = Vector2.MoveTowards(
            velocity,
            Direction * speed * SpeedMultiplier,
            acceleration * Time.deltaTime * SpeedMultiplier
        );

        _rb.velocity = velocity;
    }
}
