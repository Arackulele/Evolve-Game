using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SideGun : MonoBehaviour
{
    public GameObject Bullet;

    public float cooldown = 100;

    public float maxcooldown = 100;

    public CharacterController2D player;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (cooldown < 1) {

        GameObject b = GameObject.Instantiate(Bullet);
        b.transform.position = this.transform.position;
        b.GetComponent<BaseBulletBehavior>().damagemodifier = player.baseSideDamage;

        //fix fucked up rotation
        Quaternion n = this.transform.rotation * Quaternion.Euler(0, 0, 90f);
        b.transform.rotation = n;

        cooldown = maxcooldown * (player.basesideCooldown);
        }
        else cooldown--;

    }



}
