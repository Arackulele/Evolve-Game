using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircleEnemy : MonoBehaviour
{
    public float damage = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<CharacterController2D>() != null)
        {
            CharacterController2D.Instance.StartCoroutine(CharacterController2D.Instance.TakeDamage(damage));
        }
    }
}
