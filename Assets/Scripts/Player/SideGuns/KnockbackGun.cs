using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class KnockbackGun : MonoBehaviour
{
    public GameObject Bullet;

    public CharacterController2D ch;

    public float cooldown = 120;

    public int speed = 100;

    public float maxcooldown = 120;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (cooldown < 1) {

        GameObject b = GameObject.Instantiate(Bullet);
        b.transform.position = this.transform.position;

        b.GetComponent<BaseBulletBehavior>().damagemodifier = ch.baseDamage;

            //ch.velocity.x -= transform.rotation.w * 6;
            //ch.velocity.y -= transform.rotation.z * 6;

            ch.velocity.x += (transform.GetChild(0).right * speed * Time.deltaTime).x;

            ch.velocity.y += (transform.GetChild(0).right * speed * Time.deltaTime).y;


            //fix fucked up rotation
            Quaternion n = this.transform.rotation * Quaternion.Euler(0, 0, 90f);
        b.transform.rotation = n;

        cooldown = maxcooldown * ( ch.basesideCooldown);
        }
        else cooldown--;

    }



}
