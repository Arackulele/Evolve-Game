using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeteorBehavior : MonoBehaviour
{
    public bool update = true;

    public float speed = 1f;

    public float hp = 20f;

    public GameObject player;

    public GameObject prefab;

    private bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        if (CharacterController2D.Instance != null) transform.right = CharacterController2D.Instance.gameObject.transform.position - transform.position;
        if (CharacterController2D.Instance != null) player = CharacterController2D.Instance.gameObject;

        prefab = this.gameObject;

        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (hp < 0f && !dying) 
        {
            dying = true;
            int e = UnityEngine.Random.Range(2, 3);

            while (e > 0)
            {
                prefab.GetComponent<MeteorBehavior>().hp = 12;
                GameObject enemy = Instantiate(prefab);
                MeteorBehavior b = enemy.GetComponent<MeteorBehavior>();
                b.speed *= (float)UnityEngine.Random.Range(5000, 15000) / 10000;
                enemy.transform.localScale *= (float)UnityEngine.Random.Range(500, 2500) / 10000;
                enemy.transform.position = transform.position;
                e--;
            }

            Destroy(gameObject);
        
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<CharacterController2D>() != null)
        {
            CharacterController2D.Instance.velocity *= -0.6f;

            
            if (CharacterController2D.Instance.velocity.x < 2 && CharacterController2D.Instance.velocity.y < 2) CharacterController2D.Instance.velocity -= new  Vector2(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);


            if (CharacterController2D.Instance.velocity.x < 4 && CharacterController2D.Instance.velocity.y < 5) CharacterController2D.Instance.velocity *= 1.6f;
        }

        else if (col.GetComponent<BaseBulletBehavior>() != null)
        {
            BaseBulletBehavior Bu = col.GetComponent<BaseBulletBehavior>();
            hp -= Bu.basedamage;
            if (Bu.Sound.GetRandomItem() != null) Bu.Sound.GetRandomItem().PlayOneShot(Bu.Audio);
            if (!Bu.piercing) Destroy(Bu.gameObject);

        }
    }

    private void StayTriggerEnter2D(Collider2D col)
    {
        // move out more
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(50f);

        Destroy(gameObject);
    }

}
