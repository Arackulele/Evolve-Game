using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DashTail : TailBehaviour
{

    public override void Activate()
    {

        player.velocity *= 3;


        print("Dash Activated");

    }

}
