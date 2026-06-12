using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingBulletBehavior : BaseBulletBehavior
{
    public float rotateamount = 1;

    Vector3 targ;

    public override void UpdateBehaviour()
    {

        if (speed < 20) speed += speed * 0.01f;


        //ToDo: Implement homing to enemies

        if (!CharacterController2D.Instance.IsUnityNull()) targ = CharacterController2D.Instance.transform.position;

        float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateamount * Time.deltaTime);
    }

}
