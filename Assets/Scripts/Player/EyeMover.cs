using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EyeMover : MonoBehaviour
{

    public float RotationSpeed = 50;

    float angle;

    public EnemyBehavior closest = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        List<EnemyBehavior> l = new List<EnemyBehavior>();
        l.AddRange(Object.FindObjectsOfType<EnemyBehavior>());
        foreach (var i in l)
        {

            if (closest != null)
            {
                if (Vector3.Distance(transform.position, i.transform.position) < Vector3.Distance(transform.position, closest.transform.position)) closest = i;
            }
            else closest = i;
            print("went through enemy loop");
        }
        
        if (closest != null) angle = Mathf.Atan2(closest.transform.position.y - transform.position.y, closest.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

    }
}
