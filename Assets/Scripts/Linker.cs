using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveCondition
{
    AllActivated,
    NoneActivated,
}
public class Linker : MonoBehaviour
{
    public bool NeverDeactivate;
    public bool Active
    {
        get => _active;
        set
        {
            // if its deactiveable or not active, set active with given value
            if (!NeverDeactivate || !_active) { _active = value; }
            else { _active = true; }
        }
    }

    [SerializeField] ActiveCondition _activeCondition;
    [SerializeField] Linker[] _links;

    bool _active;

    void FixedUpdate()
    {
        // if there are no links, this linker can be activated from outside
        if (_links.Length == 0) { return; }

        if (
            _activeCondition is ActiveCondition.AllActivated
            or ActiveCondition.NoneActivated
        )
        {
            var active = true;

            foreach (var link in _links)
            {
                // if link isnt active and _activeCondition states all must be active
                // false ^ true = true
                // or if link is active but _activeCondition states all must be inactive
                // true ^ false = true
                // it means its false
                if (link.Active ^ _activeCondition == ActiveCondition.AllActivated)
                {
                    active = false;
                    break;
                }
            }

            Active = active;
        }
        // commented due to possibly being useless
        // else
        // {
        //     Active = false;
        //     var amountLeft = _amountToActivate;

        //     foreach (var link in _links)
        //     {
        //         if (link.Active) { amountLeft--; }

        //         if (amountLeft == 0)
        //         {
        //             Active = true;
        //             break;
        //         }
        //     }
        // }
    }
}