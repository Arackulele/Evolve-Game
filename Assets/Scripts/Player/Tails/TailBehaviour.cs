using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class TailBehaviour : ActivateAbility
{
    public override void ActivateConditions()
    {
        if (usable && InputSystem.actions.FindAction("Dash").ReadValue<float>() > 0) {
            Activate();
            usable = false;
            timer = cooldown;
        }
        
    }
}
