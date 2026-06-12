using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BubbleEnemyBehaviour : EnemyBehavior
{

    public GameObject launchgunBullet;

    public override void OnUpdate()
    {


        if (!CharacterController2D.Instance.IsUnityNull()) targ = CharacterController2D.Instance.transform.position;

        float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        transform.position += transform.right * speed * Time.deltaTime;



    }

    public override void OnCollide()
    {

    }

    public override void OnDie()
    {

    }

    public override void OnTakeDamage()
    {

    }

    public override void OnStart()
    {


        StartCoroutine(SpitBubbles());

        StartCoroutine(SetMaxHP());

    }


    private IEnumerator SpitBubbles()
    {

        while (this != null)
        {
            yield return new WaitForSeconds(4f);


            GameObject bullet = GameObject.Instantiate(launchgunBullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

        }
    }

}
