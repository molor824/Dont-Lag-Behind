using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Equipper : MonoBehaviour
{
    public Rigidbody2D EquippedObject => _equippedObject;

    [SerializeField] string _equipLayerName = "Equipped Prop";
    [SerializeField] string _normalLayerName = "Moveable Prop";
    [SerializeField] float _objectDistance = 2;
    [SerializeField] Trigger _equipTrigger;
    [SerializeField] float _objectSpeed = 5;
    // 1 means instant, 0 means no speed

    Rigidbody2D _rb;
    Rigidbody2D _equippedObject;
    int _equipLayer, _normalLayer;
    bool _negative;
    Vector2 _lastDirection = new(1, 0);

    public void Equip()
    {
        if (_equippedObject != null)
        {
            _equippedObject.gameObject.layer = _normalLayer;
            _equippedObject = null;
        }
        else if (_equipTrigger.IsCollided)
        {
            var equippedObject = _equipTrigger.Collisions[0].GetComponent<Rigidbody2D>();

            _equippedObject = equippedObject;
            equippedObject.gameObject.layer = _equipLayer;
        }

    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _normalLayer = LayerMask.NameToLayer(_normalLayerName);
        _equipLayer = LayerMask.NameToLayer(_equipLayerName);
    }
    void FixedUpdate()
    {
        if (_equippedObject == null) { return; }

        var plrVelocity = _rb.velocity;

        if (plrVelocity != Vector2.zero) { _lastDirection = plrVelocity.normalized; }

        var position = (Vector2)transform.position;
        var objectPosition = _equippedObject.position;
        var destination = position + _lastDirection * _objectDistance;

        _equippedObject.velocity = (destination - objectPosition) * _objectSpeed;
    }
}
