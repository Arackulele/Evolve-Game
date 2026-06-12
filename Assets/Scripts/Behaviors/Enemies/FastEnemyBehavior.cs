using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FastEnemyBehavior : EnemyBehavior
{

    public GameObject mineType;

    public override void OnUpdate()
    {

        if (!CharacterController2D.Instance.IsUnityNull()) targ = CharacterController2D.Instance.transform.position;

        float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        transform.position += transform.right * speed * Time.deltaTime;



        if (speed < 12 && Tier == 1) speed *= 1.001f;
        if (turnspeed > 2 && Tier == 1) turnspeed *= 0.9992f;

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


        if (Tier == 1) StartCoroutine(DestroyAfterTime());

        if (Tier == 2) StartCoroutine(SpitBubbles());

        StartCoroutine(SetMaxHP());

    }


    private IEnumerator SpitBubbles()
    {

        while (this != null)
        {
            yield return new WaitForSeconds(0.4f);


            GameObject bullet = GameObject.Instantiate(mineType);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

        }
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(12f);

        Destroy(gameObject);
    }





}
