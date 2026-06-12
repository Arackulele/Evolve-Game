using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCircle : MonoBehaviour
{

    public static KillCircle Instance => FindObjectOfType<KillCircle>();

    public static KillCircle lastInstance;

    // Start is called before the first frame update
    void Start()
    {
        lastInstance = Instance;
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("Enemy Collided with KillCircle");
        if (col.gameObject.GetComponent<EnemyBehavior>() != null)
        {
            Destroy(col.gameObject);
            StaticVars.highscore += 5;
        }
    }
}
