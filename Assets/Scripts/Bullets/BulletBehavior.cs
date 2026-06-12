using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletBehavior : BaseBulletBehavior
{


    public override void UpdateBehaviour()
    {

        if (speed < 20) speed += speed * 0.01f;

    }

}
