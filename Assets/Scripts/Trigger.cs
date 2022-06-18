using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
    public ReadOnlyCollection<Collider2D> Collisions => _collidedObjects.AsReadOnly();
    public bool IsCollided => _collidedObjects.Count != 0;

    [SerializeField] LayerMask _mask;

    List<Collider2D> _collidedObjects = new();

    static bool CheckLayer(LayerMask mask, int layer) => ((1 << layer) & mask) != 0;

    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (CheckLayer(_mask, collider.gameObject.layer))
        {
            _collidedObjects.Add(collider);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        _collidedObjects.Remove(collider);
    }
}