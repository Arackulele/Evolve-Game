using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class ShotGun : FrontGun
{

    public float maxAngle;

    public int bullets;

    public bool local = false;

    public override void shoot()
    {

        StartCoroutine(ShotgunShot());

    }

    public IEnumerator ShotgunShot()
    {

        int i = bullets;
        while (i > 0)
        {
            GameObject bullet = GameObject.Instantiate(Bullet);

            bullet.GetComponent<BaseBulletBehavior>().damagemodifier = CharacterController2D.Instance.baseDamage;

            bullet.transform.position = transform.GetChild(0).position;
            Quaternion NewRot = transform.rotation * Quaternion.Euler(0, 0, transform.rotation.z + UnityEngine.Random.Range(-maxAngle, maxAngle) + 90);
            bullet.transform.rotation = NewRot;

            if (local) bullet.transform.SetParent(this.transform);

            i--;

            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }





}
