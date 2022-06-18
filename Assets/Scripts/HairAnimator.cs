using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairAnimator : MonoBehaviour
{
    [SerializeField] SpriteRenderer _head;
    [SerializeField] float _hairResistance = 5;
    [SerializeField] float _minimumHairResistance = 2;

    (Rigidbody2D, Vector2)[] _hair;

    void Start()
    {
        var rbs = GetComponentsInChildren<Rigidbody2D>();
        var position = transform.position;

        _hair = new (Rigidbody2D, Vector2)[rbs.Length];
        for (var i = 0; i < rbs.Length; i++)
        {
            _hair[i] = (
                rbs[i],
                rbs[i].position - (Vector2)position
            );
        }
    }
    void FixedUpdate()
    {
        var position = (Vector2)transform.position;
        var flipped = _head.flipX;

        for (var i = 0; i < _hair.Length; i++)
        {
            var (hair, offset) = _hair[i];
            var target = offset
                * (flipped ? new Vector2(-1, 1) : new Vector2(1, 1))
                + position
            ;
            var crntPosition = hair.position;
            var resistance = Mathf.Lerp(
                _hairResistance,
                _minimumHairResistance,
                (float)i / (_hair.Length - 1)
            );

            hair.velocity = (target - crntPosition) * resistance;
        }
    }
}
