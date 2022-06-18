using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Linker))]
public class Spawner : MonoBehaviour
{
    [SerializeField] Transform _objectToSpawn;

    Linker _linker;
    bool _lastState;

    void Start()
    {
        _linker = GetComponent<Linker>();
    }
    void FixedUpdate()
    {
        var active = _linker.Active;

        if (!_lastState && active)
        {
            var clone = Instantiate(_objectToSpawn, null);

            clone.position = transform.position;
            clone.rotation = transform.rotation;
            clone.localScale = transform.localScale;

            clone.gameObject.SetActive(true);
        }

        _lastState = active;
    }
}
