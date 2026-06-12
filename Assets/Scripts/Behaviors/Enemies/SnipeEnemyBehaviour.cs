using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class SnipeEnemyBehaviour : EnemyBehavior
{

    Vector3 closest;

    public bool vertical = false;

    public string choice;

    public GameObject bullet;

    Transform gun;

    Transform gun2;

    int timer = 100;

    int cooldown = 500;


    public override void OnUpdate()
    {


        if (update)
        {

            cooldown--;

            if (!CharacterController2D.Instance.IsUnityNull()) targ = FigureOutTarget();


            float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

            if (timer < 1)
            {
                if (!closeenough) transform.position += transform.right * speed * Time.deltaTime;
            }
            else
            {
                transform.position += transform.right * speed * Time.deltaTime;
                timer--;
            }


            if (cooldown < 1)
            {

                GameObject t = GameObject.Instantiate(bullet);
                GameObject t2 = GameObject.Instantiate(bullet);

                t.transform.rotation = gun.rotation;
                t.transform.position = gun.position;
                t2.transform.rotation = gun2.rotation;
                t2.transform.position = gun2.position;

                cooldown = 500;
            }

        }

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


        gun = transform.GetChild(2);

        gun2 = transform.GetChild(3);

        closest = new Vector3(999, 0, 0);


        Vector3 edgeright = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 0f));

        Vector3 edgeleft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 0f));

        Vector3 edgedown = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));

        Vector3 edgeup = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));

        List<Vector3> edges = new List<Vector3>() { edgeright, edgeleft, edgedown, edgeup };

        foreach (var i in edges)
        {
            if (closest.x != 999)
            {
                if (Vector3.Distance(transform.position, i) < Vector3.Distance(transform.position, closest)) closest = i;
            }
            else closest = i;
        }

        if (Camera.main.WorldToViewportPoint(closest).y == 0.5f) vertical = false;
        else vertical = true;

        if (closest == edges[0]) choice = "right";
        if (closest == edges[1]) choice = "left";
        if (closest == edges[2]) choice = "down";
        if (closest == edges[3]) choice = "up";

    }

    bool closeenough = false;

    private Vector3 FigureOutTarget()
    {

        Vector3 targ;

        closeenough = false;


        if (choice == "right") closest = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 0f));

        if (choice == "left") closest = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 0f));

        if (choice == "down") closest = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));

        if (choice == "up") closest = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));



        if (vertical && CharacterController2D.Instance.transform.position.x <= transform.position.x + 0.3f && CharacterController2D.Instance.transform.position.x >= transform.position.x - 0.3f) closeenough = true;
        else if (!vertical && CharacterController2D.Instance.transform.position.y <= transform.position.y + 0.3f && CharacterController2D.Instance.transform.position.y >= transform.position.y - 0.3f) closeenough = true;


        if (vertical ) targ = new Vector3(CharacterController2D.Instance.transform.position.x, closest.y, transform.position.z);
        else targ = new Vector3(closest.x, CharacterController2D.Instance.transform.position.y, transform.position.z);

        return targ;
    }


}
