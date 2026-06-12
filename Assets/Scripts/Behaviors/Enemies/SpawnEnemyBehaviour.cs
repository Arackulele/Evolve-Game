using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpawnEnemyBehaviour : EnemyBehavior
{

    public GameObject toSpawn;

    public override void OnUpdate()
    {

        transform.position += transform.right * speed * Time.deltaTime;



        if (speed < 12) speed *= 0.9992f;

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


        if (!CharacterController2D.Instance.IsUnityNull()) targ = CharacterController2D.Instance.transform.position;

        float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

        StartCoroutine(SpawnEnemies());

    }


    private IEnumerator SpawnEnemies()
    {
        while (gameObject.activeSelf) { 
        yield return new WaitForSeconds(6f);

            GameObject t = GameObject.Instantiate(toSpawn);
            t.transform.position = transform.position;
            t.transform.rotation = transform.rotation;
        }
    }




}
