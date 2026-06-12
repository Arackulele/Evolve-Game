using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircleplayer : MonoBehaviour
{

    public CharacterController2D player;

    // Start is called before the first frame update
    void Start()
    {

        player = CharacterController2D.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<EnemyBehavior>() != null)
        {
            col.gameObject.GetComponent<EnemyBehavior>().dealDamage(2f + player.baseSideDamage);
        }
    }
}
