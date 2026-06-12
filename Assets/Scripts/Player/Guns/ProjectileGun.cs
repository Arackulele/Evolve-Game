using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class ProjectileGun : FrontGun
{

    public bool local = false;
    public override void shoot()
    {

        GameObject b = GameObject.Instantiate(Bullet);
        b.transform.position = this.transform.GetChild(0).position;

        b.GetComponent<BaseBulletBehavior>().damagemodifier = CharacterController2D.Instance.baseDamage;

        //fix fucked up rotation
        Quaternion n = this.transform.rotation * Quaternion.Euler(0, 0, 90);
        b.transform.rotation = n;

        if (local) b.transform.SetParent(this.transform);

    }


}
