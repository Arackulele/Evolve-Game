using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShotgunBulletBehavior : BaseBulletBehavior
{

    public override void UpdateBehaviour()
    {
        speed -= 0.04f;
        if (speed < 20) speed += speed * 0.01f;

    }

}
