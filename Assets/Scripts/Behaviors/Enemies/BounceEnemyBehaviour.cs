using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BounceEnemyBehavior : EnemyBehavior
{

    private Quaternion targetRotation;

    public override void OnUpdate()
    {

        if (!CharacterController2D.Instance.IsUnityNull()) targ = CharacterController2D.Instance.transform.position;

        float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.position += transform.right * speed * Time.deltaTime;


        TeleportAtScreenBorder();

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

    }


    private void TeleportAtScreenBorder()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 999 * Time.deltaTime);
        }
        else if (pos.x >= 1.0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 999 * Time.deltaTime);
        }
        if (pos.y < 0.0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 999 * Time.deltaTime);
        }
        else if (pos.y >= 1.0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 999 * Time.deltaTime);
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }




}
