using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public abstract class EnemyBehavior : MonoBehaviour
{

    public float contactdamage = 0.8f;

    public bool update = true;

    private Vector2 velocity;

    public float speed = 1f;

    public float turnspeed = 100f;

    public int Moneydrop;

    public float hp = 10;

    private float maxhp;

    public GameObject Money;

    public Vector2 targ;

    private bool hasDroppedMoney = false;

    public bool chase = true;

    public int Tier = 1;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SetMaxHP());

        OnStart();

    }

    public abstract void OnStart();

    public IEnumerator SetMaxHP()
    {
        yield return new WaitForSeconds(0.1f);

        maxhp = hp;
    }

    // Update is called once per frame
    void Update()
    {

        if (update)
        {
           if (chase) { 
            if (!CharacterController2D.Instance.IsUnityNull()) targ = CharacterController2D.Instance.transform.position;

            float angle = Mathf.Atan2(targ.y - transform.position.y, targ.x - transform.position.x) * Mathf.Rad2Deg;




            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
            }


            transform.position += transform.right * speed * Time.deltaTime;


            OnUpdate();

            //TeleportAtScreenBorder();
        }


    }

    public abstract void OnUpdate();

    public void dealDamage(float amnt)
    {

        hp -= amnt;
        if (hp <= 0 && !hasDroppedMoney)
        {

            OnDie();

            int m;
            if (amnt > 0) m = UnityEngine.Random.Range((int)Moneydrop / 2, Moneydrop);
            else m = 0;

            while (m > 0)
            {
                GameObject o = Instantiate(Money);
                o.transform.position = new Vector3(this.transform.position.x + Random.Range(-0.8f, 0.8f), this.transform.position.y + Random.RandomRange(-0.8f, 0.8f), this.transform.position.z);

                float scl = Random.Range(0.8f, 1.2f);

                o.transform.localScale *= scl;

                m--;
            }

            if ( gameObject != null ) Destroy(gameObject);
            StaticVars.highscore += 5;
            hasDroppedMoney = true;
        }

        else StartCoroutine(EnemyOnHitColor());


    }

    public abstract void OnDie();

    public abstract void OnTakeDamage();


    IEnumerator EnemyOnHitColor()
    {

        OnTakeDamage();

        SpriteRenderer rf = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();

        //ToDo: Smooth hit effect
        SpriteRenderer render = rf.gameObject.GetComponent<SpriteRenderer>();
        render.material.shader = Shader.Find("GUI/Text Shader");

        yield return new WaitForSeconds(0.1f);

        render.material.shader = Shader.Find("Sprites/Default");

        rf.color = Color.Lerp(new Color(0.7f, 0.38f, 0.38f),Color.white, hp/maxhp);


        yield break;
    }


    private void TeleportAtScreenBorder()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0f)
        {
            pos = new Vector3(1.0f, pos.y, pos.z);
        }
        else if (pos.x >= 1.0f)
        {
            pos = new Vector3(0.0f, pos.y, pos.z);
        }
        if (pos.y < 0.0f)
        {
            pos = new Vector3(pos.x, 1.0f, pos.z);
        }
        else if (pos.y >= 1.0f)
        {
            pos = new Vector3(pos.x, 0.0f, pos.z);
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<CharacterController2D>() != null)
        {

            OnCollide();

            //figure out who should take how much damage based on velocity
            CharacterController2D.Instance.StartCoroutine(CharacterController2D.Instance.TakeDamage(contactdamage));

            this.dealDamage(math.max(1f - contactdamage, 0.4f) );
            print("Enemy detected player collision");


            CharacterController2D.Instance.velocity *= -0.2f;


            if (CharacterController2D.Instance.velocity.x < 2 && CharacterController2D.Instance.velocity.y < 2) CharacterController2D.Instance.velocity -= new Vector2(transform.position.x - CharacterController2D.Instance.transform.position.x, transform.position.y - CharacterController2D.Instance.transform.position.y);

            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
        }
    }

    public abstract void OnCollide();


}
