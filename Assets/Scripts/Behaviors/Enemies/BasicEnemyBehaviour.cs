using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyBehavior : EnemyBehavior
{

    public GameObject toSpawn;
    
    public override void OnUpdate()
    {

    }

    public override void OnCollide()
    {

    }

    public override void OnDie()
    {
        if (Tier == 2)
        { 
        SpawnEnemies();
        SpawnEnemies();
        }
    }

    public override void OnTakeDamage()
    {

    }

    public override void OnStart()
    {

    }

    private void SpawnEnemies()
    {

            GameObject t = GameObject.Instantiate(toSpawn);
            t.GetComponent<EnemyBehavior>().turnspeed *= UnityEngine.Random.Range(1.2f, 0.8f);
            t.transform.position = transform.position += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            t.transform.rotation = transform.rotation;
    }


}
