using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchBulletBehavior : BaseBulletBehavior
{

    public override void UpdateBehaviour()
    {

        if (transform.localScale.x < 0.9f) transform.localScale = new Vector3(transform.localScale.x + 0.007f, transform.localScale.y + 0.007f, transform.localScale.z + 0.007f);
        if (speed < 16) speed += speed * 0.007f;

    }

}
