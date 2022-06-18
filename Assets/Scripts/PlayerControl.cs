using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Equipper))]
public class PlayerControl : MonoBehaviour
{
    Equipper _equipper;
    Movement _movement;
    Controls _controls;

    bool _equipState;

    void Start()
    {
        _controls = new();
        _controls.Player.Enable();

        _equipper = GetComponent<Equipper>();
        _movement = GetComponent<Movement>();
    }
    void Update()
    {
        var player = _controls.Player;
        var equip = player.Equip.ReadValue<float>() > 0.5f;

        _movement.Run = player.Run.ReadValue<float>() > 0.5f;
        _movement.Direction = player.Movement.ReadValue<Vector2>();

        if (!_equipState && equip) { _equipper.Equip(); }

        _equipState = equip;
    }
}
