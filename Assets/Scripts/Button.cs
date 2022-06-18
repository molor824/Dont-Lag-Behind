using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger), typeof(SpriteRenderer), typeof(Linker))]
public class Button : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] Linker _connectedButton;

    Trigger _trigger;
    Linker _linker;
    SpriteRenderer _renderer;
    Sprite _defaultSprite;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _renderer.sprite;
        _trigger = GetComponent<Trigger>();
        _linker = GetComponent<Linker>();
    }
    void FixedUpdate()
    {
        var active = _linker.Active;

        if (_connectedButton != null && active && _connectedButton.Active)
        {
            _linker.NeverDeactivate = true;
            _connectedButton.NeverDeactivate = true;
        }

        _renderer.sprite = active ? _pressedSprite : _defaultSprite;

        var collided = _trigger.IsCollided;
        _linker.Active = collided;
    }
}
