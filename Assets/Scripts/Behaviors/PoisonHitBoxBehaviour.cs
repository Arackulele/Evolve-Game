using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PoisonHitBoxBehaviour : MonoBehaviour
{
    public bool update = true;

    public bool accelerate = true;

    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            if (transform.localScale.x < 0.8f) transform.localScale = new Vector3(transform.localScale.x + 0.007f, transform.localScale.y + 0.007f, transform.localScale.z + 0.007f);
            if (speed < 4 && accelerate) speed += speed * 0.004f;
            else if (!accelerate) speed -= speed * 0.004f;
            transform.position += transform.right * speed * Time.deltaTime;
        }

    }
    int i = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.GetComponent<EnemyBehavior>() != null)
        {
            if (i < 40) i++;
            else { 
            collision.GetComponent<EnemyBehavior>().dealDamage(0.2f);

                //Sound.GetRandomItem().PlayOneShot(Audio);\
                i = 0;
            }
        }
    }



    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(4.2f);

        Destroy(gameObject);
    }

}
