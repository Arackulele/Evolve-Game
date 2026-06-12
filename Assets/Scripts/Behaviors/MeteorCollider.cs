using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeteorCollider : MonoBehaviour
{
    public bool update = true;

    public float speed = 1f;

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
        if (col.GetComponent<CharacterController2D>() != null)
        {
            CharacterController2D.Instance.velocity *= -1;

            if (CharacterController2D.Instance.velocity.x < 5 && CharacterController2D.Instance.velocity.y < 5) CharacterController2D.Instance.velocity *= 2;

        }
    }

    private void StayTriggerEnter2D(Collider2D col)
    {
        // move out more
    }


}
