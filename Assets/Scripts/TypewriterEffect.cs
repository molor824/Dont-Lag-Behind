using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] float _typewriteTime = 0.1f;
    [SerializeField] Trigger _trigger;

    TextMeshPro _txt;
    float _currentTime;
    string _text;
    bool _triggered;
    int _index;

    void Start()
    {
        _txt = GetComponent<TextMeshPro>();
        _text = _txt.text;
        _txt.text = "";
    }
    void FixedUpdate()
    {
        if (_index >= _text.Length) { return; }
        if (!_triggered)
        {
            _triggered = _trigger.IsCollided;
            return;
        }
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            return;
        }

        _currentTime = _typewriteTime;

        _txt.text += _text[_index];
        _index++;
    }
}
